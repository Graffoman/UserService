namespace Services.Contracts.Permission
{
	public class UpdatingPermissionDto
    {
        /// <summary> Наименование. </summary>
        public string Name { get; set; }

        /// <summary> Описание. </summary>
        public string? Description { get; set; }

        /// <summary>ID роли. </summary>
        public Guid RoleId { get; set; }
    }
}
