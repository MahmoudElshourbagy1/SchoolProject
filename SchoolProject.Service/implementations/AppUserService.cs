using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.infrustructure.Data;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.implementations
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailsService _emailsService;
        private readonly AppBDContext _appBDContext;
        private readonly IUrlHelper _urlHelper;
        public AppUserService(UserManager<User> userManager, IHttpContextAccessor contextAccessor, IEmailsService emailsService, AppBDContext appBDContext, IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _emailsService = emailsService;
            _appBDContext = appBDContext;
            _urlHelper = urlHelper;
        }

        public async Task<string> AddUserAsync(User user, string Password)
        {
            var trans = await _appBDContext.Database.BeginTransactionAsync();
            try
            {
                //if Email or UserName exist
                var existUser = await _userManager.FindByEmailAsync(user.Email);
                if (existUser != null) return "EmailIsExist";

                var userByUserName = await _userManager.FindByNameAsync(user.UserName);
                if (userByUserName != null) return "UserIsExist";
                //Create Email
                var CreateResult = await _userManager.CreateAsync(user, Password);
                //Faild to create Email
                if (!CreateResult.Succeeded) return string.Join(",", CreateResult.Errors.Select(x => x.Description).ToList());
                //Massage  
                await _userManager.AddToRoleAsync(user, "User");
                //send Confirm Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var requestAccessor = _contextAccessor.HttpContext.Request;
                var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
                var message = $@"
<h3>Email Confirmation</h3>
<p>Please confirm your email by clicking the button below:</p>
<a href='{returnUrl}' style='
    display:inline-block;
    padding:10px 20px;
    background-color:#28a745;
    color:white;
    text-decoration:none;
    border-radius:5px;
'>
Confirm Email
</a>
";
                // $"/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={code}";
                //message Body
                await _emailsService.SendEmailAsync(user.Email, message, "ConfirmEmail");
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }

        }
    }
}
