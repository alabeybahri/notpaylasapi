using Project.Context;

namespace Project.Repositories
{
    public interface ICategoryRepo
    {
        public CategoryProfile? categoryGetByName(string Name);
        public CategoryProfile? categoryGetByID(int ID);
        public List<CategoryProfile> categoryGetAll();
        public bool categoryCreate(string name,string description,int userID );
        public void categorydeleteByID(int ID);
        public void categorydeleteByName(string name);

    }
}
