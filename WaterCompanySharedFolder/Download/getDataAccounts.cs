using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FireSharp.Config;
using FireSharp.Interfaces;
using WaterCompanySharedFolder.pushData;

namespace WaterCompanySharedFolder.Download
{
    internal class getDataAccounts
    {
        public getDataAccounts() { }

        public List<string> Get()
        {
            List<string> list = new List<string>();
            string AuthToken = "OAp92FaHkmHSKBXVbtppjozhkVHHlBosUBPBU64T";
            string BaaePath = "https://com-inf-ca85f.firebaseio.com/";
            try
            {
                IFirebaseConfig fcon = new FirebaseConfig()
                {
                    AuthSecret = AuthToken,
                    BasePath = BaaePath
                };
                IFirebaseClient client = new FireSharp.FirebaseClient(fcon);
                var result = client.Get("Labs/");
                string account = result.Body;
                string account2 = account.Replace('"', ' ');
                string account3 = account2.Replace('/', ' ');
                string account4 = account3.Replace('{', ' ').Replace("}", " ").Replace(",", " ").Trim();
                string account5 = account4.Remove(account4.Length - 1, 1).Trim();
                list = account5.Split(':').ToList();
            }
            catch { MessageBox.Show("Check Your Internt Connection ...."); }
            return list;
        }

        public Account getAccountData(string LabsName)
        {
            Account accounts = new Account("","");
            string AuthToken = "OAp92FaHkmHSKBXVbtppjozhkVHHlBosUBPBU64T";
            string BaaePath = "https://com-inf-ca85f.firebaseio.com/";
            try
            {
                IFirebaseConfig fcon = new FirebaseConfig()
                {
                    AuthSecret = AuthToken,
                    BasePath = BaaePath
                };
                IFirebaseClient client = new FireSharp.FirebaseClient(fcon);
                var result = client.Get("Accounts"+'/'+LabsName.Trim());
                Account account = result.ResultAs<Account>();
                accounts = account;
            }
            catch { MessageBox.Show("Check Your Internt Connection ...."); }
            return accounts;
        
        }

        public List<Account> GetAccounts(List<string> Labs)
        {
            List<Account> accounts = new List<Account>();
            string AuthToken = "OAp92FaHkmHSKBXVbtppjozhkVHHlBosUBPBU64T";
            string BaaePath = "https://com-inf-ca85f.firebaseio.com/";
            try
            {
                foreach(string item in Labs)
                {
                    IFirebaseConfig fcon = new FirebaseConfig()
                    {
                        AuthSecret = AuthToken,
                        BasePath = BaaePath
                    };
                    IFirebaseClient client = new FireSharp.FirebaseClient(fcon);
                    var result = client.Get("Accounts"+'/'+item.Trim());
                    Account account = result.ResultAs<Account>();
                    accounts.Add(account);
                }
            }
            catch { MessageBox.Show("Check Your Internt Connection ...."); }
            return accounts;
        }
    }
}
