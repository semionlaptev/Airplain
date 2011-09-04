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
    public class RealObject:PositionedObject
    {
        public bool IsVisible { get; set; }
        public Texture2D Image { set; get; }

        Vector2 size_;

        public Vector2 Size
        {
            get
            {
                if (size_ == Vector2.Zero)
                    if (Image != null)
                        return new Vector2(Image.Width, Image.Height);
                    else
                        return Vector2.Zero;
                else
                    return size_;
            }
            set
            {
                size_ = value;
            }
        }

        public RealObject()
            : base()
        {
            Initialize();
        }

        public RealObject(Vector2 position) :
            base(position)
        {
            Initialize();
        }

        public RealObject(Rectangle rect)
        {
            Initialize();

            Position = new Vector2(rect.X, rect.Y);
            Size = new Vector2(rect.Width, rect.Height);
        }

        public RealObject(Vector2 position, Texture2D texture)
        {
            Initialize();
            Position = position;
            Image = texture;
        }

        public RealObject(Rectangle rect, Texture2D texture)
        {
            Initialize();

            Position = new Vector2(rect.X, rect.Y);
            Size = new Vector2(rect.Width, rect.Height);
            Image = texture;
        }

        protected new void Initialize()
        {
            base.Initialize();
            IsVisible = true;
            size_ = Vector2.Zero;
        }
    }
}
