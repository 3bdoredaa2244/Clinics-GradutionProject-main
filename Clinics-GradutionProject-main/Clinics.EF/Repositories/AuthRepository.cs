using Clinics.Core.Interfaces;
using Clinics.Core.Models;
using Clinics.Core.Models.Authentication;
using Clinics.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Clinics.EF.Repositories
{
    public class AuthRepository : IAuth
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly IConfiguration _configuration;
        protected ClinicContext _context;

        public AuthRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration, ClinicContext context)
        {
            _userManger = userManager;
            _configuration = configuration;
            _context = context;
            _context = context;
        }



        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManger.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };

            if (await _userManger.FindByNameAsync(model.Username) is not null)
                return new AuthModel { Message = "Username is already registered!" };

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManger.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };
            }
            var userId = user.Id;

            // Create a new patient with null values for address, bloodtype, and qrcode
            var patient = new Patient
            {
                UserId = userId,
                Address = null,
                bloodType = null,
                QrCode = null
            };

            // Add the new patient to the database
            _context.Patients.Add(patient);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            await _userManger.AddToRoleAsync(user, "User");


            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                Expiration = DateTime.UtcNow.AddDays(7),
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName
            };
        }
        public async Task<AuthModel> LoginAsync(Login model)
        {
            var authModel = new AuthModel();

           // var user = await _userManger.Users.FirstOrDefaultAsync(x => x.UserName == model.Username);
            var user = await _userManger.FindByNameAsync(model.Username);

            if (user == null || !await _userManger.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Username or Password is incorrect";
                return authModel;
            }
            var rolesList = await _userManger.GetRolesAsync(user);
            authModel.Roles = rolesList.ToList();

            var jwtSecurityToken = await CreateJwtToken(user);
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.Expiration = DateTime.UtcNow.AddDays(7);
            authModel.UserId =user.Id;

            return authModel;
        }     
        //Create  JWT Token XDD 
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManger.GetClaimsAsync(user);
            var roles = await _userManger.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public Task<AuthModel> ConfirmEmailAsync(string userid, string token)
        {
            throw new NotImplementedException();
        }

        public Task<AuthModel> ForgetPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AuthModel> ResetPasswordAsync(ResetPassword model)
        {
            throw new NotImplementedException();
        }
    }
}
