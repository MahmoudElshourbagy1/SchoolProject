using SchoolProject.infrustructure.infrustructureBase;

namespace SchoolProject.infrustructure.Abstracts.Views
{
    public interface IViewRepository<T> : IGenericRepositoryAsync<T> where T : class
    {
    }
}
