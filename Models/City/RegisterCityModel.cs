using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.City
{
    public class RegisterCityModel
    {
        [Required]
        public string Name { get; set; }
    }
}
