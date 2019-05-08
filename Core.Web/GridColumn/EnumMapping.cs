using System;
using Core.Entity.Enums;

namespace Core.Web.GridColumn
{
    public static class EnumMapping
    {
        public static string ToString(this Enum value)
        {
            switch (value)
            {
                case UserIsForbiddenEnum.Forbidden:
                    return "已禁用";
                case UserIsForbiddenEnum.Normal:
                    return "已启用";
                case YesOrNoEnum.All:
                    return "正常";
                default:
                    throw new Exception();
            }
        }
    }
}