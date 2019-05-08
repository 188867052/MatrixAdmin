using AutoMapper;
using Core.Model.Administration.Icon;
using Core.Model.Administration.Menu;
using Core.Model.Administration.Permission;
using Core.Model.Administration.Role;
using Core.Model.Administration.User;
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
            CreateMap<User, UserModel>();
            CreateMap<UserCreatePostModel, User>();
            CreateMap<UserEditPostModel, User>();

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
