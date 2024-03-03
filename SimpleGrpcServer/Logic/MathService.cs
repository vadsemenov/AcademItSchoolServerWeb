namespace SimpleGrpcServer.Logic;

public static class MathService
{
    public static int GetFibonacciNumber(int fibonacciNumber)
    {
        var currentNumber = 0;
        var nextNumber = 1;

        for (var i = 0; i < fibonacciNumber; i++)
        {
            var temp = currentNumber;
            currentNumber = nextNumber;
            nextNumber = temp + nextNumber;
        }

        return currentNumber;
    }
}