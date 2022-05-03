using SecurityScanner;


var mainlinePath = @"C:\Users\Administrator\Perforce\AUTOMATION_TOOLS\Unification_Tester\Mainline";
var scanner = new Scanner(mainlinePath);
scanner.Scan();