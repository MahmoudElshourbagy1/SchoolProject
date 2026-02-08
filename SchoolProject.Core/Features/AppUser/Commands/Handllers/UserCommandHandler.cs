using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.AppUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.AppUser.Commands.Handllers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPasswordCommand, Response<string>>


    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly UserManager<User> _userManager;
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _userManager = userManager;
        }
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //if Email or UserName exist
            var Email = await _userManager.FindByEmailAsync(request.Email);
            if (Email != null) return BadRequest<string>(_sharedResources[SharedResourcesKeys.EmailIsExist]);

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null) return BadRequest<string>(_sharedResources[SharedResourcesKeys.UserIsExist]);
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
            if (olduser == null) return NotFound<string>();

            //Mapping
            var newuser = _mapper.Map(request, olduser);
            //if user is Exist
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == newuser.UserName && x.Id != newuser.Id);
            if (user != null) return BadRequest<string>(_sharedResources[SharedResourcesKeys.UserIsExist]);
            //Update
            var result = await _userManager.UpdateAsync(newuser);
            //result is not success
            if (!result.Succeeded) return BadRequest<string>(_sharedResources[SharedResourcesKeys.UpdateFailed]);
            //massage
            return Success((string)_sharedResources[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //Check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //Not found
            if (user == null)
            {
                return NotFound<string>();
            }
            //Delete
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) return BadRequest<string>(_sharedResources[SharedResourcesKeys.DeletedFailed]);
            return Success((string)_sharedResources[SharedResourcesKeys.Deleted]);


        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //get user
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //if not exist return not found
            if (user == null) return NotFound<string>();
            //Change User Password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            //result
            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            return Success((string)_sharedResources[SharedResourcesKeys.Success]);
        }
    }
}
