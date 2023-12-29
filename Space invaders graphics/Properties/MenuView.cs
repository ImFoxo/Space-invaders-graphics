using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;
using System.Xml.Linq;
using System.Windows.Media;

namespace Space_invaders_graphics.Properties
{
    public static class MenuView
    {
        static Button startGameButton = new Button
        {
            Height = 40,
            Width = 100,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Content = "Start"
        }; 
        static Button highscoreGameButton = new Button
        {
            Height = 40,
            Width = 100,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Content = "High scores"
        };
        static Button exitGameButton = new Button
        {
            Height = 40,
            Width = 100,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Content = "Exit",
        };
		static Button fullScreenButton = new Button
		{
			Height = 40,
			Width = 100,
			HorizontalAlignment = HorizontalAlignment.Left,
			VerticalAlignment = VerticalAlignment.Top,
			Content = "Fullscreen",
		};

		public static void createButtons()
		{

			double height = MainWindow.mainCanvas.Height;
            double width = MainWindow.mainCanvas.Width;

            Canvas.SetLeft(startGameButton, width / 2 - 50);
            Canvas.SetTop(startGameButton, height / 2 - 65);
            MainWindow.mainCanvas.Children.Add(startGameButton);
            Canvas.SetLeft(highscoreGameButton, width / 2 - 50);
            Canvas.SetTop(highscoreGameButton, height / 2);
            MainWindow.mainCanvas.Children.Add(highscoreGameButton);
			Canvas.SetLeft(fullScreenButton, width / 2 - 50);
			Canvas.SetTop(fullScreenButton, height / 2 + 65);
			MainWindow.mainCanvas.Children.Add(fullScreenButton);
			Canvas.SetLeft(exitGameButton, width / 2 - 50);
            Canvas.SetTop(exitGameButton, height / 2 + 130);
            MainWindow.mainCanvas.Children.Add(exitGameButton);

            startGameButton.Click += new RoutedEventHandler(onStartClick);
			fullScreenButton.Click += new RoutedEventHandler(onFullScreenClick);
        }

        public static void clearButtons()
		{
			MainWindow.mainCanvas.Children.Clear();
			startGameButton.Click -= new RoutedEventHandler(onStartClick);
			fullScreenButton.Click -= new RoutedEventHandler(onFullScreenClick);
		}

		private static void onStartClick(object sender, RoutedEventArgs e)
        {
            clearButtons();
			Game.setGame();
        }

        private static void onFullScreenClick(object sender, RoutedEventArgs e)
		{
			clearButtons();
			if (MainWindow.instance.WindowState == WindowState.Maximized)
			{
				MainWindow.instance.WindowState = WindowState.Normal;
				MainWindow.instance.WindowStyle = WindowStyle.SingleBorderWindow;
                //MainWindow.mainCanvas.Height = MainWindow.instance.Height;
                //MainWindow.mainCanvas.Width = MainWindow.instance.Width;
			}
            else
            {
			    MainWindow.instance.WindowState = WindowState.Maximized;
			    MainWindow.instance.WindowStyle = WindowStyle.None;
                //MainWindow.instance.Background = new SolidColorBrush(Colors.Black);
				//MainWindow.mainCanvas.Height = MainWindow.instance.Height;
				//MainWindow.mainCanvas.Width = MainWindow.instance.Width;
			}
            createButtons();
		}
    }
}
