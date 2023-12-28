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
        public static MainWindow mainWindow;

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
        ImageBrush playerSkin = new ImageBrush();


        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;


			MenuView.createButtons();

            //gameTimer.Tick += GameLoop;
            //gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            //gameTimer.Start();

            //playerSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/player.png"));

            //mainCanvas.Focus();
            ////-----------------------------------------------
            //enemiesLeft = new Label
            //{
            //    Foreground = Brushes.White,
            //    FontSize = 16,
            //    FontWeight = FontWeights.ExtraBold,
            //    Content = "Enemies left: 0 "
            //};
            //mainCanvas.Children.Add(enemiesLeft);
            ////-----------------------------------------------
            //player = new Rectangle
            //{
            //    Fill = playerSkin,
            //    Height = 65,
            //    Width = 55,
            //    HorizontalAlignment = HorizontalAlignment.Center,
            //    VerticalAlignment = VerticalAlignment.Center,
            //};
            //Canvas.SetLeft(player, 372);
            //Canvas.SetTop(player, 409);
            //mainCanvas.Children.Add(player);
            ////-----------------------------------------------

            //makeEnemies(10);
        }

        //private void GameLoop(object sender, EventArgs e)
        //{
        //    Rect playermodel = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
        //    enemiesLeft.Content = "Enemies left: " + totalEnemies;

        //    if (goLeft == true && Canvas.GetLeft(player) > 0)
        //        Canvas.SetLeft(player, Canvas.GetLeft(player) - 10);
        //    else if (goRight == true && Canvas.GetLeft(player) + 65 < Application.Current.MainWindow.Width)
        //        Canvas.SetLeft(player, Canvas.GetLeft(player) + 10);

        //    bulletTimer -= 3;

        //    if (bulletTimer < 0)
        //    {
        //        enemyBulletMaker(Canvas.GetLeft(player) + 20, 10);
        //        bulletTimer = bulletTimerLimit;
        //    }

        //    foreach (var rec in mainCanvas.Children.OfType<Rectangle>())
        //    {
        //        if ((string)rec.Tag == "bullet")
        //        {
        //            Canvas.SetTop(rec, Canvas.GetTop(rec) - 20);
        //            if (Canvas.GetTop(rec) < 10)
        //                itemsToRemove.Add(rec);

        //            Rect bulletmodel = new Rect(Canvas.GetLeft(rec), Canvas.GetTop(rec), rec.Width, rec.Height);

        //            foreach (var enemy in mainCanvas.Children.OfType<Rectangle>())
        //            {
        //                if ((string)enemy.Tag == "enemy")
        //                {
        //                    Rect enemymodel = new Rect(Canvas.GetLeft(enemy), Canvas.GetTop(enemy), enemy.Width, enemy.Height);

        //                    if (bulletmodel.IntersectsWith(enemymodel))
        //                    {
        //                        itemsToRemove.Add(enemy);
        //                        itemsToRemove.Add(rec);
        //                        totalEnemies--;
        //                    }
        //                }
        //            }    
        //        }

        //        if ((string)rec.Tag == "enemy")
        //        {
        //            Canvas.SetLeft(rec, Canvas.GetLeft(rec) + enemySpeed);
        //            if (Canvas.GetLeft(rec) > 820)
        //            {
        //                Canvas.SetLeft(rec, -80);
        //                Canvas.SetTop(rec, Canvas.GetTop(rec) +  rec.Height + 10);
        //            }

        //            Rect enemymodel = new Rect(Canvas.GetLeft(rec), Canvas.GetTop(rec), rec.Width, rec.Height);

        //            if (playermodel.IntersectsWith(enemymodel))
        //                showGameOver("Koniecccccc");
        //        }

        //        if ((string)rec.Tag == "enemyBullet")
        //        {
        //            Canvas.SetTop(rec, Canvas.GetTop(rec) + 10);
        //            if (Canvas.GetTop(rec) > 480)
        //                itemsToRemove.Add(rec);

        //            Rect enemyBulletmodel = new Rect(Canvas.GetLeft(rec), Canvas.GetTop(rec), rec.Width, rec.Height);

        //            if (playermodel.IntersectsWith(enemyBulletmodel))
        //                showGameOver("Konieccccccccccccccccccccccccc");
        //        }
        //    }

        //    foreach (Rectangle rec in itemsToRemove)
        //    {
        //        mainCanvas.Children.Remove(rec);
        //    }

        //    if (totalEnemies == 0)
        //        showGameOver("Winnn");
        //}

        //private void KeyIsDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Left)
        //        goLeft = true;
        //    if (e.Key == Key.Right)
        //        goRight = true;
        //}

        //private void KeyIsUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Left)
        //        goLeft = false;
        //    if (e.Key == Key.Right)
        //        goRight = false;
        //    if (e.Key == Key.Space)
        //    {
        //        Rectangle newBullet = new Rectangle
        //        {
        //            Tag = "bullet",
        //            Height = 20,
        //            Width = 5,
        //            Fill = Brushes.White,
        //            Stroke = Brushes.Red
        //        };

        //        Canvas.SetTop(newBullet, Canvas.GetTop(player) - newBullet.Height);
        //        Canvas.SetLeft(newBullet, Canvas.GetLeft(player) + player.Width / 2);

        //        mainCanvas.Children.Add(newBullet);
        //    }
        //    if (e.Key == Key.Enter && gameOver == true)
        //    {
        //        System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        //        Application.Current.Shutdown();
        //    }
        //}

        //private void enemyBulletMaker(double x, double y)
        //{
        //    Rectangle enemyBullet = new Rectangle
        //    {
        //        Tag = "enemyBullet",
        //        Height = 40,
        //        Width = 15,
        //        Fill = Brushes.Yellow,
        //        Stroke = Brushes.Black,
        //        StrokeThickness = 5
        //    };

        //    Canvas.SetTop(enemyBullet, y);
        //    Canvas.SetLeft(enemyBullet, x);

        //    mainCanvas.Children.Add(enemyBullet);
        //}

        //private void makeEnemies(int limit)
        //{
        //    int left = 0;

        //    totalEnemies = limit;

        //    for (int i = 0; i < limit; i++)
        //    {
        //        ImageBrush enemySkin = new ImageBrush();

        //        Rectangle newEnemy = new Rectangle()
        //        {
        //            Tag = "enemy",
        //            Height = 45,
        //            Width = 45,
        //            Fill = enemySkin
        //        };

        //        Canvas.SetTop(newEnemy, 30);
        //        Canvas.SetLeft(newEnemy, left);
        //        mainCanvas.Children.Add(newEnemy);
        //        left -= 60;

        //        enemyImages++;
        //        if (enemyImages > 8)
        //            enemyImages = 1;

        //        switch (enemyImages)
        //        {
        //            case 1:
        //                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/invader1.gif"));
        //                break;
        //            case 2:
        //                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/invader2.gif"));
        //                break;
        //            case 3:
        //                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/invader3.gif"));
        //                break;
        //            case 4:
        //                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/invader4.gif"));
        //                break;
        //            case 5:
        //                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/invader5.gif"));
        //                break;
        //            case 6:
        //                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/invader6.gif"));
        //                break;
        //            case 7:
        //                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/invader7.gif"));
        //                break;
        //            case 8:
        //                enemySkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/invader8.gif"));
        //                break;
        //        }
        //    }
        //}

        //private void showGameOver(string msg)
        //{
        //    gameOver = true;
        //    gameTimer.Stop();
        //    enemiesLeft.Content = msg + " Press enter to play again";
        //}
    }
}
