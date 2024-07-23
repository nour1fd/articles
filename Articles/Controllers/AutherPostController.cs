using ArticleProject.Core;
using ArticleProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArticleProject.Controllers
{
    [Authorize]
    public class AutherPostController : Controller
    {
        private readonly IDataHelper<autherPost> dataHelper;
        private readonly IDataHelper<Authores> dataHelperForAuthor;
        private readonly IDataHelper<Category> dataHelperForCategory;
        private readonly IWebHostEnvironment webHost;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly Code.FilesHelper filesHelper;
        private int pageItem;
        private Task<AuthorizationResult> result;
        private string UserId;

        public AutherPostController(
            IDataHelper<autherPost> dataHelper,
            IDataHelper<Authores> dataHelperForAuthor,
            IDataHelper<Category> dataHelperForCategory,
            IWebHostEnvironment webHost,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
            this.dataHelper = dataHelper;
            this.dataHelperForAuthor = dataHelperForAuthor;
            this.dataHelperForCategory = dataHelperForCategory;
            this.webHost = webHost;
            this.authorizationService = authorizationService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            filesHelper = new Code.FilesHelper(this.webHost);
            pageItem = 10;

        }

        // GET: PostController
        public ActionResult Index(int? id)
        {
            SetUser();
            if (result.Result.Succeeded)
            {
                // Admin
                if (id == 0 || id == null)
                {
                    return View(dataHelper.GetAllData().Take(pageItem));
                }
                else
                {
                    var data = dataHelper.GetAllData().Where(x => x.Id > id).Take(pageItem);
                    return View(data);
                }
            }
            else
            {
                if (id == 0 || id == null)
                {
                    return View(dataHelper.GetDataByUserId(UserId).Take(pageItem));
                }
                else
                {
                    var data = dataHelper.GetDataByUserId(UserId).Where(x => x.Id > id).Take(pageItem);
                    return View(data);
                }
            }


        }

        public ActionResult Search(string SearchItem)
        {
            SetUser();
            if (result.Result.Succeeded)
            {
                if (SearchItem == null)
                {
                    return View("Index", dataHelper.GetAllData());
                }
                else
                {
                    return View("Index", dataHelper.Search(SearchItem));
                }
            }
            else
            {
                if (SearchItem == null)
                {
                    return View("Index", dataHelper.GetDataByUserId(UserId));
                }
                else
                {
                    return View("Index", dataHelper.Search(SearchItem).Where(x => x.UserId == UserId).ToList());
                }
            }

        }
        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            SetUser();
            return View(dataHelper.Find(id));
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            SetUser();
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CoreView.AutherPostView collection)
        {
            SetUser();

            try
            {
                var Post = new autherPost
                {
                    AddedDate = DateTime.Now,
                    Authores = collection.Authores,
                    AutherId = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.Id).First(),
                    Category = collection.Category,
                    CategoryId = dataHelperForCategory.GetAllData().Where(x => x.Name == collection.PostCategory).Select(x => x.Id).First(),
                    FullName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.FullName).First(),
                    PostCategory = collection.PostCategory,
                    PostDescription = collection.PostDescription,
                    PostTitle = collection.PostTitle,
                    UserId = UserId,
                    UserName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.UserName).First(),
                    PostImageUrl = filesHelper.UploadFile(collection.PostImageUrl, "Images")
                };
                dataHelper.Add(Post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            var authorpost = dataHelper.Find(id);
            CoreView.AutherPostView authorPostView = new CoreView.AutherPostView
            {
                AddedDate = authorpost.AddedDate,
                // Authores = authorpost.Authores,
                AuthorId = authorpost.AutherId,
                // Category = authorpost.Category,
                CategoryId = authorpost.CategoryId,
                FullName = authorpost.FullName,
                PostCategory = authorpost.PostCategory,
                PostDescription = authorpost.PostDescription,
                PostTitle = authorpost.PostTitle,
                UserId = authorpost.UserId,
                UserName = authorpost.UserName,
                Id = authorpost.Id,
            };
            return View(authorpost);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CoreView.AutherPostView collection)
        {
            try
            {
                SetUser();

                var Post = new autherPost
                {
                    AddedDate = DateTime.Now,
                    Authores = collection.Authores,
                    AutherId = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.Id).First(),
                    Category = collection.Category,
                    CategoryId = dataHelperForCategory.GetAllData().Where(x => x.Name == collection.PostCategory).Select(x => x.Id).First(),
                    FullName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.FullName).First(),
                    PostCategory = collection.PostCategory,
                    PostDescription = collection.PostDescription,
                    PostTitle = collection.PostTitle,
                    UserId = UserId,
                    UserName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.UserName).First(),
                    PostImageUrl = filesHelper.UploadFile(collection.PostImageUrl, "Images"),
                    Id = collection.Id
                };
                dataHelper.Update(id, Post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dataHelper.Find(id));
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, autherPost collection)
        {
            try
            {
                dataHelper.Delete(id);
                string filePath = "~/Images/" + collection.PostImageUrl;
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private void SetUser()
        {
            result = authorizationService.AuthorizeAsync(User, "Admin");
            UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
