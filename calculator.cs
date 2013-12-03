using System;
using System.Collections.Generic;
using System.Threading;

namespace ForTestPurposeMutawer
{
    class Program
    {
        private static int PGCD(int a, int b)
        {
            if (a < 0)
                a = -a;
            if (b < 0)
                b = -b;
            while (a != b)
            {
                if (a > b)
                    a = a - b;
                if (b > a)
                    b = b - a;
            }
            return b;
        }
        private static long factorial(int X)
        {
            long F = 1;
            for (int i = 1; i <= X; i++)
                F = F * i;
            return F;
        }
        private static long nPr(int n, int r)
        {
            return factorial(n) / factorial(n - r);
        }
        private static long nCr(int n, int r)
        {
            return nPr(n, r) * 1 / factorial(r);
        }

        private static string CalcFn(string line, string fn)
        {
            switch (fn)
            {
                case "cos":
                    return Math.Cos(Double.Parse(Calculate(line))).ToString();
                case "sin":
                    return Math.Sin(Double.Parse(Calculate(line))).ToString();
                case "tang":
                    return Math.Tan(Double.Parse(Calculate(line))).ToString();
                case "cosh":
                    return Math.Cosh(Double.Parse(Calculate(line))).ToString();
                case "sinh":
                    return Math.Sinh(Double.Parse(Calculate(line))).ToString();
                case "tangh":
                    return Math.Tanh(Double.Parse(Calculate(line))).ToString();
                case "cos-1":
                    return Math.Pow(Math.Cos(Double.Parse(Calculate(line))), -1).ToString();
                case "sin-1":
                    return Math.Pow(Math.Sin(Double.Parse(Calculate(line))), -1).ToString();
                case "tang-1":
                    return Math.Pow(Math.Tan(Double.Parse(Calculate(line))), -1).ToString();
                case "log":
                    return Math.Log10(Double.Parse(line)).ToString();
                case "ln":
                    return Math.Log(Double.Parse(line)).ToString();
                case "√":
                    return Math.Sqrt(Double.Parse(line)).ToString();
                default:
                    return "ERROR";
            }
        }
        private static string FuncCalcer(string line, string fn)
        {
            int counter, to, index;
            string str = line;
            index = line.IndexOf(fn) + fn.Length;
            to = index + 1;
            counter = 1;
            while (counter > 0)
            {
                if (line[to] == '(')
                    counter++;
                if (line[to] == ')')
                    counter--;
                to++;
            }
            index++;
            to--;
            str = Calculate(line.Substring(index, to - index));
            int from = index - fn.Length - 1, until = to + fn.Length - index + 2;
            line = line.Remove(from, until);
            line = line.Insert(from, CalcFn(str, fn));
            return line;
        }
        private static int singleOprPos(string line)
        {
            List<char> oprs = new List<char>("!²³".ToCharArray());
            int index = 1;
            while (!oprs.Contains(line[index]) && index < line.Length - 1)
            {
                index++;
            }
            return index--;
        }
        private static string SingleOprCalcerLeft(string line)
        {
            int x = 0, i = singleOprPos(line);
            string str = "";
            if (line[i - 1] == ')')
            {
                x = i - 2;
                int count = 1;
                while (count > 0)
                {
                    if (line[x] == ')')
                        count++;
                    if (line[x] == '(')
                        count--;
                    x--;
                }
                x++;
            }
            else
            {
                List<char> oprs = new List<char>("-×/C^P+!²³".ToCharArray());
                x = i - 1;
                while (!oprs.Contains(line[x]) && x > 0)
                {
                    x--;
                }
                if ((x > 0) && (line[x] != '-'))
                    x++;
            }
            switch (line[i])
            {
                case '!':
                    str = factorial(Int32.Parse(Calculate(line.Substring(x, i - x)))).ToString();
                    break;
                case '²':
                    str = Math.Pow(Double.Parse(Calculate(line.Substring(x, i - x))), 2).ToString();
                    break;
                case '³':
                    str = Math.Pow(Double.Parse(Calculate(line.Substring(x, i - x))), 3).ToString();
                    break;
            }
            line = line.Remove(x, i - x + 1);
            line = line.Insert(x, str);
            return line;
        }
        private static string FuncParser(string line)
        {
            while (line.Contains("!") || line.Contains("²") || line.Contains("³"))
            {
                line = SingleOprCalcerLeft(line);
            }
            while (line.Contains("√"))
            {
                line = FuncCalcer(line, "√");
            }
            while (line.Contains("cos-1"))
            {
                line = FuncCalcer(line, "cos-1");
            }
            while (line.Contains("sin-1"))
            {
                line = FuncCalcer(line, "sin-1");
            }
            while (line.Contains("tang-1"))
            {
                line = FuncCalcer(line, "tang-1");
            }
            while (line.Contains("cosh"))
            {
                line = FuncCalcer(line, "cosh");
            }
            while (line.Contains("sinh"))
            {
                line = FuncCalcer(line, "sinh");
            }
            while (line.Contains("tangh"))
            {
                line = FuncCalcer(line, "tangh");
            }
            while (line.Contains("cos"))
            {
                line = FuncCalcer(line, "cos");
            }
            while (line.Contains("sin"))
            {
                line = FuncCalcer(line, "sin");
            }
            while (line.Contains("tang"))
            {
                line = FuncCalcer(line, "tang");
            }
            while (line.Contains("log"))
            {
                line = FuncCalcer(line, "log");
            }
            while (line.Contains("ln"))
            {
                line = FuncCalcer(line, "ln");
            }
            return line;
        }

        private static string getResult(string line)
        {
            char[] list = { '+', '-', '×', '/', 'C', 'P', '^' };
            List<char> sym = new List<char>(list);
            string res, left, right = left = "0";
            int i, j;
            for (i = 1; i < line.Length; i++)
                if (sym.Contains(line[i]))
                {
                    break;
                }
            for (j = 0; j < i; j++)
                left = left + line[j];
            for (j = i + 1; j < line.Length; j++)
                right = right + line[j];
            if (left[1] == '-')
                left = left.Substring(1);
            res = "";
            switch (line[i])
            {
                case '+':
                    res = (Double.Parse(left) + Double.Parse(right)).ToString();
                    break;
                case '-':
                    res = (Double.Parse(left) - Double.Parse(right)).ToString();
                    break;
                case '×':
                    res = (Double.Parse(left) * Double.Parse(right)).ToString();
                    break;
                case '/':
                    res = (Double.Parse(left) / Double.Parse(right)).ToString();
                    break;
                case 'C':
                    res = nCr(Int32.Parse(left), Int32.Parse(right)).ToString();
                    break;
                case 'P':
                    res = nPr(Int32.Parse(left), Int32.Parse(right)).ToString();
                    break;
                case '^':
                    res = Math.Pow(Double.Parse(left), double.Parse(right)).ToString();
                    break;
            }
            if (res != "0")
                return res;
            else
                return "0";
        }

        private static int getFirstOpr(string line, int stage)
        {
            List<char> oprs1 = new List<char>("×/C^P".ToCharArray());
            List<char> oprs2 = new List<char>("+-".ToCharArray());
            int index = 1;
            switch (stage)
            {
                case 2:
                    while (!oprs1.Contains(line[index]))
                    {
                        index++;
                    }
                    break;
                case 3:
                    while (!oprs2.Contains(line[index]))
                    {
                        index++;
                    }
                    break;
            }
            return index--;
        }

        private static string stage1(string line)
        {
            int counter, to, index = line.IndexOf("(");
            string str = line.Substring(index);
            index = str.IndexOf("(");
            to = index + 1;
            counter = 1;

            while (counter > 0)
            {
                if (str[to] == '(')
                    counter++;
                if (str[to] == ')')
                    counter--;
                to++;
            }

            index++;

            str = str.Substring(1, to - index - 1);
            index = line.IndexOf("(");
            line = line.Remove(index, to);
            line = line.Insert(index, Calculate(str));

            return line;
        }
        private static string stage2(string line)
        {
            List<char> oprs = new List<char>("×+-/CP^".ToCharArray());
            string str;
            int index, to;

            index = getFirstOpr(line, 2);

            to = index + 1;
            index = index - 1;

            while (to < line.Length)
            {
                if (oprs.Contains(line[to]))
                    break;
                to++;
            }

            while (index > 0)
            {
                if (oprs.Contains(line[index]))
                    break;
                index--;
            }

            if (oprs.Contains(line[index]))
                index++;

            if (index > 0)
                to -= index;
            else
                index = 0;

            str = getResult(line.Substring(index, to));
            line = line.Remove(index, to);
            line = line.Insert(index, str);

            return line;
        }
        private static string stage3(string line)
        {
            List<char> oprs = new List<char>("+-".ToCharArray());
            string str;
            int index, to;

            index = getFirstOpr(line, 3);

            to = index + 1;
            index = index - 1;

            while (to < line.Length)
            {
                if (oprs.Contains(line[to]))
                    break;
                to++;
            }

            while (index > 0)
            {
                if (oprs.Contains(line[index]))
                    break;
                index--;
            }

            if (index > 0)
                to -= index;
            else
                index = 0;

            str = getResult(line.Substring(index, to));
            line = line.Remove(index, to);
            line = line.Insert(index, str);

            return line;
        }

        static string Calculate(string line)
        {
            line = line.Replace(" ", "");
            line = line.Replace("*", "×");
            line = line.Replace("Pi", Math.PI.ToString());

            line = FuncParser(line);

            while (line.Contains("("))
            {
                line = stage1(line);
            }

            while (line.Contains("×") || line.Contains("/") || line.Contains("^") || line.Contains("C") || line.Contains("P"))
            {
                line = stage2(line);
            }

            while (line.Contains("+") || line.Contains("-"))
            {
                if (line.IndexOf('-') == 0 && line.IndexOf('-', 1) < 0 && line.IndexOf('+', 1) < 0)
                        break;
                line = stage3(line);
            }

            return line;
        }

        static void Main(string[] args)
        {
            string line, ans;
            do
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(">");
                Console.ForegroundColor = ConsoleColor.White;
                line = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (line == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Leaving Now...");
                    Thread.Sleep(1000);
                }
                else
                {
                    try
                    {
                        ans = Calculate(line);
                        Console.WriteLine("ANSWER = " + ans);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR");
                    }
                }
            } while (true && line.ToUpper() != "EXIT");
        }
    }
}
