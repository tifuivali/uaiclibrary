using System.Linq;
using AutoMapper;
using UaicLibrary.DataBase.Contexts;
using UaicLibrary.Domain.UserManagement;

namespace UaicLibrary.Domain
{
    public class GeneralRepository : IGeneralRepository
    {
        private readonly UaicLibraryContext context;
        public GeneralRepository(UaicLibraryContext context)
        {
            this.context = context;
        }

        public int GetStudentId(string email)
        {
            return context.Students.SingleOrDefault(x => x.Email == email).Id;
        }

        public User GetUserByEmail(string email)
        {
            return Mapper.Map<User>(context.Users.SingleOrDefault(x => x.Email == email));
        }
    }
}
