using System;

namespace Project___Numerical_Methods
{
    class Program
    {
        static void Main(string[] args)
        {

            // سوال 1
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*P1 : ");
            Console.ResetColor();

            double h = 0.1;
            double[] xValues = { 1.1, 1.2, 1.7 };

            foreach (double x in xValues)
            {
                Console.WriteLine($"\n x : {x} , h : {h} => ans : {F_Euler(x, h)}");
            }

            Console.WriteLine("---------------------------------------------");

            foreach (double x in xValues)
            {
                double real = Real_F_Euler(x);
                double approx = F_Euler(x, h);

                Console.WriteLine($"\n x : {x} => real ans : {real}");
                Console.WriteLine($" x : {x} => |real ans - ans| = {Math.Abs(real - approx)}");
                Console.WriteLine("____");
            }

            // سوال 2
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n**P2 : ");
            Console.ResetColor();

            double[] hValues = { 0.1, 0.05, 0.025 };

            foreach (double step in hValues)
            {
                Console.WriteLine($"\n h : {step} => Trapezoidal : {Com_Trapezoidal(step)}");
            }

            Console.WriteLine("____");

            foreach (double step in hValues)
            {
                Console.WriteLine($"\n h : {step} => Simpson : {Simp(step)}");
            }

            // سوال 3
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n***P3 : ");
            Console.ResetColor();

            Console.WriteLine("\nFixed-point iteration : ");
            Fix(10, -8, 1, 2);

            Console.WriteLine("____");

            Console.WriteLine("\nNewton-Raphson : ");
            Newton(10, -8, 1, 2);

            Console.ReadKey();
        }

        // ---------------------------------------------------
        // سوال 1 - Euler Forward

        static double F_Euler(double xTarget, double h)
        {
            double x = 1.0;
            double y = 2.0;

            int n = (int)Math.Round((xTarget - x) / h);

            for (int i = 0; i < n; i++)
            {
                y = y + h * ((1 + x) / (1 + y));
                x += h;
            }

            return y;
        }

        static double Real_F_Euler(double x)
        {
            return Math.Sqrt(x * x + 2 * x + 6) - 1;
        }

        // ---------------------------------------------------
        // سوال 2 - Numerical Integration

        static double Com_Trapezoidal(double h)
        {
            double sum = 0;
            int n = (int)Math.Round(1 / h);

            for (int i = 1; i < n; i++)
            {
                sum += resu_f(Math.E + i * h);
            }

            return (h / 2) * (resu_f(Math.E) + 2 * sum + resu_f(Math.E + 1));
        }

        static double resu_f(double x)
        {
            return 1 / (x * Math.Log(x));
        }

        static double Simp(double h)
        {
            int n = (int)Math.Round(1 / h);

            if (n % 2 != 0)
                throw new Exception("Simpson rule requires even number of intervals.");

            double sum = 0;

            for (int i = 1; i < n; i++)
            {
                if (i % 2 == 0)
                    sum += 2 * resu_f(Math.E + i * h);
                else
                    sum += 4 * resu_f(Math.E + i * h);
            }

            return (h / 3) * (resu_f(Math.E) + sum + resu_f(Math.E + 1));
        }

        // ---------------------------------------------------
        // سوال 3 - Root Finding

        static void Fix(double d1, double d2, double a, double b)
        {
            double x = (a + b) / 2;
            int i = 0;
            double tol = Math.Pow(d1, d2);
            int maxIter = 100;

            while (i < maxIter)
            {
                double x2 = Math.Log(-Math.Pow(2, -x) - 2 * Math.Cos(x) + 6);

                if (Math.Abs(x2 - x) < tol)   // معیار توقف درست
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"Xk : {x2} , i : {i}");
                    Console.ResetColor();
                    return;
                }

                x = x2;
                i++;
            }

            Console.WriteLine("Fixed-point iteration did not converge.");
        }

        static void Newton(double d1, double d2, double a, double b)
        {
            double x = (a + b) / 2;
            int i = 0;
            double tol = Math.Pow(d1, d2);
            int maxIter = 100;

            while (i < maxIter)
            {
                double f = Math.Exp(x) + Math.Pow(2, -x) + 2 * Math.Cos(x) - 6;
                double df = Math.Exp(x) - Math.Log(2) * Math.Pow(2, -x) - 2 * Math.Sin(x);

                if (Math.Abs(df) < 1e-10)
                {
                    Console.WriteLine("Derivative near zero.");
                    return;
                }

                double x2 = x - f / df;

                if (Math.Abs(x2 - x) < tol)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"Xk : {x2} , i : {i}");
                    Console.ResetColor();
                    return;
                }

                x = x2;
                i++;
            }

            Console.WriteLine("Newton-Raphson did not converge.");
        }
    }
}
