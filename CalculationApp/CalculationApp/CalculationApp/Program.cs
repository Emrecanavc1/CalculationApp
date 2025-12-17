using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Ass1C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunApp();
        }

        // ===================== MAIN APP MENU =====================
        static void RunApp()
        {
            while (true)
            {
                Console.Clear();
                Console.Title = "Assignment App";

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== CALCULATION APPLICATION ===");
                Console.ResetColor();

                Console.WriteLine("1) N x N Multiplication Table (Boxed + Headed + Colored)");
                Console.WriteLine("2) Assignment Simple Table (single number 1..10)");
                Console.WriteLine("3) Calculator (basic + scientific + history)");
                Console.WriteLine("4) Exit");
                Console.Write("\nYour choice (1-4): ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    int n = ReadPositiveIntExceptionBased("Enter table size (N): ");
                    PrintMultiplicationTableGrid(n);
                }
                else if (choice == "2")
                {
                    int number = ReadPositiveIntExceptionBased("Enter a number for multiplication table: ");
                    PrintMultiplicationTable(number);
                }
                else if (choice == "3")
                {
                    RunCalculator();
                }
                else if (choice == "4")
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid choice! Please enter 1, 2, 3 or 4.");
                    Console.ResetColor();
                    AutoReturnCountdown(3);
                }
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nProgram is closing... Have a nice day!");
            Console.ResetColor();
        }

        // ===================== INPUT (exception-based) =====================
        static int ReadPositiveIntExceptionBased(string message)
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(message);
                    Console.ResetColor();

                    string input = Console.ReadLine();
                    int value = int.Parse(input);

                    if (value <= 0)
                        throw new ArgumentOutOfRangeException(nameof(value), "Number must be greater than zero!");

                    return value;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Please enter a valid integer number (e.g. 5).");
                    Console.ResetColor();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Number must be greater than zero.");
                    Console.ResetColor();
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: The number you entered is too large.");
                    Console.ResetColor();
                }
            }
        }

        // ✅ TR/EN decimal support: "12,5" or "12.5"
        static double ReadDoubleExceptionBased(string message)
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(message);
                    Console.ResetColor();

                    string input = Console.ReadLine()?.Trim() ?? "";
                    input = input.Replace(',', '.'); // allow TR comma input

                    double value = double.Parse(input, CultureInfo.InvariantCulture);
                    return value;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Please enter a valid number (e.g. 12.5 or 12,5).");
                    Console.ResetColor();
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: The number you entered is too large.");
                    Console.ResetColor();
                }
            }
        }

        // ===================== UX HELPERS =====================
        static void AutoReturnCountdown(int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"\rReturning... {i} ");
                Console.ResetColor();
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        static void Pause(string message = "\nPress any key to return to the main menu...")
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadKey(true);
        }

        static bool AskContinueCalculator()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nDo you want to continue calculating? (Y/N): ");
                Console.ResetColor();

                string ans = (Console.ReadLine() ?? "").Trim().ToUpper();

                if (ans == "Y") return true;
                if (ans == "N") return false;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter only Y or N.");
                Console.ResetColor();
            }
        }

        // ===================== EXERCISE 1 (assignment simple) =====================
        // Required: method + parameter + while loop (1..10)
        static void PrintMultiplicationTable(int number)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Multiplication table of {number} (1-10):\n");
            Console.ResetColor();

            int i = 1;
            while (i <= 10)
            {
                int result = number * i;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{number}");
                Console.ResetColor();

                Console.Write(" x ");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{i}");
                Console.ResetColor();

                Console.Write(" = ");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{result}");
                Console.ResetColor();

                i++;
            }

            Pause();
        }

        // ===================== N x N TABLE (boxed + headed + colored) =====================
        static void PrintMultiplicationTableGrid(int n)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{n} x {n} MULTIPLICATION TABLE (Headed + Boxed)\n");
            Console.ResetColor();

            int cellWidth = (n * n).ToString().Length + 2;

            PrintBorder(n, cellWidth);

            // Header row
            Console.Write("|");
            PrintHeaderCell("", cellWidth);
            for (int col = 1; col <= n; col++)
                PrintHeaderCell(col.ToString(), cellWidth);
            Console.WriteLine();

            PrintBorder(n, cellWidth);

            for (int row = 1; row <= n; row++)
            {
                Console.Write("|");
                PrintHeaderCell(row.ToString(), cellWidth);

                for (int col = 1; col <= n; col++)
                {
                    int value = row * col;
                    if (row == col) PrintValueCell(value, cellWidth, ConsoleColor.White);
                    else PrintValueCell(value, cellWidth, ConsoleColor.Green);
                }

                Console.WriteLine();
                PrintBorder(n, cellWidth);
            }

            Pause();
        }

        static void PrintBorder(int n, int cellWidth)
        {
            for (int i = 0; i < (n + 1); i++)
            {
                Console.Write("+");
                Console.Write(new string('-', cellWidth));
            }
            Console.WriteLine("+");
        }

        static void PrintHeaderCell(string text, int cellWidth)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text.PadLeft(cellWidth - 1).PadRight(cellWidth));
            Console.ResetColor();
            Console.Write("|");
        }

        static void PrintValueCell(int value, int cellWidth, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            string text = value.ToString();
            Console.Write(text.PadLeft(cellWidth - 1).PadRight(cellWidth));
            Console.ResetColor();
            Console.Write("|");
        }

        // ===================== CALCULATOR (basic + scientific + history) =====================
        static void RunCalculator()
        {
            Queue<string> history = new Queue<string>();

            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== CALCULATOR ===\n");
                Console.ResetColor();

                Console.WriteLine("BASIC OPERATIONS");
                Console.WriteLine("1) Addition (+)");
                Console.WriteLine("2) Subtraction (-)");
                Console.WriteLine("3) Multiplication (*)");
                Console.WriteLine("4) Division (/)");
                Console.WriteLine("5) Modulus (%)");
                Console.WriteLine("6) Power (a^b)");

                Console.WriteLine("\nSCIENTIFIC OPERATIONS");
                Console.WriteLine("7) Square Root (sqrt(a))");
                Console.WriteLine("8) Sine (sin(a°))");
                Console.WriteLine("9) Cosine (cos(a°))");

                Console.WriteLine("\nOTHER");
                Console.WriteLine("10) Show History (last 5)");
                Console.WriteLine("11) Back to Main Menu");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nYour choice (1-11): ");
                Console.ResetColor();

                string choice = Console.ReadLine();

                if (choice == "11")
                    return;

                if (choice == "10")
                {
                    ShowHistory(history);
                    continue;
                }

                try
                {
                    double a, b, result;
                    string expr;

                    switch (choice)
                    {
                        case "1":
                            a = ReadDoubleExceptionBased("Enter first number: ");
                            b = ReadDoubleExceptionBased("Enter second number: ");
                            result = a + b;
                            expr = $"{Fmt(a)} + {Fmt(b)} = {Fmt(result)}";
                            break;

                        case "2":
                            a = ReadDoubleExceptionBased("Enter first number: ");
                            b = ReadDoubleExceptionBased("Enter second number: ");
                            result = a - b;
                            expr = $"{Fmt(a)} - {Fmt(b)} = {Fmt(result)}";
                            break;

                        case "3":
                            a = ReadDoubleExceptionBased("Enter first number: ");
                            b = ReadDoubleExceptionBased("Enter second number: ");
                            result = a * b;
                            expr = $"{Fmt(a)} * {Fmt(b)} = {Fmt(result)}";
                            break;

                        case "4":
                            a = ReadDoubleExceptionBased("Enter first number: ");
                            b = ReadDoubleExceptionBased("Enter second number: ");
                            if (b == 0) throw new DivideByZeroException();
                            result = a / b;
                            expr = $"{Fmt(a)} / {Fmt(b)} = {Fmt(result)}";
                            break;

                        case "5":
                            a = ReadDoubleExceptionBased("Enter first number: ");
                            b = ReadDoubleExceptionBased("Enter second number: ");
                            if (b == 0) throw new DivideByZeroException();
                            result = a % b;
                            expr = $"{Fmt(a)} % {Fmt(b)} = {Fmt(result)}";
                            break;

                        case "6":
                            a = ReadDoubleExceptionBased("Enter base (a): ");
                            b = ReadDoubleExceptionBased("Enter exponent (b): ");
                            result = Math.Pow(a, b);
                            expr = $"pow({Fmt(a)}, {Fmt(b)}) = {Fmt(result)}";
                            break;

                        case "7":
                            a = ReadDoubleExceptionBased("Enter number: ");
                            if (a < 0) throw new ArgumentException("sqrt requires non-negative number.");
                            result = Math.Sqrt(a);
                            expr = $"sqrt({Fmt(a)}) = {Fmt(result)}";
                            break;

                        case "8":
                            a = ReadDoubleExceptionBased("Enter angle in degrees: ");
                            result = Math.Sin(DegToRad(a));
                            expr = $"sin({Fmt(a)}°) = {Fmt(result)}";
                            break;

                        case "9":
                            a = ReadDoubleExceptionBased("Enter angle in degrees: ");
                            result = Math.Cos(DegToRad(a));
                            expr = $"cos({Fmt(a)}°) = {Fmt(result)}";
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid choice! Please select between 1 and 11.");
                            Console.ResetColor();
                            AutoReturnCountdown(2);
                            continue;
                    }

                    PrintResultBox(expr, result);
                    AddHistory(history, expr);
                }
                catch (DivideByZeroException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nError: Division or modulus by zero is not allowed!");
                    Console.ResetColor();
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nError: {ex.Message}");
                    Console.ResetColor();
                }

                if (!AskContinueCalculator())
                    return;
            }
        }


        static bool IsBasicOpKey(char key)
            => key == '+' || key == '-' || key == '*' || key == '/' || key == '%' || key == '^';

        static void AddHistory(Queue<string> history, string item)
        {
            if (history.Count == 5) history.Dequeue();
            history.Enqueue(item);
        }

        static void ShowHistory(Queue<string> history)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== HISTORY (last 5) ===\n");
            Console.ResetColor();

            if (history.Count == 0)
            {
                Console.WriteLine("No history yet.");
            }
            else
            {
                int i = 1;
                foreach (var h in history)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{i}) {h}");
                    Console.ResetColor();
                    i++;
                }
            }

            Pause("\nPress any key to go back to calculator...");
        }

        static double DegToRad(double deg) => deg * (Math.PI / 180.0);

        // Keep output consistent (InvariantCulture with dot)
        static string Fmt(double x) => x.ToString("0.##########", CultureInfo.InvariantCulture);

        static void PrintResultBox(string expression, double result)
        {
            string line1 = $"  {expression}  ";
            string line2 = $"  Result: {Fmt(result)}  ";

            int width = Math.Max(line1.Length, line2.Length);
            string border = "+" + new string('-', width) + "+";

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(border);
            Console.WriteLine("|" + line1.PadRight(width) + "|");
            Console.WriteLine("|" + line2.PadRight(width) + "|");
            Console.WriteLine(border);
            Console.ResetColor();
        }
    }
}
