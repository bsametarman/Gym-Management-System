using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Core.Utilities.Errors;
using System.ComponentModel.DataAnnotations;
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

        public List<ValidationError> CheckName(string name)
        {
            List<ValidationError> errors = new List<ValidationError>();
            if (name == null)
                errors.Add(new ValidationError { Title = "Invalid name", Description = "Name cannot be null." });
            else if (name.Length < 2 || name.Length > 85)
                errors.Add(new ValidationError { Title = "Invalid name", Description = "Name must be between 2 and 85 characters." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<ValidationError> CheckSurname(string surname)
        {
            List<ValidationError> errors = new List<ValidationError>();
            if (surname == null)
                errors.Add(new ValidationError { Title = "Invalid surname", Description = "Surname cannot be null." });
            else if (surname.Length < 2 || surname.Length > 85)
                errors.Add(new ValidationError { Title = "Invalid surname", Description = "Surname must be between 2 and 85 characters." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<ValidationError> CheckUsername(string username)
        {
            List<ValidationError> errors = new List<ValidationError>();

            var users = _userManager.GetAll();

            foreach (var user in users.Data)
                if (user.UserName == username) {
                    errors.Add(new ValidationError { Title = "Invalid username", Description = "Username has taken please choose another one." });
                    break;
                }
            
            if (username == null)
                errors.Add(new ValidationError { Title = "Invalid username", Description = "Username cannot be null." });
            else if (username.Length < 2 || username.Length > 25)
                errors.Add(new ValidationError { Title = "Invalid username", Description = "Username must be between 2 and 25 characters." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<ValidationError> CheckPassword(string password)
        {
            List<ValidationError> errors = new List<ValidationError>();
            if (password == null)
                errors.Add(new ValidationError { Title = "Invalid password", Description = "Password cannot be null." });
            else if (password.Length < 6 || password.Length > 25)
                errors.Add(new ValidationError { Title = "Invalid password", Description = "Password must be longer than 6 characters." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<ValidationError> CheckEmail(string email)
        {
            List<ValidationError> errors = new List<ValidationError>();

            var users = _userManager.GetAll();

            foreach (var user in users.Data)
                if (user.Email == email)
                {
                    errors.Add(new ValidationError { Title = "Invalid email", Description = "Email has taken type another one." });
                    break;
                }

            if (email == null)
                errors.Add(new ValidationError { Title = "Invalid email", Description = "Email cannot be null." });
            else if (!(new EmailAddressAttribute().IsValid(email)))
                errors.Add(new ValidationError { Title = "Invalid email", Description = "Email address is not valid." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<ValidationError> CheckYearOfBirth(string yearOfBirth)
        {
            List<ValidationError> errors = new List<ValidationError>();

            if (yearOfBirth == null)
                errors.Add(new ValidationError { Title = "Invalid birth year", Description = "Birth year cannot be null." });
            else if(!int.TryParse(yearOfBirth, out _))
                errors.Add(new ValidationError { Title = "Invalid birth year", Description = "Birth year cannot be text." });
            else if (yearOfBirth.Length != 4)
                errors.Add(new ValidationError { Title = "Invalid birth year", Description = "Birth year must be 4 digits long." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<ValidationError> CheckIdentityNumber(string identityNumber)
        {
            List<ValidationError> errors = new List<ValidationError>();

            var users = _userManager.GetAll();

            foreach (var user in users.Data)
                if (user.IdentityNumber == identityNumber)
                {
                    errors.Add(new ValidationError { Title = "Invalid identity number", Description = "Identity number must be unique." });
                    break;
                }

            if (identityNumber == null)
                errors.Add(new ValidationError { Title = "Invalid identity number", Description = "Identity number cannot be null." });
            else if (!long.TryParse(identityNumber, out _))
                errors.Add(new ValidationError { Title = "Invalid identity number", Description = "Identity number cannot be text." });
            else if (identityNumber.Length != 11)
                errors.Add(new ValidationError { Title = "Invalid identity number", Description = "Identity number must be 11 digits long." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<ValidationError> CheckAddress(string address)
        {
            List<ValidationError> errors = new List<ValidationError>();
            if (address == null)
                errors.Add(new ValidationError { Title = "Invalid address", Description = "Address cannot be null." });
            else if (address.Length < 15 || address.Length > 500)
                errors.Add(new ValidationError { Title = "Invalid address", Description = "Address must be between 15 and 500 characters." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<ValidationError> CheckPhoneNumber(string phoneNumber)
        {
            List<ValidationError> errors = new List<ValidationError>();
            Regex regexValidate = new Regex("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}");

            var users = _userManager.GetAll();

            foreach (var user in users.Data)
                if (user.PhoneNumber == phoneNumber)
                {
                    errors.Add(new ValidationError { Title = "Invalid phone number", Description = "Phone number has taken please choose another one." });
                    break;
                }

            if (phoneNumber == null)
                errors.Add(new ValidationError { Title = "Invalid phone number", Description = "Phone number cannot be null." });
            else if(!regexValidate.IsMatch(phoneNumber))
                errors.Add(new ValidationError { Title = "Invalid phone number", Description = "Enter a valid phone number." });

            if (errors.Count > 0)
                return errors;
            return null;
        }

        public List<ValidationError> CheckEmergencyPhoneNumber(string emergencyPhoneNumber)
        {
            List<ValidationError> errors = new List<ValidationError>();
            Regex regexValidate = new Regex("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}");
            if (emergencyPhoneNumber == null)
                errors.Add(new ValidationError { Title = "Invalid emergency phone number", Description = "Emergency phone number cannot be null." });
            else if (!regexValidate.IsMatch(emergencyPhoneNumber))
                errors.Add(new ValidationError { Title = "Invalid emergency phone number", Description = "Enter a valid emergency phone number." });

            if (errors.Count > 0)
                return errors;
            return null;
        }
    }
}
