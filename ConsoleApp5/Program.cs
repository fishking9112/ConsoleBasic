namespace ConsoleApp5 // interface 실습 _ 2
{
    internal class Program
    {
        //인터페이스 생성
        public interface IUsable
        {
            void Use();
        }

        //인터페이스를 사용한 클래스(Item) 생성
        public class Item : IUsable
        {
            public string Name { get; set; }

            public void Use()
            {
                Console.WriteLine("아이템 {0} 을 사용했습니다." , Name);
            }
        }
        //인터페이스를 사용한 클래스(Food) 생성
        public class Food : IUsable
        {
            public string Name { get; set; }

            public void Use()
            {
                Console.WriteLine("음식 {0} 를 먹었습니다." , Name);
            }
        }

        public class Player
        {
            //인터페이스를 사용한 Item , Food 사용
            public void UseItem(IUsable item)
            {
                item.Use();
            }
        }


        static void Main(string[] args)
        {
            Player player = new Player();
            Item item = new Item() {Name = "Health Postion" };
            Item item_2 = new Item() { Name = "Mana Postion" };
            Food food = new Food() { Name = "Bob" };
            player.UseItem(item);
            player.UseItem(item_2);
            player.UseItem(food);
        }
    }
}
