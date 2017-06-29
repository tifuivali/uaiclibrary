using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UaicLibrary.DataBase.Contexts;
using UaicLibrary.Domain.StudentManagement;

namespace UaicLibrary.Domain.UserManagement
{
    public class UserProvider : IUserProvider
    {
        private readonly UaicLibraryContext dbContext;

        public UserProvider(UaicLibraryContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            var userDto = dbContext.Students.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (userDto == null)
            {
                var user2Dto = dbContext.Professors.FirstOrDefault(x => x.Email == email && x.Password == password);
                if (user2Dto == null)
                {
                    var generalUser = dbContext.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
                    if (generalUser != null)
                    {
                        generalUser.Role = UserRole.Admin;
                    }

                    return Mapper.Map<User>(generalUser);
                }
                var user = Mapper.Map<User>(user2Dto);
                if (user != null)
                {
                    user.Role = UserRole.Professor;
                }
                return user;
            }

            var user2 = Mapper.Map<User>(userDto);
            if (user2 != null)
            {
                user2.Role = UserRole.Student;
            }
            return user2;
        }

        public User GetUserBy(string email)
        {
            var userDto = dbContext.Students.FirstOrDefault(x => x.Email == email);
            if (userDto == null)
            {
                var user2Dto = dbContext.Professors.FirstOrDefault(x => x.Email == email);
                var user = Mapper.Map<User>(user2Dto);
                user.Role = UserRole.Professor;
                return user;
            }

            var user2 = Mapper.Map<User>(userDto);
            user2.Role = UserRole.Student;
            return user2;
        }

        public IList<Student> GetAll()
        {
            return dbContext.Students.ToList().Select(Mapper.Map<Student>).ToList();
        }
    }
}
