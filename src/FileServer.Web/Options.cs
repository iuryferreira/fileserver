using CommandLine;

namespace FileServer.Web;

public class Options
{
    [Option('p', "path", Required = true, HelpText = "sets the path of the files to be served")]
    public string? Path { get; set; }

    public bool Loaded { get; set; }
}

public static class OptionsLoader
{
    public static Options Options { get; private set; } = new();

    public static void LoadOptions(IEnumerable<string> args)
    {
        Parser.Default
            .ParseArguments<Options>(args)
            .WithParsed(options =>
            {
                if (string.IsNullOrEmpty(options.Path))
                {
                    Options.Loaded = false;
                    return;
                }
                Options = options;
                Options.Loaded = true;
            });
    }
}