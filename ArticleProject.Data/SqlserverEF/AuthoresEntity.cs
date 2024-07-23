using ArticleProject.Core;

namespace ArticleProject.Data.SqlserverEF
{
    public class AuthorssEntity : IDataHelper<Authores>
    {
        private DBContext db;
        private Authores table;

        public AuthorssEntity()
        {
            db = new DBContext();
        }

        public List<Authores> GetAllData()
        {
            if (db.Database.CanConnect())
            {

                return db.Authores.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<Authores> GetDataByUserId(string UserId)
        {
            throw new NotImplementedException();
        }

        public List<Authores> Search(string searchItem)
        {
            if (db.Database.CanConnect())
            {

                return db.Authores.Where(x =>
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

        public Authores Find(int id)
        {
            if (db.Database.CanConnect())
            {

                return db.Authores.Where(x => x.Id == id).First();
            }
            else
            {
                return null;
            }
        }

        public int Add(Authores table)
        {
            if (db.Database.CanConnect())
            {
                db.Authores.Add(table);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Update(int id, Authores table)
        {
            if (db.Database.CanConnect())
            {
                db = new DBContext();

                db.Authores.Update(table);
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
                table = db.Authores.Find(id);
                db.Authores.Remove(table);
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

