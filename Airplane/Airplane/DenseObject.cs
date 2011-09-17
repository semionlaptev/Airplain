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

    public delegate void CollisionEventDelegate(DenseObject obj1, DenseObject obj2);

    /// <summary>
    /// An object that can interact with other "dense" objects. 
    /// </summary>
    /// <param name="CollisionRect">Rectangle that sets "sensitive" region. Is relative to the object position.</param>
    /// <param name="CollisionEvent">Delegate method that will be called if collision happenes</param>
    public class DenseObject : PositionedObject
    {
        public Rectangle CollisionRect { set; get; }
        public CollisionEventDelegate CollisionEvent { get; set; }

        public Vector2 SizeScaled
        {
            private set { }
            get
            {
                return new Vector2(CollisionRect.Width, CollisionRect.Height) * Scale;
            }
        }

        protected DenseObject() : base()
        {

        }

        public DenseObject(Vector2 position, Rectangle rect) :
            base(position)
        {
            Initialize();
            CollisionRect = rect;
        }

        public DenseObject(Rectangle rect):
            base(new Vector2(rect.X,rect.Y))
        {
            Initialize();
            CollisionRect = new Rectangle(0, 0, rect.Width, rect.Height);         
        }


        protected new void Initialize() 
        {
            CollisionEvent = null;
        }
    }
}
