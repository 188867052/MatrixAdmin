using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.Html
{
    public class HtmlContent
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
            return new TagHelperOutput(tagName,
                                       attributes,
                                       getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
        }

        public static TagHelperOutput TagHelper(string tagName)
        {
            return new TagHelperOutput(tagName,
                                       new TagHelperAttributeList(),
                                       getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
        }

        public static TagHelperOutput TagHelper(string tagName, TagHelperAttributeList attributes, string content)
        {
            TagHelperOutput tagHelperOutput = new TagHelperOutput(tagName,
                                                                  attributes,
                                                                  getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
            tagHelperOutput.Content.SetHtmlContent(content);

            return tagHelperOutput;
        }

        public static TagHelperOutput TagHelper(string tagName, TagHelperAttribute attribute, string content)
        {
            TagHelperOutput tagHelperOutput = new TagHelperOutput(tagName,
                                                                  new TagHelperAttributeList { attribute },
                                                                  getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
            tagHelperOutput.Content.SetHtmlContent(content);

            return tagHelperOutput;
        }

        public static TagHelperOutput TagHelper(string tagName, string content)
        {
            TagHelperOutput tagHelperOutput = new TagHelperOutput(tagName,
                                                                  new TagHelperAttributeList(),
                                                                  getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
            tagHelperOutput.Content.SetHtmlContent(content);

            return tagHelperOutput;
        }

        public static TagHelperOutput TagHelper(string tagName, TagHelperAttributeList attributes, params IHtmlContent[] htmlContent)
        {
            TagHelperOutput tagHelperOutput = new TagHelperOutput(tagName,
                                                                  attributes,
                                                                  getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
            foreach (var item in htmlContent)
            {
                tagHelperOutput.Content.AppendHtml(item);
            }

            return tagHelperOutput;
        }

        public static TagHelperOutput TagHelper(string tagName, TagHelperAttribute attribute, params IHtmlContent[] htmlContent)
        {
            TagHelperOutput tagHelperOutput = new TagHelperOutput(tagName,
                                                                  new TagHelperAttributeList { attribute },
                                                                  getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
            foreach (var item in htmlContent)
            {
                tagHelperOutput.Content.AppendHtml(item);
            }

            return tagHelperOutput;
        }
    }
}