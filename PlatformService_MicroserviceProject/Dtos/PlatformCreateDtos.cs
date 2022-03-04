using System.ComponentModel.DataAnnotations;

namespace PlatformService_MicroserviceProject.Dtos
{
    public class PlatformCreateDtos
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Cost { get; set; }
    }
}
