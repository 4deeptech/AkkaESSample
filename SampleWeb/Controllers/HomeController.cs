using Domain;
using Messages;
using SampleWeb.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Akka.Actor;
using SampleWeb.Helpers;
using System.Threading;

namespace SampleWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Accounts()
        {
            List<Account> accounts = ProjectionRetriever.GetAccounts();
            //http://127.0.0.1:2113/projection/account-index/state
            return View(accounts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Account acc)
        {
            SystemActors.CommandProcessor.Tell(new CreateAccount(Guid.NewGuid(),acc.TaxNumber,acc.EntityName,acc.AccountType));
            //Simulate Eventually consistency
            Thread.Sleep(2000);
            return new RedirectResult("/home/accounts");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            AccountAddressDTO dto = new AccountAddressDTO();
            var acc = ProjectionRetriever.GetAccounts().Single(t=>t.AccountId.ToString().Equals(id));
            if(acc != null)
            {
                dto.AccountId = acc.AccountId;
                if(acc.MailingAddress != null)
                {
                    dto.StreetAddress1 = acc.MailingAddress.StreetAddress1;
                    dto.StreetAddress2 = acc.MailingAddress.StreetAddress2;
                    dto.CityTownVilla = acc.MailingAddress.CityTownVilla; 
                    dto.StateProvinceTerritory = acc.MailingAddress.StateProvinceTerritory;
                    dto.PostalCode = acc.MailingAddress.PostalCode;
                }
                return View(dto);
            }
            else
            {
                dto.AccountId = new Guid(id);
                return View(dto);
            }
        }

        [HttpPost]
        public ActionResult Edit(AccountAddressDTO dto)
        {
            SystemActors.CommandProcessor.Tell(new UpdateAccountMailingAddress(dto.AccountId, 
                new Domain.Address(dto.StreetAddress1,dto.StreetAddress2,dto.CityTownVilla,dto.StateProvinceTerritory,dto.PostalCode)));
            //Simulate Eventually consistency
            Thread.Sleep(2000);
            return new RedirectResult("/home/accounts");
        }
    }

    public class AccountAddressDTO
    {
        public Guid AccountId { get; set; }
        public string CountryCode { get; set; }
        public string StateProvinceTerritory { get; set; }
        public string PostalCode { get; set; }
        public string CityTownVilla { get; set; }
        public string Locality { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}