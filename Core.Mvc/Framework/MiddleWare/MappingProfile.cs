using AutoMapper;
using Core.Entity;
using Core.Model.Administration.Icon;
using Core.Model.Administration.Menu;
using Core.Model.Administration.Permission;
using Core.Model.Administration.Role;
using Core.Model.Administration.User;

namespace Core.Mvc.Framework.MiddleWare
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
            CreateMap<User, UserModel>();
            CreateMap<UserCreatePostModel, User>();
            CreateMap<UserEditPostModel, User>();

            CreateMap<Role, RoleModel>();
            CreateMap<RoleCreateModel, Role>();
            CreateMap<RoleCreatePostModel, Role>();
            CreateMap<RoleEditPostModel, Role>();

            CreateMap<Menu, MenuJsonModel>();
            CreateMap<MenuEditPostModel, Menu>();
            CreateMap<MenuCreatePostModel, Menu>();

            CreateMap<Icon, IconCreateViewModel>();
            CreateMap<IconCreateViewModel, Icon>();

            CreateMap<Permission, PermissionJsonModel>();

            // .ForMember(d => d.MenuName, s => s.MapFrom(x => x.Menu.Name))
            // .ForMember(d => d.PermissionTypeText, s => s.MapFrom(x => x.Type.ToString()));
            CreateMap<PermissionCreateViewModel, Permission>();
            CreateMap<PermissionEditViewModel, Permission>();
            CreateMap<Permission, PermissionEditViewModel>();
        }
    }
}
