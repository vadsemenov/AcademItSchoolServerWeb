namespace SimpleGrpcServer.Logic;

public static class MathService
{
    public static int FindFibonacciNumber(int n)
    {
        var firstVariable = 0;
        var secondVariable = 1;

        for (var i = 0; i < n; i++)
        {
            var temp = firstVariable;
            firstVariable = secondVariable;
            secondVariable = temp + secondVariable;
        }

        return firstVariable;
    }
}