namespace Core.Model.Administration.Role
{
    /// <summary>
    ///
    /// </summary>
    public class RoleEditPostModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? Status { get; set; }

        public bool? IsEnable { get; set; }

        public bool? IsForbidden { get; set; }
    }
}