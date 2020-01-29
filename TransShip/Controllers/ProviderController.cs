using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using TransShipModel.DTO;
using TransShipModel.Concret;

namespace TransShip.Controllers {
    public class ProviderController : ApiController
    {
        [HttpPost]
        public long ApproveCustomer(UserInformation id)
        {
            LogBook.LogBook.TextLog.Info("Approving customer ..... {0}", id);
            var repository = new INSecurityImplementation();
            var contryList = repository.UpdateUserState(id.customerId,id.userId,"A", Request.RequestUri.GetLeftPart(UriPartial.Authority));
            return contryList > 0 ? 1 : 0;
        }

        [HttpPost]
        public long RejectCustomer(UserInformation id)
        {
            LogBook.LogBook.TextLog.Info("Rejecting customer ..... {0}", id);
            var repository = new INSecurityImplementation();
            var contryList = repository.UpdateUserState(id.customerId,id.userId, "I", Request.RequestUri.GetLeftPart(UriPartial.Authority));
            return contryList > 0 ? 1 : 0;
        }


        [HttpGet]
        public PendingApprobalObject GetPendingApprovalRecord(decimal id)
        {
            LogBook.LogBook.TextLog.Info("Getting pending approbal information ..... {0}", id);
            var repository = new INSecurityImplementation();
            var contryList = repository.GetPendingApprobalRecord(id);
            return contryList;
        }


        [HttpPost]
        public long EraseCountry(CountryInformation userInformation)
        {
            LogBook.LogBook.TextLog.Info(string.Format("Request {0}", Request.RequestUri.GetLeftPart(UriPartial.Authority)));
            LogBook.LogBook.TextLog.Info(string.Format("User Id {0}", userInformation.Id));
            var repository = new INSecurityImplementation();
            var result = repository.EraseCountry(userInformation);
            return result >= 1 ? (long)1 : (long)0;
        }

        [HttpPost]
        public long ChangeCountry(CountryInformation information)
        {
            LogBook.LogBook.TextLog.Info(string.Format("Country Name {0}", information.Name));
            LogBook.LogBook.TextLog.Info(string.Format("Long Country Name {0}", information.LongCountryName));
            var repository = new INSecurityImplementation();
            var result = repository.UpdateCountry(information);
            return result >= 1 ? (long)1 : (long)0;
        }

        [HttpGet]
        public CountryInformation GetCountryInformation(long id)
        {
            LogBook.LogBook.TextLog.Info("Getting country information ..... {0}", id);
            var repository = new INSecurityImplementation();
            var contryList = repository.GetCountryInformation(id);
            return contryList.Any() ? contryList.First() : null;
        }

        [HttpPost]
        public long AddCountry(CountryInformation information)
        {
            LogBook.LogBook.TextLog.Info(string.Format("Country Name {0}", information.Name));
            LogBook.LogBook.TextLog.Info(string.Format("Long Country Name {0}", information.LongCountryName));
            var repository = new INSecurityImplementation();
            var result = repository.AddNewCountry(information);
            return result >= 1 ? (long)1 : (long)0;
        }

        [HttpPost]
        public long ChangePassword(ChangePassword information)
        {
            LogBook.LogBook.TextLog.Info(string.Format("Password {0}", information.password));
            LogBook.LogBook.TextLog.Info(string.Format("API Key {0}", information.APIKey));
            var repository = new INSecurityImplementation();
            var result = repository.ChangePassword(information, Request.RequestUri.GetLeftPart(UriPartial.Authority));
            return string.IsNullOrEmpty(result) ? (long)0 : (long)1;
        }

        [HttpPost]
        public long RecoverPassword(RecoverPassword recoverEmail)
        {
            LogBook.LogBook.TextLog.Info(string.Format("EMail {0}", recoverEmail.recoverEmail));            
            var repository = new INSecurityImplementation();
            var result = repository.RecoverPassword(recoverEmail.recoverEmail, Request.RequestUri.GetLeftPart(UriPartial.Authority));            
            return string.IsNullOrEmpty(result)  ? (long)0 : (long)1;
        }



        [HttpPost]
        public long EraseUser(UserInformation userInformation)
        {
            LogBook.LogBook.TextLog.Info(string.Format("Request {0}", Request.RequestUri.GetLeftPart(UriPartial.Authority)));
            LogBook.LogBook.TextLog.Info(string.Format("User Id {0}", userInformation.id));            
            var repository = new INSecurityImplementation();
            var result = repository.EraseUser(userInformation);
            return result >= 1 ? (long)1 : (long)0;
        }

        [HttpPost]
        public long ModifyUserName(UserInformation userInformation)
        {
            LogBook.LogBook.TextLog.Info(string.Format("Request {0}", Request.RequestUri.GetLeftPart(UriPartial.Authority)));
            LogBook.LogBook.TextLog.Info(string.Format("User Id {0}", userInformation.id));            
            var repository = new INSecurityImplementation();
            var result = repository.ModifyUser(userInformation);
            return result >= 1 ? (long)1 : (long)0;
        }

        [HttpPost]
        public long CreateUserName(CreateUserObject createUserObject)
        {
            LogBook.LogBook.TextLog.Info(string.Format("Request {0}", Request.RequestUri.GetLeftPart(UriPartial.Authority)));
            LogBook.LogBook.TextLog.Info(string.Format("User Name {0}", createUserObject.username));
            LogBook.LogBook.TextLog.Info(string.Format("Company Name {0}", createUserObject.companyname));
            LogBook.LogBook.TextLog.Info(string.Format("Zip Code {0}", createUserObject.zipcode));
            LogBook.LogBook.TextLog.Info(string.Format("Id Country {0}", createUserObject.idCountry));
            LogBook.LogBook.TextLog.Info(string.Format("US State {0}", createUserObject.usstate));
            LogBook.LogBook.TextLog.Info(string.Format("Canada State {0}", createUserObject.canadaprovince));
            LogBook.LogBook.TextLog.Info(string.Format("Out Side US and Canada {0}", createUserObject.outsideus));
            LogBook.LogBook.TextLog.Info(string.Format("Membership {0}", createUserObject.membership));
            var repository = new INSecurityImplementation();                       
            var result = repository.CreateUser(createUserObject, Request.RequestUri.GetLeftPart(UriPartial.Authority));
            return result >= 1 ? (long)1 : (long)0;
        }

        [HttpGet]
        public UserInformation GetUserInformation(decimal id)
        {
            LogBook.LogBook.TextLog.Info("Getting user information ..... {0}", id);
            var repository = new INSecurityImplementation();
            var contryList = repository.GetUserInformation(id);
            return contryList.Any() ? contryList.First() : null;
        }

        [HttpGet]
        public IEnumerable<Country> GetCountryList()
        {
            LogBook.LogBook.TextLog.Info("Getting country list....");
            var repository = new INSecurityImplementation();
            var contryList = repository.GetAllCountries();
            return contryList;
        }

        [HttpGet]
        public IEnumerable<MembershipLelvelObject> GetMembershipLevel()
        {
            LogBook.LogBook.TextLog.Info("Getting membership level list....");
            var repository = new INSecurityImplementation();
            var membershipLevelList = repository.GetMembershipLevelList();
            return membershipLevelList;
        }
    }
}
