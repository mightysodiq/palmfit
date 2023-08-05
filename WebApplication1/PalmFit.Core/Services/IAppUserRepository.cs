using Palmfit.Core.Dtos;

namespace Palmfit.Core.Services
{
    public interface IAppUserRepository
    {
        Task<string> CreateUser(SignUpDto userRequest);
    }
}