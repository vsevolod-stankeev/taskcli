using taskcli.Models;
using taskcli.Services;

namespace taskcli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) {
                Console.WriteLine("0");
                return;
            }
            else if (args.Length == 1) {
                Console.WriteLine("1");
            }
            else if (args.Length == 2) {
                Console.WriteLine("2");
            }
            else if (args.Length == 3) {
                Console.WriteLine("3");
            }
            else {
                Console.WriteLine(">3");
                return;
            }
        }
    }
}
