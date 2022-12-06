using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VideoTheque.DTOs;
namespace VideoTheque.ViewModels
{
    public class AgeRatingViewModel
    {
         [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("abreviation")]
        [Required]
        public string Abreviation { get; set; }
        
        [JsonPropertyName("nom")]
        [Required]
        public string Name { get; set; }

        public AgeRatingDto ToDto()
        {
            return new AgeRatingDto()
            {
                Id = this.Id,
                Abreviation = this.Abreviation,
                Name = this.Name
            };
        }

        public static AgeRatingViewModel ToModel(AgeRatingDto dto)
        {
            return new AgeRatingViewModel
            {
                Id = dto.Id, 
                Abreviation= dto.Abreviation,
                Name = dto.Name
            };
        }
    }
}