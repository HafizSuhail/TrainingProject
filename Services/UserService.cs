using Stanford_University.BusinessEntities;

namespace Stanford_University.Services
{
    public class UserService
    {
        private readonly Collegedbcontex dbContext;
        public UserService() 
        {
            dbContext = new Collegedbcontex();
        }

        public User CreateUser(string userName,string password, string email, string role)
        {
            User userObject = new User
            {
                UserName = userName,
                Password = password,
                Email = email,
                Role = role
            };

            dbContext.Users.Add(userObject);
            dbContext.SaveChanges();

            return userObject;
        }
    }
}
