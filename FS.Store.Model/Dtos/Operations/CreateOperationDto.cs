namespace FS.Store.Model.Dtos.Operations
{
    public class CreateOperationDto
    {
        public string Document { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string ManagerEmail { get; set; } = default!;
        public string ManagerPassword { get; set; } = default!;

    }
}
