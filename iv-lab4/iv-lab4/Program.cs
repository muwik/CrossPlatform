using LabsLibrary;
using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

[Command(Name = "iv-lab4", Description = "This lab work is created to manage labs #1,2,3 via command line utils"),
Subcommand(typeof(Run), typeof(Version), typeof(SetPath))]
class Lab4
{
	private const string LAB_PATH = "LAB_PATH";
	public static void Main(string[] args) => CommandLineApplication.Execute<Lab4>(args);
	private int OnExecute(CommandLineApplication app, IConsole console)
	{
		console.WriteLine("You are free to execute commands:");

		console.WriteLine($"Current platform is: " + Environment.OSVersion.Platform);
		app.ShowHelp();
		return 1;
	}

	[Command("version", Description = "All the information about this lab work")]
	private class Version
	{
		private int OnExecute(IConsole console)
		{
			Console.WriteLine("Ivan Nochovkin, Student of FIT Faculty, group IPS-43");
			console.WriteLine($"Version: 1.0.0");
			var path = SetPath.GetLabPathEnv();
			console.WriteLine($"Variable LAB_PATH: {(string.IsNullOrWhiteSpace(path) ? "not set" : path)}");
			return 1;
		}
	}

	[Command("run", Description = "Execute"), Subcommand(typeof(Lab1Presenter)), Subcommand(typeof(Lab2Presenter)), Subcommand(typeof(Lab3Presenter))]
	private class Run
	{
		private int OnExecute(IConsole console)
		{
			console.Error.WriteLine("You didn't choose the lab to execute");
			return 1;
		}

		private abstract class LabsLibrary
		{
			[Option("--input -i", Description = "input")]
			public string Input { get; } = null!;

			[Option("--output -o", Description = "output")]
			public string Output { get; } = null!;

			protected string GetInputPath()
			{
				if (File.Exists(Input))
				{
					return Input;
				}

				var path = SetPath.GetLabPathEnv();

				if (string.IsNullOrWhiteSpace(path))
				{
					path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}
				path = Path.Combine(path, "INPUT.TXT");

				if (File.Exists(path))
				{
					return path;
				}

				return string.Empty;
			}
			protected string GetOutputPath()
			{
				if (!string.IsNullOrWhiteSpace(Output))
				{
					return Output;
				}

				var path = SetPath.GetLabPathEnv();

				if (string.IsNullOrWhiteSpace(path))
				{
					path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				}

				if (Directory.Exists(path))
				{
					return Path.Combine(path, "OUTPUT.TXT");
				}

				return "OUTPUT.TXT";
			}

			protected string ReadInputFile()
			{
				string inputPath = GetInputPath();

				if (string.IsNullOrWhiteSpace(inputPath))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"input not found");
					Console.ForegroundColor = ConsoleColor.White;
					return string.Empty;
				}

				Console.WriteLine($"input - {inputPath}");

				return File.ReadAllText(inputPath);
			}
			protected void WriteOuputFile(string outputData)
			{
				string outputPath = GetOutputPath();

				Console.WriteLine($"output - {outputPath}");

				File.WriteAllText(outputPath, outputData);
			}

			protected virtual int OnExecute(IConsole console)
			{
				console.WriteLine("input - " + Input + "output - " + Output);
				return 1;
			}
		}

		[Command("lab1", Description = "Get the lab1 output")]
		private class Lab1Presenter : LabsLibrary
		{
			protected override int OnExecute(IConsole console)
			{
				string inputData = ReadInputFile();
				if (string.IsNullOrWhiteSpace(inputData))
				{
					return -1;
				}

				var result = Lab1.RunLab(Input);
				console.WriteLine($"Output: " + result);

				WriteOuputFile(result.ToString());
				return 1;
			}
		}

		[Command("lab2", Description = "Get the lab2 output")]
		private class Lab2Presenter : LabsLibrary
		{
			protected override int OnExecute(IConsole console)
			{
				string inputData = ReadInputFile();
				if (string.IsNullOrWhiteSpace(inputData))
				{
					return -1;
				}

				var result = Lab2.RunLab(Input);
				console.WriteLine($"Output: " + result);

				WriteOuputFile(result.ToString());
				return 1;
			}
		}

		[Command("lab3", Description = "Get the lab3 output")]
		private class Lab3Presenter : LabsLibrary
		{
			protected override int OnExecute(IConsole console)
			{
				string inputData = ReadInputFile();
				if (string.IsNullOrWhiteSpace(inputData))
				{
					return -1;
				}

				var result = Lab3.RunLab(Input);
				console.WriteLine($"Output: " + result);

				WriteOuputFile(result.ToString());
				return 1;
			}
		}
	}

	[Command("set-path", Description = "Set LAB_PATH")]
	private class SetPath
	{
		[Option("--path -p", Description = "set path")]
		[Required(ErrorMessage = "you should set path")]
		public string Path { get; } = null!;
		private int OnExecute(IConsole console)
		{
			Environment.SetEnvironmentVariable(LAB_PATH, Path);

			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				File.WriteAllText(".env", $"{LAB_PATH}={Path}");
			}

			return 1;
		}
		public static string GetLabPathEnv()
		{
			var path = Environment.GetEnvironmentVariable(LAB_PATH);
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				var text = File.ReadAllText(".env");
				var keyValue = text.Split('=');
				if (keyValue.Length == 2)
					path = keyValue[1];
			}
			return path ?? "";
		}
	}
}