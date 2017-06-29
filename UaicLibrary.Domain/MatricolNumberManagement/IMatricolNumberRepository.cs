namespace UaicLibrary.Domain.MatricolNumberManagement
{
    public interface IMatricolNumberRepository
    {
        bool Verify(string matricolNumber);
    }
}