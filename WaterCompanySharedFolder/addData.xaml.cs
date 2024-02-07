using WaterCompanySharedFolder.pushData;
using System.Windows;
using System.Data;
using System.Collections.Generic;

namespace WaterCompanySharedFolder
{
    /// <summary>
    /// Interaction logic for addData.xaml
    /// </summary>
    public partial class addData : Window
    {
        public string User { get; set; }
        public addData(string User)
        {
            InitializeComponent();
            this.User = User;
        }

        private void PushData(object sender, RoutedEventArgs e)
        {
            UploadFile uploadFile = new UploadFile(PathOFExelFile.Text, User);
            uploadFile.Upload();
        }

        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                PathOFExelFile.Text = openFileDialog.FileName;
        }
    }
}
