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
        #region Fields
        private Texture2D image_ = null;
        private Vector2 imageSize_ = Vector2.Zero;
        private float imageScale_ = 1.0f;
        private float imageRotation_ = 0.0f;
        private Vector2 imageOrigin_ = Vector2.Zero;
        private Rectangle sourceRect_ = Rectangle.Empty;
        #endregion

        #region Properties
        public Texture2D Image { private set { image_ = value; } get { return image_; } }
        public Vector2 ImageSize { 
            get {
                if (imageSize_ == Vector2.Zero)
                    if (Image != null)
                        return new Vector2(Image.Width, Image.Height);
                    else
                        return Vector2.Zero;
                else
                    return imageSize_;
            } 
            set {
                imageSize_ = value;
            } 
        }

        public float Width { get { return ImageSize.X; }}
        public float Height { get { return ImageSize.Y; }}
        public float ImageScale { get { return imageScale_; } set { imageScale_ = value; } }
        public float ImageRotation { get { return imageRotation_; } set { imageRotation_ = value; } }
        public Vector2 ImageOrigin { get { return imageOrigin_; } set { imageOrigin_ = value; } }
        public virtual Rectangle SourceRect {  get{return sourceRect_;} set {sourceRect_ = value;}}
        #endregion

        #region Init

        protected GameImage()
        {
        }

        public GameImage(Texture2D img)
        {
            Image = img;
            SourceRect = new Rectangle(0, 0, Image.Width, Image.Height);
        }

        public GameImage(Texture2D img, Vector2 size)
        {
            Image = img;
            SourceRect = new Rectangle(0, 0, Image.Width, Image.Height);
            ImageSize = size;
        }
        #endregion

    }
}
