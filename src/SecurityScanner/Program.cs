using SecurityScanner;

if (args.Length == 0)
{
    Console.WriteLine("Error: Need to enter path argument");
    Console.WriteLine("Usage: SecurityScanner.exe <PATH> (optional)<FILE_PATTERN> " +
                                        "PATH - Argument for the target root path to scan" +
                                        "FILE_PATTERN - Argument for file types to scan (for example: \"*.*\",\".xml\" etc..)");
    Environment.Exit(-1);
}

var pattern = ".cs";
if (args.Length == 2)
{
    pattern = args[1];
}
var path = args[0];
var scanner = new Scanner(path, pattern);
scanner.Scan();