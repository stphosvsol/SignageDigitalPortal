using System.ComponentModel.DataAnnotations;

namespace SignageRepository.Database
{

    [MetadataType(typeof(ScreenScheduleMetadata))]
    public partial class ScreenSchedule
    {
        public void CopyValues(ScreenSchedule newVal)
        {
            PositionX = newVal.PositionX;
            PositionY = newVal.PositionY;
            PositionZ = newVal.PositionZ;
            Height = newVal.Height;
            Width = newVal.Width;
            HasClose = newVal.HasClose;
            ScreenId = newVal.ScreenId;
            ChannelId = newVal.ChannelId;
            IsFullScreen = newVal.IsFullScreen;
            StartTime = newVal.StartTime;
            EndTime = newVal.EndTime;
            Day = newVal.Day;
            Year = newVal.Year;
            UserId = newVal.UserId;
        }
    }

    public class ScreenScheduleMetadata
    {
    }
}
