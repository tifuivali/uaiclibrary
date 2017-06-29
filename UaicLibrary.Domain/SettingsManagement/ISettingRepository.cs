using UaicLibrary.Common.Error;

namespace UaicLibrary.Domain.SettingsManagement
{
    public interface ISettingRepository
    {
        Result<Setting> GetHomePageContent();
        Result<Setting> UpdateHomePageContent(string content);
    }
}