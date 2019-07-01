using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Core.Shared.Utilities;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.Html
{
    public static class HtmlContent
    {
        public static string ToString(IHtmlContent content)
        {
            using (var writer = new StringWriter())
            {
                content.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }

        public static TagHelperOutput TagHelper(string tagName, TagHelperAttributeList attributes)
        {
            Check.NotEmpty(tagName, nameof(tagName));
            Check.NotEmpty(attributes, nameof(attributes));

            return new TagHelperOutput(tagName,
                                       attributes,
                                       getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
        }

        public static TagHelperOutput TagHelper(string tagName)
        {
            Check.NotEmpty(tagName, nameof(tagName));

            return new TagHelperOutput(tagName,
                                       new TagHelperAttributeList(),
                                       getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
        }

        public static TagHelperOutput TagHelper(string tagName, TagHelperAttributeList attributes, params string[] content)
        {
            Check.NotEmpty(tagName, nameof(tagName));
            Check.NotEmpty(attributes, nameof(attributes));

            TagHelperOutput tagHelperOutput = new TagHelperOutput(tagName,
                                                                  attributes,
                                                                  getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
            return tagHelperOutput.AppendHtml(content);
        }

        public static TagHelperOutput TagHelper(string tagName, TagHelperAttribute attribute, string content)
        {
            Check.NotEmpty(tagName, nameof(tagName));
            Check.NotNull(attribute, nameof(attribute));

            TagHelperOutput tagHelperOutput = new TagHelperOutput(tagName,
                                                                  new TagHelperAttributeList { attribute },
                                                                  getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
            return tagHelperOutput.AppendHtml(content);
        }

        public static TagHelperOutput TagHelper(string tagName, string content)
        {
            Check.NotEmpty(tagName, nameof(tagName));
            Check.NotEmpty(content, nameof(content));

            TagHelperOutput tagHelperOutput = new TagHelperOutput(tagName,
                                                                  new TagHelperAttributeList(),
                                                                  getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
            return tagHelperOutput.AppendHtml(content);
        }

        public static TagHelperOutput TagHelper(string tagName, TagHelperAttributeList attributes, params IHtmlContent[] htmlContent)
        {
            Check.NotEmpty(tagName, nameof(tagName));
            Check.NotEmpty(attributes, nameof(attributes));

            TagHelperOutput tagHelperOutput = new TagHelperOutput(tagName,
                                                                  attributes,
                                                                  getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
            return tagHelperOutput.AppendHtml(htmlContent);
        }

        public static TagHelperOutput TagHelper(string tagName, TagHelperAttribute attribute, params IHtmlContent[] htmlContent)
        {
            Check.NotEmpty(tagName, nameof(tagName));
            Check.NotNull(attribute, nameof(attribute));

            TagHelperOutput tagHelperOutput = new TagHelperOutput(tagName,
                                                                  new TagHelperAttributeList { attribute },
                                                                  getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
            return tagHelperOutput.AppendHtml(htmlContent);
        }

        private static TagHelperOutput AppendHtml(this TagHelperOutput tagHelperOutput, params IHtmlContent[] htmlContent)
        {
            foreach (var item in htmlContent)
            {
                tagHelperOutput.Content.AppendHtml(item);
            }

            return tagHelperOutput;
        }

        private static TagHelperOutput AppendHtml(this TagHelperOutput tagHelperOutput, params string[] content)
        {
            Check.NotNull(tagHelperOutput, nameof(tagHelperOutput));

            foreach (var item in content)
            {
                tagHelperOutput.Content.AppendHtml(item);
            }

            return tagHelperOutput;
        }
    }
}