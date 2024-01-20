namespace VectorTask;

public class Program
{
    public static void Main(string[] args)
    {
        var vector1 = new Vector(4);
        var components = new double[] { 13.5, 9.3, 3.6, 8.9, 25.7, 33.0 };
        var vector2 = new Vector(components);
        var vector3 = new Vector(5, components);
        var vector4 = new Vector(vector3);

        Console.WriteLine("Размер вектора 1: " + vector1.GetSize());
        Console.WriteLine("Длина вектора 1: " + vector1.GetLength());
        Console.WriteLine("Размер вектора 2: " + vector2.GetSize());
        Console.WriteLine("Длина вектора 2: " + vector2.GetLength());
        Console.WriteLine("Размер вектора 3: " + vector3.GetSize());
        Console.WriteLine("Длина вектора 3: " + vector3.GetLength());
        Console.WriteLine("Размер вектора 4: " + vector4.GetSize());
        Console.WriteLine("Длина вектора 4: " + vector4.GetLength());

        vector3.Add(vector4);
        Console.WriteLine("Вектор 3 после прибавления вектора 4: " + vector3);
        Console.WriteLine("Размер вектора 3 после прибавления вектора 4: " + vector2.GetSize());
        Console.WriteLine("Длина вектора 3 после прибавления вектора 4: " + vector2.GetLength());

        vector3.Subtract(vector2);
        Console.WriteLine("Вектор 3 после вычитания вектора 2: " + vector2);
        Console.WriteLine("Вектор 3 компонент по индексу 1: " + vector2.GetComponentByIndex(1));

        vector3.SetComponentByIndex(3, 14.0);
        Console.WriteLine("Присвоить компоненту по индексу2 значение 14: " + vector3);

        var sumVector = Vector.GetSum(vector3, vector4);
        Console.WriteLine("Сложение вектора 3 и 4: " + sumVector);

        var differenceVector = Vector.GetDifference(vector3, vector4);
        Console.WriteLine("Вычитание векторов 3 и 4: " + differenceVector);

        vector4.MultiplyByScalar(10.0);
        Console.WriteLine("Умножение вектора 4 на скаляр 10: " + vector4);

        Console.WriteLine("Разворот вектора 3: " + vector3.Reverse());

        var scalarProduct = Vector.GetScalarProduct(vector2, vector3);
        Console.WriteLine("Скалярное произведение вектора 2 и 3: " + scalarProduct);

        Console.Read();
    }
}