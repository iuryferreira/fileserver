using System.Text.Encodings.Web;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace FileServer.Web.Sorters;

public class SortByNameDirectoryFormatter : HtmlDirectoryFormatter
{
    public SortByNameDirectoryFormatter() : base(HtmlEncoder.Default) { }

    public override Task GenerateContentAsync(HttpContext context, IEnumerable<IFileInfo> contents)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (contents == null) throw new ArgumentNullException(nameof(contents));
        var sortedContents = contents.OrderBy(content => content.Name);
        return base.GenerateContentAsync(context, sortedContents);
    }
}