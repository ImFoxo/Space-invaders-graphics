using Space_invaders_console;
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
        static List<EnemyMissile> enemyMissiles = new List<EnemyMissile>();
        static List<Enemy> enemies = new List<Enemy>();

        static int bulletTimer = 90, bulletTimerLimit = 90;

        private static bool isPaused = false;

        static string playerMoveDirection;

        static int score = 0;
        static Label scoreLabel = new Label() { Content = "Score: " + score, Foreground = Brushes.White, FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P") };

        static int waveCount = 1;

        static DispatcherTimer gameTimer = new DispatcherTimer();

        //Zapisywanie wyniku
        static Label textLabel = new Label() { Content = "Enter your nickname:", Foreground = Brushes.White, FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"), };
        static TextBox textBox = new TextBox() { Width = 300, FontSize = 20, FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"), };
        static Button nameTextButton = new Button() 
        {
            Content = "Accept",
            Height = 20,
            Width = 150,
            FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
            Foreground = Brushes.Green,
            Background = Brushes.Transparent,
            FontSize = 20,
            BorderThickness = new Thickness(0),
        };

        public static void setGame()
        {
            score = 0;
            waveCount = 1;textBox.Text = "";
            playerMissiles.Clear();
            enemies.Clear();
            enemyMissiles.Clear();


            MainWindow.instance.KeyDown += KeyIsDown;
            MainWindow.instance.KeyUp += KeyIsUp;

			gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

			MainWindow.mainCanvas.Background = new SolidColorBrush(Colors.Black);
            Canvas.SetLeft(scoreLabel, 0);
            Canvas.SetTop(scoreLabel, 0);
            MainWindow.mainCanvas.Children.Add(scoreLabel);

            createEntities();
            player.health = 3;
            player.isDead = false;
		}

        private static void GameLoop(object sender, EventArgs e)
        {
            if (isPaused) 
                return;

            if (enemies.Count == 0)
            {
                spawnEnemies(6 + waveCount++);
                score += 10;
            }

            moveEntities();

            bulletTimer -= 3;
            if (bulletTimer < 0)
            {
                EnemyMissile newMissile = new EnemyMissile(player.x + 20, enemies.First().y);
                enemyMissiles.Add(newMissile);
                MainWindow.mainCanvas.Children.Add(newMissile.model);
                bulletTimer = bulletTimerLimit;
            }

            checkForCollisions();
            drawEntities();
            updateTopLabel();
            if (player.isDead) endGame();
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
            foreach (EnemyMissile em in enemyMissiles)
            {
                Canvas.SetLeft(em.model, em.x);
                Canvas.SetTop(em.model, em.y);
            }
            foreach (Enemy em in enemies)
            {
                if (em.x > 0)
                {
                    Canvas.SetLeft(em.model, em.x);
                    Canvas.SetTop(em.model, em.y); 
                }
			}
        }

        private static void moveEntities()
        {
            player.move(playerMoveDirection);
            foreach (PlayerMissile pm in playerMissiles)
            {
                pm.move();
            }
            foreach (EnemyMissile em in enemyMissiles)
            {
                em.move();
            }
            Enemy.checkIfEnemyOnEdge(enemies);
            foreach (Enemy en in enemies)
            {
                en.move();
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
                Canvas.SetTop(newEnemy.model, -1000);
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
            foreach(PlayerMissile pm in playerMissiles)
            {
                if (pm.y < 0)
                {
                    playerMissiles.Remove(pm);
                    MainWindow.mainCanvas.Children.Remove(pm.model);
                    break;
                }
            }
            foreach (EnemyMissile em in enemyMissiles)
            {
                if (em.y > MainWindow.mainCanvas.Height)
                {
                    enemyMissiles.Remove(em);
                    MainWindow.mainCanvas.Children.Remove(em.model);
                    break;
                }
                if (em.hitbox.IntersectsWith(player.hitbox))
                {
                    player.dealDamage(1);
                    enemyMissiles.Remove(em);
                    MainWindow.mainCanvas.Children.Remove(em.model);
                    break;
                }
            }
        }

        private static void drawPauseOverlay()
        {
            TextBlock bcgk = new TextBlock() 
            { 
                Width = MainWindow.mainCanvas.Width,
                Height = MainWindow.mainCanvas.Height,
                Text = "  Game paused",
                Opacity = 0.4,
                FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Press Start 2P"),
                Foreground = Brushes.Green,
                Background = Brushes.Black,
                FontSize = 50,
            };
			Canvas.SetTop(bcgk, 0);
			Canvas.SetLeft(bcgk, 0);
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

        public static void addScore() { score++; }

        private static void updateTopLabel()
        {
            scoreLabel.Content = "Score: " + score + "  Hp: " + Player.getInstance().health;
        }

        private static void endGame()
        {
            MainWindow.mainCanvas.Background = new SolidColorBrush(Colors.Transparent);
            MainWindow.instance.KeyDown -= KeyIsDown;
            MainWindow.instance.KeyUp -= KeyIsUp;
            MainWindow.mainCanvas.Children.Clear();
            gameTimer.Stop();
            gameTimer.Tick -= GameLoop;
            getPlayerName();
        }

        private static void getPlayerName()
        {
            MainWindow.instance.Background.Opacity = 0.3;
            Canvas.SetTop(textLabel, MainWindow.mainCanvas.Height / 3 - 50);
            Canvas.SetLeft(textLabel, MainWindow.mainCanvas.Width / 2 - 130);
            MainWindow.mainCanvas.Children.Add(textLabel);
            Canvas.SetTop(textBox, MainWindow.mainCanvas.Height / 3 );
            Canvas.SetLeft(textBox, MainWindow.mainCanvas.Width / 2 - 150);
            MainWindow.mainCanvas.Children.Add(textBox);
            Canvas.SetTop(nameTextButton, MainWindow.mainCanvas.Height / 3 + 50);
            Canvas.SetLeft(nameTextButton, MainWindow.mainCanvas.Width / 2 - 75);
            MainWindow.mainCanvas.Children.Add(nameTextButton);

            nameTextButton.Click += new RoutedEventHandler(onNameTextButtonClick);
        }

        private static void onNameTextButtonClick (object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length > 15 || textBox.Text.Length < 3 || textBox.Text.Contains(' '))
            {
                Canvas.SetLeft(textLabel, MainWindow.mainCanvas.Width / 2 - 220);
                textLabel.Content = "         Enter your nickname\n(min 3, max 15 characters, no space):";
            }
            else
            {
                nameTextButton.Click -= new RoutedEventHandler(onNameTextButtonClick);
                HighScore.updateScores(textBox.Text, score);
                MenuView.loadMenu();
            }
        }
    }
}