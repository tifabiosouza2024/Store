namespace FS.Store.Model.Dtos.Components
{
    public class FloatingButtonDto
    {
        public string MainIcon { get; set; } = "bi bi-plus-lg";
        public string MainColor { get; set; } = "btn-primary";
        public string MainUrl { get; set; } = null!;
        public string MainOnClick { get; set; } = "toggleFab()";
        public bool IsSubmit { get; set; }
        public string? MainTitle { get; set; }
        public string? MainBtnId { get; set; }
        public List<FloatingButtonItemDto> Buttons { get; set; } = new();
    }
}
