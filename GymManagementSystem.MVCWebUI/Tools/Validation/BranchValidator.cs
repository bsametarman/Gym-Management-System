using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Core.Utilities.Errors;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GymManagementSystem.MVCWebUI.Tools.Validation
{
    public class BranchValidator
    {
		private IBranchService _branchService;
        public BranchValidator(IBranchService branchService)
        { 
			_branchService= branchService;
        }

        public List<ValidationError> CheckName(string name)
        {
			List<ValidationError> errors = new List<ValidationError>();

            if (name == null)
                errors.Add(new ValidationError{ Title = "Null branch name", Description = "Branch name cannot be null."});
			else if (name.Length < 3 || name.Length > 50)
				errors.Add(new ValidationError{ Title = "Branch name length is wrong", Description = "Branch name must be between 3 and 50 characters." });

			return errors;
		}

        public List<ValidationError> CheckDescription(string description)
        {
			List<ValidationError> errors = new List<ValidationError>();

			if (description == null)
				errors.Add(new ValidationError{ Title = "Null branch name", Description = "Branch name cannot be null." });
			else if (description.Length < 10 || description.Length > 500)
				errors.Add(new ValidationError{ Title = "Description length is wrong", Description = "Description must be between 10 and 500 characters." });

			return errors;
		}

        public List<ValidationError> CheckEmail(string email)
        {
			List<ValidationError> errors = new List<ValidationError>();

			var branches = _branchService.GetAll();

			if (email == null)
				errors.Add(new ValidationError { Title = "Invalid email", Description = "Email cannot be null." });
			else if (!(new EmailAddressAttribute().IsValid(email)))
				errors.Add(new ValidationError { Title = "Invalid email", Description = "Email address is not valid." });

			foreach (var branch in branches.Data)
				if (branch.Email == email)
					errors.Add(new ValidationError { Title = "Invalid Email", Description = "Email has taken." });

			return errors;
		}

        public List<ValidationError> CheckAddress(string address)
        {
			List<ValidationError> errors = new List<ValidationError>();

			if (address == null)
				errors.Add(new ValidationError { Title = "Null address", Description = "Address cannot be null." });
			else if (address.Length < 10 || address.Length > 500)
				errors.Add(new ValidationError { Title = "Address length is wrong", Description = "Address must be between 10 and 500 characters." });

			return errors;
		}

		public List<ValidationError> CheckPhoneNumber(string phoneNumber)
		{
			List<ValidationError> errors = new List<ValidationError>();

			var branches = _branchService.GetAll();

			Regex regexValidate = new Regex("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}");

			if (phoneNumber == null)
				errors.Add(new ValidationError { Title = "Null phone number", Description = "Phone number cannot be null." });
			else if (!regexValidate.IsMatch(phoneNumber))
				errors.Add(new ValidationError { Title = "Invalid phone number", Description = "Enter valid phone number." });

			foreach (var branch in branches.Data)
				if (branch.PhoneNumber == phoneNumber)
					errors.Add(new ValidationError { Title = "Invalid Email", Description = "Email has taken." });

			return errors;
		}

		public List<ValidationError> CheckCity(string city)
		{
			List<ValidationError> errors = new List<ValidationError>();

			if (city == null)
				errors.Add(new ValidationError { Title = "Null city name", Description = "City name cannot be null." });
			else if (city.Length < 2 || city.Length > 50)
				errors.Add(new ValidationError { Title = "City name length is wrong", Description = "City name must be between 2 and 50 characters." });

			return errors;
		}

		public List<ValidationError> CheckCountry(string country)
		{
			List<ValidationError> errors = new List<ValidationError>();

			if (country == null)
				errors.Add(new ValidationError { Title = "Null country name", Description = "Country name cannot be null." });
			else if (country.Length < 2 || country.Length > 50)
				errors.Add(new ValidationError { Title = "Country name length is wrong", Description = "Country name must be between 2 and 50 characters." });

			return errors;
		}

		public List<ValidationError> CheckSquareMeters(string squareMeters)
		{
			List<ValidationError> errors = new List<ValidationError>();

			if (squareMeters == null)
				errors.Add(new ValidationError { Title = "Null square meters", Description = "Square meters cannot be null." });
			else if (squareMeters.Length < 2 || squareMeters.Length > 50)
				errors.Add(new ValidationError { Title = "Square meters length is wrong", Description = "Square meters must be between 2 and 50 characters." });

			return errors;
		}

		public List<ValidationError> CheckBranchWorkingTime(string weekdaysOpeningTime, string weekdaysClosingTime, string weekendsOpeningTime, string weekendsClosingTime)
		{
			List<ValidationError> errors = new List<ValidationError>();

			if (weekdaysOpeningTime == null)
				errors.Add(new ValidationError { Title = "Null weekdays opening time", Description = "Weekdays opening time cannot be null." });
			else if (weekdaysOpeningTime.Length < 2 || weekdaysOpeningTime.Length > 50)
				errors.Add(new ValidationError { Title = "Weekdays opening time length is wrong", Description = "Weekdays opening time must be between 2 and 50 characters." });

			if (weekdaysClosingTime == null)
				errors.Add(new ValidationError { Title = "Null weekdays closing time", Description = "Weekdays closing time cannot be null." });
			else if (weekdaysClosingTime.Length < 2 || weekdaysClosingTime.Length > 50)
				errors.Add(new ValidationError { Title = "Weekdays closing time length is wrong", Description = "Weekdays closing time must be between 2 and 50 characters." });

			if (weekendsOpeningTime == null)
				errors.Add(new ValidationError { Title = "Null weekends opening time", Description = "Weekends opening time cannot be null." });
			else if (weekendsOpeningTime.Length < 2 || weekendsOpeningTime.Length > 50)
				errors.Add(new ValidationError { Title = "Weekends opening time length is wrong", Description = "Weekends opening time must be between 2 and 50 characters." });

			if (weekendsClosingTime == null)
				errors.Add(new ValidationError { Title = "Null weekends closing time", Description = "Weekends closing time cannot be null." });
			else if (weekendsClosingTime.Length < 2 || weekendsClosingTime.Length > 50)
				errors.Add(new ValidationError { Title = "Weekends closing time length is wrong", Description = "Weekends closing time must be between 2 and 50 characters." });

			return errors;
		}

		public List<ValidationError> CheckCapacity(int capacity)
		{
			List<ValidationError> errors = new List<ValidationError>();

			if (capacity < 100 || capacity > 700)
				errors.Add(new ValidationError { Title = "Capacity length is wrong", Description = "Capacity must be between 100 and 700." });

			return errors;
		}

		public List<ValidationError> CheckNumberOfTools(int numberOfTools)
		{
			List<ValidationError> errors = new List<ValidationError>();

			if (numberOfTools < 20 || numberOfTools > 500)
				errors.Add(new ValidationError { Title = "Number of tools length is wrong", Description = "Number of tools must be between 20 and 500." });

			return errors;
		}

		public List<ValidationError> CheckNumberOfEmployee(int numberOfEmployee)
		{
			List<ValidationError> errors = new List<ValidationError>();

			if (numberOfEmployee < 5 || numberOfEmployee > 100)
				errors.Add(new ValidationError { Title = "Number of employee length is wrong", Description = "Number of employee must be between 5 and 100." });

			return errors;
		}
	}
}
