using FileServer.Web;
using FileServer.Web.Sorters;
using Microsoft.Extensions.FileProviders;

OptionsLoader.LoadOptions(args);
if (!OptionsLoader.Options.Loaded) return;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://*:5001;http://*:5000");

builder.Services.AddDirectoryBrowser();
var app = builder.Build();

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(OptionsLoader.Options.Path),
    EnableDirectoryBrowsing = true,
    DirectoryBrowserOptions = {  Formatter = new SortByNameDirectoryFormatter() }
});

app.Run();