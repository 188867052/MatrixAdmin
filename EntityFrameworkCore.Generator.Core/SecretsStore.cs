using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json.Linq;

namespace EntityFrameworkCore.Generator
{
    public class SecretsStore
    {
        private readonly string _secretsFilePath;
        private readonly IDictionary<string, string> _secrets;

        public SecretsStore(string userSecretsId)
        {
            this._secretsFilePath = PathHelper.GetSecretsPathFromSecretsId(userSecretsId);

            // workaround bug in configuration
            var secretDir = Path.GetDirectoryName(this._secretsFilePath);
            Directory.CreateDirectory(secretDir);

            this._secrets = this.Load(userSecretsId);
        }

        public int Count => this._secrets.Count;

        public string this[string key] => this._secrets[key];

        public bool ContainsKey(string key) => this._secrets.ContainsKey(key);

        public IEnumerable<KeyValuePair<string, string>> AsEnumerable() => this._secrets;

        public void Clear() => this._secrets.Clear();

        public void Set(string key, string value) => this._secrets[key] = value;

        public void Remove(string key)
        {
            if (this._secrets.ContainsKey(key))
            {
                this._secrets.Remove(key);
            }
        }

        public virtual void Save()
        {
            var secretDir = Path.GetDirectoryName(this._secretsFilePath);
            Directory.CreateDirectory(secretDir);

            var contents = new JObject();
            if (this._secrets != null)
            {
                foreach (var secret in this._secrets.AsEnumerable())
                {
                    contents[secret.Key] = secret.Value;
                }
            }

            File.WriteAllText(this._secretsFilePath, contents.ToString(), Encoding.UTF8);
        }

        protected virtual IDictionary<string, string> Load(string userSecretsId)
        {
            return new ConfigurationBuilder()
                .AddJsonFile(this._secretsFilePath, optional: true)
                .Build()
                .AsEnumerable()
                .Where(i => i.Value != null)
                .ToDictionary(i => i.Key, i => i.Value, StringComparer.OrdinalIgnoreCase);
        }
    }
}
