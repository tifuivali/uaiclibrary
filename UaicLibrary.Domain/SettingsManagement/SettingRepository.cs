using System.Linq;
using AutoMapper;
using UaicLibrary.Common.Error;
using UaicLibrary.Common.Settings;
using UaicLibrary.DataBase.Contexts;
using UaicLibrary.DataBase.Models;

namespace UaicLibrary.Domain.SettingsManagement
{
    public class SettingRepository : ISettingRepository
    {
        private readonly UaicLibraryContext context;
        public SettingRepository(UaicLibraryContext context)
        {
            this.context = context;
        }

        public Result<Setting> GetHomePageContent()
        {
            var settingDto = context.Settings.SingleOrDefault(x => x.Key == Settings.HomePageContentSetting);
            if (settingDto == null) return Result.Fail<Setting>(Errors.HomePageContentNotExist);
            return Result.Ok(Mapper.Map<Setting>(settingDto));
        }

        public Result<Setting> UpdateHomePageContent(string content)
        {
            var settingDto = context.Settings.SingleOrDefault(x => x.Key == Settings.HomePageContentSetting);
            if (settingDto != null)
            {
                settingDto.Value = content;
                context.SaveChanges();
                return Result.Ok(Mapper.Map<Setting>(settingDto));
            }
            settingDto = new SettingDto
            {
                Key = Settings.HomePageContentSetting,
                Value = content
            };
            context.Settings.Add(settingDto);
            context.SaveChanges();
            return Result.Ok(Mapper.Map<Setting>(settingDto));
        }
    }
}
