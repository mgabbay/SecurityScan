using SecurityScanner;

//var mainlinePath = @"C:\Users\Administrator\Perforce\AUTOMATION_TOOLS\Unification_Tester\Mainline";



if (args.Length != 1)
{
    Console.WriteLine("Usage: argument for required path root to scan");
    Environment.Exit(-1);
}

var path = args[0];
var scanner = new Scanner(path);
scanner.Scan();