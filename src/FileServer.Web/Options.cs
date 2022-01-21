using CommandLine;

namespace FileServer.Web;

public class Options
{
    [Option('p', "path", Required = true, HelpText = "sets the path of the files to be served")]
    public string? Path { get; set; }

    public bool Loaded { get; private set; }

    public void Ok() => Loaded = true;
    public void Error() => Loaded = false;
}

public static class OptionsLoader
{
    public static Options Options { get; private set; } = new();

    public static void LoadOptions(IEnumerable<string> args)
    {
        Parser.Default.ParseArguments<Options>(args).WithParsed(Validate);
    }

    private static void Validate(Options options)
    {
        Options = options;

        if (string.IsNullOrEmpty(options.Path))
        {
            Options.Error();
            return;
        }

        var path = Path.GetFullPath(options.Path);
        if (Directory.Exists(path)) Options.Path = path;

        Options.Ok();
    }
}