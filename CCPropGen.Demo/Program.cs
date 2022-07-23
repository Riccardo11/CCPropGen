using Maui.CCPropGen;

namespace CCPropGen.Demo
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ciaoooooooooo");
            HelloFrom("Hello fdsald");


            new Prova();
        }

        static partial void HelloFrom(string name);
    }

    [CCPropGen(nameof(Program), typeof(Prova), "mi", typeof(string))]
    public partial class Prova
    {

    }
}