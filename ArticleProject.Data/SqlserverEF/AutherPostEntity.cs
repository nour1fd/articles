using ArticleProject.Core;

namespace ArticleProject.Data.SqlserverEF
{
    public class AutherPostEntity : IDataHelper<autherPost>
    {
        private DBContext db;
        private autherPost table;

        public AutherPostEntity()
        {
            db = new DBContext();
        }

        public List<autherPost> GetAllData()
        {
            if (db.Database.CanConnect())
            {

                return db.autherPost.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<autherPost> GetDataByUserId(string UserId)
        {
            if (db.Database.CanConnect())
            {

                return db.autherPost.Where(x => x.UserId == UserId.ToString()).ToList();
            }
            else
            {
                return null;
            }
        }

        public List<autherPost> Search(string searchItem)
        {
            if (db.Database.CanConnect())
            {

                return db.autherPost.Where(x =>
                x.FullName.Contains(searchItem)
               || x.FullName.Contains(searchItem)
              || x.Id.ToString().Contains(searchItem)
                ).ToList();
            }
            else
            {
                return null;
            }
        }

        public autherPost Find(int id)
        {
            if (db.Database.CanConnect())
            {

                return db.autherPost.Where(x => x.Id == id).First();
            }
            else
            {
                return null;
            }
        }

        public int Add(autherPost table)
        {
            if (db.Database.CanConnect())
            {
                db.autherPost.Add(table);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Update(int id, autherPost table)
        {
            if (db.Database.CanConnect())
            {
                db = new DBContext();

                db.autherPost.Update(table);
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
                table = db.autherPost.Find(id);
                db.autherPost.Remove(table);
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

