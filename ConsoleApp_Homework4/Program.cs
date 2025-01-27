namespace ConsoleApp_Homework4
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //과제 4. 사용자로부터 키(m)와 체중(kg)를 입력받아, BMI 지수를 계산하는 프로그램을 만들어봅시다.
            //공식은 BMI = 체중(kg) / 키(m)^2 입니다.

            Console.Write("키 를 입력하세요 : ");
            float height = float.Parse( Console.ReadLine() );

            Console.Write("몸무게를 입력하세요 : ");
            float weight = float.Parse( Console.ReadLine() );

            float bmi = weight / (height * height);

            Console.WriteLine("당신의 bmi 는 " + bmi + "입니다.");
        }
    }
}
