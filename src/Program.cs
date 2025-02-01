using src.Commands;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0){
                System.Console.WriteLine("No command provided.");
                return;
            }

            ICommand command = args[0] switch{
                "init" => new InitCommand(),
                _ => null
            };

            if(command == null){
                System.Console.WriteLine("Invalid command.");
                return;
            }

            command.Execute();
        }
    }
}
