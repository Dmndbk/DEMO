using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPF_RepairRequests
{
    /// <summary>
    /// Логика взаимодействия для AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow : Window
    {
        DataBaseEntities dbEntities = new DataBaseEntities();
        public AuthenticationWindow()
        {
            InitializeComponent();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            string Login = LoginBox.Text.ToLower().Trim();
            string Pass = PasswordBox.Password.ToLower().Trim();
            Repository.UserAutorization(Login, Pass);

            if (Login.Length < 3 || Pass.Length < 3)
            {
                MessageBox.Show("Заполните все поля!");
                LoginBox.ToolTip = "Поле введено не корректно";
                PasswordBox.ToolTip = "Поле введено не корректно";
            }
            else
            {
                var user = dbEntities.User.Where(b => b.Login == Login && b.Password == Pass).FirstOrDefault();
               
                if (user != null)
                {
                    var userRole = dbEntities.Role.Where(r => r.Id == user.Role_Id);
                    var role = userRole.First().Name;

                    if (role == "Manager")
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else if (role == "Client")
                    {
                        ClientWindow clientWindow = new ClientWindow();
                        clientWindow.Show();
                        this.Close();
                    }
                    else if (role == "Specialist")
                    {
                        SpecialistWindow specWindow = new SpecialistWindow();
                        specWindow.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка авторизации");
                }
            }
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow regWindow = new RegistrationWindow();
            regWindow.Show();
            this.Close();
        }
    }
}
