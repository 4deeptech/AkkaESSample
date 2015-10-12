using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SampleWeb.Helpers
{
    public static class ProjectionRetriever
    {
        public static List<Account> GetAccounts()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://127.0.0.1:2113/projection/account-index/state");
                return JsonConvert.DeserializeObject<AccountListing>(json).Accounts;
            }
        }
    }

    public class AccountListing
    {
        public List<Account> Accounts;
    }
}