using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Account
    {
        /// <summary>
        /// Unique system generated identifier for account
        /// </summary>
        public Guid AccountId { get; set; }

        public string EntityName { get; set; }
        public string TaxNumber { get; set; }
        public AccountType AccountType { get; set; }
        public Address MailingAddress { get; set; }

        public Account()
        {
        }
    }

    public enum AccountType
    {
        Individual = 0,
        Business = 1
    }
}
