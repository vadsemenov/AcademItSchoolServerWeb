namespace VectorTask.Tests;

public class VectorTests
{
    [Fact]
    public void Compare_TwoVectorsSum_ReturnsTrue()
    {
        var expected = new Vector(new double[] { 2, 4, 6 });

        var vector1 = new Vector(new double[] { 1, 2, 3 });
        var vector2 = new Vector(new double[] { 1, 2, 3 });

        var result = Vector.GetSum(vector1, vector2);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Compare_TwoVectorsDifference_ReturnsTrue()
    {
        var expected = new Vector(new double[] { 0, 0, 0 });

        var vector1 = new Vector(new double[] { 1, 2, 3 });
        var vector2 = new Vector(new double[] { 1, 2, 3 });

        var result = Vector.GetDifference(vector1, vector2);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void Compare_TwoVectorsScalarProduct_ReturnsFalse(double expected)
    {
        var vector1 = new Vector(new double[] { 1, 2, 3 });
        var vector2 = new Vector(new double[] { 1, 2, 3 });

        var result = Vector.GetScalarProduct(vector1, vector2);

        Assert.NotEqual(expected, result);
    }
}