using ArticleProject.Core;

namespace ArticleProject.Data.SqlserverEF
{
    public class CategoryEntity : IDataHelper<Category>
    {
        private DBContext db;
        private Category table;

        public CategoryEntity()
        {
            db = new DBContext();
        }

        public List<Category> GetAllData()
        {
            if (db.Database.CanConnect())
            {

                return db.Category.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<Category> GetDataByUserId(string UserId)
        {
            throw new NotImplementedException();
        }

        public List<Category> Search(string searchItem)
        {
            if (db.Database.CanConnect())
            {

                return db.Category.Where(x => x.Name.Contains(searchItem)
                || x.Id.ToString().Contains(searchItem)).ToList();
            }
            else
            {
                return null;
            }
        }

        public Category Find(int id)
        {
            if (db.Database.CanConnect())
            {

                return db.Category.Where(x => x.Id == id).First();
            }
            else
            {
                return null;
            }
        }

        public int Add(Category table)
        {
            if (db.Database.CanConnect())
            {
                db.Category.Add(table);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Update(int id, Category table)
        {
            if (db.Database.CanConnect())
            {
                db = new DBContext();

                db.Category.Update(table);
                db.SaveChanges();

                return 1;

            }
            else
            {
                return 0;
            }
        }

        public int Delete(int id)
        {
            if (db.Database.CanConnect())
            {
                table = db.Category.Find(id);
                db.Category.Remove(table);
                db.SaveChanges();

                return 1;

            }
            else
            {
                return 0;
            }
        }
    }

}

