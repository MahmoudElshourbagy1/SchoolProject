using SchoolProject.Data.Entities.Identity;
using SchoolProject.infrustructure.infrustructureBase;

namespace SchoolProject.infrustructure.Abstracts
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {
    }
}
