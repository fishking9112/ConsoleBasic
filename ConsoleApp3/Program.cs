namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 1; int y = 2;
            swap(x, y);
            Console.WriteLine($"{x}, {y}");
        }

        static void swap(int a , int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
    }
}
