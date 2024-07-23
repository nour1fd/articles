using ArticleProject.Core;
using ArticleProject.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Articles.Pages

{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDataHelper<ArticleProject.Core.Category> dataHelperForCategory;
        private readonly IDataHelper<autherPost> dataHelperForAutherPost;
        public readonly int NOFITme;

        public IndexModel(ILogger<IndexModel> logger,
            IDataHelper<ArticleProject.Core.Category> dataHelperForCategory,
            IDataHelper<ArticleProject.Core.autherPost> dataHelperForAutherPost
            
            )
        {
            NOFITme = 6;
            _logger = logger;
            this.dataHelperForCategory = dataHelperForCategory;
            this.dataHelperForAutherPost = dataHelperForAutherPost;
            ListOfAutherPost=new List<ArticleProject.Core.autherPost>();
            ListOfCategory = new List<ArticleProject.Core.Category>();
        }
        public List<ArticleProject.Core.Category> ListOfCategory { get; set; }
        public List<ArticleProject.Core.autherPost> ListOfAutherPost { get; set; }

        public void OnGet(string LoadState,string CategoryName,string search,int id)
        {
            GetALlCategory();

            if (LoadState == null || LoadState == "All")
            {
            GetALlAutherPost();


            }else if (LoadState == "ByCategory")
            {
                GetALlDAtaBYCAtegoryName(CategoryName);
            }
        else if(LoadState=="search"){
                GetALlDAtaBySearch(search);
            }
            else if (LoadState=="Next")
            {

                GetNextData(id);
            } 
            else if (LoadState=="prev")
            {

                GetNextData(id-NOFITme);
            }
        }
        //Get categoty
        private void GetALlCategory()
        {
            ListOfCategory = dataHelperForCategory.GetAllData();
        }
        private void GetALlAutherPost()
        {
            ListOfAutherPost = dataHelperForAutherPost.GetAllData().Take(NOFITme).ToList();
        }
        private void GetALlDAtaBYCAtegoryName(string CategoryName)
        {
            ListOfAutherPost = dataHelperForAutherPost.GetAllData().
                Where(x=>x.PostCategory==CategoryName).Take(NOFITme).ToList();
        }
         private void GetALlDAtaBySearch(string searchItem)
        {
            ListOfAutherPost = dataHelperForAutherPost.Search(searchItem);
        }
        private void GetNextData(int id)
        {
            ListOfAutherPost = dataHelperForAutherPost.GetAllData().Where(x=>x.Id>id).Take(NOFITme).ToList();
        }



    }
}
