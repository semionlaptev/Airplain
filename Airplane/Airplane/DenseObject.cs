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

using System.Diagnostics;

namespace Airplane
{

    /// <summary>
    /// An object that can interact with other "dense" objects. 
    /// </summary>
    /// <param name="CollisionRect">Rectangle that sets "sensitive" region. It is relative to the object position.</param>
    /// <param name="CollisionEvent">A delegate method that will be called if the collision happens</param>
    public class DenseObject : PositionedObject
    {
        #region Fields
        private Rectangle collisionRect_ = Rectangle.Empty;
        #endregion

        #region Properties
        public Rectangle CollisionRect { get { return collisionRect_; } set { collisionRect_ = value; } }
        public Rectangle CollisionRectPositionedScaled
        {
            get
            {
                return new Rectangle(
                    (int)(this.Position.X + this.CollisionRect.X),
                    (int)(this.Position.Y + this.CollisionRect.Y),
                    (int)(this.CollisionRect.Width * this.Scale),
                    (int)(this.CollisionRect.Height * this.Scale));
            }
        }
        public Vector2 SizeScaled
        {
            private set { }
            get
            {
                return new Vector2(CollisionRect.Width, CollisionRect.Height) * Scale;
            }
        }
        #endregion

        #region Initialization

        public DenseObject(Vector2 position, Rectangle rect) :
            base(position)
        {
            CollisionRect = rect;
        }

        public DenseObject(Rectangle rect):
            base(new Vector2(rect.X,rect.Y))
        {
            CollisionRect = new Rectangle(0, 0, rect.Width, rect.Height);         
        }

        public DenseObject(Vector2 position, Texture2D image):
            base(position)
        {
            CollisionRect = new Rectangle(0, 0, image.Width, image.Height);   
        }

        #endregion
    }
}
