namespace ConsoleApp2
{
    class Person
    {
        private string name = "HuckP";
        private int age;

        //프로퍼티로 인한 접근제한자 설정
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set 
            { 
                if(value > 0)
                    age = value;
            }
        }



        //자동 접근 제한자 는 변수이면서 프로퍼티이다.
        public string Name_1 { get; set; } = "Name : HuckP";
        public string Name_1_1 { get; set; }
        private string Name_2 { get; set; } = "private Name : HuckP_2";   // Private 못가져옴 get set 이 필요없음

        //하려면
        private string name_2_1 = "private Name : HuckP_2_1";
        public string Name_2_1
        {
            get { return name_2_1; }
            private set { name_2_1 = value; }
        }
        
        // 불가능
        // private string Name { public get;  set; } = "HuckP";
        

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();

            Console.WriteLine(person.Name);
            Console.WriteLine(person.Name_1);
            person.Name_1_1 = "HuckP";
            Console.WriteLine(person.Name_1_1);
            //Console.WriteLine(person.Name_2);
            Console.WriteLine(person.Name_2_1);
            person.Age = -10;
            Console.WriteLine(person.Age);
        }
    }

}
