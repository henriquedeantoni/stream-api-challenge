using stream_api_challenge.Enums;
using System.ComponentModel.DataAnnotations;

namespace stream_api_challenge.Models
{
    public class MovieModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required or must be informed")]
        [MaxLength(50, ErrorMessage = "Title must be equal or less than 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Gender is required or must be informed")]
        public GenderEnum Gender { get; set; }

        [Required(ErrorMessage = "ReleaseDate is required")]
        public DateTime ReleaseDate { get; set; }

        
        public List<StreamingModel> Streamings { get; set; }

        public List<RatingModel>? Ratings { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime DateUpdated { get; set; } = DateTime.Now.ToLocalTime();
    }
}
