using System;
using System.IO;


RenameDirrs(@"C:\olp", "BMO", "AAV");
RenameFiles(@"C:\olp", "BMO", "AAV");
RenameFiles(@"C:\olp", "bmo", "aav");
RenameFiles(@"C:\olp", "Bmo", "Aav");

static void RenameFiles(string v1, string v2, string v3)
{
  Console.WriteLine($"  {v1}   {v2}   {v3}");

  foreach (var src in Directory.GetFiles(v1, $"*{v2}*", SearchOption.AllDirectories))
  {
    var dst = src.Replace(v2, v3);

    Console.WriteLine($"  {src,-70}   {dst}");
    File.Move(src, dst);
  }
}
static void RenameDirrs(string v1, string v2, string v3)
{
  Console.WriteLine($"  {v1}   {v2}   {v3}");

  foreach (var src in Directory.GetDirectories(v1, $"*{v2}*", SearchOption.AllDirectories))
  {
    var dst = src.Replace(v2, v3);

    Console.WriteLine($"  {src,-40}   {dst}");
    Directory.Move(src, dst);
  }
}

