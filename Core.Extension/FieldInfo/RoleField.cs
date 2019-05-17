namespace Core.Entity
{
    /// <summary>
    /// TODO: It is better to use T4 template to generate the field info.
    /// </summary>
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
            public static readonly IntegerField IsLocked = new IntegerField(nameof(CreateByUserField), nameof(IsLocked));
            public static CollectionField UserRoleMapping = new CollectionField(nameof(CreateByUserField), "[" + nameof(UserRoleMapping) + "]");
        }
    }
}
