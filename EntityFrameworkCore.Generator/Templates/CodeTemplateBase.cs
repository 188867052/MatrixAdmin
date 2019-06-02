using System.Collections.Generic;
using System.IO;
using System.Text;
using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.Parsing;
using Microsoft.EntityFrameworkCore.Internal;

namespace EntityFrameworkCore.Generator.Templates
{
    public abstract class CodeTemplateBase
    {
        protected CodeTemplateBase(GeneratorOptions options)
        {
            this.Options = options;
            this.CodeBuilder = new IndentedStringBuilder();
            this.RegionParser = new RegionParser();
        }

        public GeneratorOptions Options { get; }

        protected RegionParser RegionParser { get; }

        protected IndentedStringBuilder CodeBuilder { get; }

        public static bool IsLastIndex<T>(IList<T> list, T element)
        {
            if (list.IndexOf(element) == list.Count - 1)
            {
                return true;
            }

            return false;
        }

        public virtual void WriteCode(string path)
        {
            var fullPath = Path.GetFullPath(path);
            var directory = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var output = this.WriteCode();

            if (File.Exists(fullPath))
            {
                this.MergeOutput(fullPath, output);
            }
            else
            {
                File.WriteAllText(fullPath, output);
            }
        }

        public abstract string WriteCode();

        protected virtual void MergeOutput(string fullPath, string outputContent)
        {
            var outputRegions = this.RegionParser.ParseRegions(outputContent);

            var originalContent = File.ReadAllText(fullPath);
            var originalRegions = this.RegionParser.ParseRegions(originalContent);
            var originalBuilder = new StringBuilder(originalContent);

            int offset = 0;
            foreach (var pair in outputRegions)
            {
                var outputRegion = pair.Value;
                if (!originalRegions.TryGetValue(pair.Key, out var originalRegion))
                {
                    // log error
                    continue;
                }

                int startIndex = originalRegion.StartIndex + offset;
                int beforeReplace = originalBuilder.Length;
                int length = (originalRegion.EndIndex + offset) - startIndex;

                originalBuilder.Remove(startIndex, length);
                originalBuilder.Insert(startIndex, outputRegion.Content);

                int afterReplace = originalBuilder.Length;

                offset += afterReplace - beforeReplace;
            }

            var finalContent = originalBuilder.ToString();
            if (originalContent == finalContent)
            {
                return;
            }

            File.WriteAllText(fullPath, finalContent);
        }
    }
}