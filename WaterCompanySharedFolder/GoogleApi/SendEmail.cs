using System;
using System.IO;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace WaterCompanySharedFolder.GoogleApi
{
    internal class SendEmail
    {
        string[] Scopes = { GmailService.Scope.GmailSend };
        string ApplicationName = "WaterLuxor";
        string Email;
        string Subject;
        string FileLink;
        public SendEmail(string email, string subject, string fileLink)
        {
            Email=email;
            Subject=subject;
            FileLink=fileLink;
        }
        public void Send()
        {
            UserCredential credential;
            //read your credentials file
            using (FileStream stream = new FileStream(@"./APIGmail.json", FileMode.Open, FileAccess.Read))
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                path = Path.Combine(path, ".credentials/gmail-dotnet-quickstart.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(path, true)).Result;
            }

            string message = $"To: {Email}\r\nSubject: {Subject}\r\nContent-Type: text/html;charset=utf-8\r\n\r\n<h1>{FileLink}</h1>";
            //call your gmail service
            var service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential, ApplicationName = ApplicationName });
            var msg = new Google.Apis.Gmail.v1.Data.Message();
            msg.Raw = Base64UrlEncode(message.ToString());
            service.Users.Messages.Send(msg, "me").Execute();
        }
        string Base64UrlEncode(string input)
        {
            var data = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(data).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
    }
}
