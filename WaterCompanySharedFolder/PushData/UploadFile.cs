using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaterCompanySharedFolder.pushData
{
    internal class UploadFile
    {
        static int state = 0;
        private string pathFile; 
        private string LabLocation; 
        public UploadFile(string pathFile,string LabLocation)
        {
            state = 0;
            this.LabLocation = LabLocation;
            this.pathFile=pathFile;
        }
        public void Upload()
        {
            var cancellation = new CancellationTokenSource();
            var stream = File.Open(pathFile, FileMode.Open);
            var year = DateTime.Today.Year;
            var Month = DateTime.Today.Month;
            try
            {
                var task = new FirebaseStorage(
                    "com-inf-ca85f.appspot.com",
                    new FirebaseStorageOptions
                    {
                        ThrowOnCancel = true
                    })
                    .Child("Reports")
                    .Child(LabLocation+$":{year}-{Month}"+".xlsx")
                    .PutAsync(stream, cancellation.Token);

                task.Progress.ProgressChanged += (s, e) =>
                {
                    if (e.Percentage == 100)
                    {
                        if (state == 0)
                        {
                            state = 1;
                            System.Windows.MessageBox.Show("done");
                        }
                    }
                };

            }
            catch
            {

            }

        }
    }
}
