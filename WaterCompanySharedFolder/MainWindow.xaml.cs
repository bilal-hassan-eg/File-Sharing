using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WaterCompanySharedFolder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string User { get; set; }
        public MainWindow(string User)
        {
            InitializeComponent();
            this.User = User;
            if(User != "Admin") { CreateUserBtn.Visibility = Visibility.Collapsed; 
                SendFileToMasterBtn.Visibility = Visibility.Collapsed; }
            else { CreateUserBtn.Visibility = Visibility.Visible; 
                SendFileToMasterBtn.Visibility = Visibility.Visible; }
        }

        private void enterData(object sender, RoutedEventArgs e)
        {
            addData addData = new addData(User);
            addData.Show();
        }


        private void CreateUser(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
        }

        private void Download(object sender, RoutedEventArgs e)
        {
            selectDate selectDate = new selectDate(User);
            selectDate.Show();

        }
        private void SendFile(object sender, RoutedEventArgs e)
        {
            SendFileToMaster sendFile = new SendFileToMaster();
            sendFile.Show();
        }
        
    }
}
