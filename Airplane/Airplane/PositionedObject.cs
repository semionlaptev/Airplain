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
    /// <summary>
    /// An ingame object.
    /// </summary>
    /// <param name="Position">A 2D vector.</param>
    /// <param name="Size">Gets or sets a 2D vector that stores width and height of an object's image. By default it is the same as the loaded image size.</param>
    /// <param name="Image">A </param>
    public abstract class PositionedObject: GameObject
    {
        
        public Vector2 Position {get;set;} 
        public float Scale { set; get; }
        public float Rotation { set; get; }
        public Vector2 Speed { get; set; }

        public PositionedObject()
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

