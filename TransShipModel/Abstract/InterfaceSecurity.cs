using System.Collections.Generic;
using TransShipModel.DTO;

namespace TransShipModel.Abstract {
    public interface INSecurity
    {
        bool CheckUserName(string userName, string passWord);
        List<Country> GetAllCountries();
        string ConfirmUserName(string keyInformation);
        List<UserInformation> GetUserList();
        List<CountryInformation> GetCountryList();
        List<CusotmerInformation> GetPendingCustomers();
        PendingApprobalObject GetPendingApprobalRecord(decimal id);
        List<UserInformation> GetUserInformation(string email);
    }
}
