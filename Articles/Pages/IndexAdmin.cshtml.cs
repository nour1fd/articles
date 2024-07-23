using ArticleProject.Core;
using ArticleProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace myarticlesProject.Pages
{

    [Authorize]
    public class IndexAdminModel : PageModel
    {
        private readonly IDataHelper<autherPost> dataHelper;

        public IndexAdminModel(IDataHelper<autherPost> dataHelper)
        {
            this.dataHelper = dataHelper;
        }

        public int AllPost { get; set; }
        public int PostLastMouth { get; set; }
        public int PostThisYear { get; set; }
        public void OnGet()
        {
            var datem = DateTime.Now.AddMonths(-1);
            var datey = DateTime.Now.AddYears(-1);
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            AllPost = dataHelper.GetDataByUserId(userid).Count;
            PostLastMouth = dataHelper.GetDataByUserId(userid).Where(x => x.AddedDate >= datem).Count();
            PostThisYear = dataHelper.GetDataByUserId(userid).Where(x => x.AddedDate >= datey).Count();
        }
    }
}
