using System;
using Infrastructure.Extensions;

namespace SignageRepository.Database
{
    public class VwDeviceJson
    {
        public static VwDevice ModelEnt;
        public static string Key = ModelEnt.PropertyName(e => e.DeviceId);
        public static string Columns = String.Join(",", new[]
        {
            ModelEnt.PropertyName(e => e.DeviceId), 
            ModelEnt.PropertyName(e => e.Ip), 
            ModelEnt.PropertyName(e => e.HostName), 
            ModelEnt.PropertyName(e => e.ScreenName), 
            ModelEnt.PropertyName(e => e.DatetimeDevice),
            ModelEnt.PropertyName(e => e.UserName)
        });
    }
}
