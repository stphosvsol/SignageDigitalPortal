using System;
using Infrastructure.Extensions;

namespace SignageRepository.Database
{
    public class VwChannelJson
    {
        public static VwChannel ModelEnt;
        public static string Key = ModelEnt.PropertyName(e => e.ChannelId);
        public static string Columns = String.Join(",", new[]
        {
            ModelEnt.PropertyName(e => e.ChannelId), 
            ModelEnt.PropertyName(e => e.Name), 
            ModelEnt.PropertyName(e => e.LstMedias),
            ModelEnt.PropertyName(e => e.DatetimeChannel),
            ModelEnt.PropertyName(e => e.UserName)
        });
    }
}
