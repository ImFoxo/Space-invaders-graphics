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
using Space_invaders_console;
using System.Windows.Media.Imaging;

namespace Space_invaders_graphics.Properties
{
    public static class MenuView
    {
        static double buttonWidth = 220;
        static double buttonHeight = 45;
        static Button startGameButton = new Button
        {
            Height = buttonHeight,
            Width = buttonWidth,
            Content = "Start",
            FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
            Foreground = Brushes.Green,
            Background = Brushes.Transparent,
            FontSize = 20,
            BorderThickness = new Thickness(0),
        };
        static Button descriptionButton = new Button
        {
            Height = buttonHeight,
            Width = buttonWidth,
            Content = "Description",
            FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
            Foreground = Brushes.Green,
            Background = Brushes.Transparent,
            FontSize = 20,
            BorderThickness = new Thickness(0),
        };
        static Button highscoreButton = new Button
        {
            Height = buttonHeight,
            Width = buttonWidth,
            Content = " High\nscores",
            FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
            Foreground = Brushes.Green,
            Background = Brushes.Transparent,
            FontSize = 20,
            BorderThickness = new Thickness(0),
        };
        static Button exitGameButton = new Button
        {
            Height = buttonHeight,
            Width = buttonWidth,
            Content = "Exit",
            FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
            Foreground = Brushes.Green,
            Background = Brushes.Transparent,
            FontSize = 20,
            BorderThickness = new Thickness(0),
        };
		static Button fullScreenButton = new Button
        {
            Height = buttonHeight,
            Width = buttonWidth,
			Content = "Fullscreen",
            FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
            Foreground = Brushes.Green,
            Background = Brushes.Transparent,
            FontSize = 20,
            BorderThickness = new Thickness(0),
        };


		public static void loadMenu()
        {
            MainWindow.instance.Background.Opacity = 0.5;
            MainWindow.mainCanvas.Children.Clear();

            double height = MainWindow.mainCanvas.Height;
            double width = MainWindow.mainCanvas.Width;

            Canvas.SetLeft(startGameButton, width / 2 - buttonWidth / 2);
            Canvas.SetTop(startGameButton, height / 2 - 130);
            MainWindow.mainCanvas.Children.Add(startGameButton);
            Canvas.SetLeft(descriptionButton, width / 2 - buttonWidth / 2);
            Canvas.SetTop(descriptionButton, height / 2 - 65);
            MainWindow.mainCanvas.Children.Add(descriptionButton);
            Canvas.SetLeft(highscoreButton, width / 2 - buttonWidth / 2);
            Canvas.SetTop(highscoreButton, height / 2);
            MainWindow.mainCanvas.Children.Add(highscoreButton);
			Canvas.SetLeft(fullScreenButton, width / 2 - buttonWidth / 2);
			Canvas.SetTop(fullScreenButton, height / 2 + 65);
			MainWindow.mainCanvas.Children.Add(fullScreenButton);
			Canvas.SetLeft(exitGameButton, width / 2 - buttonWidth / 2);
            Canvas.SetTop(exitGameButton, height / 2 + 130);
            MainWindow.mainCanvas.Children.Add(exitGameButton);

            startGameButton.Click += new RoutedEventHandler(onStartClick);
            descriptionButton.Click += new RoutedEventHandler(onDescriptionClick);
            highscoreButton.Click += new RoutedEventHandler(onHighscoreClick);
			fullScreenButton.Click += new RoutedEventHandler(onFullScreenClick);
            exitGameButton.Click += new RoutedEventHandler(onExitClick);
        }

        public static void closeMenu()
		{
			MainWindow.mainCanvas.Children.Clear();
			startGameButton.Click -= new RoutedEventHandler(onStartClick);
            descriptionButton.Click -= new RoutedEventHandler(onDescriptionClick);
            highscoreButton.Click -= new RoutedEventHandler(onHighscoreClick);
            fullScreenButton.Click -= new RoutedEventHandler(onFullScreenClick);
            exitGameButton.Click -= new RoutedEventHandler(onExitClick);
        }

		private static void onStartClick(object sender, RoutedEventArgs e)
        {
            closeMenu();
			Game.setGame();
        }
        
        private static void onDescriptionClick(object sender, RoutedEventArgs e)
        {
            closeMenu();
            Description.showDescription();
        }

        private static void onHighscoreClick(object sender, RoutedEventArgs e)
        {
            closeMenu();
            HighScore.showScoreBoard();
        }

        private static void onFullScreenClick(object sender, RoutedEventArgs e)
		{
			closeMenu();
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
            loadMenu();
		}

        private static void onExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
