using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace WPF_RepairRequests
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        DataBaseEntities dbEntities = new DataBaseEntities();
        public IEnumerable<object> Table { get; set; }
        public ReportWindow(int requestId)
        {
            InitializeComponent();

            var request = dbEntities.Request.Single(r => r.Id == requestId);
            int clientId = (int)request.Client_Id;
            int employeeId = (int)request.Employee_Id;
            int equpmentId = (int)request.Equipment_Id;
            int defectId = (int)request.Defect_Id;

            idTextBox.Text = request.Id.ToString();
            descriptionTextBox.Text = request.Description;
            clientTextBox.Text = dbEntities.Client.Single(c => c.Id == clientId).FirstName + " " + dbEntities.Client.Single(c => c.Id == clientId).LastName;
            emplyeeTextBox.Text = dbEntities.Employee.Single(e => e.Id == employeeId).FirstName + " " + dbEntities.Employee.Single(e => e.Id == employeeId).LastName;
            equpmentTextBox.Text = dbEntities.Equipment.Single(eq => eq.Id == equpmentId).Name;
            defectTextBox.Text = dbEntities.TypeOfDefect.Single(d => d.Id == defectId).Name;
            DateTime cretionDate = (DateTime)request.CreationDate;
            creationDateTextBox.Text = cretionDate.ToString("d");
            closeDateTextBox.Text = DateTime.Now.ToString("d");
        }

        private void SaveReport_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
