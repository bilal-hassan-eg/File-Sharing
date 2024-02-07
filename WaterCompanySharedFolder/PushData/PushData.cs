using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using System.Windows;
using System.Data;
using System.Collections.Generic;

namespace WaterCompanySharedFolder.pushData
{
    internal class PushData
    {
        string user;
        string pass;

        public PushData(string user, string pass)
        {
            this.user = user;
            this.pass = pass;
        }

        public void Upload()
        {
            string AuthToken = "OAp92FaHkmHSKBXVbtppjozhkVHHlBosUBPBU64T";
            string BaaePath = "https://com-inf-ca85f.firebaseio.com/";
            try
            {
                IFirebaseConfig fcon = new FirebaseConfig()
                {
                    AuthSecret = AuthToken,
                    BasePath = BaaePath
                };
                Account account = new Account(user, pass);
                IFirebaseClient client = new FireSharp.FirebaseClient(fcon);
                client.Set("Accounts/"+account.Name, account);
                client.Set("Labs/"+account.Name,"");
            }
            catch { MessageBox.Show("Check Your Internt Connection ...."); }

        }
    }
}
