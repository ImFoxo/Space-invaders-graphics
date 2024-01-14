using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Windows.Controls;
using System.Windows.Media;
using Space_invaders_graphics;
using System.Windows;
using System.Runtime.CompilerServices;
using Space_invaders_graphics.Properties;

namespace Space_invaders_console
{
	internal static class HighScore
	{
		static string filePath = "highScore.txt";
		static string[] names = new string[10];
		static int[] scores = new int[10];

        static Button returnButton = new Button() 
		{ 
			Content = "Return",
            FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
            Foreground = Brushes.Green,
            Background = Brushes.Transparent,
            FontSize = 20,
            BorderThickness = new Thickness(0),
        };

        public static void downloadScores()
		{
			if (!File.Exists(filePath))
			{
				File.Create(filePath).Close();
				uploadScores();
			}

			string[] lines = File.ReadAllLines(filePath);

			for (int i = 0; i < 10; i++)
			{
				names[i] = lines[i].Split(' ')[3];
				scores[i] = Int32.Parse(lines[i].Split(' ')[6]);
			}
		}

		static void uploadScores()
		{
			using (StreamWriter sw = new StreamWriter(filePath))
			{
				for (int i = 0; i < 10; i++)
				{
					sw.WriteLine("#{0} - Nick: {1} - Score: {2}", i + 1, names[i], scores[i]);
				}
			}
		}

		public static bool updateScores(string name, int score)
		{
			for (int i = 0; i < 10; i++)
			{
				if (score > scores[i])
				{
					for (int j = 9; j > i; j--)
					{
						names[j] = names[j - 1];
						scores[j] = scores[j - 1];
					}
					names[i] = name;
					scores[i] = score;
					uploadScores();
					return true;
				}
			}
			return false;
		}

		public static void showScoreBoard()
		{
            MainWindow.instance.Background.Opacity = 0.3;
            Label titleLabel = new Label() { Content = "Top 10 highscores:", Foreground = Brushes.YellowGreen, FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"), };
			Canvas.SetTop(titleLabel, 10);
			Canvas.SetLeft(titleLabel, MainWindow.mainCanvas.Width / 2 - 135);
			MainWindow.mainCanvas.Children.Add(titleLabel);

			for (int i = 0; i < 10; i++)
			{
				string formatterText = String.Format("#{0} - Nick: {1} - Score: {2}", i + 1, names[i], scores[i]);
                Label newScore = new Label() { Content = formatterText, Foreground = Brushes.White, FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"), };
				Canvas.SetTop(newScore, 40 * (i + 1) + 20);
				Canvas.SetLeft(newScore, MainWindow.mainCanvas.Width / 2 - 160);
				MainWindow.mainCanvas.Children.Add(newScore);
			}

			Canvas.SetTop(returnButton, MainWindow.mainCanvas.Height - 80);
			Canvas.SetLeft(returnButton, MainWindow.mainCanvas.Width - 150);
			returnButton.Click += new RoutedEventHandler(onReturnClick);
			MainWindow.mainCanvas.Children.Add(returnButton);
		}

		static void onReturnClick(object sender, EventArgs e)
		{
            returnButton.Click -= new RoutedEventHandler(onReturnClick);
			MenuView.loadMenu();
        }
	}
}
