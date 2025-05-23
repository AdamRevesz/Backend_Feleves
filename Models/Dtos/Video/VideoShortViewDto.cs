// VideoShortViewDto.cs
namespace Models.Dtos.Video
{
    public class VideoShortViewDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ApprovalRate { get; set; } = string.Empty;
        public Models.User Owner { get; set; } = new Models.User();
        public string Category { get; set; } = string.Empty;
        public string Resolution { get; set; } = string.Empty;
    }
}
