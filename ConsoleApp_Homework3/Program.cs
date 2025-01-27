namespace ConsoleApp_Homework3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //과제 3. 섭씨온도를 입력받아 , 화씨온도로 변환하는 프로그램을 만들어봅시다.
            //공식은 화찌 = (섭씨 *  9/5) + 32 입니다.

            Console.Write(" 섭씨 온도를 입력해주세요 : ");
            float celsius = float.Parse( Console.ReadLine() );

            float fahrenheit = (celsius * 9 / 5) + 32;

            string message = $" 섭씨 {celsius}'C 는 화씨 {fahrenheit}'F 입니다 .";

            Console.WriteLine(message);
        }
    }
}
