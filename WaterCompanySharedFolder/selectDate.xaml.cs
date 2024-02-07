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

namespace WaterCompanySharedFolder
{
    /// <summary>
    /// Interaction logic for selectDate.xaml
    /// </summary>
    public partial class selectDate : Window
    {
        public string User { get; set; }
        public selectDate(string user)
        {
            this.User = user;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WaterCompanySharedFolder.Download.getFiles getFiles = new Download.getFiles("C:/");
            getFiles.GET(User, datePicker.Text);
        }
    }
}
