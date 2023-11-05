using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.RegularExpressions;

namespace GymManagementSystem.MVCWebUI.Tools.Validation
{
    public class UserValidator
    {
        private IUserService _userManager;

        public UserValidator(IUserService userManager)
        {
            _userManager = userManager;
        }

        public List<IdentityError> CheckName(string name)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (name == null)
                errors.Add(new IdentityError { Code = "Invalid name", Description = "Name cannot be null." });
            else if (name.Length < 2 || name.Length > 85)
                errors.Add(new IdentityError { Code = "Invalid name", Description = "Name must be between 2 and 85 characters." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<IdentityError> CheckSurname(string surname)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (surname == null)
                errors.Add(new IdentityError { Code = "Invalid surname", Description = "Surname cannot be null." });
            else if (surname.Length < 2 || surname.Length > 85)
                errors.Add(new IdentityError { Code = "Invalid surname", Description = "Surname must be between 2 and 85 characters." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<IdentityError> CheckUsername(string username)
        {
            List<IdentityError> errors = new List<IdentityError>();

            var users = _userManager.GetAll();

            foreach (var user in users.Data)
                if (user.UserName == username) {
                    errors.Add(new IdentityError { Code = "Invalid username", Description = "Username has taken please choose another one." });
                    break;
                }
            
            if (username == null)
                errors.Add(new IdentityError { Code = "Invalid username", Description = "Username cannot be null." });
            else if (username.Length < 2 || username.Length > 25)
                errors.Add(new IdentityError { Code = "Invalid username", Description = "Username must be between 2 and 25 characters." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<IdentityError> CheckPassword(string password)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (password == null)
                errors.Add(new IdentityError { Code = "Invalid password", Description = "Password cannot be null." });
            else if (password.Length < 6 || password.Length > 25)
                errors.Add(new IdentityError { Code = "Invalid password", Description = "Password must be longer than 6 characters." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<IdentityError> CheckEmail(string email)
        {
            List<IdentityError> errors = new List<IdentityError>();

            var users = _userManager.GetAll();

            foreach (var user in users.Data)
                if (user.Email == email)
                {
                    errors.Add(new IdentityError { Code = "Invalid email", Description = "Email has taken type another one." });
                    break;
                }

            if (email == null)
                errors.Add(new IdentityError { Code = "Invalid email", Description = "Password cannot be null." });
            else if (!(new EmailAddressAttribute().IsValid(email)))
                errors.Add(new IdentityError { Code = "Invalid email", Description = "Email address is not valid." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<IdentityError> CheckYearOfBirth(string yearOfBirth)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (yearOfBirth == null)
                errors.Add(new IdentityError { Code = "Invalid birth year", Description = "Birth year cannot be null." });
            else if(!int.TryParse(yearOfBirth, out _))
                errors.Add(new IdentityError { Code = "Invalid birth year", Description = "Birth year cannot be text." });
            else if (yearOfBirth.Length != 4)
                errors.Add(new IdentityError { Code = "Invalid birth year", Description = "Birth year must be 4 digits long." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<IdentityError> CheckIdentityNumber(string identityNumber)
        {
            List<IdentityError> errors = new List<IdentityError>();

            var users = _userManager.GetAll();

            foreach (var user in users.Data)
                if (user.IdentityNumber == identityNumber)
                {
                    errors.Add(new IdentityError { Code = "Invalid identity number", Description = "Identity number must be unique." });
                    break;
                }

            if (identityNumber == null)
                errors.Add(new IdentityError { Code = "Invalid identity number", Description = "Identity number cannot be null." });
            else if (!long.TryParse(identityNumber, out _))
                errors.Add(new IdentityError { Code = "Invalid identity number", Description = "Identity number cannot be text." });
            else if (identityNumber.Length != 11)
                errors.Add(new IdentityError { Code = "Invalid identity number", Description = "Identity number must be 11 digits long." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<IdentityError> CheckAddress(string address)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if (address == null)
                errors.Add(new IdentityError { Code = "Invalid address", Description = "Address cannot be null." });
            else if (address.Length < 15 || address.Length > 500)
                errors.Add(new IdentityError { Code = "Invalid address", Description = "Address must be between 15 and 500 characters." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<IdentityError> CheckPhoneNumber(string phoneNumber)
        {
            List<IdentityError> errors = new List<IdentityError>();
            Regex regexValidate = new Regex("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}");

            var users = _userManager.GetAll();

            foreach (var user in users.Data)
                if (user.PhoneNumber == phoneNumber)
                {
                    errors.Add(new IdentityError { Code = "Invalid phone number", Description = "Phone number has taken please choose another one." });
                    break;
                }

            if (phoneNumber == null)
                errors.Add(new IdentityError { Code = "Invalid phone number", Description = "Phone number cannot be null." });
            else if(!regexValidate.IsMatch(phoneNumber))
                errors.Add(new IdentityError { Code = "Invalid phone number", Description = "Enter a valid phone number." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<IdentityError> CheckEmergencyPhoneNumber(string emergencyPhoneNumber)
        {
            List<IdentityError> errors = new List<IdentityError>();
            Regex regexValidate = new Regex("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}");
            if (emergencyPhoneNumber == null)
                errors.Add(new IdentityError { Code = "Invalid emergency phone number", Description = "Emergency phone number cannot be null." });
            else if (!regexValidate.IsMatch(emergencyPhoneNumber))
                errors.Add(new IdentityError { Code = "Invalid emergency phone number", Description = "Enter a valid emergency phone number." });

            if (errors.Count > 0)
                return errors;
            return null;
        }
    }
}
