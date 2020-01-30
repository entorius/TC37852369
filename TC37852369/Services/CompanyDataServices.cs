using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;
using TC37852369.Repository;

namespace TC37852369.Services
{
    public class CompanyDataServices
    {
        CompanyDataRepository companyDataRepository = new CompanyDataRepository();
        public async Task<CompanyData> GetCompanyData()
        {
            return await companyDataRepository.GetCompanyData();
        }
        public async Task<CompanyData> EditCompanyData(string address, string companyName, string email,
            string phoneNumber, string webPageAddress,string emailSurename, string emailPassword,
            Image companyLogo)
        {
            string companyLogoLink = "";

            



            bool companyDataSaved = await companyDataRepository.EditCompanyData(address, companyName, email, phoneNumber,
                webPageAddress, companyLogoLink, emailSurename, emailPassword);

            if (companyDataSaved)
            {
                CompanyData companyData = new CompanyData(address, companyName, email, phoneNumber,
                webPageAddress, companyLogoLink, emailSurename, emailPassword);

                return companyData;
            }
            return null;
        }
    }
}
