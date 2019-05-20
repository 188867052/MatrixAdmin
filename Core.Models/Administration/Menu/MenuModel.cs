using System;
using Core.Entity.Enums;

namespace Core.Model.Administration.Menu
{
    public class MenuModel
    {
        public MenuModel()
        {
        }

        public MenuModel(Entity.Menu entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Url = entity.Url;
            this.Alias = entity.Alias;
            this.Icon = entity.Icon;
            this.ParentName = entity.ParentName;
            this.Level = entity.Level;
            this.Description = entity.Description;
            this.Sort = entity.Sort;
            this.CreateTime = entity.CreateTime;
            this.CreateByUserName = entity.CreateByUserName;
            this.UpdateTime = entity.UpdateTime;
            this.UpdateByUserName = entity.UpdateByUserName;
        }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Alias { get; set; }

        public string Icon { get; set; }

        public string ParentName { get; set; }

        public int Level { get; set; }

        public string Description { get; set; }

        public int Sort { get; set; }

        public int IsDefaultRouter { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreateByUserName { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string UpdateByUserName { get; set; }

        public bool IsEnable { get; set; }

        public ForbiddenStatusEnum Status { get; set; }

        public int CreateByUserId { get; set; }

        public int UpdateByUserId { get; set; }

        public int Id { get; set; }

        public int ParentId { get; set; }

        public static MenuModel Convert(Entity.Menu item)
        {
            return new MenuModel(item);
        }
    }
}