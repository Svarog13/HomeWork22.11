using System;

class Program
{
    static void Main(string[] args)
    {
        Action<DateTimeDelegate> printDateTime = action => Console.WriteLine(action());
        Action<double> printArea = area => Console.WriteLine($"Area: {area}");

        printDateTime(IsCurrentTime);
        printDateTime(IsCurrentDate);
        printDateTime(IsCurrentDayOfWeek);

        CalculateAreaDelegate triangleAreaFunc = Triangle.CalculateArea;
        printArea(triangleAreaFunc(3, 4, 5));

        CalculateRectangleAreaDelegate rectangleAreaFunc = Rectangle.CalculateArea;
        printArea(rectangleAreaFunc(4, 6));
    }

    class Triangle
    {
        public static double CalculateArea(double a, double b, double c)
        {
            double s = (a + b + c) / 2;
            double area = Math.Sqrt(s * (s - a) * (s - b) * (s - c));
            return area;
        }
    }

    class Rectangle
    {
        public static double CalculateArea(double length, double width)
        {
            double area = length * width;
            return area;
        }
    }

    static string IsCurrentTime()
    {
        DateTime currentTime = DateTime.Now;
        return "Current Time: " + currentTime.ToShortTimeString();
    }

    static string IsCurrentDate()
    {
        DateTime currentTime = DateTime.Now;
        return "Current Date: " + currentTime.ToShortDateString();
    }

    static string IsCurrentDayOfWeek()
    {
        DateTime currentTime = DateTime.Now;
        return "Current Day of the Week: " + currentTime.DayOfWeek;
    }

    delegate string DateTimeDelegate();
    delegate double CalculateAreaDelegate(double a, double b, double c);
    delegate double CalculateRectangleAreaDelegate(double length, double width);
}