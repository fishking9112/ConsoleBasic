namespace ConsoleApp4   // Interface 실습
{
    internal class Program
    {
        public interface IMovable
        {
            void Move(int x, int y);

        }

        public class Player : IMovable
        {
            public void Move(int x, int y)
            {
                // 이동 구현
            }
        }
        public class Enemy : IMovable
        {
            public void Move(int x , int y)
            {
                // 이동 구현
            }
        }
        static void Main(string[] args)
        {
            IMovable movableObj1 = new Player();
            IMovable movebleObj2 = new Enemy();

            movableObj1.Move( 1 , 2);
            movebleObj2.Move( 3 , 4);
        }
    }
}
