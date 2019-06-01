using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EntityFrameworkCore.Generator
{
    public class UniqueNamer
    {
        private readonly ConcurrentDictionary<string, HashSet<string>> _names;

        public UniqueNamer()
        {
            this._names = new ConcurrentDictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
            this.Comparer = StringComparer.OrdinalIgnoreCase;

            // add existing
            this.UniqueContextName("ChangeTracker");
            this.UniqueContextName("Configuration");
            this.UniqueContextName("Database");
            this.UniqueContextName("InternalContext");
        }

        public IEqualityComparer<string> Comparer { get; set; }

        public string UniqueName(string bucketName, string name)
        {
            var hashSet = this._names.GetOrAdd(bucketName, k => new HashSet<string>(this.Comparer));
            string result = this.MakeUnique(name, hashSet.Contains);
            hashSet.Add(result);

            return result;
        }

        public string UniqueClassName(string className)
        {
            const string globalClassName = "global::ClassName";
            return this.UniqueName(globalClassName, className);
        }

        public string UniqueModelName(string @namespace, string className)
        {
            string globalClassName = "global::ModelClass::" + @namespace;
            return this.UniqueName(globalClassName, className);
        }

        public string UniqueContextName(string name)
        {
            const string globalContextname = "global::ContextName";
            return this.UniqueName(globalContextname, name);
        }

        public string UniqueRelationshipName(string name)
        {
            const string globalContextname = "global::RelationshipName";
            return this.UniqueName(globalContextname, name);
        }

        public string MakeUnique(string name, Func<string, bool> exists)
        {
            string uniqueName = name;
            int count = 1;

            while (exists(uniqueName))
            {
                uniqueName = string.Concat(name, count++);
            }

            return uniqueName;
        }
    }
}
