using System.Linq;
using UaicLibrary.DataBase.Contexts;

namespace UaicLibrary.Domain.MatricolNumberManagement
{
    public class MatricolNumberRepository : IMatricolNumberRepository
    {
        private readonly UaicLibraryContext dbContext;

        public MatricolNumberRepository(UaicLibraryContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Verify(string matricolNumber)
        {
            var matricolExists = dbContext.MatricolNumbers.Any(x => x.Matricol == matricolNumber);
            return matricolExists;
        }
    }
}
