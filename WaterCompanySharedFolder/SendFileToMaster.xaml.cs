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

using WaterCompanySharedFolder.GoogleApi;
namespace WaterCompanySharedFolder
{
    /// <summary>
    /// Interaction logic for SendFileToMaster.xaml
    /// </summary>
    public partial class SendFileToMaster : Window
    {
        public SendFileToMaster()
        {
            InitializeComponent();
        }
        private void SendEmail(object sender,RoutedEventArgs e)
        {
            try
            {
                if (PathOFExelFile.Text.Trim() != "" && EmailToSend.Text.Trim() != "" && Subject.Text.Trim() != "")
                {
                    UploadFileDrive uploadFileDrive = new UploadFileDrive(PathOFExelFile.Text, EmailToSend.Text);
                    string idFile = uploadFileDrive.Upload();
                    string LinkFile = $"https://drive.google.com/file/d/{idFile}/view";
                    WaterCompanySharedFolder.GoogleApi.SendEmail sendEmail = new SendEmail(EmailToSend.Text, Subject.Text, LinkFile);
                    sendEmail.Send();
                    MessageBox.Show("Done");
                }
                else
                {
                    MessageBox.Show("Enter Data");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                PathOFExelFile.Text = openFileDialog.FileName;
        }
    }
}
