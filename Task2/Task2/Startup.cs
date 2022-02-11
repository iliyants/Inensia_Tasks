using Task2;

internal class Startup
{
    static void Main()
    {

        Console.WriteLine("Please enter a gross salary: ");

        decimal grossValue;
        var input = Console.ReadLine();


        while(!decimal.TryParse(input, out grossValue) || decimal.Parse(input) < 0)
        {
            Console.WriteLine("Please enter a valid positive number.");

            input = Console.ReadLine();
        }

        Console.WriteLine($"The net salary is {TaxCalculator.CalculateNetValue(grossValue)} IDR.");

    }
}