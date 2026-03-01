using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.infrustructure.Abstracts.Functions;
using SchoolProject.infrustructure.Data;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.implementations
{
    public class InstructorService : IInstructorService
    {
        private readonly AppBDContext _appBDContext;
        private readonly IInstructorsFunctionsRepository _instructorsFunctionsRepository;
        private readonly IInstructorsRepository _instructorsRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public InstructorService(AppBDContext appBDContext, IInstructorsFunctionsRepository instructorsFunctionsRepository, IInstructorsRepository instructorsRepository, IHttpContextAccessor httpContextAccessor, IFileService fileService)
        {
            _appBDContext = appBDContext;
            _instructorsFunctionsRepository = instructorsFunctionsRepository;
            _instructorsRepository = instructorsRepository;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }

        public async Task<decimal> GetSalaryInstructorOfSummation()
        {
            decimal result = 0;
            result = _instructorsFunctionsRepository.GetSalaryInstructorOfSummation("select dbo.GetSalarySummation()");
            return result;
        }

        public async Task<bool> IsNameArExist(string nameAr)
        {
            //Check if the name is Exist Or not
            var student = _instructorsRepository.GetTableNoTracking().Where(x => x.ENameAr.Equals(nameAr)).FirstOrDefault();
            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameArExistExcludeSelf(string nameAr, int id)
        {
            //Check if the name is Exist Or not
            var student = await _instructorsRepository.GetTableNoTracking().Where(x => x.ENameAr.Equals(nameAr) & x.InsId != id).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExist(string nameEn)
        {
            //Check if the name is Exist Or not
            var student = await _instructorsRepository.GetTableNoTracking().Where(x => x.ENameEn.Equals(nameEn)).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExistExcludeSelf(string nameEn, int id)
        {
            //Check if the name is Exist Or not
            var student = await _instructorsRepository.GetTableNoTracking().Where(x => x.ENameEn.Equals(nameEn) & x.InsId != id).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }
        public async Task<string> AddInstructorAsync(Instructor instructor, IFormFile file)
        {
            var context = _httpContextAccessor.HttpContext.Request;
            var baseUrl = context.Scheme + "://" + context.Host + "/";
            var imageUrl = await _fileService.UploadFileAsync("Instructors", file);
            switch (imageUrl)
            {
                case "NoFileUploaded": return "NoFileUploaded";
                case "FailedToUploadFile": return "FailedToUploadFile";
            }
            instructor.Image = baseUrl + imageUrl;
            try
            {
                await _instructorsRepository.AddAsync(instructor);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedInAdd";
            }
        }
    }
}
