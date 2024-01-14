using Space_invaders_console;
using Space_invaders_graphics.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Space_invaders_graphics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow instance;
        public static Canvas mainCanvas;

        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            instance.ResizeMode = ResizeMode.NoResize;
            instance.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Background.jpg")) };
            instance.Background.Opacity = 0.5;
            setUpCanvas();
            HighScore.downloadScores();
            MenuView.loadMenu();
        }

        private void setUpCanvas()
        {
            mainCanvas = new Canvas();
            mainCanvas.Height = 500;
            mainCanvas.Width = 800;
            mainCanvas.Background = new SolidColorBrush(Colors.Transparent);
            Content = mainCanvas;
        }
    }
}
