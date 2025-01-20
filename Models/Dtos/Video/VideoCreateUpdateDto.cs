// VideoCreateUpdateDto.cs
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.Video
{
    public class VideoCreateUpdateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Body { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }
    }
}
