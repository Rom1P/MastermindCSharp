using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Mastermind
{
    class Program
    {

        static void Main(string[] args)
        {
            
            int mod = ChoiceMod();

            if (mod == 1)
            {
                AgainstComputer();
            }

            else if (mod == 2)
            {
                AgainstPlayer();
            }




            Console.WriteLine("\n Press Enter to quit the client...");
            Console.Read();


        }

        

        static void DisplayColors()
        {

            Console.Write("Each color has an associated number :    ");

            ConsoleColor CurrentBackgroundColor = Console.BackgroundColor;

            /* 1 = Yellow, 2 = Blue, 3=Red, 4 = Green, 5 = White, 6 = DarkMagenta*/

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write(1);


            Console.BackgroundColor = CurrentBackgroundColor;
            Console.Write("  ");

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(2);


            Console.BackgroundColor = CurrentBackgroundColor;
            Console.Write("  ");

            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(3);


            Console.BackgroundColor = CurrentBackgroundColor;
            Console.Write("  ");    

            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write(4);


            Console.BackgroundColor = CurrentBackgroundColor;
            Console.Write("  ");

            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(5);


            Console.BackgroundColor = CurrentBackgroundColor;
            Console.Write("  ");

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(6);



            Console.BackgroundColor = CurrentBackgroundColor;
        }

        static void GlobalDisplay(List<List<int>> Input, List<List<int>> Answers)
        {
            Console.Clear();

            DisplayColors();

            for (int i = 0; i < Input.Count; i++)
            {
                Display(Input[i]);
                DisplayCheck(Answers[i]);
            }

        }

        static int ChoiceMod()
        {
            int mod = 0;

            bool validMod = false;

            int intUserInput;

            while (!validMod)
            {


                Console.WriteLine("1 : VS BOT   2 : VS PLAYER");

                /* Try Catch in case of user enter someting else an int*/
                try
                {
                    intUserInput = Convert.ToInt32(Console.ReadLine());
                }

                catch
                {
                    Console.WriteLine("Please input a number");
                    continue;
                }

                if (intUserInput == 1)
                {
                    Console.WriteLine("VS BOT");
                    validMod = true;
                    mod = 1;
                }


                else if (intUserInput == 2)
                {
                    Console.WriteLine("VS PLAYER");
                    validMod = true;
                    mod = 2;
                }

                else
                {
                    Console.WriteLine("Please enter a valid number");
                }


            }


            return mod;
        }

        static void AgainstComputer()
        {

            int Attempts = 10;

            bool validAttemps = false;


            Console.WriteLine("Enter Number of attemps : ");

            while (!validAttemps)
            {
                /* Try Catch in case of user enter someting else an int*/
                try
                {
                    Attempts = Convert.ToInt32(Console.ReadLine());
                    validAttemps = true;
                }

                catch
                {
                    Console.WriteLine("Please input a number");
                }

            }

            bool validColumns = false;


            Console.WriteLine("Enter Number of Columns : ");

            int Columns = 4;

            while (!validColumns)
            {
                /* Try Catch in case of user enter someting else an int*/
                try
                {
                    Columns = Convert.ToInt32(Console.ReadLine());

                    if (Columns >= 4 && Columns <= 6)
                    {
                        validColumns = true;
                    }

                    else
                    {

                        Console.WriteLine("Please input a number between 4 and 6");
                    }
                }

                catch
                {
                    Console.WriteLine("Please input a number");
                }
            }

            List<List<int>> GlobalListInput = new List<List<int>>();


            List<List<int>> GlobalListAnswer = new List<List<int>>();


            Random RandomNumber = new Random();

            List<int> Combinaison = new List<int>();

            for (int i = 1; i <= Columns; i++)
            {
                int NewNumber = RandomNumber.Next(1, 7);

                Combinaison.Add(NewNumber);
            }

            foreach (int TempNumber in Combinaison)
            {
                Console.Write(TempNumber);
            }

            Console.WriteLine("");

            Display(Combinaison);

            bool win = false;


            GlobalDisplay(GlobalListInput, GlobalListAnswer);

            while (!win)
            {

                
                List<int> Input = new List<int>();

                Input = InputAnswer(Columns);


                GlobalListInput.Add(Input);

                List<int> Return = new List<int>();

                Return = CheckAnswer(Input, Combinaison);



                GlobalListAnswer.Add(Return);

                GlobalDisplay(GlobalListInput, GlobalListAnswer);

                win = CheckWin(Input, Combinaison);

                if (!win)
                {
                    Attempts--;
                    if (Attempts == 0)
                    {
                        Console.WriteLine("You loose");
                    }
                }

                
            }







        }

        static void Display(List<int> Combinaison)
        {
            ConsoleColor CurrentBackgroundColor = Console.BackgroundColor;
            foreach (int TempNumber in Combinaison)
            {
                switch (TempNumber)
                {
                    case 1:
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        break;
                    case 2:
                        Console.BackgroundColor = ConsoleColor.Blue;
                        break;
                    case 3:
                        Console.BackgroundColor = ConsoleColor.Red;
                        break;
                    case 4:
                        Console.BackgroundColor = ConsoleColor.Green;
                        break;
                    case 5:
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        break;
                    case 6:
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        break;

                }

                Console.Write("X");
                Console.BackgroundColor = CurrentBackgroundColor;
                Console.Write(" ");



            }
            Console.BackgroundColor = CurrentBackgroundColor;

            Console.Write(" : ");

        }

        static void DisplayCheck(List<int> Result)
        {
            int GoodPlacement = 0;
            int Exists = 0;
            int NotHere = 0;
            foreach (int TempNumber in Result)
            {
                switch (TempNumber)
                {
                    case 0:
                        GoodPlacement++;
                        break;
                    case 1:
                        Exists++;
                        break;
                    case 2:
                        NotHere++;
                        break;


                }
            }

            Console.WriteLine(GoodPlacement + " at correct place " + Exists + " at wrong place");

        }

        static List<int> InputAnswer(int Columns)
        {

            Console.Write("Input : ");

            List<int> ListInput = new List<int>();

            bool ValidInput = false;

            int input;
            String inputString = "";

            while (!ValidInput)
            {
                /* Try Catch in case of user enter someting else an int*/
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());

                    inputString = input.ToString();

                    int length = inputString.Length;

                    if (length == Columns)
                    {
                        ValidInput = true;
                        for (int i = 0; i < inputString.Length; i++)
                        {
                            String inputStringTemp = inputString[i].ToString();
                            int TempNumber = Int32.Parse(inputStringTemp);
                            ListInput.Add(TempNumber);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a combinaison with excepeted length");
                    }
                }

                catch
                {
                    Console.WriteLine("Please input only numbers");
                }

            }

            return ListInput;
        }

        

        static String InputCombinaisonPVP(int Columns)
        {

            Console.Write("Combinaison : ");
            

            bool ValidInput = false;

            int input;
            String inputString = "";

            while (!ValidInput)
            {
                /* Try Catch in case of user enter someting else an int*/
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());

                    inputString = input.ToString();

                    int length = inputString.Length;

                    if (length == Columns)
                    {
                        ValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a combinaison with excepeted length");
                    }
                }

                catch
                {
                    Console.WriteLine("Please input only numbers");
                }

            }

            return inputString;
        }

        static List<int> CheckAnswer(List<int> CombinaisonInput, List<int> Combinaison)
        {



            List<int> Result = new List<int>();
            for (int i = 0; i < CombinaisonInput.Count; i++)
            {
                if (CombinaisonInput[i] == Combinaison[i])
                {
                    Result.Add(0);
                }

                else if (Combinaison.Contains(CombinaisonInput[i]))
                {
                    Result.Add(1);
                }

                else
                {
                    Result.Add(2);
                }
            }



            return Result;
        }

        static bool CheckWin(List<int> CombinaisonInput, List<int> Combinaison)
        {
            if (CombinaisonInput.SequenceEqual(Combinaison))
            {
                Console.WriteLine("You Win");
                return true;
            }

            else
            {

                return false;
            }

        }


        

        static void AgainstPlayer()
        {


            TcpClient tcpClient = new TcpClient();
            NetworkStream networkStream = default(NetworkStream);
            string readData = string.Empty;
            
            tcpClient.Connect("127.0.0.1", 8000);
            networkStream = tcpClient.GetStream();

            byte[] outStream = Encoding.ASCII.GetBytes("startGame");
            networkStream.Write(outStream, 0, outStream.Length);
            networkStream.Flush();
            
            Task taskOpenEndpoint = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    networkStream = tcpClient.GetStream();
                    byte[] message = new byte[4096];
                    int bytesRead;
                    bytesRead = 0;

                    try
                    {
                        bytesRead = networkStream.Read(message, 0, 4096);
                    }
                    catch
                    {
                    }
                    
                    ASCIIEncoding encoder = new ASCIIEncoding();
                    string Message = encoder.GetString(message, 0, bytesRead);

                    Thread.Sleep(500);

                    if (Message == "startGame")
                    {
                        Console.WriteLine("An opponent has benn found");

                        outStream = Encoding.ASCII.GetBytes("player2");
                        networkStream.Write(outStream, 0, outStream.Length);
                        networkStream.Flush();

                       

                        bool validColumns = false;


                        Console.WriteLine("Enter Number of Columns : ");

                        int Columns = 4;

                        while (!validColumns)
                        {
                            /* Try Catch in case of user enter someting else an int*/
                            try
                            {
                                Columns = Convert.ToInt32(Console.ReadLine());

                                if (Columns >= 4 && Columns <= 6)
                                {
                                    validColumns = true;
                                }

                                else
                                {

                                    Console.WriteLine("Please input a number between 4 and 6");
                                }
                            }

                            catch
                            {
                                Console.WriteLine("Please input a number");
                            }
                        }


                        String Combinaison;

                        Combinaison = InputCombinaisonPVP(Columns);

                        outStream = Encoding.ASCII.GetBytes("launch"+Combinaison.ToString());
                        networkStream.Write(outStream, 0, outStream.Length);
                        networkStream.Flush();

                        


                    }

                    else if (Message.StartsWith("launch"))
                    {
                        

                        String CombinaisonString = Message.Substring(6);


                        Console.WriteLine(CombinaisonString);

                        int Columns = 4;

                        List<int> Combinaison = new List<int>();

                        for (int i = 0; i < CombinaisonString.Length; i++)
                        {
                            String inputStringTemp = CombinaisonString[i].ToString();
                            int TempNumber = Int32.Parse(inputStringTemp);
                            Combinaison.Add(TempNumber);
                        }


                        List<List<int>> GlobalListInput = new List<List<int>>();


                        List<List<int>> GlobalListAnswer = new List<List<int>>();


                        bool win = false;


                        GlobalDisplay(GlobalListInput, GlobalListAnswer);

                        while (!win)
                        {
                            List<int> Input = new List<int>();

                            Input = InputAnswer(Columns);


                            GlobalListInput.Add(Input);

                            List<int> Return = new List<int>();


                            Return = CheckAnswer(Input, Combinaison);



                            GlobalListAnswer.Add(Return);

                            GlobalDisplay(GlobalListInput, GlobalListAnswer);
                            

                            win = CheckWin(Input, Combinaison);




                        }
                    }
                }
            });

            while (true) ;
        }

    }
}