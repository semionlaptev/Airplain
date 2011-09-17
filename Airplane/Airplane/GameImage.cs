using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Airplane
{
    public class GameImage: GameObject, IDrawable
    {
        private Texture2D image_;
        public Texture2D Image { private set { image_ = value; } get { return image_; } }

        Vector2 size_;
        public Vector2 ImageSize { 
            get {
                if (size_ == Vector2.Zero)
                    if (Image != null)
                        return new Vector2(Image.Width, Image.Height);
                    else
                        return Vector2.Zero;
                else
                    return size_;
            } 
            set {
                size_ = value;
            } 
        }

        public float Width { get { return ImageSize.X; }}
        public float Height { get { return ImageSize.Y; }}

        public float ImageScale { set; get; }
        public float ImageRotation { set; get; }

        public Vector2 ImageOrigin { set; get; }
        public virtual Rectangle SourceRect { set; get; }

        protected GameImage()
        {
            Initialize();
        }

        public GameImage(Texture2D img)
        {
            Initialize();
            Image = img;
            SourceRect = new Rectangle(0, 0, Image.Width, Image.Height);
        }

        public GameImage(Texture2D img, Vector2 size)
        {
            Initialize();
            Image = img;
            SourceRect = new Rectangle(0, 0, Image.Width, Image.Height);
            ImageSize = size;
        }

        private void Initialize()
        {
            ImageScale = 1.0f;
            ImageOrigin = Vector2.Zero;
            ImageRotation = 0.0f;

            SourceRect = new Rectangle(0, 0, 0, 0);
            size_ = Vector2.Zero;
            Image = null;
        }

    }
}
