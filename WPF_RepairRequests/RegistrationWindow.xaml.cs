using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_RepairRequests
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        DataBaseEntities dbEntities = new DataBaseEntities();
        public RegistrationWindow()
        {
            InitializeComponent();
            positionComboBox.ItemsSource = dbEntities.Position.OrderBy(p => p.Id).Select(p => p.Name).ToList();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            string login = loginBox.Text.Trim();
            string pass1 = passBox1.Text.Trim();
            string pass2 = passBox2.Text.Trim();
            string firstName = firstNameBox.Text.Trim();
            string lastName = lastNameBox.Text.Trim();
            int position = positionComboBox.SelectedIndex + 1;

            if ((login.Length < 3) || (firstName.Length < 3) || lastName.Length < 3)
            {
                MessageBox.Show("Логин, имя и фамилия должны содержать не менее 3 символов!");
            }
            else if ( (pass1.Length < 3) || (pass2.Length < 3))
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов!");
            }
            else if (pass1 != pass2)
            {
                MessageBox.Show("Пароли не совпадают!");
            }
            else
            {
                try
                {
                    User newUser = new User()
                    {
                        Login = login,
                        Password = pass1,
                        Role_Id = position
                    };
                    dbEntities.User.Add(newUser);

                    Employee newEmployee = new Employee()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Position_Id = position,
                        DateOfBirth = birthDatePicker.SelectedDate,
                        User_Id = newUser.Id
                    };

                    dbEntities.Employee.Add(newEmployee);


                    dbEntities.SaveChanges();


                    MessageBox.Show("Работник успешно зарегестрирован");
                    AuthenticationWindow authWindow = new AuthenticationWindow();
                    authWindow.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationWindow authWindow = new AuthenticationWindow();
            authWindow.Show();
            this.Close();
        }
    }
}
