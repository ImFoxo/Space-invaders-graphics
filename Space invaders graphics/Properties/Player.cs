﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Space_invaders_graphics.Properties
{
    internal class Player
    {
        public double x { get; set; }
        public  double y { get; set; }
        public int health;
        private int damage;
        private float attackSpeed;
        private float missileSpeed;
        private float scoreMultiplier;
        public ImageBrush skin;
        public Rectangle model;
        public Rect hitbox;
        public bool isDead = false;
        private PlayerBonus bonus;
        private int score { get; set; }
        private static Player instance;

        private Player()
        {
            this.x = MainWindow.mainCanvas.Width / 2;
            this.y = MainWindow.mainCanvas.Height - 105;
            this.health = 3;
            this.damage = 1;
            this.attackSpeed = 2;
            this.missileSpeed = 6;
            this.scoreMultiplier = 1;
            this.skin = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/player.png")) };
            this.model = new Rectangle { Tag = "player", Fill = this.skin, Height = 65, Width = 55, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            this.hitbox = new Rect(x, y, model.Width, model.Height);
            this.isDead = false;
            this.bonus = null;
            this.score = 0;
        }

        public static Player getInstance()
        {
            if (instance == null) 
                instance = new Player();
            return instance;
        }
            
        public void move(string direction)
        {
            if (direction == "right" && x + 75 < MainWindow.mainCanvas.Width)
                x += 10;
            else if (direction == "left" && x > 0)
                x -= 10;
            hitbox.X = x;
        }

        public PlayerMissile shootMissile()
        {
            return new PlayerMissile(x + model.Width / 2, y, missileSpeed, damage);
        }

        public void addBonus()
        {

        }

        public void setBonusStrategy(PlayerBonus strategy)
        {

        }

        public void dealDamage(int dmg)
        {
            health -= dmg;
            if (health == 0) 
                isDead = true;
        }
    }
}
