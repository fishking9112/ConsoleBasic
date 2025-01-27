namespace ConsoleApp_Homework2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //과제 2
            //두 수를 입력 받고 사칙연산의 결과를 출력하세요.
            Console.Write("첫번쨰 수를 입력하세요. : ");
            int inputNum_1 = int.Parse(Console.ReadLine());
            Console.Write("두번쨰 수를 입력하세요. : ");
            int inputNum_2 = int.Parse(Console.ReadLine());

            Console.WriteLine("더하기  : " + (inputNum_1 + inputNum_2));
            Console.WriteLine("빼기   : " + (inputNum_1 - inputNum_2));
            Console.WriteLine("곱하기  : " + (inputNum_1 * inputNum_2));
            Console.WriteLine("나누기 :   " +  (inputNum_1 / inputNum_2));
        }
    }
}
