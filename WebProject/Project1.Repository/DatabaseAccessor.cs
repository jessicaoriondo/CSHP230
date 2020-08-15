using Project1.ClassDatabase;

namespace Project1.Repository
{
    class DatabaseAccessor
    {
       
        private static readonly ClassDbEntities entities;

        static DatabaseAccessor()
        {
            entities = new ClassDbEntities();
            entities.Database.Connection.Open();
        }

        public static ClassDbEntities Instance
        {
            get
            {
                return entities;
            }
        }
    }
    
}
