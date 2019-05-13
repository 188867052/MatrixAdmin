namespace Core.Model.Administration.Role
{
    /// <summary>
    ///
    /// </summary>
    public class RoleCreateModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool? IsForbidden { get; set; }

        public bool? IsEnable { get; set; }
    }
}
