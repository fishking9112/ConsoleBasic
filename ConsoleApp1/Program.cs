//namespace Helloworld
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Hello, World!");
//        }
//    }
//}

namespace StringTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str1 = "Hello, World !";
            string str2 = new string('h', 5);

            string str3 = str1 + " " + str2;
            
            Console.WriteLine(str3);

            string[] str4 = str1.Split(',');

            Console.WriteLine(str4[0]);
            Console.WriteLine(str4[1]);

            int index = str1.IndexOf("World");
            Console.WriteLine(index);

            string newStr = str1.Replace("World", "Universe");
            Console.WriteLine(newStr);
            Console.WriteLine(str1);

            string str5 = "123";
            int num = int.Parse(str5);
            Console.WriteLine(num);

            num += 10;

            Console.WriteLine(num.ToString());
            Console.WriteLine(str1 == str2);
            Console.WriteLine(string.Compare(str1, str2));

            string name = "John";
            int age = 30;
            string message = string.Format("My name is {0} and I'm {1} years old." , name, age);

            Console.WriteLine(message);
            message = $"My name is {name} and I'm {age} years old.";
            Console.WriteLine(message);
        }
    }
}