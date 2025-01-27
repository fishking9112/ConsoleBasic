using System.Collections.Specialized;

namespace ConsoleApp_Homework5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //2-5 숫자 맞추기
            //컴퓨터는 1에서 100까지의 숫자를 임의로 선택하고, 사용자는 이 숫자를 맞추어야 합니다.
            //사용자가 입력한 숫자가 컴퓨터의 숫자보다 크면 "너무 큽니다!"라고 알려주고, 작으면 "너무 작습니다!"라고 알려줍니다.
            //사용자가 숫자를 맞추면 게임이 종료됩니다.

            //난수 생성
            Random  random = new Random();
            int randomNumber = random.Next(1, 101);

            //도전 횟수
            int tryCount = 0;

            //입력
            int inputNum;
            bool isNum = false;

            //시작
            Console.WriteLine("숫자 맞추기 게임을 시작합니다 !");
            Console.WriteLine("1 부터 100까지의 숫자 중 하나를 맞춰보세요 !");
            Console.WriteLine("============================================");

            while (true)
            {
                Console.Write("숫자를 입력 하세요 : ");
                isNum = int.TryParse(Console.ReadLine() , out inputNum);

                //카운트 올려주기
                tryCount++;

                //예외처리 - 숫자만 입력가능
                if (isNum == false)
                {
                    Console.WriteLine("숫자만 입력해주세요 !!");
                    continue;
                }
                //예외처리 - 1 ~ 100 사이의 숫자만 입력 가능.
                if(inputNum > 100 || inputNum < 0)
                {
                    Console.WriteLine("숫자만 입력해주세요 !!");
                    continue;
                }

                //비교
                if (randomNumber > inputNum)
                {
                    Console.WriteLine("너무 작습니다 !");
                }
                else if (randomNumber < inputNum)
                {
                    Console.WriteLine("너무 큽니다 !");
                }


                //정답 처리                
                if(randomNumber == inputNum)
                {
                    string strMessage = $"축하합니다 ! {tryCount}번 만에 정답을 맞추셨습니다 !";
                    Console.WriteLine(strMessage);
                    break;
                }
            }
        }
    }
}
