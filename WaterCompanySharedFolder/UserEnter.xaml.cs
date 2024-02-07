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
using System.Windows.Shapes;
using WaterCompanySharedFolder.Download;
using WaterCompanySharedFolder.pushData;

namespace WaterCompanySharedFolder
{
    /// <summary>
    /// Interaction logic for UserEnter.xaml
    /// </summary>
    public partial class UserEnter : Window
    {
        public UserEnter()
        {
            InitializeComponent();
        }

        private void signIn(object sender, RoutedEventArgs e)
        {
            if (User.Text.Trim() != "" && Pass.Text.Trim() != "")
            {
                if (User.Text.Trim() == "Admin" && Pass.Text.Trim() == "Admin123")
                {
                    MainWindow mainWindow = new MainWindow("Admin");
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    Account AccountDetails;
                    getDataAccounts getDataAccounts = new getDataAccounts();
                    List<string> LabsName = getDataAccounts.Get();
                    foreach(string item in LabsName)
                    {
                        if(item.Trim() == User.Text.Trim())
                        {
                            AccountDetails = getDataAccounts.getAccountData(item);
                            if(AccountDetails.Name == User.Text.Trim() && AccountDetails.Password == Pass.Text.Trim())
                            {
                                MainWindow mainWindow = new MainWindow(AccountDetails.Name);
                                mainWindow.Show();
                                this.Close();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter Account Details ");
            }
        }
    }
}
