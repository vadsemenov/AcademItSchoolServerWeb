using Grpc.Core;
using SimpleGrpcServer.Logic;

namespace SimpleGrpcServer.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloResponse> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloResponse
            {
                Message = "Hello " + request.Name
            });
        }

        public override Task<CalculateSumResponse> CalculateSum(CalculateSumRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CalculateSumResponse
            {
                Sum = request.Numbers.Sum()
            });
        }

        public override Task<GetFibonacciNumberResponse> GetFibonacciNumber(GetFibonacciNumberRequest request, ServerCallContext context)
        {
            if (request.IndexOfFibonacciNumber < 0)
            {
                _logger.LogError("Bad Fibonacci number");

                throw new RpcException(Status.DefaultCancelled, "Bad Fibonacci number");
            }

            var fibonacciNumber = MathService.GetFibonacciNumber(request.IndexOfFibonacciNumber);

            return Task.FromResult(new GetFibonacciNumberResponse
            {
                Number = fibonacciNumber
            });
        }
    }
}
