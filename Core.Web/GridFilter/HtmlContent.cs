using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.Web.GridFilter
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
            attributes = attributes ?? new TagHelperAttributeList();
            TagHelperOutput tagHelperOutput = new TagHelperOutput(
                tagName,
                attributes,
                getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(
                    new DefaultTagHelperContent()));

            return tagHelperOutput;
        }

        public static TagHelperOutput TagHelper(string tagName)
        {
            TagHelperOutput tagHelperOutput = new TagHelperOutput(
                tagName,
                new TagHelperAttributeList(),
                getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(
                    new DefaultTagHelperContent()));

            return tagHelperOutput;
        }

        public static TagHelperOutput TagHelper(string tagName, TagHelperAttributeList attributes, string content)
        {
            attributes = attributes ?? new TagHelperAttributeList();
            TagHelperOutput tagHelperOutput = new TagHelperOutput(
                tagName,
                attributes,
                getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(
                    new DefaultTagHelperContent()));
            tagHelperOutput.Content.SetHtmlContent(content);

            return tagHelperOutput;
        }

        public static TagHelperOutput TagHelper(string tagName, TagHelperAttributeList attributes, params IHtmlContent[] htmlContent)
        {
            attributes = attributes ?? new TagHelperAttributeList();
            TagHelperOutput tagHelperOutput = new TagHelperOutput(
                tagName,
                attributes,
                getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(
                    new DefaultTagHelperContent()));
            foreach (var item in htmlContent)
            {
                tagHelperOutput.Content.AppendHtml(item);
            }

            return tagHelperOutput;
        }
    }
}