using System;
using EmpAssembly;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;  // to read the app/web config values
using System.IO;             // to read/write files
using System.Diagnostics;    // to initiate stop watch
using Newtonsoft.Json; // to deserialize / serialize the JSON data
using System.Net; // to initiate WebClient call to read Web aPI JSON


namespace ConsoleApplication1
{


    abstract class GreenTree {
        string leaves_num;
        public void getNumberofLeaves() {
            leaves_num = "10";
        }
        public abstract void Land();
        public virtual void Water() { }
    }

    class Mango : GreenTree
    {
        public new void getNumberofLeaves() { }
        public override void Land() { }
        public override void Water() { }
    }




    interface ICar
    {
        string Wheel { get; set; }
        string Steering { get; set; }
        string Pedal { get; set; }
        string Engline { get; set; }
        void Drive();
    }

    abstract class Huyndai
    {
        public string Wheel { get; set; }
        public string Steering { get; set; }
        public string Pedal { get; set; }
        public dynamic testdrive { get; set; }
        private string Engline { get; set; }

        public void testeDrive() { }

        /*abstract method - declaration only allowed and it should be 
         implemented in the class that inheriting this abstarct class*/
        public abstract void GearBox();

        /*1.  non abstract method - it can be changed in base class using overridde keyword 
          2. virtual keyword - indicated it may/maynot be changed by base class*/
        public virtual void Drive()
        {
            Console.Write("Hyundai Driving");
        }

    }

    class i20 : Huyndai
    {

        public i20()
        {
            Wheel = "";
        }
        public virtual void FastDrive()
        {
            Console.Write("i20 Fast Driving");
        }

        public override void GearBox()
        {

        }
        public override void Drive() {

        }
    }

    class i20Sport : Huyndai
    {
        public override void GearBox()
        {
            throw new NotImplementedException();
        }
    }

    class i20Basic : Huyndai
    {
        public override void GearBox()
        {
            throw new NotImplementedException();
        }
    }

    class i20Advanced : i20
    {
        public override void FastDrive()
        {
            Console.Write("i20 advanced Fast Driving");
        }
    }


    class punto : ICar
    {
        public string Wheel { get; set; }
        public string Steering { get; set; }
        public string Pedal { get; set; }
        public string Engline { get; set; }

        public string Turbo { get; set; }

        public void Drive()
        {
            Console.Write("Driving");
        }
    }

    class baleno : ICar
    {
        public string Wheel { get; set; }
        public string Steering { get; set; }
        public string Pedal { get; set; }
        public string Engline { get; set; }

        public void Drive()
        {
            Console.Write("Driving");
        }
    }


    interface Tree
    {
        int division(int a, int b);
    }
    abstract class M1
    {
        public int add(int a, int b)
        {
            return (a - b);
        }
    }
    class M2 : M1, Tree
    {
        public new int add(int a, int b)
        {
            return (a + b);
        }
        public int division(int a, int b)
        {
            return (a / b);
        }
        public int mul(int a, int b)
        {
            return (a * b);
        }
    }

    public delegate void DelgTriger();
    class Program
    {
        public void intiDelegateMethod() {
            Console.WriteLine("Delegate Started");
        }
        public void endDelegateMethod()
        {
            Console.WriteLine("Delegate ended");
        }
        class Cat
        {
            // Auto-implemented properties.
            public int Age { get; set; }
            public string Name { get; set; }
        }

        static void Main(string[] args)
        {
            Book book = new Book();
            book.start();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Program program = new Program();
            DelgTriger delgTriger = new DelgTriger(program.intiDelegateMethod);
            delgTriger +=  program.endDelegateMethod;
            delgTriger();
            List<Cat> cats = new List<Cat>
        {
            new Cat(){ Name = "Sylvester", Age=8 },
            new Cat(){ Name = "Whiskers", Age=2 },
            new Cat(){ Name = "Sasha", Age=14 }
        };
            var a = cats.Select(c => c.Name).FirstOrDefault(); // select the first object name in the list and returns, if the list length is zero, then returns null
            var b = cats.Where(c => c.Age == 8).Any(); // return true or false
            var d = cats.Where(c => c.Age == 8).Select(c => new { c.Name, c.Age }).First(); //Filter the data based on where condition and returns the object with select (like sql select query over the where subquery)
            M2 obj = new M2();
            int result = obj.add(4, 6);
            int result2 = obj.division(500, 5);
            Console.WriteLine("the resut for additions is {0}\nthe result for the division is {1}", result, result2);
            Console.ReadLine();


            //To log a content in a text file
            string path = ConfigurationSettings.AppSettings["FilePath"];
            string FileContent1 = "The result for additions is " + result;
            string FileContent2 = "The result for division is " + result2;
            //example 1 - to write all text in single line
            System.IO.File.WriteAllText(path, FileContent1);
            //example 2 - to write a line by line
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                file.WriteLine(FileContent1);
                file.WriteLine(FileContent2);
            }
            //example 3 - to append the line in exsiting file content
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
            {
                stopwatch.Stop();
                file.WriteLine("Third line");
                file.WriteLine("Time elapsed:{0}", stopwatch.Elapsed);
            }


            //To read a log file
            string logged_text = System.IO.File.ReadAllText(path);
            Console.WriteLine("\nThe logged value is:");
            Console.WriteLine("********************");
            Console.WriteLine(logged_text);
            Console.ReadLine();

            //To read large text from lorem ipsum
            string ltextpath = ConfigurationSettings.AppSettings["FilePath_Largetext"];
            string largetext = System.IO.File.ReadAllText(ltextpath);
            Console.WriteLine("\nThe large text value is:");
            Console.WriteLine("*************************");
            Console.WriteLine(largetext);
            Console.ReadLine();



            /* Reading JSON data from json file*/
            using (StreamReader r = new StreamReader("employee.json"))   /* since the json file peroperties set to copy always - no need to mention path*/
            {
                var json = r.ReadToEnd();
                /*fro jsonconvert - need to install the Newtonsoft dll*/
                dynamic ResultItems = JsonConvert.DeserializeObject(json);   //without model class -without casting to new model
                List<EmpModel> Items = JsonConvert.DeserializeObject<List<EmpModel>>(json);   //with model class  - casting to model
                foreach (var val in ResultItems) {
                    Console.WriteLine("Name :" + val.Name);
                    Console.WriteLine("Id :" + val.Id);
                }
                Console.WriteLine("\n*************\n");
                foreach (var val in Items)
                {
                    Console.WriteLine("Name :" + val.Name);
                    Console.WriteLine("Id :" + val.Id);
                }
            }

            /* Reading JSON data from API json URL*/
            using (WebClient wc = new WebClient()) {
                var json = wc.DownloadString("https://jsonplaceholder.typicode.com/posts/1/comments");
                dynamic ResultItems = JsonConvert.DeserializeObject(json);
                Console.WriteLine("\n*************\n");
                foreach (var val in ResultItems)
                {
                    Console.WriteLine("Name :" + val.name);
                    Console.WriteLine("Email :" + val.email);
                }
            }
            Console.ReadLine();

        }
    }

}
