using ConsoleApp.DataModels;

namespace Core.Entity
{
    public class UserStatusModel
    {
        public UserStatusModel(UserStatus userStatus)
        {
            this.Id = userStatus.Id;
            this.Name = userStatus.Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}