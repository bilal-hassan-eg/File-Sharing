using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Requests;
using Google.Apis.Util;
using Google.Apis.Drive.v3.Data;
using System.Windows;
using System.IO;
using System.Threading;

namespace WaterCompanySharedFolder.GoogleApi
{
    class UploadFileDrive
    {
        string[] Scopes = { DriveService.Scope.Drive };
        string ApplicationName = "WaterLuxor";
        string path;
        string Email;
        public UploadFileDrive(string path, string email)
        {
            this.path=path;
            Email=email;
        }

        public string Upload()
        {
            UserCredential credential;
            credential = GetCredentials();
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            string id = UploadBasicImage(path, service);
            string pageToken = null;
            do
            {
                ListFiles(service, ref pageToken);
            } while (pageToken!=null);
            return id;
        }

        private void ListFiles(DriveService service, ref string pageToken)
        {
            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 1;
            //listRequest.Fields = "nextPageToken, files(id, name)";
            listRequest.Fields = "nextPageToken, files(name)";
            listRequest.PageToken = pageToken;
            listRequest.Q = "mimeType='document/xlsx'";

            // List files.
            var request = listRequest.Execute();
            if (request.Files != null && request.Files.Count > 0)
            {
                pageToken = request.NextPageToken;
            }
            else
            {
                MessageBox.Show("No File Found");
            }
        }

        private string UploadBasicImage(string path, DriveService service)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File();
            fileMetadata.Name = Path.GetFileName(path);
            fileMetadata.MimeType = "document/xlsx";
            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, "document/xlsx");

                request.Fields = "id";
                request.Upload();
                Google.Apis.Drive.v3.Data.File file = request.ResponseBody;

                Permission newPermission = new Permission();
                newPermission.EmailAddress = Email;
                newPermission.Type = "user";
                newPermission.Role = "writer";

                PermissionsResource.CreateRequest insertRequest = service.Permissions.Create(newPermission, file.Id);
                insertRequest.SendNotificationEmail = false;
                insertRequest.Execute();
            }

            var file1 = request.ResponseBody;
            return file1.Id;

        }

        private UserCredential GetCredentials()
        {
            UserCredential credential;

            using (var stream = new FileStream(@"./APIDrive.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                // Console.WriteLine("Credential file saved to: " + credPath);
            }

            return credential;
        }
    }
}