using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Space_invaders_graphics.Properties
{
    internal static class Description
    {
        static Button returnButton = new Button()
        {
            Content = "Return",
            FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
            Foreground = Brushes.Green,
            Background = Brushes.Transparent,
            FontSize = 20,
            BorderThickness = new Thickness(0),
        };

        public static void showDescription()
        {
            MainWindow.instance.Background.Opacity = 0.3;
            Label desc = new Label() 
            {
                Foreground = Brushes.YellowGreen,
                //Background = Brushes.Black,
                FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
                Content = "Goal of the game is to beat aliens attacking player.\n\n" +
                "Game can't be beaten, aim is to gain as many points as possible.\n\n" +
                "Every defeated alien gives 1 point, every wave extra 10.\n\n" +
                "\n\n" +
                "Player moves using arrow keys nad shoots with spacebar.\n\n" +
                "\n\n" +
                "Aliens aslo shoot purple missiles, which should be avoided\n\n" +
                "as getting hit decreases players health points.\n\n" +
                "The game end when playrs healf goes down to 0,\n\n" +
                "or the invaders reach his ship."
            };
            Canvas.SetTop(desc, 30);
            Canvas.SetLeft(desc, 8);
            MainWindow.mainCanvas.Children.Add(desc);

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
