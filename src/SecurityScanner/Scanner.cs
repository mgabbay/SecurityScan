using System.Text;

namespace SecurityScanner;

public class Scanner
{
    private List<string> _files;
    private readonly List<string> ScanItems = Constants.ScanItems;
    private List<string> _dirtyFiles = new List<string>();
    private int _hitsCounter;
    private int[] elementCounter = new int[Constants.ScanItems.Count];

    public Scanner(string path, string filePattern = "*.cs")
    {
        _files = Directory.GetFiles(path, filePattern, SearchOption.AllDirectories).ToList();
        if (filePattern == "*.cs")
        {
            _files.RemoveAll(x => x.Contains(".g.cs"));
        }
    }

    public void Scan()
    {
        var sb = new StringBuilder();
        var falsePositives = Constants.FalsePositives;
        GetSuspectedFiles(sb);
        sb.AppendLine("File list loaded");
        sb.AppendLine("============================");
        sb.AppendLine("============================");
        sb.AppendLine("");
        var total = _dirtyFiles.Count;
        for (var j = 0; j < total; j++)
        {
            ScanSuspectedFiles(j, total, falsePositives, sb);
        }
        HitsCountReport(sb);
        File.WriteAllText(@"./result.txt", sb.ToString());
    }

    private void ScanSuspectedFiles(int j, int total, List<string> falsePositives, StringBuilder sb)
    {
        Console.WriteLine($"Scanning {j} out of {total}");
        var lines = File.ReadAllLines(_dirtyFiles[j]);
        bool added = false;

        for (var i = 0; i < lines.Length; i++)
        {
            for (var l = 0; l < ScanItems.Count; l++)
            {
                if (lines[i].Contains(ScanItems[l]) &&
                    !falsePositives.Exists(x => lines[i].ToLower().Contains(x.ToLower())))
                {
                    if (ScanItems[l] == "token")
                    {
                        if (lines[i].Length < 120)
                            continue;
                    }

                    if (!added)
                    {
                        sb.AppendLine($"File {_dirtyFiles[j]}");
                        added = true;
                    }

                    sb.AppendLine($"{lines[i].ToString()}");
                    sb.AppendLine($"\t'{ScanItems[l]}' at line {i}");
                    _hitsCounter++;
                    elementCounter[l]++;
                }
            }
        }
    }

    private void HitsCountReport(StringBuilder sb)
    {
        sb.AppendLine("============================");
        sb.AppendLine($"Found {_hitsCounter} hits");
        for (var i = 0; i < ScanItems.Count; i++)
        {
            sb.AppendLine($"'{ScanItems[i]}' : {elementCounter[i]} times ");
        }
    }

    private void GetSuspectedFiles(StringBuilder sb)
    {
        foreach (var file in _files)
        {
            var txt = File.ReadAllText(file).ToLower();
            if (ScanItems.Any(x => txt.Contains(x)))
            {
                _dirtyFiles.Add(file);
                sb.AppendLine($"File: {file}");
            }
        }
    }
}