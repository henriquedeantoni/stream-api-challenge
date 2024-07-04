using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace stream_api_challenge.Models
{
    public class RatingModel
    {
        public int Id { get; set; }

        [MaxLength(300, ErrorMessage = "Comments must be at maximum 300 characters")]
        public string Comments { get; set; }

        [Range(1, 5, ErrorMessage = "The ratings must be a value beetween 1 until 5")]
        public int Rating { get; set; }

        //Foreign Key
        public int MovieId { get; set; }
    }
}
