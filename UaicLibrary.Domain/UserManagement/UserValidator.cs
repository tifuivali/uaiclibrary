using UaicLibrary.Common.Error;
using UaicLibrary.Domain.MatricolNumberManagement;

namespace UaicLibrary.Domain.UserManagement
{
    public class UserValidator : IModelValidator<User>
    {
        private readonly IMatricolNumberRepository matricolNumberRepository;
        private readonly IUserProvider userProvider;

        public UserValidator(IMatricolNumberRepository matricolNumberRepository, IUserProvider userProvider)
        {
            this.matricolNumberRepository = matricolNumberRepository;
            this.userProvider = userProvider;
        }

        public Result Validate(User model)
        {
            var validateUserExistsResult = ValidateUserExists(model);
            if (validateUserExistsResult.IsSuccess)
                return Result.Combine(ValidateMatricol(model),
                    ValidateRole(model));

            return validateUserExistsResult;
        }

        private Result ValidateMatricol(User user)
        {
            if (user.Role == UserRole.Professor)
            {
                return Result.Ok();
            }
            return matricolNumberRepository.Verify(user.MatricolNumber)
                ? Result.Ok()
                : Result.Fail("MatricolDoesNotExists");
        }

        private Result ValidateUserExists(User user)
        {
            var userDomain = userProvider.GetUserByEmailAndPassword(user.Email, user.Password);
            return userDomain == null ? Result.Ok() : Result.Fail("AccountAlreadyExists");
        }

        private Result ValidateRole(User user)
        {
            if (user.Role == UserRole.Professor || user.Role == UserRole.Student)
                return Result.Ok();

            return Result.Fail("RoleIsRequired");
        }
    }
}