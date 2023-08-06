using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
namespace GoodSpace
{
    public class Person: IComparable<Person>
    {
        public int Salary { get; private set; }
        public string Position { get; private set; }
        public string Name { get; private set; }
        public string Phone { private set; get; }
    
        public Person(string[] dataArray)
        {
            Name = dataArray[0];
            Position = dataArray[1];
            Salary = Int32.Parse(dataArray[2]);
            Phone = dataArray[3];
        }
        public int CompareTo(Person? person) 
        {
            if (person == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                return Salary - person.Salary;
            }
            
        }
        public override string ToString()
        {
            return $"{Name} {Position} {Salary} {Phone} {base.ToString()}" ;
        }
    }
    
    class Program
    {
        static public void Main()
        {
            int avarageSalary;
            int avarageSum = 0;
            string? s;
            List<Person> personArray = new List<Person>();
            string[] stArray = new string[4];

            using (StreamReader sr = new StreamReader("C:\\Users\\Nickv\\OneDrive\\Documents\\TextFileMD.txt"))
            {
                while ((s = sr.ReadLine()) != null)
                {
                    Person person = new Person(s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                    personArray.Add(person);
                    avarageSum += person.Salary;
                }
            }
            avarageSalary = avarageSum / personArray.Count();

            //Order by salary
            var orderedSalary = from person in personArray
                                orderby person.Salary descending
                                select person;
            foreach(Person person in orderedSalary)
            {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine("\n");


            //Avarage Salary
            avarageSalary = avarageSum / personArray.Count();

            Console.WriteLine("Salary more than avarage by 50% ");
            //Unique Position and Salary > Avarage more than 50%
            var uniquePositions = (from person in personArray
                                   where person.Salary > avarageSalary * 1.5
                                   select person).Distinct();

            foreach (var person in uniquePositions) 
            {
                Console.WriteLine($"{person.Position} {person.Name}");
            }

            //Groups of equal positions
            var equalPositions = from person in personArray
                                 group person.Name by person.Position;

            foreach (var group in equalPositions)
            {
                Console.WriteLine($"\n{group.Key}");
                foreach (var person in group)
                {
                    Console.WriteLine(person.ToString());
                }
            }
        }
    }
}


