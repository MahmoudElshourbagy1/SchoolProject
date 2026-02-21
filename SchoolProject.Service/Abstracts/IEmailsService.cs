namespace SchoolProject.Service.Abstracts
{
    public interface IEmailsService
    {
        public Task<string> SendEmailAsync(string email, string Message, string? reason);
    }
}
