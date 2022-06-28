using Project.Context;

namespace Project.Repositories
{
    public interface ICategoryRepo
    {
        public CategoryProfile? categoryGetByName(string Name);

        public List<CategoryProfile> categoryGetAll();

        public bool categoryCreate(string name,string description,int userID );

    }
}
