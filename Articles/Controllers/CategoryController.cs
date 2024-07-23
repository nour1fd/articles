using ArticleProject.Core;
using ArticleProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArticleProject.Controllers
{
    [Authorize("Admin")]

    public class CategoryController : Controller

    {
        private readonly IDataHelper<Category> dataHelper;
        private int PageItem;

        public CategoryController(IDataHelper<Category> dataHelper)
        {
            this.dataHelper = dataHelper;
            PageItem = 5;
        }
        // GET: Category
        public ActionResult Index(int? id)
        {
            if (id == 0 || id == null)
            {
                return View(dataHelper.GetAllData().Take(PageItem));
            }
            else
            {
                var data = dataHelper.GetAllData().Where(x => x.Id > id).Take(PageItem);
                return View(data);
            }
        }
        public ActionResult search(string searchItem)
        {
            if (searchItem == null)
            {
                return View("Index", dataHelper.GetAllData());
            }
            return View("Index", dataHelper.Search(searchItem));
        }

        // GET: Category/Details/5


        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category collection)
        {

            var result = dataHelper.Add(collection);
            if (result == 1)
            {
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return View();
            }


        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dataHelper.Find(id));
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category collection)
        {
            try
            {
                var result = dataHelper.Update(id, collection);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dataHelper.Find(id));
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category collection)
        {
            try
            {
                var result = dataHelper.Delete(id);
                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
