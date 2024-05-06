using System.Diagnostics;

internal class Program
{
    public static void Main(string[] args)
    {
        String directoryPath = Directory.GetCurrentDirectory();
        Console.WriteLine("Current Directory: " + directoryPath);
        DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
        FileInfo[] files = directoryInfo.GetFiles("*.jar");

        Console.WriteLine("Scanning for build info.");
        DirectoryInfo renJavaDirectory = new DirectoryInfo(directoryPath + "\\renjava\\");
        if (renJavaDirectory.Exists)
        {
            Console.WriteLine("RenJava Directory exists...");
        }
        FileInfo[] renJavaFiles = renJavaDirectory.GetFiles("build.info");
        Console.WriteLine("Detected Files: " + renJavaFiles.Length);
        String jarFile = "";
        try
        {
            StreamReader sr = new StreamReader(renJavaFiles[0].FullName);
            String line = sr.ReadLine();
            while (line != null)
            {
                Console.WriteLine("Line: " + line);
                if (line.StartsWith("file="))
                {
                    line = line.Replace("file=", "");
                    Console.WriteLine("File = " + renJavaFiles[0].Name);
                    jarFile = line;
                }
                line = sr.ReadLine();
            }

            sr.Close();
        } catch(Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        } finally
        {
            Console.WriteLine("Completed...");
        }
        

        Console.WriteLine("Looking for Jar File...");
        foreach (FileInfo file in files)
        {
            Console.WriteLine("Jar File: " + jarFile);
            Console.WriteLine("File: " + file.Name);

            if (jarFile.Length != 0 && jarFile.Equals(file.Name)) {
                executeFile(file);
                break;
            } else if (jarFile.Length == 0)
            {
                executeFile(file);
                break;
            }
        }
    }

    private static void executeFile(FileInfo file)
    {
        Console.WriteLine("Processing " + file.Name);

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
    }
}