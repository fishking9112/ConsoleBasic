namespace ConsoleApp5 // interface 실습 _ 2
{
    internal class Program
    {
        public interface IUsable
        {
            void Use();
        }

        public class Item : IUsable
        {
            public string Name { get; set; }

            public void Use()
            {
                Console.WriteLine("아이템 {0} 을 사용했습니다." , Name);
            }
        }

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
