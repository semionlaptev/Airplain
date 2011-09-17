using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Airplane
{
    public class PositionedObject: GameObject
    {
        
        public Vector2 Position {get;set;} 
        public float Scale { set; get; }
        public float Rotation { set; get; }
        public Vector2 Speed { get; set; }

        public Vector2 Origin { get; set; }
        //private Vector2 origin_;
        //public Vector2 Origin { get { return new Vector2(origin_.X * Scale, origin_.Y*Scale); } set { origin_ = value; } } //TODO: test it with scale

        protected PositionedObject()
        {
            Initialize();
        }

        public PositionedObject(Vector2 position)
        {
            Initialize();
            Position = position;
        }

        protected void Initialize()
        {
                       
            Scale = 1.0f;
            Rotation = 0.0f;
            Position = Vector2.Zero;
            Speed = Vector2.Zero;
            Origin = Vector2.Zero;            
        }

        public virtual void Move()
        {
            Position += Speed;
        }
        public virtual void Move(Vector2 parent_speed)
        {
            Position += Speed+parent_speed;
        }

    }
}

