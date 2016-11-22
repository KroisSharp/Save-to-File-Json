using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Save_to_File_Json
{
    class Person : IEquatable<Person>

    {
        //instance field
        public int alder;
        public string navn;

        //ctor - man kan skrive int alder = 0 for at få 0 til at være default indtil man ændre dem (som man altid gør)
        // dette betyder vi kan slette fra vores liste se linje 89 uden at oplyse aldern 
        public Person(string navn, int alder = 0)
        {
            this.navn = navn;
            this.alder = alder;
        }

        // fordi vi gerne vil se om 2 er ens vælger vi at arve fra IEquatable<> og siger Person klassen arver.
        //vi siger altså hvis other ikke indeholder noget null så false og vi kan lave navnet. hvis navn = et andet navn får vi det tilbage.
        public bool Equals(Person other)
        {
            if (other == null)
            {
                return false;
            }
            else return (this.navn.Equals(other.navn));
        }



        class Program
        {

            static void Main(string[] args)
            {
                //det store problem på værende tidspunkt er den vil prøve at læse data.json men den findens ikke derfor skal vi oprette en tom.
                Console.WriteLine("reading from data.json");

                //her læser vi fra Data.Json
                string jsonSTRING = File.ReadAllText("data.json");
                //her fortæller vi hvad vi skal gøre hvis den findes vi vil gerne smide den data der er fra data.json
                //ind i vores personliste 
                List<Person> myList = JsonConvert.DeserializeObject<List<Person>>(jsonSTRING);

                //hvis "mylist" er tom altså ingen data i json filen skal vi lave en ny instance af listen
                // ellers kan vi ikke smide noget i vores liste.
                if (myList == null)
                {
                    myList = new List<Person>();
                }


                string input = "";
                int inputInt = 0;
                string inputString = "";


                while (input != "q")
                {
                    Console.WriteLine("Tryk a for at tilføje en person");
                    Console.WriteLine("Tryk d for at slette en person");
                    Console.WriteLine("tryk s for at vise indhold");
                    Console.WriteLine("tryk q for at lukke");
                    Console.WriteLine("tryk nu");
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "a":
                            Console.Clear();
                            Console.WriteLine("tilføj person");
                            Console.WriteLine("Skriv et Navn");
                            inputString = Console.ReadLine();
                            Console.WriteLine("Skriv alder Skriv kun tal!");
                            inputInt = Convert.ToInt32(Console.ReadLine());
                            myList.Add(new Person(inputString, inputInt));
                            Console.WriteLine($"tilføjet person: {inputString} som er {inputInt} gammel \n ");
                            break;
                        case "d":
                            Console.Clear();
                            Console.WriteLine("Du vil gerne slette en person");
                            Console.WriteLine("skriv hvem du vil slette");
                            inputString = Console.ReadLine();
                            myList.Remove(new Person(inputString));
                            Console.WriteLine($"Sletter {inputString}");
                            break;
                        case "s":
                            Console.Clear();
                            Console.WriteLine("viser liste");
                            foreach (Person i in myList)
                            {
                                Console.WriteLine(i.navn + i.alder);
                            }
                            break;
                        case "q":
                            Console.Clear();
                            Console.WriteLine("Lukker");
                            break;
                        // hvis man ikke skriver nogle af de rigtige cases går den i Default
                        default:
                            Console.Clear();
                            Console.WriteLine("Prøv igen (:");
                            break;
                    }

                }

                //vi vil gerne gemme vores data før vi lukker programmet
                Console.WriteLine("Gemmer Data til data.json");
                string data = JsonConvert.SerializeObject(myList);
                //vi sætter altså data til at være = den serializerede udgave af mylist som vi derfor kan gemme data.json er fil navnet bemærk .json
                File.WriteAllText("data.json", data);
                Console.ReadLine();


                //sidste kode før
            }



        }
    }
}
