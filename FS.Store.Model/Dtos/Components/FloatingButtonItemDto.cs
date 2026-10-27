namespace FS.Store.Model.Dtos.Components
{
    public class FloatingButtonItemDto
    {
        public string? Icon { get; set; }
        public string Color { get; set; } = "btn-primary";
        public string? Url { get; set; }
        public string? OnClick { get; set; }
        public string? Title { get; set; }
        public string? BtnId { get; set; }
    }
}
