using System;
using System.Collections.Generic;
using Core.Extension.ExpressionBuilder.Common;

namespace Core.Extension.ExpressionBuilder.Configuration
{
    public class Settings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            this.SupportedTypes = new List<SupportedType>
            {
                new SupportedType { TypeGroup = TypeGroup.Number, Type = typeof(int) },
                new SupportedType { TypeGroup = TypeGroup.Number, Type = typeof(uint) },
                new SupportedType { TypeGroup = TypeGroup.Number, Type = typeof(byte) },
                new SupportedType { TypeGroup = TypeGroup.Number, Type = typeof(sbyte) },
                new SupportedType { TypeGroup = TypeGroup.Number, Type = typeof(short) },
                new SupportedType { TypeGroup = TypeGroup.Number, Type = typeof(ushort) },
                new SupportedType { TypeGroup = TypeGroup.Number, Type = typeof(ulong) },
                new SupportedType { TypeGroup = TypeGroup.Number, Type = typeof(float) },
                new SupportedType { TypeGroup = TypeGroup.Number, Type = typeof(double) },
                new SupportedType { TypeGroup = TypeGroup.Number, Type = typeof(decimal) },

                new SupportedType { TypeGroup = TypeGroup.Boolean, Type = typeof(bool) },
            };
            var typeGroups = new Dictionary<TypeGroup, HashSet<Type>>
            {
                { TypeGroup.Text, new HashSet<Type> { typeof(string), typeof(char) } },
                { TypeGroup.Number, new HashSet<Type> { typeof(int), typeof(uint), typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
                { TypeGroup.Boolean, new HashSet<Type> { typeof(bool) } },
                { TypeGroup.Date, new HashSet<Type> { typeof(DateTime) } },
                { TypeGroup.Nullable, new HashSet<Type> { typeof(Nullable<>), typeof(string) } }
            };

            // var configSection = ConfigurationManager.GetSection(ExpressionBuilderConfig.SectionName) as ExpressionBuilderConfig;
            // if (configSection == null)
            // {
            //    return;
            // }

            // settings.SupportedTypes = new List<SupportedType>();
            // foreach (ExpressionBuilderConfig.SupportedTypeElement supportedType in configSection.SupportedTypes)
            // {
            //    Type type = Type.GetType(supportedType.Type, false, true);
            //    if (type != null)
            //    {
            //        settings.SupportedTypes.Add(new SupportedType { TypeGroup = supportedType.TypeGroup, Type = type });
            //    }
            // }
        }

        public List<SupportedType> SupportedTypes { get; }
    }
}