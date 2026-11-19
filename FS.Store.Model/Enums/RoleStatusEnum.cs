namespace FS.Store.Model.Enums
{
    /// <summary>
    /// Status de vinculo do usuário x operação x função, sendo:<br />
    /// 1. Pendente<br />
    /// 2. Ativo<br />
    /// 3. Rejeitado<br />
    /// 4. Revogado<br />
    /// </summary>
    public enum RoleStatusEnum
    {
        Pending = 1,
        Active = 2,
        Rejected = 3,
        Revoked = 4
    }
}
