using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;
using TC37852369.Repository;

namespace TC37852369.Services
{
    public class UserServices
    {
        UserRepository userRepository = new UserRepository();
        LastEntityIdentificationNumberServices lastEntityIdentificationNumberServices =
            new LastEntityIdentificationNumberServices(); 
        public async Task<bool> addUser(string username, string password, string mail, string phoneNumber, string name, string surename)
        {
            LastIdentificationNumber lastIdentificationNumber = await lastEntityIdentificationNumberServices.getUserLastIdentificationNumber();
            return await userRepository.addUser(username,password,mail,phoneNumber,name,surename, lastIdentificationNumber.id);
        }

        public async Task<User> GetUser(string username, string password)
        {
            return await userRepository.GetUser(username,password);
        }

        public async Task<bool> deleteUser(string user_Id)
        {
            return await userRepository.deleteUser(user_Id);
        }
        public bool isUsernameCorrect(string username)
        {
            if (username.Length >= 6)
            {
                return true;
            }
            return false;
        }
        public bool isPasswodCorrect(string password, string confirmPassword)
        {
            if (password.Length >= 6 && confirmPassword.Length >= 6 && password.Equals(confirmPassword))
            {
                return true;
            }
            return false;
        }
    }
}
