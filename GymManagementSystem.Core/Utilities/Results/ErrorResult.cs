using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(bool success, string message) : base(success, message)
        {
        }

        public ErrorResult(string message) : base(false, message)
        {
        }

        public ErrorResult() : base(false)
        {
        }
    }
}
