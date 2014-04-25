using System.ComponentModel.DataAnnotations;
using SignageRepository.Resources;

namespace SignageRepository.Database
{
    [MetadataType(typeof(ScreenMetadata))]

    public partial class Screen
    {

    }

    public class ScreenMetadata
    {
        [Required(ErrorMessageResourceType = typeof(ResSharedRep), ErrorMessageResourceName = "REQUIRED_FIELD")]
        [Display(Name= "FIELD_NAME", ResourceType = typeof(ResSharedRep))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(ResSharedRep), ErrorMessageResourceName = "LENGTH_FIELD")]
        public string Name { get; set; }

        /*
             <Required(ErrorMessageResourceType:=GetType(ResSharedRep), ErrorMessageResourceName:="REQUIRED_FIELD")>
    <Display(Name:="FIELD_NAME", ResourceType:=GetType(ResSharedRep))>
    <StringLength(250, MinimumLength:=1, ErrorMessageResourceType:=GetType(ResSharedRep), ErrorMessageResourceName:="LENGTH_FIELD")>
    Public Property Name As String*/
    }
}
