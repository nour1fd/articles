namespace ArticleProject.Data
{
    public interface IDataHelper<Table>

    {
        //read
        List<Table> GetAllData();

        List<Table> GetDataByUserId(string UserId);

        List<Table> Search(string searchItem);

        Table Find(int id);




        //write

        int Add(Table table);

        int Update(int id, Table table); //Edit

        int Delete(int id);



    }
}
