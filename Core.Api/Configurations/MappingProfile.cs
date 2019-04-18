using AutoMapper;
using Core.Api.Entities;
using Core.Api.Models.Icon;
using Core.Api.Models.Menu;
using Core.Api.Models.Permission;
using Core.Api.Models.Role;
using Core.Api.Models.User;
using Core.Api.ViewModels.Rbac.User;

namespace Core.Api.Configurations
{
    public interface IProfile
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class MappingProfile : Profile, IProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public MappingProfile()
        {
            CreateMap<User, UserJsonModel>();
            CreateMap<UserCreateViewModel, User>();
            CreateMap<UserEditViewModel, User>();

            CreateMap<Role, RoleJsonModel>();
            CreateMap<RoleCreateViewModel, Role>();

            CreateMap<Menu, MenuJsonModel>();
            CreateMap<MenuCreateViewModel, Menu>();
            CreateMap<MenuEditViewModel, Menu>();

            CreateMap<Icon, IconCreateViewModel>();
            CreateMap<IconCreateViewModel, Icon>();

            CreateMap<Permission, PermissionJsonModel>()
                .ForMember(d => d.MenuName, s => s.MapFrom(x => x.Menu.Name))
                .ForMember(d => d.PermissionTypeText, s => s.MapFrom(x => x.Type.ToString()));
            CreateMap<PermissionCreateViewModel, Permission>();
            CreateMap<PermissionEditViewModel, Permission>();
            CreateMap<Permission, PermissionEditViewModel>();
        }
    }
}
