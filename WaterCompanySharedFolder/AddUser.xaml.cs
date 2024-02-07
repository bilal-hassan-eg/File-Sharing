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
using WaterCompanySharedFolder.pushData;
using WaterCompanySharedFolder.Download;
namespace WaterCompanySharedFolder
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {

        public AddUser()
        {
            InitializeComponent();
            Refresh();
        }
        private void Refresh()
        {
            try
            {
                getDataAccounts getDataAccounts = new getDataAccounts();
                List<Account> accounts = getDataAccounts.GetAccounts(getDataAccounts.Get());
                List<string> accountsString = new List<string>();
                foreach (Account account in accounts)
                {
                    accountsString.Add(account.Name + "      " + account.Password);
                }
                ListAccounts.ItemsSource = accountsString;
            }
            catch { }

        }
        private void CreateNew(object sender, RoutedEventArgs e)
        {
            if (User.Text != "" && Pass.Text != "")
            {
                PushData pushData = new PushData(User.Text, Pass.Text);
                pushData.Upload();
                Refresh();
            }
            else
            {
                MessageBox.Show("Enter Data");
            }
        }
    }
}
