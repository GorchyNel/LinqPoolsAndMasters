using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kurs_OOP
{
    class Pool
    {
        public int KeyP { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string KindOf { get; set; } //спортивный, оздоровительный, комбинированный
        public Pool(int key, string name, string adress, string kind_of)
        {
            KeyP = key;
            Name = name;
            Adress = adress;
            KindOf = kind_of;
        }
    }
    class Master
    {
        private string[] days = {
            "понедельник", "вторник", "среда", "четверг", "пятница", "суббота", "воскресенье"
        };
        private static Dictionary<string, int> DayToNum = new Dictionary<string, int>()
            {
            { "понедельник", 0 },
            { "вторник", 1 },
            { "среда", 2 },
            { "четверг", 3 },
            { "пятница", 4 },
            { "суббота", 5 },
            { "воскресенье", 6 },
        };


        public string[] Days { get; }
        public int KeyM { get; set; }
        public string Name { get; set; }
        public int GrNumber { get; set; }
        public List<string> Shedulle = new List<string>();

        public Master(int key, string name, int gr, uint[] sh)
        {
            KeyM = key;
            Name = name;
            GrNumber = gr;

            //Ввод расписания - 1, 2, ..., 7 (где 1 - пн, 2 - вт и тд)
            for (int i = 0; i < sh.Length; i++)
                if (sh[i] < 7) //базовая проверка (чтобы не было исключений)
                    Shedulle.Add(Days[sh[i] - 1]);
        }

        public int[] ShedulleToNums()
        {
            int[] Sh_Nums = new int[Shedulle.Count];

            int i = 0;
            foreach (string day in Shedulle)
            {
                Sh_Nums[i++] = DayToNum[day];
            }
            return Sh_Nums;
        }

        public string GetShedulleOnStr()
        {
            string ShedulleOnStr = "";

            for (int i = 0; i < Shedulle.Count; i++)
            {
                if (i < Shedulle.Count - 1)
                    ShedulleOnStr += Shedulle[i] + ", ";
                else
                    ShedulleOnStr += Shedulle[i];
            }

            return ShedulleOnStr;
        }
    }
    class Group
    {
        public int KeyG { get; set; }

        public int Number { get; set; }

        //начинающие, подростки, взрослые и спортсмены 
        public string Category { get; set; }

        public int SubNumber { get; set; }
        public Group(int key, int number, string category, int sub_number)
        {
            KeyG = key;
            Number = number;
            Category = category;
            SubNumber = sub_number;
        }
    }

    class Subscription
    {


        public int KeyS { get; set; }
        public int SubNumber { get; set; }
        public string Category { get; set; } //начинающие, подростки, взрослые и спортсмены
        public int TimesOfWeek { get; set; } //1,2,3,5
        public int Price { get; set; }
        public Subscription(int key, int sub_number, string category, int times_of_week, int price)
        {
            KeyS = key;
            SubNumber = sub_number;
            Category = category;
            TimesOfWeek = times_of_week;
            Price = price;
        }
    }

    class Program
    {
        static string[] path = { "Бассейн.txt", "Тренер.txt", "Группа.txt", "Абонемент.txt" };
        static int[] CountOfFields = { 4, 4, 4, 5 };

        static List<Pool> pools = new List<Pool>();
        static List<Master> masters = new List<Master>();
        static List<Group> groups = new List<Group>();
        static List<Subscription> subs = new List<Subscription>();

        static void Load(string path, int NumberOfList, int count)
        {
            StreamReader FileR = new StreamReader(path, Encoding.Default);
            string str;
            while ((str = FileR.ReadLine()) != null)//пока не конец файла
            {
                string[] data = new string[count];
                data = str.Split(';');
                switch (NumberOfList)
                {
                    case 0:
                        pools.Add(new Pool(Convert.ToInt32(data[0]), data[1], data[2], data[3]));
                        break;

                    case 1:
                        string[] sh_str = data[3].Split(',');
                        uint[] sh = new uint[sh_str.Length];

                        for (int i = 0; i < sh.Length; i++)
                            sh[i] = uint.Parse(sh_str[i]);

                        masters.Add(new Master(Convert.ToInt32(data[0]), data[1], Convert.ToInt32(data[2]), sh));
                        break;

                    case 2:
                        groups.Add(new Group(Convert.ToInt32(data[0]),
                            Convert.ToInt32(data[1]), data[2], Convert.ToInt32(data[3])));
                        break;

                    case 3:
                        subs.Add(new Subscription(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]), data[2],
                            Convert.ToInt32(data[3]), Convert.ToInt32(data[4])));
                        break;
                }
            }
        }
        static void Main(string[] args)
        {
            //загрузка данных
            for (int i = 0; i < CountOfFields.Length; i++)
                Load(path[i], i, CountOfFields[i]);
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                char choise = Console.ReadKey().KeyChar;
                switch (choise)
                {
                    case '1': //Добавление группы
                        Console.Clear();

                        int key = 0, number = 0;
                        int sub_number = 0;
                        string category;
                        while (key == 0 || number == 0 || sub_number == 0)
                        {
                            try
                            {
                                if (key == 0)
                                {
                                    Console.WriteLine("Введите ключ группы: ");
                                    key = int.Parse(Console.ReadLine());
                                }

                                if (number == 0)
                                {
                                    Console.WriteLine("Введите номер группы: ");
                                    number = int.Parse(Console.ReadLine());
                                }

                                if (sub_number == 0)
                                {
                                    Console.WriteLine("Введите номер абонемента: ");
                                    sub_number = int.Parse(Console.ReadLine());
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }

                        Console.WriteLine("Введите категорию: ");
                        category = Console.ReadLine();


                        Group kria = new Group(key, number, category, sub_number);//созд объект класса и добавл в лист
                        groups.Add(kria);
                        break;

                    case '2'://Удаление группы
                        Console.Clear();
                        Console.WriteLine("Выберите группу из списка (введите номер группы): ");

                        Console.WriteLine(" {0,-15}{1,-15}{2,-15}", "Номер группы: ", "Номер абонемента", "Категория");
                        foreach (Group gr in groups) 
                        {
                            Console.WriteLine(" {0,-15}{1,-15}{2,-15}", gr.Number, gr.SubNumber, gr.Category);
                        }

                        int num = -1;

                        while (num == -1)
                        {
                            try
                            {
                                bool flag = true; 
                                num = int.Parse(Console.ReadLine());

                                int k = 0;
                                foreach (Group gr in groups)
                                {
                                    if (gr.Number == num)
                                    {
                                        flag = false;

                                        groups.RemoveAt(k);//удаляем элемент

                                        break;
                                    }
                                    k++;
                                }

                                if (flag)
                                    throw new Exception(); //передача упр. на catch

                            }
                            catch
                            {
                                num = -1;
                                Console.WriteLine("Введите номер из списка!\n");
                                continue;
                            }                           
                        }

                        Console.WriteLine("Группа успешно удалена!");
                        Console.ReadKey();
                        break;

                    case '3'://Список тренеров по бассейнам
                        var MastersByPools = pools.GroupJoin(masters, p => p.KeyP, m => m.KeyM,
                            (p, m) => new { p.Name, MastersOnPool = m });

                        //Вывод
                        foreach (var pool in MastersByPools)
                        {
                            Console.WriteLine("  {0}:", pool.Name);
                            foreach (Master ms in pool.MastersOnPool)
                            {
                                Console.WriteLine("\t{0} ({1})", ms.Name, ms.GetShedulleOnStr());
                            }
                        }

                        Console.ReadKey();
                        break;

                    case '4'://Итоговая прибыль каждого тренера в каждом бассейне

                        var GroupPrice = groups.Join(subs, g => g.SubNumber, s => s.SubNumber,
                            (g, s) => new
                            {
                                g.Number,//для связи с тренером
                                Price = s.Price * s.TimesOfWeek //доход тренера в неделю с группы
                            });

                        var MastersPrice = masters.Join(GroupPrice, m => m.GrNumber, gp => gp.Number,
                            (m, gp) => new { m.KeyM, m.Name, gp.Price });

                        var MastersByPool = pools.GroupJoin(MastersPrice, p => p.KeyP, m => m.KeyM,
                               (p, m) => new { p.Name, MastersOnPool = m });

                        //Вывод
                        foreach (var pool in MastersByPool)
                        {
                            Console.WriteLine("  {0}:", pool.Name);
                            foreach (var ms in pool.MastersOnPool)
                            {
                                Console.WriteLine("\t{0} - {1}р в неделю", ms.Name, ms.Price);
                            }
                        }

                        Console.ReadKey();
                        break;

                    case '5'://Тренеры, работающие с начинающими
                        var GroupsByMaster =
                            masters.Join(groups.Where(g => g.Category == "начинающие"), //оставляем группы с начинающими
                                                                                        //оcтальные группы отсеиваем
                            m => m.GrNumber, g => g.Number,
                            (m, g) => new { m.Name, Shedulle = m.GetShedulleOnStr() });

                        //Вывод
                        foreach (var master in GroupsByMaster)
                        {
                            Console.WriteLine("  {0} ({1})", master.Name, master.Shedulle);
                        }

                        Console.ReadKey();
                        break;

                    case '6'://Список посетителей, занимающихся с заданным тренером
                        Console.WriteLine("Выберите тренера из списка (введите номер):");
                        masters = masters.OrderBy(m => m.Name[0]).ToList();//Сортируем тренеров по алфавиту
                        int j = 1;
                        foreach (Master m in masters)
                        {
                            Console.WriteLine("{0},{1}", j++, m.Name);
                        }
                        int NumberChoise = int.Parse(Console.ReadLine()) - 1;

                        try
                        {
                            Console.WriteLine("Тренер: {0}", masters[NumberChoise].Name);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("В списке нет введенного номера!");
                            break;
                        }

                        var ChoisedMaster/*длина равна 1*/ = masters.Where(m => m.KeyM == masters[NumberChoise].KeyM).//получаем выбранного тренера
                            GroupJoin(groups, m => m.GrNumber, g => g.Number, (m, g) =>
                                new { m.Name, Groups = g }
                            );
                        Console.WriteLine("Список групп: ");
                        Console.WriteLine("\tНомер группы\tКатегория");
                        foreach (var grp in (ChoisedMaster.ToArray())[0].Groups)
                            Console.WriteLine("\t{0}\t{1}", grp.Number, grp.Category);
                        Console.ReadKey();
                        break;

                    case '7'://Количество групп в каждом бассейне по дням недели

                        var GroupShedulle = masters.Join(groups, m => m.GrNumber, g => g.Number,
                           (m, g) => new { m.KeyM, g.Number, Shedulle = m.ShedulleToNums() });

                        var GroupPool = pools.GroupJoin(GroupShedulle, p => p.KeyP, g => g.KeyM,
                               (p, g) => new { p.Name, GrsOnPool = g });


                        string[] week = masters[0].Days; //получаем массив дней недели

                        foreach (var pool in GroupPool)
                        {
                            Console.WriteLine("  {0}:", pool.Name);

                            uint[] CountGroupsByDays = new uint[week.Length];

                            //сбор статистики по группам
                            foreach (var grp in pool.GrsOnPool)
                            {
                                for (int i = 0; i < grp.Shedulle.Length; i++)
                                {
                                    CountGroupsByDays[grp.Shedulle[i]]++;
                                }
                            }

                            for (int i = 0; i < week.Length; i++)
                            {
                                Console.WriteLine("\t{0}:\t{1}", week[i], CountGroupsByDays[i]);
                            }
                        }

                        Console.ReadKey();
                        break;

                    case '8'://бассейн с максимальной выручкой
                        var GroupPrice2 = groups.Join(subs, g => g.SubNumber, s => s.SubNumber,
                            (g, s) => new
                            {
                                g.Number,//для связи с тренером
                                Price = s.Price * s.TimesOfWeek //доход тренера в неделю с группы
                            });

                        var MastersPrice2 = masters.Join(GroupPrice2, m => m.GrNumber, gp => gp.Number,
                            (m, gp) => new { m.KeyM, m.Name, gp.Price });

                        var Pools = pools.GroupJoin(MastersPrice2, p => p.KeyP, m => m.KeyM,
                               (p, m) => new { p.Name, Revenue = m.Sum(ms => ms.Price) });

                        int MaxRevenue = Pools.Max(p => p.Revenue);


                        //Вывод
                        Console.WriteLine("\tНазвание бассейна\tВыручка");
                        foreach (var pool in Pools.Where(p => p.Revenue == MaxRevenue))
                        {
                            Console.WriteLine("\t{0}\t{1}", pool.Name, pool.Revenue);
                        }

                        Console.ReadKey();
                        break;

                    case '9'://Выход
                        Console.Clear();
                        do
                        {
                            Console.WriteLine("Вы уверены, что хотите выйти? \n1.Да \n2.Нет");
                            choise = Console.ReadKey().KeyChar;
                        } while (choise != '1' && choise != '2');
                        if (choise == '1') return;
                        break;
                }
            }
        }
    }
}