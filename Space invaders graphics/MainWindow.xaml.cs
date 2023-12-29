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

		bool goLeft, goRight;

        Rectangle player;
        Label enemiesLeft;

        List<Rectangle> itemsToRemove = new List<Rectangle>();

        int enemyImages = 0;
        int bulletTimer = 0;
        int bulletTimerLimit = 90;
        int totalEnemies = 0;
        int enemySpeed = 6;
        bool gameOver = false;

        DispatcherTimer gameTimer = new DispatcherTimer();
        ImageBrush background = new ImageBrush();


        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            instance.ResizeMode = ResizeMode.NoResize;
            instance.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Background.jpg")) };
            setUpCanvas();
			MenuView.createButtons();
        }

        private void setUpCanvas()
        {
            mainCanvas = new Canvas();
            mainCanvas.Height = 500;
            mainCanvas.Width = 800;
            mainCanvas.Background = new SolidColorBrush(Colors.Transparent);
            Content = mainCanvas;
        }

        //private void GameLoop(object sender, EventArgs e)
        //{

        //    bulletTimer -= 3;

        //    if (bulletTimer < 0)
        //    {
        //        enemyBulletMaker(Canvas.GetLeft(player) + 20, 10);
        //        bulletTimer = bulletTimerLimit;
        //    }

        //    foreach (var rec in mainCanvas.Children.OfType<Rectangle>())
        //    
        //}
    }
}
