namespace ExcelExport2;

public record Person(int Age, string FirstName, string SecondName, string PhoneNumber)
{
    public int Age { get; } = Age;
   
    public string FirstName { get; } = FirstName;
    
    public string SecondName { get; } = SecondName;
    
    public string PhoneNumber { get; } = PhoneNumber;
}