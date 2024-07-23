using ArticleProject.Core;
using ArticleProject.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace myarticlesProject.Pages
{
    public class AllUsersModel : PageModel
    {
        public readonly int NOFITme;
        private readonly IDataHelper<Authores> dataHelper;

        public AllUsersModel(
            IDataHelper<ArticleProject.Core.Authores> dataHelper

            )
        {
            NOFITme = 6;
            ListOfAuther = new List<ArticleProject.Core.Authores>();
            this.dataHelper = dataHelper;
        }
        public List<ArticleProject.Core.Authores> ListOfAuther { get; set; }

        public void OnGet(string LoadState, string search, int id)
        {

            if (LoadState == null || LoadState == "All")
            {
                GEtALlAuther();


            }
            
            else if (LoadState == "search")
            {
                GetALlDAtaBySearch(search);
            }
            else if (LoadState == "Next")
            {

                GetNextData(id);
            }
            else if (LoadState == "prev")
            {

                GetNextData(id - NOFITme);
            }
        }
        //Get categoty
      
        private void GEtALlAuther()
        {
            ListOfAuther = dataHelper.GetAllData().Take(NOFITme).ToList();
        }
       
        private void GetALlDAtaBySearch(string searchItem)
        {
            ListOfAuther = dataHelper.Search(searchItem);
        }
        private void GetNextData(int id)
        {
            ListOfAuther = dataHelper.GetAllData().Where(x => x.Id > id).Take(NOFITme).ToList();
        }



    }
}
