using Grpc.Net.Client;
using SimpleGrpcServer;

namespace SimpleGrpcClient
{
    public class Program
    {
        private static Greeter.GreeterClient? _client;

        public static void Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5283");
            _client = new Greeter.GreeterClient(channel);

            var waitTime = new TimeSpan(0, 0, 1);

            var helloStringTask = GetHello("User").WaitAsync(waitTime);

            Console.WriteLine($"Message from server: {helloStringTask.Result}");

            var numbers = new List<int> { 1, 3, 5, 6, };
            var calculateSumResponseTask = GetSum(numbers).WaitAsync(waitTime);

            Console.WriteLine($"Sum result: {calculateSumResponseTask.Result}");

            var fibonacciNumber = 10;
            var findFibonacciNumberTask = FindFibonacciNumber(fibonacciNumber).WaitAsync(waitTime);

            Console.WriteLine($"10 Fibonacci number: {findFibonacciNumberTask.Result}");

            Console.Read();
        }

        private static async Task<string> GetHello(string name)
        {
            var response = await _client!.SayHelloAsync(new HelloRequest
            {
                Name = name
            });

            return response.Message;
        }

        private static async Task<int> GetSum(List<int> numbers)
        {
            var response = await _client!.CalculateSumAsync(new CalculateSumRequest
            {
                Numbers = { numbers }
            });

            return response.Sum;
        }

        private static async Task<int> FindFibonacciNumber(int fibonacciNumber)
        {
            var response = await _client!.FindFibonacciNumberAsync(new FindFibonacciNumberRequest
            {
                FibonacciNumber = fibonacciNumber
            });

            return response.Number;
        }
    }
}
