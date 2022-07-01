namespace Project.Services
{
    public interface IAuthorizationService
    {
        public int solveTokenUserID(HttpContext context);
        public string solveTokenUserName(HttpContext context);

    }
}
