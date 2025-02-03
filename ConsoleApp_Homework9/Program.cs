namespace ConsoleApp_Homework9
{
    ///// 캐릭터

    //인터페이스
    
    public interface ICharacter
    {
        //캐릭터 필드

        // 이름
        string Name { get; }
        // 체력
        int Health { get; set; }
        int MaxHealth { get; set; }
        // 공격력
        int AttMin {  get; }
        int AttMax { get; }
        int Attack { get; } // 넘겨줄 최종 공격력

        // 상태
        bool IsDead { get; }


        //캐릭터가 데미지를 받아 체력이 감소
        void TakeDamage(int _damage);
    }
    // 워리어 ( 플레이어 ) 클래스
    public class Warrior : ICharacter
    {
        public string Name { get; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int AttMin { get; set; }
        public int AttMax { get; set; }

        //람다 실습
        public bool IsDead => Health <= 0;

        public int Attack => new Random().Next(AttMin, AttMax);

        public Warrior( string _name , int _MaxHealth , int _AttMin , int _AttMax)
        {
            // 필드 초기화
            Name = _name;
            Health = _MaxHealth;
            MaxHealth = _MaxHealth;
            AttMin = _AttMin;
            AttMax = _AttMax;
        }

        public void TakeDamage(int _damage)
        {
            //데미지 계산
            Health -= _damage;
            if (IsDead)
            {
                Console.WriteLine("{0}이(가) 죽었습니다 ㅠㅠ.", Name);
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine($"{Name}이(가) {_damage}의 데미지를 받았습니다. HP : {Health}");
            }
        }
    }

    // 몬스터 클래스
    public class Monster : ICharacter
    {

        public string Name { get; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int AttMin { get; set; }
        public int AttMax { get; set; }

        //람다 실습
        public bool IsDead => Health <= 0;

        public int Attack => new Random().Next(AttMin, AttMax);

        // enum 실습
        public enum MonsterType { Monster_Nomal , Monster_Boss};
        public MonsterType Type { get; set; }

        public Monster(string _name, int _MaxHealth, int _AttMin, int _AttMax , MonsterType _Type)
        {
            // 필드 초기화
            Name = _name;
            Health = _MaxHealth;
            MaxHealth = _MaxHealth;
            AttMin = _AttMin;
            AttMax = _AttMax;
            Type = _Type;
        }

        public void TakeDamage(int _damage)
        {
            //데미지 계산
            Health -= _damage;
            if (IsDead)
            {
                Console.WriteLine("{0}을(를) 물리쳤다 !!.", Name);
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine($"{Name}이(가) {_damage}의 데미지를 받았습니다. HP : {Health}");
            }
        }
    }

    // 몬스터 - 고블린
    public class Goblin : Monster
    {
        // Monster의 생성자를 이용한 초기화
        public Goblin(string _name, int _MaxHealth, int _AttMin, int _AttMax , MonsterType _type) : base(_name, _MaxHealth, _AttMin, _AttMax , _type) { }
    }
    // 몬스터 - 드래곤
    public class Dragon : Monster
    {
        // Monster의 생성자를 이용한 초기화
        public Dragon(string _name, int _MaxHealth, int _AttMin, int _AttMax, MonsterType _type) : base(_name, _MaxHealth, _AttMin, _AttMax, _type) { }
    }

    // 아이템
    // 아이템 인터페이스

    public interface IItem
    {
        string Name { get; }
        string Description { get; }
        void Use(Warrior _warrior); // 아이템은 플레이어만 사용한다
    }
    // 아이템 - 체력포션
    public class HealthPotion : IItem
    {
        public string Name { get; } = "체력 포션";
        public string Description { get; } = " 체력이 50 증가합니다.";
        public void Use(Warrior _warrior)
        {
            Console.WriteLine($"{Name} 을 사용합니다 . {Description}");
            _warrior.Health += 50;
            if(_warrior.Health > 100)
            {
                _warrior.Health = 100;
            }
        }
    }

    // 아이템 - 공격력 증가 포션
    public class StrengthPotion : IItem
    {
        public string Name { get; } = "공격력 포션";
        public string Description { get; } = " 공격력이 10 증가합니다.";
        public void Use(Warrior _warrior)
        {
            Console.WriteLine($"{Name} 을 사용합니다 . {Description}");
            _warrior.AttMin += 10;
            _warrior.AttMax += 10;
        }
    }

    // 스테이지
    public class Stage
    {
        private ICharacter warrior;
        private ICharacter monster;
        private List<IItem> rewards;

        //이벤트 델리게이트 실습
        public delegate void GameEvent(ICharacter _character);

        public event GameEvent OnCharacterDeath; // 캐릭터가 죽었을 때

        public Stage(ICharacter _warrior , ICharacter monster , List<IItem> rewards)
        {
            this.warrior = _warrior;
            this.monster = monster;
            this.rewards = rewards;
            OnCharacterDeath += StageClear;
        }

        private void Render_State()
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine($"{warrior.Name} \t\t {monster.Name}");
            Console.WriteLine($"HP : {warrior.Health} / {warrior.MaxHealth} \t\t HP : {monster.Health} / {monster.MaxHealth}");
            Console.WriteLine($"Att : {warrior.AttMin} ~ {warrior.AttMax} \t\t Att : {monster.AttMin} ~ {monster.AttMax}");
            Console.WriteLine("==========================================");
        }
        // 스테이지 시작
        public void Start()
        {
            // 플레이어와 몬스터가 교대로 턴을 진행
            // 플레이어나 몬스터중 하나가 죽으면 스테이지 종료 , 결과 출력
            // 스테이지가 끝날 때 , 플레이어가 살아있으면 보상중 하나를 선택하여 사용한다


            //정보 출력
            Console.WriteLine($" 스테이지 시작 ! ");
            //전투
            while (true)
            {
                //플레이어 항상 선공
                Render_State();
                Console.WriteLine($"{warrior.Name} 의 공격 ! ");
                monster.TakeDamage(warrior.Attack);
                Console.WriteLine();
                Thread.Sleep(1000);

                if (monster.IsDead == true) break;

                //몬스터 공격
                Render_State();
                Console.WriteLine($"{monster.Name} 의 공격 ! ");
                warrior.TakeDamage(monster.Attack);
                Console.WriteLine();
                Thread.Sleep(1000);
            }

            if(warrior.IsDead == true)
            {
                OnCharacterDeath?.Invoke(warrior);
            }
            else if (monster.IsDead == true)
            {
                OnCharacterDeath?.Invoke(monster);
            }

        }

        private void StageClear(ICharacter character)
        {
            //몬스터가 죽으면 스테이지 클리어
            if(character is Monster)
            {
                Console.WriteLine($" 스테이지 클리어 ! ");

                Monster.MonsterType monsterType = ((Monster)character).Type;

                switch (monsterType)
                {
                    case Monster.MonsterType.Monster_Nomal:
                        Console.WriteLine("보상을 선택 하세요 ! ");

                        Console.WriteLine("==========================================");

                        int i = 0;
                        foreach (var item in rewards)
                        {
                            Console.WriteLine($"{i} : {item.Name}");
                            i++;
                        }

                        Console.WriteLine("==========================================");

                        while (true)
                        {
                            Console.Write("선택 할 아이템 : ");

                            //Nullalbe 형 실습

                            int selectInput = int.Parse(Console.ReadLine());

                            if (selectInput < 0 || selectInput >= rewards.Count())
                            {
                                Console.WriteLine("그 아이템은 없는데요 ? ");
                                Thread.Sleep(2000);
                                continue;
                            }

                            if (rewards[selectInput] != null)
                            {
                                Console.WriteLine($"{rewards[selectInput].Name}을(를) 사용합니다.");
                                rewards[selectInput].Use((Warrior)warrior);
                                Console.WriteLine($"{rewards[selectInput].Description}");
                                Thread.Sleep(2000);

                                break;
                            }
                        }
                        break;
                    case Monster.MonsterType.Monster_Boss:
                        Console.WriteLine(" 보스를 처치하였습니다 !");
                        Thread.Sleep(2000);
                        break;
                }
            }
            //플레이어가 죽으면 게임오버
            else
            {
                Console.WriteLine($" 게임 오버 ㅠㅠ ");
                Thread.Sleep(2000);
            }
        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            //필드 셋팅
            Warrior player = new Warrior("Warrior", 100, 10, 20);

            //보상 아이템 셋팅
            List<IItem> stageClearRewards = new List<IItem> { new HealthPotion(), new StrengthPotion() };


            // 마을
            while(true)
            {
                Console.Clear();
                Console.WriteLine("던전을 선택 ! ");
                Console.WriteLine("1. 일반 던전\t2. 보스 던전");
                int inputSelect = int.Parse(Console.ReadLine());

                switch(inputSelect)
                {
                    case 1:
                        Console.WriteLine(" 일반 던전에 진입합니다 .");
                        Thread.Sleep(1000);
                        //스테이지 셋팅
                        Goblin goblin = new Goblin("Goblin", 50, 5, 10 , Monster.MonsterType.Monster_Nomal);
                        Stage stage_Nomal = new Stage(player, goblin, stageClearRewards);
                        stage_Nomal.Start();
                        break;
                    case 2:
                        Console.WriteLine(" !! 보스 던전에 진입합니다 !!");
                        Thread.Sleep(1000);
                        Dragon dragon = new Dragon("Dragon", 100, 15, 30, Monster.MonsterType.Monster_Boss);
                        Stage stage_Boss = new Stage(player, dragon, stageClearRewards);
                        stage_Boss.Start();
                        break;
                    default:
                        Console.WriteLine("거긴 던전이 없는데요 ??");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }
    }
}
