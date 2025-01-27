using System;
namespace ConsoleApp_Homework1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //과제 1
            //이름과 나이를 입력 받고 출력하는 코드를 작성하세요.

            Console.Write(" 이름 : ");
            string inputName = Console.ReadLine();
            Console.Write(" 나이 : ");
            int inputAge = int.Parse(Console.ReadLine());

            string message = $"당신의 이름은 {inputName} , 나이는 {inputAge} 살 입니다. ";
            Console.WriteLine(message);
        }
    }
}
