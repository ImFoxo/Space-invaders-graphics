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


        static string playerMoveDirection;

        static DispatcherTimer gameTimer = new DispatcherTimer();

        public static void setGame()
        {
            MainWindow.mainWindow.KeyDown += KeyIsDown;
            MainWindow.mainWindow.KeyUp += KeyIsUp;
            
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

            createEntities();

		}

        private static void GameLoop(object sender, EventArgs e)
        {
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
            MainWindow.mainWindow.mainCanvas.Children.Add(player.model);

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
                MainWindow.mainWindow.mainCanvas.Children.Add(newMissile.model);
            }
        }

		private static void spawnEnemies(int limit)
        {
            int left = 0;

            for (int i = 0; i < limit; i++)
            {
                Enemy newEnemy = new Enemy(left, 30, 3);
                enemies.Add(newEnemy);
                MainWindow.mainWindow.mainCanvas.Children.Add(newEnemy.model);
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
                        MainWindow.mainWindow.mainCanvas.Children.Remove(pm.model);
						break;
                    }
                }
                if (enemy.isDead)
                {
                    enemies.Remove(enemy);
                    MainWindow.mainWindow.mainCanvas.Children.Remove(enemy.model);
                    break;
                }
            }
        }
    }
}