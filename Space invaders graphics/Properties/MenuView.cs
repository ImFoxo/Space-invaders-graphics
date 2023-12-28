using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;
using System.Xml.Linq;

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
            //Canvas.Left = 350,
            //Canvas.Top = 242,
        }; 
        static Button highscoreGameButton = new Button
        {
            Height = 40,
            Width = 100,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Content = "High scores"
            //Canvas.Left = 350,
            //Canvas.Top = 242,
        };
        static Button exitGameButton = new Button
        {
            Height = 40,
            Width = 100,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Content = "Exit",
            //Canvas.Left = 350,
            //Canvas.Top = 242,
        };

        public static void createButtons()
        {
            Canvas.SetLeft(startGameButton, 350);
            Canvas.SetTop(startGameButton, 242);
            MainWindow.mainWindow.mainCanvas.Children.Add(startGameButton);
            Canvas.SetLeft(highscoreGameButton, 350);
            Canvas.SetTop(highscoreGameButton, 307);
            MainWindow.mainWindow.mainCanvas.Children.Add(highscoreGameButton);
            Canvas.SetLeft(exitGameButton, 350);
            Canvas.SetTop(exitGameButton, 372);
            MainWindow.mainWindow.mainCanvas.Children.Add(exitGameButton);

            startGameButton.Click += new RoutedEventHandler(onStartClick);
        }

        private static void onStartClick(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.mainCanvas.Children.Clear();
            Game.setGame();
        }
    }
}
