using System;

namespace ConsoleApp_Homework6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //틱택토 게임 만들기
            char[] arr = { '0' , '1', '2', '3', '4', '5', '6', '7', '8'};
            bool Player_1_Turn = true;
            int selectNum;

            while(true)
            {
                //보드 그리기
                RenderBorad(arr);

                //셀 선택 검사
                selectNum = SelectSell(arr, Player_1_Turn);

                //셀에 값 넣어주기
                arr[selectNum] = Player_1_Turn ? 'O' : 'X';

                //승리 조건 검사
                if (CheckWin(arr) == 1)
                {
                    //화면 갱신 후 마무리
                    RenderBorad(arr);
                    Console.WriteLine("\n\t !! Player {0} 이(가) 승리 !!\n", Player_1_Turn ? 1 : 2);
                    break;
                }
                else if(CheckWin(arr) == -1)    // 무승부
                {
                    //화면 갱신 후 마무리
                    RenderBorad(arr);
                    Console.WriteLine("\n\t !! 무 승 부 !!\n");
                    break;
                }

                // 턴 넘기기
                // true 일땐 false , false 일땐 true 로 바꿔준다.
                Player_1_Turn = !Player_1_Turn;
            }
        }

        static void RenderBorad(char[] _arr)
        {
            Console.Clear();
            Console.WriteLine("Tic Tac Toe Game !");
            Console.WriteLine("플레이어 1 : O , 플레이어 2 : X\n");

            Console.WriteLine("=================================================\n");

            Console.WriteLine("\t\t|\t\t|\t\t");
            Console.WriteLine($"\t{_arr[0]}\t|\t{_arr[1]}\t|\t{_arr[2]}\t");
            Console.WriteLine("\t\t|\t\t|\t\t");
            Console.WriteLine("----------------|---------------|----------------");
            Console.WriteLine("\t\t|\t\t|\t\t");
            Console.WriteLine($"\t{_arr[3]}\t|\t{_arr[4]}\t|\t{_arr[5]}\t");
            Console.WriteLine("\t\t|\t\t|\t\t");
            Console.WriteLine("----------------|---------------|----------------");
            Console.WriteLine("\t\t|\t\t|\t\t");
            Console.WriteLine($"\t{_arr[6]}\t|\t{_arr[7]}\t|\t{_arr[8]}\t");
            Console.WriteLine("\t\t|\t\t|\t\t");

            Console.WriteLine("\n=================================================");
        }
        static int SelectSell(char[] _arr, bool _Turn)
        {
            while(true)
            {
                int selectNum;
                bool isNum;
                if (_Turn)
                {
                    Console.WriteLine(" 플레이어 1 의 차례 ! ");
                }
                else
                {
                    Console.WriteLine(" 플레이어 2 의 차례 ! ");
                }

                Console.Write("숫자를 입력 하세요 : ");
                isNum = int.TryParse(Console.ReadLine(), out selectNum);

                //예외처리 - 숫자만 입력가능
                if (isNum == false)
                {
                    Console.WriteLine("숫자만 입력해주세요 !!");
                    continue;
                }
                //예외처리 - 0 ~ 9 사이의 숫자만 입력 가능.
                if (selectNum > 9 || selectNum < 0)
                {
                    Console.WriteLine("숫자만 입력해주세요 !!");
                    continue;
                }


                //중복검사
                if (_arr[selectNum] != 'O' && _arr[selectNum] != 'X')  // 비어있는 경우
                {
                    // Turn 에 맞춰 플래그 넣어준다 ,
                    // 1 : Player 1 // 2 : Player 2
                    _arr[selectNum] = _Turn ? 'O' : 'X';
                }
                else
                {
                    // 이미 플레이어 x 의 플래그가 있는경우
                    Console.WriteLine(" 이미 Player {0} 이(가) 선택한 칸 입니다 !" , _Turn ? 1 : 2);
                    continue;
                }

                //턴 넘기기
                //_Turn = !_Turn; // true 일땐 false , false 일땐 true 로 바꿔준다.
                if (_Turn)
                    _Turn = false;
                else
                    _Turn = true;

                return selectNum;
            }
        }

        static int CheckWin(char[] _arr)
        {
            //승리조건 .. 노가다 예상됨

            /*
             * 승리 조건
             * 가로
             * 0 1 2
             * 3 4 5
             * 6 7 8
             * 세로
             * 0 3 6
             * 1 4 7
             * 2 5 8
             * 대각선
             * 0 4 8
             * 2 4 6
             */

            //가로
            if (_arr[0] == _arr[1] && _arr[1] == _arr[2])
                return 1;
            if (_arr[3] == _arr[4] && _arr[4] == _arr[5])
                return 1;
            if (_arr[6] == _arr[7] && _arr[7] == _arr[8])
                return 1;
            //세로
            if (_arr[0] == _arr[3] && _arr[3] == _arr[6])
                return 1;
            if (_arr[1] == _arr[4] && _arr[4] == _arr[7])
                return 1;
            if (_arr[2] == _arr[5] && _arr[5] == _arr[8])
                return 1;
            //세로
            if (_arr[0] == _arr[4] && _arr[4] == _arr[8])
                return 1;
            if (_arr[2] == _arr[4] && _arr[4] == _arr[6])
                return 1;


            // 무승부
            if (_arr[0] != '0' && _arr[1] != '1' && _arr[2] != '2' && _arr[3] != '3' && _arr[4] != '4' && _arr[5] != '5' && _arr[6] != '6' && _arr[7] != '7' && _arr[8] != '8')
                return -1;

            return 0;
        }

    }
}
