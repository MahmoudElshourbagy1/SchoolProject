using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Commands.Handllers
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>

    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, UserManager<User> userManager, SignInManager<User> signInManager, IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }

        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if the user exists in the database and if the password is correct
            var user = await _userManager.FindByNameAsync(request.UserName);
            //Return the user not found error if the user does not exist or the password is incorrect
            if (user == null)
            {
                return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.UserNameIsNotExist]);
            }
            //Try to sign in the user and generate a token for them
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            //Return an error if the sign-in process fails for any reason
            if (!signInResult.Succeeded)
            {
                return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]);
            }
            var result = await _authenticationService.GetJWTToken(user);
            //Return a token if the user is authenticated successfully
            return Success(result);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var accesstoken = _authenticationService.ReadJWTToken(request.Token);
            var userIdAndexpiryDate = await _authenticationService.ValidateDetails(accesstoken, request.Token, request.RefreshToken);
            switch (userIdAndexpiryDate)
            {
                case ("Invalid token", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.Invalidtoken]);
                case ("Token is Not Expired", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.TokenisNotExpired]);
                case ("RefreshToken is not Found", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.RefreshTokenisnotFound]);
                case ("Refresh Token is Expired", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.RefreshTokenisExpired]);

            }

            //Generate refresh Token
            var (userId, expiryDate) = userIdAndexpiryDate;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound<JwtAuthResult>();
            }
            var result = await _authenticationService.GetRefreshToken(user, accesstoken, expiryDate, request.RefreshToken);
            return Success(result);
        }
    }
}
