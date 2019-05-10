using Core.Entity;

namespace Core.Model.Administration.User
{
    public class UserStatusModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserStatusModel"/> class.
        /// </summary>
        public UserStatusModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserStatusModel"/> class.
        /// </summary>
        /// <param name="userStatus"></param>
        public UserStatusModel(UserStatus userStatus)
        {
            this.Id = userStatus.Id;
            this.Name = userStatus.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}