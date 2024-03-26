using Clinics.Core.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Interfaces
{
    public interface IAuth
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(Login model);
        Task<AuthModel> ConfirmEmailAsync(string userid, string token);
        Task<AuthModel> ForgetPasswordAsync(string email);
        Task<AuthModel> ResetPasswordAsync(ResetPassword model);

    }
}
