using System;

namespace dll
{
    public static class Class1
    {
        public static int Add(int num1, int num2)
        {
            return num1 + num2;
        }

        public static double Add(int num1, double num2)
        {
            return  Convert.ToDouble(num1) + num2;
        }

        public static double Add(double num1, int num2)
        {
            return num1 + Convert.ToDouble(num2);
           
        }

        public static double Add(double num1, double num2)
        {
            return num1 + num2;
        }
        
        public static int Minus(int num1, int num2)
        {
            return num1 - num2;
        }

        public static double Minus(int num1, double num2)
        {
            return  Convert.ToDouble(num1) -num2;
        }

        public static double Minus(double num1, int num2)
        {
            return num1 - Convert.ToDouble(num2);
           
        }

        public static double Minus(double num1, double num2)
        {
            return num1 - num2;
        }
        
        public static int Multiple(int num1, int num2)
        {
            return num1 * num2;
        }

        public static double Multiple(int num1, double num2)
        {
            return  Convert.ToDouble(num1) * num2;
        }

        public static double Multiple(double num1, int num2)
        {
            return num1 * Convert.ToDouble(num2);
           
        }

        public static double Multiple(double num1, double num2)
        {
            return num1 * num2;
        }
        
        public static int Division(int num1, int num2)
        {
            return num1 / num2;
        }

        public static double Division(int num1, double num2)
        {
            return  Convert.ToDouble(num1) / num2;
        }

        public static double Division(double num1, int num2)
        {
            return num1 / Convert.ToDouble(num2);
           
        }

        public static double Division(double num1, double num2)
        {
            return num1 / num2;
        }
    }
}