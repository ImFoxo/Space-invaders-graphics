using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Space_invaders_graphics.Properties
{
    public static class Game
    {
        static Player player;
        static List<PlayerMissile> playerMissiles = new List<PlayerMissile>();
        //static List<EnemyMissile> enemyMissile = new List<EnemyMissile>();
        static List<Enemy> enemies = new List<Enemy>();

        private static bool isPaused = false;

        static string playerMoveDirection;

        static DispatcherTimer gameTimer = new DispatcherTimer();

        public static void setGame()
        {
            MainWindow.instance.KeyDown += KeyIsDown;
            MainWindow.instance.KeyUp += KeyIsUp;

			gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

			MainWindow.mainCanvas.Background = new SolidColorBrush(Colors.Black);

			createEntities();
		}

        private static void GameLoop(object sender, EventArgs e)
        {
            if (isPaused) 
                return;

            if (enemies.Count == 0)
                spawnEnemies(6);

            moveEntities();

            //bulletTimer -= 3;

            //if (bulletTimer < 0)
            //{

            //}

            checkForCollisions();
            drawEntities();
        }

        private static void drawEntities()
        {
            Canvas.SetLeft(player.model, player.x);
            Canvas.SetTop(player.model, player.y);

            foreach (PlayerMissile pm in playerMissiles)
            {
                Canvas.SetLeft(pm.model, pm.x);
                Canvas.SetTop(pm.model, pm.y);
            }

            foreach (Enemy em in enemies)
			{
				Canvas.SetLeft(em.model, em.x);
				Canvas.SetTop(em.model, em.y);
			}
        }

        private static void moveEntities()
        {
            player.move(playerMoveDirection);
            foreach (PlayerMissile pm in playerMissiles)
            {
                pm.move();
            }
            Enemy.checkIfEnemyOnEdge(enemies);
            foreach (Enemy em in enemies)
            {
                em.move();
            }
        }

        private static void createEntities()
        {
            player = Player.getInstance();
            player.model = new Rectangle
            {
                Fill = player.skin,
                Height = 65,
                Width = 55,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
            Canvas.SetLeft(player.model, player.x);
            Canvas.SetTop(player.model, player.y);
            MainWindow.mainCanvas.Children.Add(player.model);

			spawnEnemies(6);
		}

        private static void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                playerMoveDirection = "left";
            else if (e.Key == Key.Right)
                playerMoveDirection = "right";
        }

        private static void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right)
                playerMoveDirection = "";
            if (e.Key == Key.Space)
            {
                PlayerMissile newMissile = player.shootMissile();
                playerMissiles.Add(newMissile);
                MainWindow.mainCanvas.Children.Add(newMissile.model);
			}
			if (e.Key == Key.Escape)
            {
                isPaused = !isPaused;
                if (isPaused)
                    drawPauseOverlay();
                else
				{
                    erasePauseOverlay();
				}
			}

		}

		private static void spawnEnemies(int limit)
        {
            int left = 0;

            for (int i = 0; i < limit; i++)
            {
                Enemy newEnemy = new Enemy(left, 30, 3);
                enemies.Add(newEnemy);
                MainWindow.mainCanvas.Children.Add(newEnemy.model);
                left -= 60;
            }
        }

        private static void checkForCollisions()
        {
            foreach (Enemy enemy in enemies)
            {
                foreach (PlayerMissile pm in playerMissiles)
                {
                    if (enemy.hitbox.IntersectsWith(pm.hitbox))
                    {
                        enemy.getHit(pm);
                        playerMissiles.Remove(pm);
                        MainWindow.mainCanvas.Children.Remove(pm.model);
						break;
                    }
                }
                if (enemy.isDead)
                {
                    enemies.Remove(enemy);
                    MainWindow.mainCanvas.Children.Remove(enemy.model);
                    break;
                }
            }
        }

        private static void drawPauseOverlay()
        {
            TextBlock bcgk = new TextBlock() 
            { 
                Background = Brushes.DarkGray,
                Width = MainWindow.mainCanvas.Width / 3,
                Height = MainWindow.mainCanvas.Height / 3,
                Text = "Gra paused"
            };
			Canvas.SetTop(bcgk, MainWindow.mainCanvas.Height / 3);
			Canvas.SetLeft(bcgk, MainWindow.mainCanvas.Width / 3);
            MainWindow.mainCanvas.Children.Add(bcgk);
		}

        private static void erasePauseOverlay()
		{
			foreach (UIElement el in MainWindow.mainCanvas.Children)
			{
				if (el.GetType() == typeof(TextBlock))
				{
					MainWindow.mainCanvas.Children.Remove(el);
					break;
				}
			}
		}
    }
}