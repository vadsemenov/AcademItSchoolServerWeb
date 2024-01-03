namespace ConditionalCompilation
{
    public class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            Console.WriteLine("This run under Debug!");
#else
            Console.WriteLine("This run under Release!");
#endif

            Console.Read();
        }
    }
}
