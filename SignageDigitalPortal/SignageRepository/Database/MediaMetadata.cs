using System.ComponentModel.DataAnnotations;
using SignageRepository.Resources;

namespace SignageRepository.Database
{
    [MetadataType(typeof(MediaMetadata))]

    public partial class Media
    {

    }

    public class MediaMetadata
    {
        [Required(ErrorMessageResourceType = typeof(ResSharedRep), ErrorMessageResourceName = "REQUIRED_FIELD")]
        [Display(Name = "FIELD_NAME", ResourceType = typeof(ResSharedRep))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(ResSharedRep), ErrorMessageResourceName = "LENGTH_FIELD")]
        public string LogicName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResSharedRep), ErrorMessageResourceName = "REQUIRED_FIELD")]
        [Display(Name = "FIELD_URL", ResourceType = typeof(ResSharedRep))]
        [StringLength(4000, MinimumLength = 10, ErrorMessageResourceType = typeof(ResSharedRep), ErrorMessageResourceName = "LENGTH_FIELD")]
        public string Url { get; set; }

    }
}
