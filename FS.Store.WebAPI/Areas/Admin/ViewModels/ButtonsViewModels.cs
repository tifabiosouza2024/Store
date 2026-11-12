namespace FS.Store.WebAPI.Areas.Admin.ViewModels
{
    public class ButtonsViewModels
    {
        private ButtonsViewModels()
        {
        }
        public ButtonsViewModels(object? attribute, bool isDropdown = false, bool hasText = false)
        {
            Attribute = attribute;
            IsDropdown = isDropdown;
            HasText = hasText;
        }

        public object? Attribute { get; set; }
        public bool IsDropdown { get; set; } = false;
        public bool HasText { get; set; } = false;
    }
}
