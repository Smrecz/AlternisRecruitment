namespace Recruitment.Functions.Services
{
    public interface IEncryptionService
    {
        string CalculateMd5(string input);
    }
}