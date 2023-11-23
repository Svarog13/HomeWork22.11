using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        Console.WriteLine("Even numbers:");
        PrintFilteredNumbers(numbers, IsEven);

        Console.WriteLine("\nOdd numbers:");
        PrintFilteredNumbers(numbers, IsOdd);

        Console.WriteLine("\nPrime  numbers:");
        PrintFilteredNumbers(numbers, IsPrime);

        Console.WriteLine("\nFibonacci numbers:");
        PrintFilteredNumbers(numbers, IsFibonacci);

        Console.ReadKey();
    }

    static void PrintFilteredNumbers(int[] numbers, FilterDelegate filter)
    {
        foreach (var number in numbers)
        {
            if (filter(number))
            {
                Console.Write(number + " ");
            }
        }
        Console.WriteLine();
    }

    static bool IsEven(int number)
    {
        return number % 2 == 0;
    }
    static bool IsOdd(int number)
    {
        return number % 2 != 0;
    }

    static bool IsPrime(int number)
    {
        if (number < 2)
            return false;

        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
                return false;
        }
        return true;
    }

    static bool IsFibonacci(int number)
    {
        int a = 0, b = 1;
        while (b < number)
        {
            int temp = a;
            a = b;
            b = temp + b;
        }
        return b == number;
    }

    delegate bool FilterDelegate(int number);
}

