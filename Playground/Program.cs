﻿using System;
using System.IO;
using System.Linq;
using System.Threading;
/// Сделать обучение смещения
/// Сделать вычисление ошибки
/// Сделать полноценный режим обучения
/// Атака
/// Маг атака
/// Усилить атаку
/// Усилить защиту
/// Восстановить ману
/// 
namespace Playground
{
    internal class Program
    {
        public static double[] Calculate(double[] values)
        {
            double maxVal = values.Max();
            double[] expValues = values.Select(v => Math.Exp(v - maxVal)).ToArray();
            double sumExpValues = expValues.Sum();
            return expValues.Select(v => v / sumExpValues).ToArray();
        }

        private static readonly string[] frames = {
           @"
   * . *

  . * . *

   * . *
    ",
        @"
   . * .

  * . * .

   . * .
    ",
        @"
   * . *

  . * . *

   * . *
    ",
        @"
   . * .

  * . * .

   . * .
    ",
    };

        public static void Animate()
        {
            ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Magenta };
            Random random = new Random();
            int consoleWidth = Console.WindowWidth;

            for (int i = 0; i < 1000000; i++)  // Repeat the animation 10 times
            {
                foreach (var frame in frames)
                {
                    Console.SetCursorPosition(0, 15);
                    Console.ForegroundColor = colors[random.Next(colors.Length)];

                    string[] frameLines = frame.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var line in frameLines)
                    {
                        for (int j = 0; j < consoleWidth; j++)
                        {
                            Console.Write(line[j % line.Length]);
                        }
                        Console.WriteLine();
                    }

                    Thread.Sleep(1000);
                }
            }
        }
        public static float NeuroPlay(float P1HP, float P2HP, float P1MP, float P2MP, float P1PATT, float P2PATT, float P1D, float P2D, float P1MATT, float P2MATT, float[,] weights)
        {
            float Bias = -0.3f; //смещение
            float Choice = 0f;
            float[] data = { P1HP / 100f, -P2HP / 100f, P1MP / 100f, -P2MP / 100f, P1PATT / 10f, -P2PATT / 10f, P1D / 10f, -P2D / 10f, P1MATT / 10f, -P2MATT / 10f }; //нормализация путём деления

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data.Length; j++)
                {
                    Choice += data[i] * weights[i, j]; //Умножаем данные и веса
                }
            }
            Choice = Choice + Bias; //Прибавляем смещение
            //Choice = (float)(1 / (1 + Math.Exp(-Choice))); //Сигмойдная функция активации
            Choice = (float)Math.Tanh(Choice); //tahn


            return Choice;
        }




        public static void ShowMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("Cделал - ");
            Console.Write("ᕦ( ~ ◔ ᴥ ◔ ~ )੭━");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("☆ﾟ.*･｡ﾟ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
                __          __                         _  ___ _ _       
                \ \        / /      ｡       *         | |/ (_) | | ☆
                 \ \  /\  / /_ _ _ __ ______ _  __ _  | ' / _| | | __ _ 
                  \ \/  \/ / _` | '__|_  / _` |/ _` | |  < | | | |/ _` |
                   \  /\  / (_| | |   / / (_| | (_| | | . \| | | | (_| |            
                    \/  \/ \__,_|_|  /___\__,_|\__, | |_|\_\_|_|_|\__,_|  © Никаких прав не защищено,всё потеряли ʕ◕‿◕ʔ/
");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("1.Играть");
            Console.WriteLine("2.Обучение");
            Console.WriteLine("3.Включить новогодние звёздочки");
            Console.WriteLine("V 0.3");
            Console.WriteLine("");

        }
        public static void ShowUI(float P1HP, float P2HP, float P1MP, float P2MP, float P1PATT, float P2PATT, float P1D, float P2D, float P1MATT, float P2MATT, int turn, bool P1Live, bool P2Live, float ch1, float ch2)
        {
            Console.Clear();
            Console.SetCursorPosition(5, 3);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Противник 1");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(5, 4);
            Console.WriteLine("Здоровье " + P1HP + " / 100");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(5, 5);
            Console.WriteLine("Защита " + P1D);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(5, 6);
            Console.WriteLine("Мана " + P1MP + " / 100");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(5, 7);
            Console.WriteLine("Физ.урон " + P1PATT);
            Console.SetCursorPosition(5, 8);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Маг урон " + P1MATT);
            Console.SetCursorPosition(5, 9);
            if (P1Live == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Состояние: [ЖИВ]");

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Состояние: [МЕРТВ]");
            }
            Console.ForegroundColor = ConsoleColor.White;
            /////////////////////////////////////////////////
            Console.SetCursorPosition(50, 3);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Противник 2");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(50, 4);
            Console.WriteLine("Здоровье " + P2HP);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(50, 5);
            Console.WriteLine("Защита " + P2D);
            Console.SetCursorPosition(50, 6);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Мана " + P2MP);
            Console.SetCursorPosition(50, 7);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Физ.урон " + P2PATT);
            Console.SetCursorPosition(50, 8);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Маг урон " + P2MATT);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(50, 9);
            if (P2Live == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Состояние: [ЖИВ]");

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Состояние: [МЕРТВ]");
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(80, 2);
            Console.WriteLine("Ход: " + turn);
            Console.SetCursorPosition(80, 3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Результат 1: " + ch1);
            Console.SetCursorPosition(80, 4);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Результат 2:" + ch2);

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            for (int j = 0; j < 100; j++)
            {
                Console.SetCursorPosition(j, 0);
                Console.WriteLine("#");
                Console.SetCursorPosition(j, 10);
                Console.WriteLine("#");
            }
            for (int j = 0; j < 10; j++)
            {
                Console.SetCursorPosition(0, j);
                Console.WriteLine("#");
                Console.SetCursorPosition(100, j);
                Console.WriteLine("#");
            }



        }
        static void Main(string[] args)
        {

            int UserAction = 0;
            int GameTurn = 1;
            Neuron Neuron1 = new Neuron();
            Neuron Neuron2 = new Neuron();
            Character Player1 = new Character();
            Character Player2 = new Character();


            while (true)
            {
                GameTurn = 0;
                Player1.HP = 100;
                Player1.IsDead = false;
                Player1.defence = 1;
                Player1.Mana = 100;
                Player1.MagicAttack = 1;
                Player1.PhysAttack = 3;

                Player2.HP = 100;
                Player2.IsDead = false;
                Player2.defence = 1;
                Player2.Mana = 100;
                Player2.MagicAttack = 1;
                Player2.PhysAttack = 3;



                ShowMenu();
                UserAction = Convert.ToInt32(Console.ReadLine());



                if (UserAction == 1)
                {
                    while (Player1.IsDead == false && Player2.IsDead == false) // проверка смерти
                    {
                        Thread.Sleep(200);
                        if (Player1.HP <= 0)
                        {
                            Player1.IsDead = true;
                            Console.Beep();
                        }
                        else if (Player2.HP <= 0)
                        {
                            Player2.IsDead = true;
                            Console.Beep();
                        }
                        float choice2 = NeuroPlay(Player1.HP, Player2.HP, Player1.Mana, Player2.Mana, Player1.PhysAttack, Player2.PhysAttack, Player1.defence, Player2.defence, Player1.MagicAttack, Player2.MagicAttack, Neuron2.weights);
                        float choice = NeuroPlay(Player1.HP, Player2.HP, Player1.Mana, Player2.Mana, Player1.PhysAttack, Player2.PhysAttack, Player1.defence, Player2.defence, Player1.MagicAttack, Player2.MagicAttack, Neuron1.weights);
                        ShowUI(Player1.HP, Player2.HP, Player1.Mana, Player2.Mana, Player1.PhysAttack, Player2.PhysAttack, Player1.defence, Player2.defence, Player1.MagicAttack, Player2.MagicAttack, GameTurn, Player1.IsDead, Player2.IsDead, choice, choice2);
                        //ShowMenu();
                        Console.WriteLine("[DEV] GameTurn%2 = " + GameTurn % 2);
                        if (GameTurn % 2 == 1)
                        {
                            if (Player1.Mana < 0)
                            {
                                Player1.Mana = 0;
                            }
                            else if (Player1.Mana > 100)
                            {
                                Player1.Mana = 100;
                            }
                            else if (Player2.Mana < 0)
                            {
                                Player2.Mana = 0;
                            }
                            else if (Player2.Mana > 100)
                            {
                                Player2.Mana = 100;
                            }
                            else if (Player1.HP > 100)
                            {
                                Player1.HP = 100;
                            }
                            else if (Player2.HP > 100)
                            {
                                Player2.HP = 100;
                            }

                            if (choice <= -0.5f)
                            {
                                Player1.Mana = Player1.Mana + 15;
                            }
                            else if (choice > -0.5f && choice < 0)
                            {
                                Player1.Mana = Player1.Mana - 35;
                                Player1.MagicAttack = Player1.MagicAttack + 2;
                            }
                            else if (choice >= 0.1f && choice < 0.3f)
                            {
                                Player1.PhysAttack = Player1.PhysAttack + 1.5f;
                            }
                            else if (choice >= 0.3f && choice < 0.5f)
                            {
                                Player2.HP = (Player2.HP + Player2.defence) - Player1.PhysAttack;
                            }
                            else if (choice >= 0.5f && choice < 0.7)
                            {
                                Player1.Mana = Player1.Mana - 25;
                                Player2.HP = Player2.HP - Player1.MagicAttack;
                            }
                            else if (choice > 0.7)
                            {
                                Player1.defence = Player1.defence + 2;
                            }


                        }
                        else
                        {

                            if (choice2 <= -0.5f)
                            {
                                Player2.Mana = Player2.Mana + 15;

                            }
                            else if (choice2 > -0.5f && choice2 < 0)
                            {
                                Player2.Mana = Player2.Mana - 35;
                                Player2.MagicAttack = Player2.MagicAttack + 2;
                            }
                            else if (choice2 >= 0.1f && choice2 < 0.3f) // Диапазон от 0.1 до 0.3
                            {
                                Player2.PhysAttack = Player2.PhysAttack + 1.5f;
                            }
                            else if (choice2 >= 0.3f && choice2 < 0.5f) // Обновлено для учета нового диапазона
                            {
                                Player1.HP = (Player1.HP + Player1.defence) - Player2.PhysAttack;
                            }
                            else if (choice2 >= 0.5f && choice2 < 0.7)
                            {
                                Player2.Mana = Player2.Mana - 25;
                                Player1.HP = Player1.HP - Player2.MagicAttack;
                            }
                            else if (choice2 > 0.7)
                            {
                                Player2.defence = Player2.defence + 2;
                            }

                        }

                        //Console.ReadLine();
                        GameTurn++;
                    }


                }


                else if (UserAction == 2) //обучение
                {
                    float Error = 0f; //разница между хп обоих нейросетей,что служит для выявлении ошибки
                    int iterations = 0;
                    Console.WriteLine();
                    Console.Write("Введите количество ходов в одном цикле обучения(Рекомендовано 50-70-100): ");
                    Console.WriteLine();
                    int LearningIterations = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Введите количество игр,которая сыграет нейросеть сама с собой(Рекомендовано 1000-10000): ");
                    int QuanityOfGames = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();

                    Random random = new Random();
                    for (int i = 0; i < Neuron2.weights.GetLength(0); i++)// заполнение рандомными весами
                    {


                        for (int j = 0; j < Neuron2.weights.GetLength(1); j++)
                        {
                            Neuron2.weights[i, j] = (float)random.NextDouble() * 2 - 1;
                            Neuron1.weights[i, j] = (float)random.NextDouble() * 2 - 1;

                        }


                    }
                    //float randomFloat = (float)random.NextDouble() * 2 - 1; // случайное float значение от 1 до -1 для того чтобы поменять веса.Существует здесь для быстрого доступа 
                    for (int j = 0; j < QuanityOfGames; j++) //количество игр которые сыграет сама с собой нейросеть
                    {
                        GameTurn = 0;
                        Console.WriteLine("[j = " + j + "]");
                        Player1.HP = 100;
                        Player1.IsDead = false;
                        Player1.defence = 1;
                        Player1.Mana = 100;
                        Player1.MagicAttack = 1;
                        Player1.PhysAttack = 3;

                        Player2.HP = 100;
                        Player2.IsDead = false;
                        Player2.defence = 1;
                        Player2.Mana = 100;
                        Player2.MagicAttack = 1;
                        Player2.PhysAttack = 3;

                        for (int i = 0; i < LearningIterations; i++) //Сам цикл игры который маштабируется пользователем путём ввода  количества итераций "игры"
                        {

                            float choice2 = NeuroPlay(Player1.HP, Player2.HP, Player1.Mana, Player2.Mana, Player1.PhysAttack, Player2.PhysAttack, Player1.defence, Player2.defence, Player1.MagicAttack, Player2.MagicAttack, Neuron2.weights);
                            float choice = NeuroPlay(Player1.HP, Player2.HP, Player1.Mana, Player2.Mana, Player1.PhysAttack, Player2.PhysAttack, Player1.defence, Player2.defence, Player1.MagicAttack, Player2.MagicAttack, Neuron1.weights);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(i + ".");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(" x 1 = " + choice);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("  x 2 = " + choice2);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("    " + DateTime.Now);
                            Console.WriteLine();
                            if (GameTurn % 2 == 1)
                            {
                                if (Player1.Mana < 0)
                                {
                                    Player1.Mana = 0;
                                }
                                else if (Player1.Mana > 100)
                                {
                                    Player1.Mana = 100;
                                }
                                else if (Player2.Mana < 0)
                                {
                                    Player2.Mana = 0;
                                }
                                else if (Player2.Mana > 100)
                                {
                                    Player2.Mana = 100;
                                }
                                else if (Player1.HP > 100)
                                {
                                    Player1.HP = 100;
                                }
                                else if (Player2.HP > 100)
                                {
                                    Player2.HP = 100;
                                }

                                if (choice <= -0.5f)
                                {
                                    Player1.Mana = Player1.Mana + 15;
                                }
                                else if (choice > -0.5f && choice < 0)
                                {
                                    Player1.Mana = Player1.Mana - 35;
                                    Player1.MagicAttack = Player1.MagicAttack + 2;
                                }
                                else if (choice >= 0.1f && choice < 0.3f)
                                {
                                    Player1.PhysAttack = Player1.PhysAttack + 1.5f;
                                }
                                else if (choice >= 0.3f && choice < 0.5f)
                                {
                                    Player2.HP = (Player2.HP + Player2.defence) - Player1.PhysAttack;
                                }
                                else if (choice >= 0.5f)
                                {
                                    Player1.Mana = Player1.Mana - 25;
                                    Player2.HP = Player2.HP - Player1.MagicAttack;
                                }
                                else if (choice >= 0.5f && choice < 0.7)
                                {
                                    Player1.defence = Player1.defence + 2;
                                }
                                else if (choice > 0.7)
                                {
                                    Player1.defence = Player1.defence + 2;
                                }


                            }
                            else
                            {
                                if (Player1.Mana < 0)
                                {
                                    Player1.Mana = 0;
                                }
                                else if (Player1.Mana > 100)
                                {
                                    Player1.Mana = 100;
                                }
                                else if (Player2.Mana < 0)
                                {
                                    Player2.Mana = 0;
                                }
                                else if (Player2.Mana > 100)
                                {
                                    Player2.Mana = 100;
                                }
                                else if (Player1.HP > 100)
                                {
                                    Player1.HP = 100;
                                }
                                else if (Player2.HP > 100)
                                {
                                    Player2.HP = 100;
                                }

                                if (choice2 <= -0.5f)
                                {
                                    Player2.Mana = Player2.Mana + 15;
                                }
                                else if (choice2 > -0.5f && choice2 < 0)
                                {
                                    Player2.Mana = Player2.Mana - 35;
                                    Player2.MagicAttack = Player2.MagicAttack + 2;
                                }
                                else if (choice2 >= 0.1f && choice2 < 0.3f) // Диапазон от 0.1 до 0.3
                                {
                                    Player2.PhysAttack = Player2.PhysAttack + 1.5f;
                                }
                                else if (choice2 >= 0.3f && choice2 < 0.5f) // Обновлено для учета нового диапазона
                                {
                                    Player1.HP = (Player1.HP + Player1.defence) - Player2.PhysAttack;
                                }
                                else if (choice2 >= 0.5f && choice2 < 0.7)
                                {
                                    Player2.Mana = Player2.Mana - 25;
                                    Player1.HP = Player1.HP - Player2.MagicAttack;
                                }
                                else if (choice2 > 0.7)
                                {
                                    Player2.defence = Player2.defence + 2;
                                }
                            }
                            if (Player1.HP > Player2.HP)
                            {
                                Error = Player1.HP - Player2.HP;
                            }
                            else if (Player2.HP > Player1.HP)
                            {
                                Error = Player2.HP - Player1.HP;
                            }
                            else
                            {
                                Error += Player1.HP / 1000;
                            }


                            GameTurn++;

                        }




                        if (Player1.HP <= 0 || Player1.IsDead == true || Player1.HP > Player2.HP) //Изменения весов,т.е обучение нейросети сука,тут очень сильно усложнённый код ОЧЕНЬ (НАДО ИСПРАВИТЬ)
                        {
                            Random rand1 = new Random();
                            for (int i = 0; i < Neuron2.weights.GetLength(0); i++)
                            {
                                for (int d = 0; d < Neuron2.weights.GetLength(1); d++)
                                {
                                    Neuron2.weights[i, d] = (Neuron1.weights[i, d] / 1000 + (float)random.NextDouble() + Error) / 10;
                                    Neuron2.weights[i, d] = (float)Math.Tanh(Neuron2.weights[i, d]);
                                }
                            }

                            Console.Write("Изменения весов для 2 нейросети: ");


                        }
                        else if (Player2.HP <= 0 || Player2.IsDead == true || Player2.HP > Player1.HP)
                        {
                            Random rand2 = new Random();
                            for (int i = 0; i < Neuron2.weights.GetLength(0); i++)
                            {
                                for (int a = 0; a < Neuron2.weights.GetLength(1); a++)
                                {

                                    Neuron1.weights[i, a] = (Neuron2.weights[i, a] / 1000 + (float)random.NextDouble() + Error) / 10;
                                    Neuron1.weights[i, a] = (float)Math.Tanh(Neuron1.weights[i, a]);
                                }
                            }

                            Console.WriteLine("Изменения весов для 1 нейросети: ");
                            for (int x = 0; x < Neuron1.weights.GetLength(0); x++)
                            {
                                for (int b = 0; b < Neuron1.weights.GetLength(1); b++)
                                {
                                    Console.Write("(" + "[" + x + "] " + Neuron1.weights[x, b] + ")");
                                }
                            }
                            Console.WriteLine("");
                        }
                        else
                        {
                            for (int i = 0; i < Neuron1.weights.GetLength(0); i++)
                            {
                                for (int c = 0; c < Neuron2.weights.GetLength(1); c++)
                                {
                                    Neuron1.weights[i, c] = (Neuron1.weights[i, c] / 1000 + (float)random.NextDouble() / 10);
                                    Neuron2.weights[i, c] = (-Neuron2.weights[i, c] / 1000 + (float)random.NextDouble() / 10);
                                    Neuron1.weights[i, c] = (float)Math.Tanh(Neuron1.weights[i, c]);
                                    Neuron2.weights[i, c] = (float)Math.Tanh(Neuron2.weights[i, c]);

                                }

                                Console.WriteLine("Изменения весов для обоих нейросетей: ");
                                for (int x = 0; x < Neuron1.weights.GetLength(0); x++)
                                {
                                    for (int c = 0; c < Neuron2.weights.GetLength(1); c++)
                                    {
                                        Console.Write("1. (" + "[" + x + "] " + Neuron1.weights[x, c] + ")");
                                    }
                                }
                                Console.WriteLine();
                                for (int y = 0; y < Neuron2.weights.GetLength(0); y++)
                                {

                                    //Console.Write("2. (" + "[" + y + "] " + Neuron2.weights[y,j] + ")");
                                }
                                Console.WriteLine("");
                            }
                            iterations++;
                        }
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("");
                        Console.WriteLine("[Цикл обучения завершён]    Всего циклов: " + iterations + "   Итераций в цикле обучения: " + LearningIterations);
                    }
                }
                else if (UserAction == 3)
                {
                    Animate();
                    /*  string FilePath = @"C:\Users\arkuz\Desktop\Neuro 2.0\NeuroWeights1.txt";
                      string content = "Neuro 1 ";
                      for (int i = 0; i < Neuron1.weights.Length; i++)
                      {
                          for (int j = 0; j < Neuron2.weights.Length; j++)
                          {
                              content = content + Neuron1.weights[i, j].ToString() + ", ";
                          }
                      }
                      content = content + "! " + " Neuro2: ";
                      for (int x = 0; x < Neuron2.weights.Length; x++)
                      {
                          for (int j = 0; j < Neuron2.weights.Length; j++)
                          {
                              content += Neuron2.weights[x, j].ToString() + ", ";
                          }
                      }
                      Console.WriteLine("Файл создан по пути: " + FilePath);
                      //string readContent = File.ReadAllText(FilePath);
                      // Console.WriteLine(readContent);

                      if (File.Exists(FilePath))
                      {
                          Console.WriteLine("Файл уже существует");

                      }
                      else


                      {
                          File.Create(FilePath).Close();

                      }

                  }
                */


                    Console.WriteLine("Введите любое число чтобы вернуться назад");
                    Console.ReadLine();
                    Console.Clear();

                }
            }
        }
        public class Neuron
        {
            public float[,] weights = new float[10, 10];
        }
        public class Character
        {
            public float HP = 100;
            public float Mana = 100;
            public float defence = 1;
            public float PhysAttack = 3;
            public float MagicAttack = 1;
            public bool IsDead = false;
        }

    }
}

