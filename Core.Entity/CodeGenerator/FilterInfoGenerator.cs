using System;
using System.Linq;
using System.Reflection;

// Scaffold-DbContext -Force "Data Source=.;Initial Catalog=CoreApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer
namespace Core.Entity
{
    internal class FilterInfoGenerator
    {
        public static void GenerateFildInfo()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            Console.WriteLine("namespace Core.Entity");
            Console.WriteLine("{");
            foreach (Type item in types.Where(o => !o.Name.Contains("Enum") && !o.Name.Contains("CoreApi") && !o.Name.Contains("Partial") && !o.Name.Contains("<>") && !o.Name.Contains("Program") && !o.Name.Contains("Generator")))
            {
                PrintClass(item);
            }

            Console.WriteLine("}");
        }

        private static void PrintClass(Type item, string propertyName = "", bool isNested = false)
        {
            string className = string.IsNullOrEmpty(propertyName) ? item.Name : propertyName;
            if (className.Contains("PrivateImplementationDetails"))
            {
                return;
            }

            Console.WriteLine($"public partial class {className}Field");
            Console.WriteLine("{");
            PropertyInfo[] propertyInfos = item.GetProperties();
            foreach (var property in propertyInfos)
            {
                PrintProperty(property, className, isNested);
            }

            Console.WriteLine("}");
        }

        private static void PrintProperty(PropertyInfo property, string parentClassName, bool isNested = false)
        {
            string type = property.PropertyType.ToString();
            string parameter = string.Empty;
            parameter = isNested ? $"nameof({parentClassName}Field),nameof({property.Name})" : $"nameof({property.Name})";
            Type t = null;
            if (type.Contains("System.Collections.Generic.ICollection"))
            {
                string @class = type.Split('[')[1].Replace("]", string.Empty);
                t = Type.GetType(@class, true, true);
                type = "System.Collections.Generic.ICollection";
            }

            if (!type.Contains("System"))
            {
                t = Type.GetType(type, true, true);
                type = "Entity";
            }

            switch (type)
            {
                case "System.String":
                case "System.Nullable`1[System.String]":
                    Console.WriteLine($"public static {nameof(StringField)} {property.Name} = new {nameof(StringField)}({parameter});");
                    Console.WriteLine();
                    break;
                case "System.DateTime":
                case "System.Nullable`1[System.DateTime]":
                    Console.WriteLine($"public static {nameof(DateTimeField)} {property.Name} = new {nameof(DateTimeField)}({parameter});");
                    Console.WriteLine();
                    break;
                case "System.Decimal":
                case "System.Nullable`1[System.Decimal]":
                    Console.WriteLine($"public static {nameof(DecimalField)} {property.Name} = new {nameof(DecimalField)}({parameter});");
                    Console.WriteLine();
                    break;
                case "System.Int32":
                case "System.Nullable`1[System.Int32]":
                    Console.WriteLine($"public static {nameof(IntegerField)} {property.Name} = new {nameof(IntegerField)}({parameter});");
                    Console.WriteLine();
                    break;
                case "System.Boolean":
                case "System.Nullable`1[System.Boolean]":
                    Console.WriteLine($"public static {nameof(BooleanField)} {property.Name} = new {nameof(BooleanField)}({parameter});");
                    Console.WriteLine();
                    break;
                case "System.Guid":
                case "System.Nullable`1[System.Guid]":
                    Console.WriteLine($"public static {nameof(BooleanField)} {property.Name} = new {nameof(BooleanField)}({parameter});");
                    Console.WriteLine();
                    break;
                case "System.Collections.Generic.ICollection":
                    if (!isNested)
                    {
                        Console.WriteLine($"public static {nameof(CollectionField)} {property.Name} = new {nameof(CollectionField)}({parameter});");
                        Console.WriteLine();
                        PrintClass(t, property.Name, true);
                    }

                    break;
                case "Entity":
                    if (!isNested)
                    {
                        PrintClass(t, property.Name, true);
                    }

                    break;
                default:
                    Console.WriteLine();
                    break;
            }
        }
    }
}