using System.Threading.Tasks;
using UaicLibrary.Domain.UserManagement;

namespace UaicLibrary.Web.Security
{
    public interface ITokenGenerator
    {
        Task<string> GenerateTokenForUser(User user);
    }
}