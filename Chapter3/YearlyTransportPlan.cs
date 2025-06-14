using System.Globalization;

namespace Chapter3;

public class YearlyTransportPlan
{
    public enum MeanEnum
    {
        Car,
        Bus,
        Subway,
        Bike,
        Walk
    }

    public static void Demo()
    {
        Random random = new();
        var meansCount = Enum.GetNames<MeanEnum>().Length;
        var year = DateTime.Now.Year;
        var means = new MeanEnum[12][];
        for (var m = 1; m <= 12; m++)
        {
            int daysCount = DateTime.DaysInMonth(year, m);
            means[m - 1] = new MeanEnum[daysCount];
            for (var d = 1; d <= daysCount; d++)
            {
                var mean = random.Next(meansCount);
                means[m - 1][d - 1] = (MeanEnum)mean;
            }
        }

        var months = GetMonthNames();
        int nameLength = months.Max(n => n.Length) + 2;
        for (var m = 1; m <= 12; m++)
        {
            string month = months[m - 1];
            Console.Write($"{month}:".PadRight(nameLength));
            for (var d = 1; d <= means[m - 1].Length; d++)
            {
                MeanEnum mean = means[m - 1][d - 1];
                (char character, ConsoleColor color) = Get(mean);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = color;
                Console.Write(character);
                Console.ResetColor();
                Console.Write(" ");
            }
            Console.WriteLine();
        }

        string[] GetMonthNames()
        {
            CultureInfo culture = new("en");
            string[] names = new string[12];
            foreach (int m in Enumerable.Range(1, 12))
            {
                DateTime firstDay = new(DateTime.Now.Year, m, 1);
                string name = firstDay.ToString("MMMM", culture);
                names[m - 1] = name;
            }

            return names;
        }

        (char Char, ConsoleColor Color) Get(MeanEnum mean)
        {
            return mean switch
            {
                MeanEnum.Bike   => ('B', ConsoleColor.Blue),
                MeanEnum.Bus    => ('U', ConsoleColor.DarkGreen),
                MeanEnum.Car    => ('C', ConsoleColor.Red),
                MeanEnum.Subway => ('S', ConsoleColor.Magenta),
                MeanEnum.Walk   => ('W', ConsoleColor.DarkYellow),
                _               => throw new Exception("unknown type")
            };
        }

    }
    
}