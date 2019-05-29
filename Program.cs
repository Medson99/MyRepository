using System;
using System.Collections.Generic;
using System.Linq;

// Тестовое задание для Junior .NET developer  
//Гурин Вадим, e-mail: herison777@gmail.com
namespace TestConsole
{
	public static class Program
	{
		static void Main(string[] args)
		{
			bool enterCheck = false;
			do
			{
				Console.Clear();
				do
				{
					try
					{
						Console.Write("Введите индекс начала последовательности2: ");
						var begin = int.TryParse(Console.ReadLine(), out var res) ? res : 0;
						Console.Write("Введите индекс конца последовательности: ");
						var end = int.TryParse(Console.ReadLine(), out var result) ? result : 0;
						(begin, end).ToTuple().GetPart().ForEach(num => Console.Write($"{num}; "));

						if (end > begin)
						{
							enterCheck = true;
						}
					}
					catch (Exception)
					{
						Console.WriteLine("Введены не верные данные, попробуйте ещё раз!");
						Console.ReadKey();
						Console.Clear();
						enterCheck = false;
					}
				} while (!enterCheck);

				Console.WriteLine("\nНажмите <Esc> для выхода или любую клавижу для продолжения");
			} while (Console.ReadKey().Key != ConsoleKey.Escape);
		}

		private static List<long> GetPart(this Tuple<int, int> bounds)
		{
			var initList = new List<long> { 1, 1, 1 };

			for (var i = 0; i < bounds.Item2 - 3; i++)
			{
				initList.Add(initList.Skip(Math.Max(0, initList.Count - 3)).Sum());
			}

			return initList.GetRange(bounds.Item1 - 1, bounds.Item2 - bounds.Item1 + 1).ToList();
		}
	}
}
