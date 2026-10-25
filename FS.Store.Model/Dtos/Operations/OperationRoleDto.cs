namespace FS.Store.Model.Dtos.Operations
{
    public class OperationRoleDto
    {
        public Guid OperationId { get; set; }
        public List<string>? Roles { get; set; }
    }
}
