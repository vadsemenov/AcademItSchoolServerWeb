namespace SimpleGrpcServer.Logic;

public static class MathService
{
    public static int GetFibonacciNumber(int indexOfFibonacciNumber)
    {
        var currentFibonacciNumber = 0;
        var nextFibonacciNumber = 1;

        for (var i = 0; i < indexOfFibonacciNumber; i++)
        {
            var temp = currentFibonacciNumber;
            currentFibonacciNumber = nextFibonacciNumber;
            nextFibonacciNumber = temp + nextFibonacciNumber;
        }

        return currentFibonacciNumber;
    }
}