using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;

namespace Core.Extension
{
    /// <summary>
    /// 把 DataTable 转换为对象集合列表List.
    /// </summary>
    public static class DataTableConvert
    {
        // 用于构造Emit的DataRow中获取字段的方法信息
        private static readonly MethodInfo getValueMethod = typeof(DataRow).GetMethod("get_Item", new[] { typeof(int) });

        // 用于构造Emit的DataRow中判断是否为空行的方法信息
        private static readonly MethodInfo isDBNullMethod = typeof(DataRow).GetMethod("IsNull", new[] { typeof(int) });

        // 使用字典存储实体的类型以及与之对应的Emit生成的转换方法
        private static readonly Dictionary<Type, Delegate> rowMapMethods = new Dictionary<Type, Delegate>();

        // 把DataRow转换为对象的委托声明
        private delegate T Load<T>(DataRow dataRecord);

        /// <summary>
        /// 将DataTable转换成泛型对象列表.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="dt">dt.</param>
        /// <returns>List.</returns>
        public static List<T> ToList<T>(this DataTable dt)
        {
            List<T> list = new List<T>();
            if (dt == null)
            {
                return list;
            }

            // 声明 委托Load<T>的一个实例rowMap
            Load<T> rowMap = null;

            // 从rowMapMethods查找当前T类对应的转换方法，没有则使用Emit构造一个。
            if (!rowMapMethods.ContainsKey(typeof(T)))
            {
                DynamicMethod method = new DynamicMethod("DynamicCreateEntity_" + typeof(T).Name, typeof(T), new[] { typeof(DataRow) }, typeof(T), true);
                ILGenerator generator = method.GetILGenerator();
                LocalBuilder result = generator.DeclareLocal(typeof(T));
                generator.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
                generator.Emit(OpCodes.Stloc, result);

                for (int index = 0; index < dt.Columns.Count; index++)
                {
                    PropertyInfo propertyInfo = typeof(T).GetProperty(dt.Columns[index].ColumnName);
                    Label endIfLabel = generator.DefineLabel();
                    if (propertyInfo != null && propertyInfo.GetSetMethod() != null)
                    {
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, index);
                        generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                        generator.Emit(OpCodes.Brtrue, endIfLabel);
                        generator.Emit(OpCodes.Ldloc, result);
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, index);
                        generator.Emit(OpCodes.Callvirt, getValueMethod);
                        generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
                        generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());
                        generator.MarkLabel(endIfLabel);
                    }
                }

                generator.Emit(OpCodes.Ldloc, result);
                generator.Emit(OpCodes.Ret);

                // 构造完成以后传给rowMap
                rowMap = (Load<T>)method.CreateDelegate(typeof(Load<T>));
            }
            else
            {
                rowMap = (Load<T>)rowMapMethods[typeof(T)];
            }

            // 遍历 DataTable 的rows集合，调用rowMap把DataRow转换为对象（T）
            foreach (DataRow info in dt.Rows)
            {
                list.Add(rowMap(info));
            }

            return list;
        }
    }
}
