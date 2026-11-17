namespace FS.Store.Model.Dtos
{
    public class AssignUserToOperationDto
    {
        public int RoleId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
