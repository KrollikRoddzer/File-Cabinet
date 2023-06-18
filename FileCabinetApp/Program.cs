﻿namespace FileCabinetApp
{
    using System.Globalization;
    using FileCabinetApp.Service;

    public static class Program
    {
        private const string DeveloperName = "Stanislau Zaitsau";
        private const string HintMessage = "Enter your command, or enter 'help' to get help.";
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private static bool isRunning = true;

        private static FileCabinetService fileCabinetService = new FileCabinetService();

        private static Tuple<string, Action<string>>[] commands = new Tuple<string, Action<string>>[]
        {
            new Tuple<string, Action<string>>("help", PrintHelp),
            new Tuple<string, Action<string>>("exit", Exit),
            new Tuple<string, Action<string>>("stat", Stat),
            new Tuple<string, Action<string>>("create", Create),
            new Tuple<string, Action<string>>("list", GetRecords),
            new Tuple<string, Action<string>>("edit", Edit),
        };

        private static string[][] helpMessages = new string[][]
        {
            new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
            new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
            new string[] { "stat", "prints record count", "The 'stat' command prints record counts." },
            new string[] { "create", "creates a new record", "The 'create' command creates a new record." },
            new string[] { "list", "prints all records", "The 'list' command prints all records." },
            new string[] { "edit", "edits the record if it's exist. Input format: edit {id(integer)}", "The 'edit' command edits the record if it's exist." },
        };

        public static void Main(string[] args)
        {
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");
            Console.WriteLine(Program.HintMessage);
            Console.WriteLine();

            do
            {
                Console.Write("-> ");
                var line = Console.ReadLine();
                var inputs = line != null ? line.Split(' ', 2) : new string[] { string.Empty, string.Empty };
                const int commandIndex = 0;
                var command = inputs[commandIndex];

                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine(Program.HintMessage);
                    continue;
                }

                var index = Array.FindIndex(commands, 0, commands.Length, i => i.Item1.Equals(command, StringComparison.InvariantCultureIgnoreCase));
                if (index >= 0)
                {
                    const int parametersIndex = 1;
                    var parameters = inputs.Length > 1 ? inputs[parametersIndex] : string.Empty;
                    commands[index].Item2(parameters);
                }
                else
                {
                    PrintMissedCommandInfo(command);
                }
            }
            while (isRunning);
        }

        private static void PrintMissedCommandInfo(string command)
        {
            Console.WriteLine($"There is no '{command}' command.");
            Console.WriteLine();
        }

        private static void PrintHelp(string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                var index = Array.FindIndex(helpMessages, 0, helpMessages.Length, i => string.Equals(i[Program.CommandHelpIndex], parameters, StringComparison.InvariantCultureIgnoreCase));
                if (index >= 0)
                {
                    Console.WriteLine(helpMessages[index][Program.ExplanationHelpIndex]);
                }
                else
                {
                    Console.WriteLine($"There is no explanation for '{parameters}' command.");
                }
            }
            else
            {
                Console.WriteLine("Available commands:");

                foreach (var helpMessage in helpMessages)
                {
                    Console.WriteLine("\t{0}\t- {1}", helpMessage[Program.CommandHelpIndex], helpMessage[Program.DescriptionHelpIndex]);
                }
            }

            Console.WriteLine();
        }

        private static void Exit(string parameters)
        {
            Console.WriteLine("Exiting an application...");
            isRunning = false;
        }

        private static void Stat(string parameters)
        {
            var recordsCount = Program.fileCabinetService.GetStat();
            Console.WriteLine($"{recordsCount} record(s).");
        }

        private static void Create(string parameters)
        {
            int id;
            while (true)
            {
                try
                {
                    Console.Write("First name: ");
                    string? firstName;
                    firstName = Console.ReadLine();

                    Console.Write("Last name: ");
                    string? lastName;
                    lastName = Console.ReadLine();

                    Console.Write("Date of birth: ");
                    string? dateOfBirth;
                    dateOfBirth = Console.ReadLine();
                    DateTime date;
                    if (!DateTime.TryParse(dateOfBirth, out date))
                    {
                        throw new ArgumentException(string.Format("Date of birth must be between {0} and {1}.\nCorrect date format is dd/mm/yyyy", new DateTime(1950, 1, 1).ToString("yyyy-MMM-dd", FileCabinetRecord.DateFormat), DateTime.Now.ToString(DateTime.Now.ToString("yyyy-MMM-dd", FileCabinetRecord.DateFormat))));
                    }

                    Console.Write("Sex: ");
                    string? sex_ = Console.ReadLine();
                    char sex = sex_ is null || sex_.Length != 1 ? throw new ArgumentException("Sex must be m or f.") : sex_[0];

                    Console.Write("Post index: ");
                    string? postIndexString = Console.ReadLine();
                    int postIndex = int.Parse(postIndexString is null ? throw new ArgumentException("Post index must be not null.") : postIndexString);

                    Console.Write("Country: ");
                    string? country = Console.ReadLine();
                    id = fileCabinetService.CreateRecord(firstName, lastName, date, sex, postIndex, country);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message + '\n' + "Try again.\n");
                }
            }

            Console.WriteLine($"Record #{id} is created.");
        }

        private static void GetRecords(string parameters)
        {
            foreach (var record in fileCabinetService.GetRecords())
            {
                Console.WriteLine(record.ToString());
            }
        }

        private static void Edit(string parameters)
        {
            int id;
            if (!int.TryParse(parameters, out id))
            {
                Console.WriteLine("Incorrect id format. Id must be integer greater 0.");
                return;
            }

            try
            {
                fileCabinetService.EditRecord(id);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}