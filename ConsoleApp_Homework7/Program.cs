using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        // 맵 사이즈
        int mapSize_X = 80;
        int mapSize_Y = 20;

        //점수
        int Score = 0;

        // 뱀의 초기 위치와 방향을 설정하고, 그립니다.
        Point p = new Point(4, 5, '*');
        Snake snake = new Snake(p, 4, Direction.RIGHT);
        
        // 음식의 위치를 무작위로 생성하고, 그립니다.
        FoodCreator foodCreator = new FoodCreator(mapSize_X, mapSize_Y, '$');
        Point food = foodCreator.CreateFood();
        
        // 게임 루프: 이 루프는 게임이 끝날 때까지 계속 실행됩니다.
        while (true)
        {
            Console.Clear();

            //Food
            food.Draw();

            //Snake
            
            // 키입력
            snake.KeyInput();
            // 움직이기
            snake.Move();
            // 음식먹기
            if(snake.Eat(food))
            {
                //음식 새로 만들기
                food = foodCreator.CreateFood();
                Score++;
            }

            // 렌더링(Draw)
            snake.Draw();

            // 맵 표시
            DrawMap(mapSize_X,mapSize_Y);

            // 점수 표시
            DrawScore(mapSize_Y, Score);

            // 벽충돌처리 , 몸통충돌처리
            if ( snake.CheckCollMap(mapSize_X, mapSize_Y) || snake.CheckCollBody())
            {
                //게임오버
                DrawGameOver(mapSize_X, mapSize_Y, Score);
                break;
            }

            Thread.Sleep(100 - Score);

        }
    }
    
    static void DrawMap(int _mapSize_X , int _mapSize_Y)
    {
        for(int i = 0; i <= _mapSize_X; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.Write("=");
            Console.SetCursorPosition(i, _mapSize_Y);
            Console.Write("=");
        }
        for(int i = 1; i < _mapSize_Y; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("|");
            Console.SetCursorPosition(_mapSize_X, i);
            Console.Write("|");
        }
    }
    static void DrawScore(int _mapSize_Y , int _score)
    {
        Console.SetCursorPosition( 5 , _mapSize_Y + 2);
        Console.Write("현재 점수 : {0}", _score);
    }
    static void DrawGameOver(int _mapSize_X, int _mapSize_Y, int _score)
    {
        for(int i = 0; i < 40; i++)
        {
            Console.SetCursorPosition((_mapSize_X / 2) - 20 + i, ((_mapSize_Y / 2) - 4));
            Console.Write("=");
        }

        Console.SetCursorPosition((_mapSize_X / 2) - 10, ((_mapSize_Y / 2) - 1));
        Console.WriteLine(" G A M E   O V E R !! ");
        Console.SetCursorPosition((_mapSize_X / 2) - 5, ((_mapSize_Y / 2) + 1));
        Console.Write("Score : {0}" , _score);

        for (int i = 0; i < 40; i++)
        {
            Console.SetCursorPosition((_mapSize_X / 2) - 20 + i, ((_mapSize_Y / 2) + 4));
            Console.Write("=");
        }

        Console.SetCursorPosition(_mapSize_X, _mapSize_Y);
    }
}


public class Point
{
    public int x { get; set; }
    public int y { get; set; }
    public char sym { get; set; }

    // Point 클래스 생성자
    public Point(int _x, int _y, char _sym)
    {
        x = _x;
        y = _y;
        sym = _sym;
    }

    // 점을 그리는 메서드
    public void Draw()
    {
        Console.SetCursorPosition(x, y);
        Console.Write(sym);
    }

    // 점을 지우는 메서드
    public void Clear()
    {
        sym = ' ';
        Draw();
    }

    // 두 점이 같은지 비교하는 메서드
    public bool IsHit(Point p)
    {
        return p.x == x && p.y == y;
    }
}

//  이 클래스는 뱀의 상태와 이동, 음식 먹기, 자신의 몸에 부딪혔는지 확인 등의 기능을 담당합니다.
public class Snake
{
    private List<Point> SnakeBodys = new List<Point>();

    private int length;
    private Direction dir;

    //생성자
    public Snake(Point _p, int _length, Direction _Dir)
    {
        this.length = _length;
        this.dir = _Dir;

        //초기 위치 셋팅
        for (int i = 0; i < _length; i++)
        {
            Point p = new Point(_p.x, _p.y, _p.sym);
            SnakeBodys.Add(p);
            _p.x += 1;
        }
    }

    public void Draw()
    {
        foreach (Point pt in SnakeBodys)
        {
            Console.SetCursorPosition(pt.x, pt.y);
            Console.Write("*");
        }
    }

    //Move 함수
    public void Move()
    {
        // 맨 뒤을 지우고 , 맨 앞에 놓는 식으로 움직이자.
        // -> 이중연결 리스트가 아니라 AddFront 할 수 없으니 구조 변경.
        // 뒤에서 추가해야한다 .. 앞뒤 바꿔서 생각해서 다시 만들자

        // SnakeBody
        // 뒤 [Head][Body][Body][Tail] 앞

        //뱀의 맨 꼬리 지우기
        Point Tail = SnakeBodys.First();
        SnakeBodys.Remove(Tail);
        Tail.Clear();

        //머리 앞에 한칸 놓기
        Point Head = NextStep();
        SnakeBodys.Add(Head);


    }

    //음식먹기
    public bool Eat(Point _food)
    {
        Point Head = NextStep();

        //머리로 음식 먹으면
        if(Head.IsHit(_food))
        {
            //꼬리쪽 한칸 추가
            _food.sym = Head.sym;
            SnakeBodys.Add(_food);
            length += 1;

            return true;
        }

        return false;

    }

    //다음 머리가 있을 곳 리턴하는 함수
    private Point NextStep()
    {
        Point Head = SnakeBodys.Last();

        Point nextPoint = new Point(Head.x, Head.y, Head.sym);

        switch (dir)
        {
            case Direction.LEFT:
                nextPoint.x -= 1;
                break;
            case Direction.RIGHT:
                nextPoint.x += 1;
                break;
            case Direction.UP:
                nextPoint.y -= 1;
                break;
            case Direction.DOWN:
                nextPoint.y += 1;
                break;
        }

        return nextPoint;
    }

    //키 입력
    public void KeyInput()
    {
        if (Console.KeyAvailable)
        {
            //키입력
            var key = Console.ReadKey(true).Key;
            //ConsoleKey 로 리턴받는거 var로 받아봄 !

            //역방향 키입력 불가
            
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if(dir != Direction.RIGHT) dir = Direction.LEFT;
                    break;
                case ConsoleKey.RightArrow:
                    if(dir != Direction.LEFT) dir = Direction.RIGHT;
                    break;
                case ConsoleKey.UpArrow:
                    if(dir != Direction.DOWN) dir = Direction.UP;
                    break;
                case ConsoleKey.DownArrow:
                    if(dir != Direction.UP) dir = Direction.DOWN;
                    break;
            }
        }
    }

    //머리가 벽에 닿는지 체크
    public bool CheckCollMap(int _mapSize_X , int _mapSize_Y)
    {
        Point head = SnakeBodys.Last();

        if (head.x <= 0 || head.x >= _mapSize_X) return true;
        if(head.y <= 0 || head.y >= _mapSize_Y) return true;

        return false;
    }

    //머리가 몸에 닿는지 체크
    public bool CheckCollBody()
    {
        Point head = SnakeBodys.Last();

        //머리 빼고 검사 -1;
        for(int i = 0; i < SnakeBodys.Count - 1; i++)
        {
            if (head.IsHit(SnakeBodys[i]))
                return true;
        }

        return false;
    }
}
//이 클래스는 맵의 크기 내에서 무작위 위치에 음식을 생성하는 역할을 합니다.
public class FoodCreator
{
    private int size_X;
    private int size_Y;
    private char shape;

    //랜덤
    Random rand = new Random();

    public FoodCreator(int _size_X, int _size_Y , char _shape)
    {
        this.size_X = _size_X;
        this.size_Y = _size_Y;
        this.shape = _shape;
    }

    public Point CreateFood()
    {
        //랜덤값으로 음식 만들기
        int x = rand.Next(1 , size_X);
        int y = rand.Next(1 , size_Y);

        Point p = new Point(x , y, shape);
        return p;
    }
}

// 방향을 표현하는 열거형입니다.
public enum Direction
{
    LEFT,
    RIGHT,
    UP,
    DOWN
}