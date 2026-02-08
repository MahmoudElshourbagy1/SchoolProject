using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.AppUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.AppUser.Commands.Handllers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>

    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //if Email or UserName exist
            var Email = await _userManager.FindByEmailAsync(request.Email);
            if (Email != null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserIsExist]);
            //mapping
            var identityUser = _mapper.Map<User>(request);
            //Create Email
            var CreateResult = await _userManager.CreateAsync(identityUser, request.Password);
            //Faild to create Email
            if (!CreateResult.Succeeded) return BadRequest<string>(CreateResult.Errors.FirstOrDefault().Description);
            //Massage
            //Create user
            return Created("");
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            //Check if user is exist
            var olduser = await _userManager.FindByIdAsync(request.Id.ToString());
            //Not found
            if (olduser == null)
            {
                return NotFound<string>();
            }
            //Mapping
            var newuser = _mapper.Map(request, olduser);
            //Update
            var result = await _userManager.UpdateAsync(newuser);
            //result is not success
            if (!result.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);
            //massage
            return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);
        }
    }
}
