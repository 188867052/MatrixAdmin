namespace Core.Entity
{
    public class UserField
    {
        public static StringField LoginName = new StringField(nameof(LoginName));
        public static IntegerField IsLocked = new IntegerField(nameof(IsLocked));
    }
}
