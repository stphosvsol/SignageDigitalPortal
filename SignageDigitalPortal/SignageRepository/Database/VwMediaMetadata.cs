using System;
using Infrastructure.Extensions;

namespace SignageRepository.Database
{
    public class VwMediaJson
    {
        public static VwMedia ModelEnt;
        public static string Key = ModelEnt.PropertyName(e => e.MediaId);
        public static string Columns = String.Join(",", new[]
        {
            ModelEnt.PropertyName(e => e.MediaId), 
            ModelEnt.PropertyName(e => e.LogicName), 
            ModelEnt.PropertyName(e => e.Size),
            ModelEnt.PropertyName(e => e.Type),
            ModelEnt.PropertyName(e => e.Length),
            ModelEnt.PropertyName(e => e.Url),
            ModelEnt.PropertyName(e => e.DatetimeMedia),
            ModelEnt.PropertyName(e => e.UserName)
        });
    }
}
