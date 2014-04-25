using System;
using Infrastructure.Extensions;

namespace SignageRepository.Database
{
    public class VwScreenJson
    {
        public static VwScreen ModelEnt;
        public static string Key = ModelEnt.PropertyName(e => e.ScreenId);
        public static string Columns = String.Join(",", new[]
        {
            ModelEnt.PropertyName(e => e.ScreenId), 
            ModelEnt.PropertyName(e => e.Name), 
            ModelEnt.PropertyName(e => e.LstChannels),
            ModelEnt.PropertyName(e => e.DatetimeScreen),
            ModelEnt.PropertyName(e => e.ScreenSize),
            ModelEnt.PropertyName(e => e.UserName)
        });
    }
}
