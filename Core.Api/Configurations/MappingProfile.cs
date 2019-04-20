using AutoMapper;
using Core.Models;
using Core.Models.Entities;
using Core.Models.Models.Menu;
using MenuJsonModel = Core.Api.Models.Menu.MenuJsonModel;

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
