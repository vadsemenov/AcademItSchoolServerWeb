using Grpc.Net.Client;
using SimpleGrpcServer;

namespace SimpleGrpcClient
{
    public class Program
    {
        private static Greeter.GreeterClient _client = null!;

        public static void Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5283");
            _client = new Greeter.GreeterClient(channel);

            var helloStringTask = GetHello("User");

            Console.WriteLine($"Message from server: {helloStringTask.Result}");

            var numbers = new List<int> { 1, 3, 5, 6, };
            var calculateSumResponseTask = GetSum(numbers);

            Console.WriteLine($"Sum result: {calculateSumResponseTask.Result}");

            var indexOfFibonacciNumber = 10;
            var findFibonacciNumberTask = GetFibonacciNumber(indexOfFibonacciNumber);

            Console.WriteLine($"10 Fibonacci number: {findFibonacciNumberTask.Result}");

            Console.Read();
        }

        private static async Task<string> GetHello(string name)
        {
            var response = await _client.SayHelloAsync(new HelloRequest
            {
                Name = name
            }, deadline: DateTime.UtcNow.AddSeconds(1));

            return response.Message;
        }

        private static async Task<int> GetSum(List<int> numbers)
        {
            var response = await _client.CalculateSumAsync(new CalculateSumRequest
            {
                Numbers = { numbers }
            }, deadline: DateTime.UtcNow.AddSeconds(1));

            return response.Sum;
        }

        private static async Task<int> GetFibonacciNumber(int indexOfFibonacciNumber)
        {
            var response = await _client.GetFibonacciNumberAsync(new GetFibonacciNumberRequest
            {
                IndexOfFibonacciNumber = indexOfFibonacciNumber
            }, deadline: DateTime.UtcNow.AddSeconds(1));

            return response.Number;
        }
    }
}
