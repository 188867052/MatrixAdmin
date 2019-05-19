using System;
using Core.Entity.Enums;
using Core.Web.Enums;
using Microsoft.Extensions.Logging;

namespace Core.Web.GridColumn
{
    public static class EnumMapping
    {
        public static string ToString(this Enum value)
        {
            switch (value)
            {
                case IsForbiddenEnum.Forbidden:
                    return "已禁用";
                case IsForbiddenEnum.Normal:
                    return "已启用";
                case YesOrNoEnum.All:
                    return "正常";

                case LogLevel.Information:
                    return "信息";
                case LogLevel.Debug:
                    return "调试";
                case LogLevel.Error:
                    return "错误";

                case SqlTypeEnum.None:
                    return string.Empty;
                case SqlTypeEnum.Select:
                    return "Select";
                case SqlTypeEnum.Create:
                    return "Create";
                case SqlTypeEnum.Update:
                    return "Update";
                case SqlTypeEnum.Delete:
                    return "Delete";
                case SqlTypeEnum.Insert:
                    return "Insert";

                default:
                    throw new ArgumentException(value.ToString());
            }
        }
    }
}