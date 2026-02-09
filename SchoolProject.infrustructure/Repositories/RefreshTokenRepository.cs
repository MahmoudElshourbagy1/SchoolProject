
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.infrustructure.Abstracts;
using SchoolProject.infrustructure.Data;
using SchoolProject.infrustructure.infrustructureBase;

namespace SchoolProject.infrustructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>,
        IRefreshTokenRepository
    {
        private readonly DbSet<UserRefreshToken> _userRefreshTokens;
        public RefreshTokenRepository(AppBDContext dbContext) : base(dbContext)
        {
            _userRefreshTokens = dbContext.Set<UserRefreshToken>();
        }
    }
}
