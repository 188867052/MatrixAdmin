namespace Core.Model.Administration.Role
{
    /// <summary>
    ///
    /// </summary>
    public class RoleCreatePostModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool? Status { get; set; }

        public bool? IsEnable { get; set; }
    }
}