using ArticleProject.Core;
using ArticleProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArticleProject.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorizationService authorizationService;
        private readonly IDataHelper<Authores> dataHelper;
        private readonly IWebHostEnvironment webHost;
        private readonly Code.FilesHelper filesHelper;

        public AuthorController(IAuthorizationService authorizationService,

            IDataHelper<Authores> dataHelper, IWebHostEnvironment webHost)
        {
            this.authorizationService = authorizationService;
            this.dataHelper = dataHelper;
            this.webHost = webHost;
            filesHelper = new Code.FilesHelper(this.webHost);
        }

        // GET: A uthorController
        [Authorize("Admin")]

        public ActionResult Index()
        {
            return View(dataHelper.GetAllData());
        }
        [Authorize("Admin")]

        public ActionResult search(string searchItem)
        {
            if (searchItem == null)
            {
                return View("Index", dataHelper.GetAllData());
            }
            return View("Index", dataHelper.Search(searchItem));
        }


        [Authorize]

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var auther = dataHelper.Find(id);
            CoreView.AuthorView authorView = new CoreView.AuthorView()
            {
                Id = auther.Id,
                Bio = auther.Bio,
                Facebook = auther.Facebook,
                FullName = auther.FullName,
                Instagram = auther.Instagram,
                Twitter = auther.Twitter,
                UserId = auther.UserId,
                UserName = auther.UserName,
            };


            return View(authorView);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public ActionResult Edit(int id, CoreView.AuthorView collection)
        {
            try
            {
                var auther = new Authores
                {
                    Id = collection.Id,
                    Bio = collection.Bio,
                    Facebook = collection.Facebook,
                    FullName = collection.FullName,
                    Instagram = collection.Instagram,
                    Twitter = collection.Twitter,
                    UserId = collection.UserId,
                    UserName = collection.UserName,
                    ProfilImgUrl = filesHelper.UploadFile(collection.ProfilImgUrl, "images"),
                };
                dataHelper.Update(id, auther);
                var result = authorizationService.AuthorizeAsync(User, "Admin");
                if (result.Result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Redirect("/IndexAdmin");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        [Authorize("Admin")]

        public ActionResult Delete(int id)
        {
            var auther = dataHelper.Find(id);
            CoreView.AuthorView authorView = new CoreView.AuthorView()
            {
                Id = auther.Id,
                Bio = auther.Bio,
                Facebook = auther.Facebook,
                FullName = auther.FullName,
                Instagram = auther.Instagram,
                Twitter = auther.Twitter,
                UserId = auther.UserId,
                UserName = auther.UserName,
            };

            return View(authorView);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Admin")]

        public ActionResult Delete(int id, Authores collection)
        {
            try
            {
                dataHelper.Delete(id);
                string filePath = "C:\\Users\\nour\\source\\repos\\Articles\\Articles\\wwwroot\\images\\" + collection.ProfilImgUrl;

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
    }
}
