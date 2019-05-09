using Core.Entity;

namespace Core.Model.Administration.User
{
    public class UserStatusModel
    {
        public UserStatusModel()
        {

        }

        public UserStatusModel(UserStatus userStatus)
        {
            this.Id = userStatus.Id;
            this.Name = userStatus.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}