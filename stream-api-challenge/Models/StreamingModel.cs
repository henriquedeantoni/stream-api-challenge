using stream_api_challenge.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace stream_api_challenge.Models
{
    public class StreamingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Streaming is required or must be informed")]
        public StreamingEnum StreamingName { get; set; }

        //Foreign Key
        public int MovieId { get; set; }

    }
}

