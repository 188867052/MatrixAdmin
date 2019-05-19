using AutoMapper;
using Core.Entity;
using Core.Model.Administration.Icon;
using Core.Model.Administration.Menu;
using Core.Model.Administration.Permission;
using Core.Model.Administration.Role;
using Core.Model.Administration.User;

namespace Core.Api.MiddleWare
{
    /// <summary>
    /// MappingProfile.
    /// </summary>
    public class MappingProfile : Profile, IProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            this.CreateMap<User, UserModel>();
            this.CreateMap<UserCreatePostModel, User>();
            this.CreateMap<UserEditPostModel, User>();

            this.CreateMap<Role, RoleModel>();
            this.CreateMap<RoleCreateModel, Role>();
            this.CreateMap<RoleCreatePostModel, Role>();
            this.CreateMap<RoleEditPostModel, Role>();

            this.CreateMap<Menu, MenuJsonModel>();
            this.CreateMap<MenuCreateViewModel, Menu>();
            this.CreateMap<MenuEditViewModel, Menu>();

            this.CreateMap<Icon, IconCreateViewModel>();
            this.CreateMap<IconCreateViewModel, Icon>();

            this.CreateMap<Permission, PermissionJsonModel>();

                // .ForMember(d => d.MenuName, s => s.MapFrom(x => x.Menu.Name))
                // .ForMember(d => d.PermissionTypeText, s => s.MapFrom(x => x.Type.ToString()));
            this.CreateMap<PermissionCreateViewModel, Permission>();
            this.CreateMap<PermissionEditViewModel, Permission>();
            this.CreateMap<Permission, PermissionEditViewModel>();
        }
    }
}
