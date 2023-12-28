using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace Space_invaders_graphics.Properties
{
    internal class Enemy
    {
		public double x { get; set; }
		public double y { get; set; }
		private int health;
		private int damage;
		private float attackSpeed;
		public ImageBrush skin;
		public Rectangle model;
		public Rect hitbox;
		public bool isDead;
		private static string moveDirection = "right";
		private static Random skinSelector = new Random();

		public Enemy(double x, double y, int hp)
		{
			string imgPath = String.Format("pack://application:,,,/Images/invader{0}.gif", skinSelector.Next(8) + 1);

			this.x = x;
			this.y = y;
			this.health = hp;
			this.damage = 1;
			this.attackSpeed = 2;
			this.skin = new ImageBrush { ImageSource = new BitmapImage(new Uri(imgPath)) };
			this.model = new Rectangle { Tag = "enemy", Fill = this.skin, Height = 45, Width = 45, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
			this.hitbox = new Rect(x, y, model.Width, model.Height);
			this.isDead = false;
		}

		public static void checkIfEnemyOnEdge(List<Enemy> el)
		{
			if (moveDirection == "right")
			{
				foreach (Enemy enemy in el)
					if (enemy.x + 55 >= MainWindow.mainWindow.Width)
					{
						moveDirection = "left";
						foreach (Enemy e in el)
						{
							e.y += 20;
							e.hitbox.Y = e.y;
						}
						break;
					}
			}
			else
			{
				foreach (Enemy enemy in el)
				{
					if (enemy.x - 10 < 0)
					{
						moveDirection = "right";
						foreach (Enemy e in el)
						{
							e.y += 20;
							e.hitbox.Y = e.y;
						}
						break;
					}
				}
			}
		}
		public void move()
		{
			if (moveDirection == "right")
				x += 5;
			else
				x -= 5;
			hitbox.X = x;
		}
		public void getHit(PlayerMissile missile)
		{
			health -= missile.getDamage();
			if (health < 0)
				isDead = true;
		}
	}
}
