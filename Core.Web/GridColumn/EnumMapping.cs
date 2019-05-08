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
                case UserStatusEnum.All:
                    return "所有";
                case UserStatusEnum.Forbidden:
                    return "已禁用";
                case UserStatusEnum.Normal:
                    return "正常";
                case YesOrNoEnum.All:
                    return "正常";
                default:
                    throw new Exception();
            }
        }
    }
}