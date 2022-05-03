using SecurityScanner;

//var mainlinePath = @"C:\Users\Administrator\Perforce\AUTOMATION_TOOLS\Unification_Tester\Mainline";
var path = args[0];
var scanner = new Scanner(path);
scanner.Scan();