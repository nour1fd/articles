using ArticleProject.Core;
using ArticleProject.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace myarticlesProject.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IDataHelper<autherPost> dataHelperForAutherPost;
        public autherPost AutherPost;

        public ArticleModel(IDataHelper<ArticleProject.Core.autherPost> dataHelperForAutherPost
)
        {
            this.dataHelperForAutherPost = dataHelperForAutherPost;
            AutherPost = new autherPost();

        }

        public void OnGet()
        {var id = HttpContext.Request.RouteValues["id"];
            AutherPost = dataHelperForAutherPost.Find(Convert.ToInt32(id));
        }
    }
}
