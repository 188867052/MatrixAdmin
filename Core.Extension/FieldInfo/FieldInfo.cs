namespace Core.Entity
{
    public class ConfigurationField
    {
        public static StringField Value = new StringField(nameof(Value));

        public static IntegerField Id = new IntegerField(nameof(Id));

        public static StringField Key = new StringField(nameof(Key));
    }

    public class IconField
    {
        public static IntegerField Id = new IntegerField(nameof(Id));

        public static StringField Code = new StringField(nameof(Code));

        public static StringField Size = new StringField(nameof(Size));

        public static StringField Color = new StringField(nameof(Color));

        public static StringField Custom = new StringField(nameof(Custom));

        public static StringField Description = new StringField(nameof(Description));

        public static DateTimeField CreatedOn = new DateTimeField(nameof(CreatedOn));

        public static StringField CreatedByUserName = new StringField(nameof(CreatedByUserName));

        public static DateTimeField ModifiedOn = new DateTimeField(nameof(ModifiedOn));

        public static StringField ModifiedByUserName = new StringField(nameof(ModifiedByUserName));

        public static BooleanField CreatedByUserGuid = new BooleanField(nameof(CreatedByUserGuid));

        public static BooleanField ModifiedByUserGuid = new BooleanField(nameof(ModifiedByUserGuid));

        public static BooleanField IsEnable = new BooleanField(nameof(IsEnable));

        public static BooleanField Status = new BooleanField(nameof(Status));
    }

    public class LogField
    {
        public static IntegerField Id = new IntegerField(nameof(Id));

        public static DateTimeField CreateTime = new DateTimeField(nameof(CreateTime));

        public static StringField Message = new StringField(nameof(Message));

        public static IntegerField LogLevel = new IntegerField(nameof(LogLevel));
    }

    public class MenuField
    {
        public static BooleanField Guid = new BooleanField(nameof(Guid));

        public static StringField Name = new StringField(nameof(Name));

        public static StringField Url = new StringField(nameof(Url));

        public static StringField Alias = new StringField(nameof(Alias));

        public static StringField Icon = new StringField(nameof(Icon));

        public static BooleanField ParentGuid = new BooleanField(nameof(ParentGuid));

        public static StringField ParentName = new StringField(nameof(ParentName));

        public static IntegerField Level = new IntegerField(nameof(Level));

        public static StringField Description = new StringField(nameof(Description));

        public static IntegerField Sort = new IntegerField(nameof(Sort));

        public static IntegerField IsDefaultRouter = new IntegerField(nameof(IsDefaultRouter));

        public static DateTimeField CreatedOn = new DateTimeField(nameof(CreatedOn));

        public static StringField CreatedByUserName = new StringField(nameof(CreatedByUserName));

        public static DateTimeField ModifiedOn = new DateTimeField(nameof(ModifiedOn));

        public static StringField ModifiedByUserName = new StringField(nameof(ModifiedByUserName));

        public static BooleanField CreatedByUserGuid = new BooleanField(nameof(CreatedByUserGuid));

        public static BooleanField ModifiedByUserGuid = new BooleanField(nameof(ModifiedByUserGuid));

        public static BooleanField IsEnable = new BooleanField(nameof(IsEnable));

        public static BooleanField Status = new BooleanField(nameof(Status));

        public class PermissionField
        {
            public static StringField Id = new StringField(nameof(PermissionField), nameof(Id));

            public static BooleanField MenuGuid = new BooleanField(nameof(PermissionField), nameof(MenuGuid));

            public static StringField Name = new StringField(nameof(PermissionField), nameof(Name));

            public static StringField ActionCode = new StringField(nameof(PermissionField), nameof(ActionCode));

            public static StringField Icon = new StringField(nameof(PermissionField), nameof(Icon));

            public static StringField Description = new StringField(nameof(PermissionField), nameof(Description));

            public static IntegerField Type = new IntegerField(nameof(PermissionField), nameof(Type));

            public static DateTimeField CreatedTime = new DateTimeField(nameof(PermissionField), nameof(CreatedTime));

            public static StringField CreatedByUserName = new StringField(nameof(PermissionField), nameof(CreatedByUserName));

            public static DateTimeField UpdateTime = new DateTimeField(nameof(PermissionField), nameof(UpdateTime));

            public static StringField UpdateByUserName = new StringField(nameof(PermissionField), nameof(UpdateByUserName));

            public static BooleanField CreateByUserId = new BooleanField(nameof(PermissionField), nameof(CreateByUserId));

            public static BooleanField UpdateByUserId = new BooleanField(nameof(PermissionField), nameof(UpdateByUserId));

            public static BooleanField IsEnable = new BooleanField(nameof(PermissionField), nameof(IsEnable));

            public static BooleanField Status = new BooleanField(nameof(PermissionField), nameof(Status));
        }
    }

    public class PermissionField
    {
        public static StringField Id = new StringField(nameof(Id));

        public static BooleanField MenuGuid = new BooleanField(nameof(MenuGuid));

        public static StringField Name = new StringField(nameof(Name));

        public static StringField ActionCode = new StringField(nameof(ActionCode));

        public static StringField Icon = new StringField(nameof(Icon));

        public static StringField Description = new StringField(nameof(Description));

        public static IntegerField Type = new IntegerField(nameof(Type));

        public static DateTimeField CreatedTime = new DateTimeField(nameof(CreatedTime));

        public static StringField CreatedByUserName = new StringField(nameof(CreatedByUserName));

        public static DateTimeField UpdateTime = new DateTimeField(nameof(UpdateTime));

        public static StringField UpdateByUserName = new StringField(nameof(UpdateByUserName));

        public static BooleanField CreateByUserId = new BooleanField(nameof(CreateByUserId));

        public static BooleanField UpdateByUserId = new BooleanField(nameof(UpdateByUserId));

        public static BooleanField IsEnable = new BooleanField(nameof(IsEnable));

        public static BooleanField Status = new BooleanField(nameof(Status));

        public class MenuGuField
        {
            public static BooleanField Guid = new BooleanField(nameof(MenuField), nameof(Guid));

            public static StringField Name = new StringField(nameof(MenuField), nameof(Name));

            public static StringField Url = new StringField(nameof(MenuField), nameof(Url));

            public static StringField Alias = new StringField(nameof(MenuField), nameof(Alias));

            public static StringField Icon = new StringField(nameof(MenuField), nameof(Icon));

            public static BooleanField ParentGuid = new BooleanField(nameof(MenuField), nameof(ParentGuid));

            public static StringField ParentName = new StringField(nameof(MenuField), nameof(ParentName));

            public static IntegerField Level = new IntegerField(nameof(MenuField), nameof(Level));

            public static StringField Description = new StringField(nameof(MenuField), nameof(Description));

            public static IntegerField Sort = new IntegerField(nameof(MenuField), nameof(Sort));

            public static IntegerField IsDefaultRouter = new IntegerField(nameof(MenuField), nameof(IsDefaultRouter));

            public static DateTimeField CreatedOn = new DateTimeField(nameof(MenuField), nameof(CreatedOn));

            public static StringField CreatedByUserName = new StringField(nameof(MenuField), nameof(CreatedByUserName));

            public static DateTimeField ModifiedOn = new DateTimeField(nameof(MenuField), nameof(ModifiedOn));

            public static StringField ModifiedByUserName = new StringField(nameof(MenuField), nameof(ModifiedByUserName));

            public static BooleanField CreatedByUserGuid = new BooleanField(nameof(MenuField), nameof(CreatedByUserGuid));

            public static BooleanField ModifiedByUserGuid = new BooleanField(nameof(MenuField), nameof(ModifiedByUserGuid));

            public static BooleanField IsEnable = new BooleanField(nameof(MenuField), nameof(IsEnable));

            public static BooleanField Status = new BooleanField(nameof(MenuField), nameof(Status));
        }

        public class RolePermissionMappingField
        {
            public static StringField PermissionCode = new StringField(nameof(RolePermissionMappingField), nameof(PermissionCode));

            public static DateTimeField CreatedOn = new DateTimeField(nameof(RolePermissionMappingField), nameof(CreatedOn));

            public static IntegerField RoleId = new IntegerField(nameof(RolePermissionMappingField), nameof(RoleId));

            public static IntegerField Id = new IntegerField(nameof(RolePermissionMappingField), nameof(Id));
        }
    }

    public class RoleField
    {
        public static StringField Name = new StringField(nameof(Name));

        public static StringField Description = new StringField(nameof(Description));

        public static DateTimeField CreateTime = new DateTimeField(nameof(CreateTime));

        public static StringField CreatedByUserName = new StringField(nameof(CreatedByUserName));

        public static DateTimeField UpdateTime = new DateTimeField(nameof(UpdateTime));

        public static StringField ModifiedByUserName = new StringField(nameof(ModifiedByUserName));

        public static BooleanField IsSuperAdministrator = new BooleanField(nameof(IsSuperAdministrator));

        public static BooleanField IsEnable = new BooleanField(nameof(IsEnable));

        public static BooleanField IsForbidden = new BooleanField(nameof(IsForbidden));

        public static IntegerField Id = new IntegerField(nameof(Id));

        public static IntegerField ModifiedByUserId = new IntegerField(nameof(ModifiedByUserId));

        public static IntegerField CreateByUserId = new IntegerField(nameof(CreateByUserId));

        public class CreateByUserField
        {
            public static StringField LoginName = new StringField(nameof(UserField), nameof(LoginName));

            public static StringField DisplayName = new StringField(nameof(UserField), nameof(DisplayName));

            public static StringField Password = new StringField(nameof(UserField), nameof(Password));

            public static StringField Avatar = new StringField(nameof(UserField), nameof(Avatar));

            public static IntegerField UserType = new IntegerField(nameof(UserField), nameof(UserType));

            public static IntegerField IsLocked = new IntegerField(nameof(UserField), nameof(IsLocked));

            public static IntegerField Status = new IntegerField(nameof(UserField), nameof(Status));

            public static DateTimeField CreateTime = new DateTimeField(nameof(UserField), nameof(CreateTime));

            public static StringField CreatedByUserName = new StringField(nameof(UserField), nameof(CreatedByUserName));

            public static DateTimeField UpdateTime = new DateTimeField(nameof(UserField), nameof(UpdateTime));

            public static StringField ModifiedByUserName = new StringField(nameof(UserField), nameof(ModifiedByUserName));

            public static StringField Description = new StringField(nameof(UserField), nameof(Description));

            public static BooleanField CreatedByUserId = new BooleanField(nameof(UserField), nameof(CreatedByUserId));

            public static BooleanField ModifiedByUserId = new BooleanField(nameof(UserField), nameof(ModifiedByUserId));

            public static BooleanField IsEnable = new BooleanField(nameof(UserField), nameof(IsEnable));

            public static IntegerField Id = new IntegerField(nameof(UserField), nameof(Id));

            public static BooleanField IsDeleted = new BooleanField(nameof(UserField), nameof(IsDeleted));

            public static IntegerField UserStatusId = new IntegerField(nameof(UserField), nameof(UserStatusId));

            public static StringField RoleName = new StringField(nameof(UserField), nameof(RoleName));
        }

        public class ModifiedByUserField
        {
            public static StringField LoginName = new StringField(nameof(UserField), nameof(LoginName));

            public static StringField DisplayName = new StringField(nameof(UserField), nameof(DisplayName));

            public static StringField Password = new StringField(nameof(UserField), nameof(Password));

            public static StringField Avatar = new StringField(nameof(UserField), nameof(Avatar));

            public static IntegerField UserType = new IntegerField(nameof(UserField), nameof(UserType));

            public static IntegerField IsLocked = new IntegerField(nameof(UserField), nameof(IsLocked));

            public static IntegerField Status = new IntegerField(nameof(UserField), nameof(Status));

            public static DateTimeField CreateTime = new DateTimeField(nameof(UserField), nameof(CreateTime));

            public static StringField CreatedByUserName = new StringField(nameof(UserField), nameof(CreatedByUserName));

            public static DateTimeField UpdateTime = new DateTimeField(nameof(UserField), nameof(UpdateTime));

            public static StringField ModifiedByUserName = new StringField(nameof(UserField), nameof(ModifiedByUserName));

            public static StringField Description = new StringField(nameof(UserField), nameof(Description));

            public static BooleanField CreatedByUserId = new BooleanField(nameof(UserField), nameof(CreatedByUserId));

            public static BooleanField ModifiedByUserId = new BooleanField(nameof(UserField), nameof(ModifiedByUserId));

            public static BooleanField IsEnable = new BooleanField(nameof(UserField), nameof(IsEnable));

            public static IntegerField Id = new IntegerField(nameof(UserField), nameof(Id));

            public static BooleanField IsDeleted = new BooleanField(nameof(UserField), nameof(IsDeleted));

            public static IntegerField UserStatusId = new IntegerField(nameof(UserField), nameof(UserStatusId));

            public static StringField RoleName = new StringField(nameof(UserField), nameof(RoleName));
        }

        public class RolePermissionMappingField
        {
            public static StringField PermissionCode = new StringField(nameof(RolePermissionMappingField), nameof(PermissionCode));

            public static DateTimeField CreatedOn = new DateTimeField(nameof(RolePermissionMappingField), nameof(CreatedOn));

            public static IntegerField RoleId = new IntegerField(nameof(RolePermissionMappingField), nameof(RoleId));

            public static IntegerField Id = new IntegerField(nameof(RolePermissionMappingField), nameof(Id));
        }

        public class UserRoleMappingField
        {
            public static IntegerField Id = new IntegerField(nameof(UserRoleMappingField), nameof(Id));

            public static IntegerField UserId = new IntegerField(nameof(UserRoleMappingField), nameof(UserId));

            public static IntegerField RoleId = new IntegerField(nameof(UserRoleMappingField), nameof(RoleId));

            public static DateTimeField CreateTime = new DateTimeField(nameof(UserRoleMappingField), nameof(CreateTime));
        }
    }

    public class RolePermissionMappingField
    {
        public static StringField PermissionCode = new StringField(nameof(PermissionCode));

        public static DateTimeField CreatedOn = new DateTimeField(nameof(CreatedOn));

        public static IntegerField RoleId = new IntegerField(nameof(RoleId));

        public static IntegerField Id = new IntegerField(nameof(Id));

        public class PermissionCodeNavigationField
        {
            public static StringField Id = new StringField(nameof(PermissionField), nameof(Id));

            public static BooleanField MenuGuid = new BooleanField(nameof(PermissionField), nameof(MenuGuid));

            public static StringField Name = new StringField(nameof(PermissionField), nameof(Name));

            public static StringField ActionCode = new StringField(nameof(PermissionField), nameof(ActionCode));

            public static StringField Icon = new StringField(nameof(PermissionField), nameof(Icon));

            public static StringField Description = new StringField(nameof(PermissionField), nameof(Description));

            public static IntegerField Type = new IntegerField(nameof(PermissionField), nameof(Type));

            public static DateTimeField CreatedTime = new DateTimeField(nameof(PermissionField), nameof(CreatedTime));

            public static StringField CreatedByUserName = new StringField(nameof(PermissionField), nameof(CreatedByUserName));

            public static DateTimeField UpdateTime = new DateTimeField(nameof(PermissionField), nameof(UpdateTime));

            public static StringField UpdateByUserName = new StringField(nameof(PermissionField), nameof(UpdateByUserName));

            public static BooleanField CreateByUserId = new BooleanField(nameof(PermissionField), nameof(CreateByUserId));

            public static BooleanField UpdateByUserId = new BooleanField(nameof(PermissionField), nameof(UpdateByUserId));

            public static BooleanField IsEnable = new BooleanField(nameof(PermissionField), nameof(IsEnable));

            public static BooleanField Status = new BooleanField(nameof(PermissionField), nameof(Status));
        }

        public class RoleField
        {
            public static StringField Name = new StringField(nameof(RoleField), nameof(Name));

            public static StringField Description = new StringField(nameof(RoleField), nameof(Description));

            public static DateTimeField CreateTime = new DateTimeField(nameof(RoleField), nameof(CreateTime));

            public static StringField CreatedByUserName = new StringField(nameof(RoleField), nameof(CreatedByUserName));

            public static DateTimeField UpdateTime = new DateTimeField(nameof(RoleField), nameof(UpdateTime));

            public static StringField ModifiedByUserName = new StringField(nameof(RoleField), nameof(ModifiedByUserName));

            public static BooleanField IsSuperAdministrator = new BooleanField(nameof(RoleField), nameof(IsSuperAdministrator));

            public static BooleanField IsEnable = new BooleanField(nameof(RoleField), nameof(IsEnable));

            public static BooleanField IsForbidden = new BooleanField(nameof(RoleField), nameof(IsForbidden));

            public static IntegerField Id = new IntegerField(nameof(RoleField), nameof(Id));

            public static IntegerField ModifiedByUserId = new IntegerField(nameof(RoleField), nameof(ModifiedByUserId));

            public static IntegerField CreateByUserId = new IntegerField(nameof(RoleField), nameof(CreateByUserId));
        }
    }

    public class UserField
    {
        public static StringField LoginName = new StringField(nameof(LoginName));

        public static StringField DisplayName = new StringField(nameof(DisplayName));

        public static StringField Password = new StringField(nameof(Password));

        public static StringField Avatar = new StringField(nameof(Avatar));

        public static IntegerField UserType = new IntegerField(nameof(UserType));

        public static IntegerField IsLocked = new IntegerField(nameof(IsLocked));

        public static IntegerField Status = new IntegerField(nameof(Status));

        public static DateTimeField CreateTime = new DateTimeField(nameof(CreateTime));

        public static StringField CreatedByUserName = new StringField(nameof(CreatedByUserName));

        public static DateTimeField UpdateTime = new DateTimeField(nameof(UpdateTime));

        public static StringField ModifiedByUserName = new StringField(nameof(ModifiedByUserName));

        public static StringField Description = new StringField(nameof(Description));

        public static BooleanField CreatedByUserId = new BooleanField(nameof(CreatedByUserId));

        public static BooleanField ModifiedByUserId = new BooleanField(nameof(ModifiedByUserId));

        public static BooleanField IsEnable = new BooleanField(nameof(IsEnable));

        public static IntegerField Id = new IntegerField(nameof(Id));

        public static BooleanField IsDeleted = new BooleanField(nameof(IsDeleted));

        public static IntegerField UserStatusId = new IntegerField(nameof(UserStatusId));

        public class UserStatusField
        {
            public static IntegerField Id = new IntegerField(nameof(UserStatusField), nameof(Id));

            public static StringField Name = new StringField(nameof(UserStatusField), nameof(Name));
        }

        public class RoleCreateByUserField
        {
            public static StringField Name = new StringField(nameof(RoleField), nameof(Name));

            public static StringField Description = new StringField(nameof(RoleField), nameof(Description));

            public static DateTimeField CreateTime = new DateTimeField(nameof(RoleField), nameof(CreateTime));

            public static StringField CreatedByUserName = new StringField(nameof(RoleField), nameof(CreatedByUserName));

            public static DateTimeField UpdateTime = new DateTimeField(nameof(RoleField), nameof(UpdateTime));

            public static StringField ModifiedByUserName = new StringField(nameof(RoleField), nameof(ModifiedByUserName));

            public static BooleanField IsSuperAdministrator = new BooleanField(nameof(RoleField), nameof(IsSuperAdministrator));

            public static BooleanField IsEnable = new BooleanField(nameof(RoleField), nameof(IsEnable));

            public static BooleanField IsForbidden = new BooleanField(nameof(RoleField), nameof(IsForbidden));

            public static IntegerField Id = new IntegerField(nameof(RoleField), nameof(Id));

            public static IntegerField ModifiedByUserId = new IntegerField(nameof(RoleField), nameof(ModifiedByUserId));

            public static IntegerField CreateByUserId = new IntegerField(nameof(RoleField), nameof(CreateByUserId));
        }

        public class RoleModifiedByUserField
        {
            public static StringField Name = new StringField(nameof(RoleField), nameof(Name));

            public static StringField Description = new StringField(nameof(RoleField), nameof(Description));

            public static DateTimeField CreateTime = new DateTimeField(nameof(RoleField), nameof(CreateTime));

            public static StringField CreatedByUserName = new StringField(nameof(RoleField), nameof(CreatedByUserName));

            public static DateTimeField UpdateTime = new DateTimeField(nameof(RoleField), nameof(UpdateTime));

            public static StringField ModifiedByUserName = new StringField(nameof(RoleField), nameof(ModifiedByUserName));

            public static BooleanField IsSuperAdministrator = new BooleanField(nameof(RoleField), nameof(IsSuperAdministrator));

            public static BooleanField IsEnable = new BooleanField(nameof(RoleField), nameof(IsEnable));

            public static BooleanField IsForbidden = new BooleanField(nameof(RoleField), nameof(IsForbidden));

            public static IntegerField Id = new IntegerField(nameof(RoleField), nameof(Id));

            public static IntegerField ModifiedByUserId = new IntegerField(nameof(RoleField), nameof(ModifiedByUserId));

            public static IntegerField CreateByUserId = new IntegerField(nameof(RoleField), nameof(CreateByUserId));
        }

        public class UserRoleMappingField
        {
            public static IntegerField Id = new IntegerField(nameof(UserRoleMappingField), nameof(Id));

            public static IntegerField UserId = new IntegerField(nameof(UserRoleMappingField), nameof(UserId));

            public static IntegerField RoleId = new IntegerField(nameof(UserRoleMappingField), nameof(RoleId));

            public static DateTimeField CreateTime = new DateTimeField(nameof(UserRoleMappingField), nameof(CreateTime));
        }

        public class RoleField
        {
            public static StringField Name = new StringField(nameof(RoleField), nameof(Name));

            public static StringField Description = new StringField(nameof(RoleField), nameof(Description));

            public static DateTimeField CreateTime = new DateTimeField(nameof(RoleField), nameof(CreateTime));

            public static StringField CreatedByUserName = new StringField(nameof(RoleField), nameof(CreatedByUserName));

            public static DateTimeField UpdateTime = new DateTimeField(nameof(RoleField), nameof(UpdateTime));

            public static StringField ModifiedByUserName = new StringField(nameof(RoleField), nameof(ModifiedByUserName));

            public static BooleanField IsSuperAdministrator = new BooleanField(nameof(RoleField), nameof(IsSuperAdministrator));

            public static BooleanField IsEnable = new BooleanField(nameof(RoleField), nameof(IsEnable));

            public static BooleanField IsForbidden = new BooleanField(nameof(RoleField), nameof(IsForbidden));

            public static IntegerField Id = new IntegerField(nameof(RoleField), nameof(Id));

            public static IntegerField ModifiedByUserId = new IntegerField(nameof(RoleField), nameof(ModifiedByUserId));

            public static IntegerField CreateByUserId = new IntegerField(nameof(RoleField), nameof(CreateByUserId));
        }

        public class RoleMappingField
        {
            public static IntegerField Id = new IntegerField(nameof(UserRoleMappingField), nameof(Id));

            public static IntegerField UserId = new IntegerField(nameof(UserRoleMappingField), nameof(UserId));

            public static IntegerField RoleId = new IntegerField(nameof(UserRoleMappingField), nameof(RoleId));

            public static DateTimeField CreateTime = new DateTimeField(nameof(UserRoleMappingField), nameof(CreateTime));
        }

        public static StringField RoleName = new StringField(nameof(RoleName));
    }

    public class UserRoleMappingField
    {
        public static IntegerField Id = new IntegerField(nameof(Id));

        public static IntegerField UserId = new IntegerField(nameof(UserId));

        public static IntegerField RoleId = new IntegerField(nameof(RoleId));

        public static DateTimeField CreateTime = new DateTimeField(nameof(CreateTime));

        public class RoleField
        {
            public static StringField Name = new StringField(nameof(RoleField), nameof(Name));

            public static StringField Description = new StringField(nameof(RoleField), nameof(Description));

            public static DateTimeField CreateTime = new DateTimeField(nameof(RoleField), nameof(CreateTime));

            public static StringField CreatedByUserName = new StringField(nameof(RoleField), nameof(CreatedByUserName));

            public static DateTimeField UpdateTime = new DateTimeField(nameof(RoleField), nameof(UpdateTime));

            public static StringField ModifiedByUserName = new StringField(nameof(RoleField), nameof(ModifiedByUserName));

            public static BooleanField IsSuperAdministrator = new BooleanField(nameof(RoleField), nameof(IsSuperAdministrator));

            public static BooleanField IsEnable = new BooleanField(nameof(RoleField), nameof(IsEnable));

            public static BooleanField IsForbidden = new BooleanField(nameof(RoleField), nameof(IsForbidden));

            public static IntegerField Id = new IntegerField(nameof(RoleField), nameof(Id));

            public static IntegerField ModifiedByUserId = new IntegerField(nameof(RoleField), nameof(ModifiedByUserId));

            public static IntegerField CreateByUserId = new IntegerField(nameof(RoleField), nameof(CreateByUserId));
        }

        public class UserField
        {
            public static StringField LoginName = new StringField(nameof(UserField), nameof(LoginName));

            public static StringField DisplayName = new StringField(nameof(UserField), nameof(DisplayName));

            public static StringField Password = new StringField(nameof(UserField), nameof(Password));

            public static StringField Avatar = new StringField(nameof(UserField), nameof(Avatar));

            public static IntegerField UserType = new IntegerField(nameof(UserField), nameof(UserType));

            public static IntegerField IsLocked = new IntegerField(nameof(UserField), nameof(IsLocked));

            public static IntegerField Status = new IntegerField(nameof(UserField), nameof(Status));

            public static DateTimeField CreateTime = new DateTimeField(nameof(UserField), nameof(CreateTime));

            public static StringField CreatedByUserName = new StringField(nameof(UserField), nameof(CreatedByUserName));

            public static DateTimeField UpdateTime = new DateTimeField(nameof(UserField), nameof(UpdateTime));

            public static StringField ModifiedByUserName = new StringField(nameof(UserField), nameof(ModifiedByUserName));

            public static StringField Description = new StringField(nameof(UserField), nameof(Description));

            public static BooleanField CreatedByUserId = new BooleanField(nameof(UserField), nameof(CreatedByUserId));

            public static BooleanField ModifiedByUserId = new BooleanField(nameof(UserField), nameof(ModifiedByUserId));

            public static BooleanField IsEnable = new BooleanField(nameof(UserField), nameof(IsEnable));

            public static IntegerField Id = new IntegerField(nameof(UserField), nameof(Id));

            public static BooleanField IsDeleted = new BooleanField(nameof(UserField), nameof(IsDeleted));

            public static IntegerField UserStatusId = new IntegerField(nameof(UserField), nameof(UserStatusId));

            public static StringField RoleName = new StringField(nameof(UserField), nameof(RoleName));
        }
    }

    public class UserStatusField
    {
        public static IntegerField Id = new IntegerField(nameof(Id));

        public static StringField Name = new StringField(nameof(Name));

        public class UserField
        {
            public static StringField LoginName = new StringField(nameof(UserField), nameof(LoginName));

            public static StringField DisplayName = new StringField(nameof(UserField), nameof(DisplayName));

            public static StringField Password = new StringField(nameof(UserField), nameof(Password));

            public static StringField Avatar = new StringField(nameof(UserField), nameof(Avatar));

            public static IntegerField UserType = new IntegerField(nameof(UserField), nameof(UserType));

            public static IntegerField IsLocked = new IntegerField(nameof(UserField), nameof(IsLocked));

            public static IntegerField Status = new IntegerField(nameof(UserField), nameof(Status));

            public static DateTimeField CreateTime = new DateTimeField(nameof(UserField), nameof(CreateTime));

            public static StringField CreatedByUserName = new StringField(nameof(UserField), nameof(CreatedByUserName));

            public static DateTimeField UpdateTime = new DateTimeField(nameof(UserField), nameof(UpdateTime));

            public static StringField ModifiedByUserName = new StringField(nameof(UserField), nameof(ModifiedByUserName));

            public static StringField Description = new StringField(nameof(UserField), nameof(Description));

            public static BooleanField CreatedByUserId = new BooleanField(nameof(UserField), nameof(CreatedByUserId));

            public static BooleanField ModifiedByUserId = new BooleanField(nameof(UserField), nameof(ModifiedByUserId));

            public static BooleanField IsEnable = new BooleanField(nameof(UserField), nameof(IsEnable));

            public static IntegerField Id = new IntegerField(nameof(UserField), nameof(Id));

            public static BooleanField IsDeleted = new BooleanField(nameof(UserField), nameof(IsDeleted));

            public static IntegerField UserStatusId = new IntegerField(nameof(UserField), nameof(UserStatusId));

            public static StringField RoleName = new StringField(nameof(UserField), nameof(RoleName));
        }
    }
}
