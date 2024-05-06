using System.Diagnostics;

internal class Program
{
    public static void Main(string[] args)
    {
        String directoryPath = Directory.GetCurrentDirectory();
        Console.WriteLine("Current Directory: " + directoryPath);
        DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
        FileInfo[] files = directoryInfo.GetFiles("*.jar");
        foreach (FileInfo file in files)
        {
            Console.WriteLine("Executing jar: " + file.Name);
            var processInfo = new ProcessStartInfo(Directory.GetCurrentDirectory() + "\\jdk\\windows\\bin\\javaw.exe", "-jar " + file.Name)
            {
                CreateNoWindow = true,
                UseShellExecute = true,
            };
            if (processInfo != null)
            {
                Process process = Process.Start(processInfo);
            }
            break;
        }
    }
}