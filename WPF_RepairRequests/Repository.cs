using System.Linq;

namespace WPF_RepairRequests
{
    public class Repository
    {
        public static User user;
        
        public static void UserAutorization(string Login, string Pass)
        {
            DataBaseEntities dbEntities = new DataBaseEntities();
            user = dbEntities.User.Where(b => b.Login == Login && b.Password == Pass).FirstOrDefault();
        }

        //public static void EmployeeAuthorization(string Login, string Pass)
        //{
        //    //employee = Demo_exEntities.GetContext().Сотрудник.Where(b => b.Логин == Login && b.Пароль == Pass).FirstOrDefault();
        //}
    }
}
