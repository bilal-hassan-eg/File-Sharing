using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WaterCompanySharedFolder.Download
{
    internal class getFiles
    {
        private string pathOfNewFiles;
        public getFiles(string pathOfNewFiles)
        {
            this.pathOfNewFiles=pathOfNewFiles;
        }

        public async Task GET(string User,string date)
        {
            string[] arr = date.Split('/');
            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (User == "Admin")
            {
                getDataAccounts getDataAccounts = new getDataAccounts();
                List<string> Labs = getDataAccounts.Get();
                foreach (string LabItem in Labs)
                {
                    try
                    {

                        FirebaseStorage storage = new FirebaseStorage("com-inf-ca85f.appspot.com");
                        var starsRef = storage.Child($"Reports/{LabItem.Trim()}:{arr[2]}-{arr[0]}.xlsx");
                        string link = await starsRef.GetDownloadUrlAsync();
                        WebClient webClient = new WebClient();
                        await webClient.DownloadFileTaskAsync(link, $"{docPath}"+@"\"+$"{LabItem.Trim()}.xlsx");
                        MessageBox.Show($"{LabItem}:{arr[2]}-{arr[0]} Done");

                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("404"))
                        {
                            MessageBox.Show($"{LabItem} Not Found");
                        }
                        else
                        {
                            MessageBox.Show("Check Internet Connection");
                        }
                    }
                }
                MessageBox.Show("Done");
            }
            else
            {
                try
                {
                    FirebaseStorage storage = new FirebaseStorage("com-inf-ca85f.appspot.com");
                    var starsRef = storage.Child($"Reports/{User}.xlsx");
                    string link = await starsRef.GetDownloadUrlAsync();
                    WebClient webClient = new WebClient();
                    await webClient.DownloadFileTaskAsync(link, $"{docPath}"+$"{User}.xlsx");
                    MessageBox.Show("Done");
                }
                catch(Exception ex)
                {
                    if (ex.Message.Contains("404"))
                    {
                        MessageBox.Show($"{User} Not Found");
                    }
                    else
                    {
                        MessageBox.Show("Check Internet Connection");
                    }
                }
            }
        }
    }
}
