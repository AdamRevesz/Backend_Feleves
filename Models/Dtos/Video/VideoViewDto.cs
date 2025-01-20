// VideoViewDto.cs
using System.Collections.Generic;

namespace Models.Dtos.Video
{
    public class VideoViewDto
    {
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? Category { get; set; }
        public int Likes { get; set; }
        public List<Comments>? Comments { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Resolution => $"{Width}x{Height}";
    }
}
