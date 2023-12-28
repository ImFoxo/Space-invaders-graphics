using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Space_invaders_graphics.Properties
{
    public class PlayerMissile : IMissile
    {
        public double x, y;
        private float speed;
        private int damage;
        public Rectangle model;
        public Rect hitbox;

        public PlayerMissile(double x, double y, float speed, int damage)
        {
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.damage = damage;
            this.model = new Rectangle { Tag = "playerMissile", Height = 20, Width = 5, Fill = Brushes.White,  Stroke = Brushes.Red    };
			this.hitbox = new Rect(x, y, model.Width, model.Height);
		}

		public int getDamage()
        {
            return damage;
        }

        public void move()
        {
            y -= speed;
            hitbox.Y = y;
        }
    }
}