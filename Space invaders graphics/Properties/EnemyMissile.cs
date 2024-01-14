using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Space_invaders_graphics.Properties
{
    public class EnemyMissile : IMissile
    {
        public double x, y;
        public Rectangle model;
        public Rect hitbox;

        public EnemyMissile(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.model = new Rectangle { Tag = "enemyMissile", Height = 20, Width = 5, Fill = Brushes.Purple };
			this.hitbox = new Rect(x, y, model.Width, model.Height);
		}



        public void move()
        {
            y += 6;
            hitbox.Y = y;
        }
    }
}