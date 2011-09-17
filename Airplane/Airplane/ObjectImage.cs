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
   /* public enum LOOP_TYPE
    {
        LOOP_NONE = 0,
        LOOP_NORMAL = 1,
        LOOP_PINGPONG = 2,
        LOOP_BACKWARD = 3
    };

    public abstract class GameAnimation
    {

        public Vector2 Size { get { return frameSize_; } set{frameSize_ = value;} }
        public float Width { get { return Size.X; } private set { } }
        public float Height { get { return Size.Y; } private set { } }
        public int AnimationFPS { 
            get { 
                return animationFPS_; 
            } 
            set { 
                animationFPS_ = value; 
                animationSpeed_ = 1.0f / animationFPS_ * 1000; 
            } 
        }
        public Rectangle SourceRect
        {
            get
            {
                return new Rectangle(
                    (int)(Size.X * currentFrame_.X),
                    (int)(Size.Y * currentFrame_.Y),
                    (int)(Size.X),
                    (int)(Size.Y));
            }
        }

        private Vector2 frameSize_;
        private int frameRows_;
        private int frameCols_;
        private Point currentFrame_;
        private int animationFPS_;
        private float animationSpeed_;
        private double lastDrawTime_;

        //private List<Point> currentSequence_;
        //private Dictionary<string, AnimationSequence> animations_ = new Dictionary<string, AnimationSequence>();

        public GameAnimation()
        {
            Initialize();
        }

        /// <summary>
        /// Constructor that sets the image as a static sprite.
        /// </summary>
        /// <param name="img">Image</param>
        public GameAnimation(Texture2D img):
            base(img)
        {
            Initialize();
        }

        /// <summary>
        /// Constructor that sets the image as the animated sprite.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="rows">Number of rows in the image.</param>
        /// <param name="cols">Number of columns in the image.</param>
        public GameAnimation(Texture2D image, int rows, int cols)
        {
            Initialize();
            Image = image;
            SetAnimation(rows, cols);
        }

        void Initialize()
        {
            //default initialization for a static sprite
            frameRows_ = 1;
            frameCols_ = 1;
            currentFrame_ = new Point(0, 0);

            //it is very sad the it is neccessary to check fps when the image is a static sprite
            animationFPS_ = 8;
            animationSpeed_ = 1.0f / animationFPS_ * 1000;
        }

        /// <summary>
        /// Set the number of frames in the image.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="cols">Number of cols.</param>
        public void SetAnimation(int rows, int cols)
        {
            if (rows > 0 && cols > 0)
            {
                frameRows_ = rows;
                frameCols_ = cols;
                //frame size 
                Size = new Vector2(Image.Width / cols,Image.Height / rows);
            }
            else
            {
                throw new Exception("GameAnimation::SetAnimationFrames: wrong parametres.");
            }
        }
        
        /*public void AddAnimation(string sequencename, AnimationSequence sequence)
        {
            animations_[sequencename] = sequence;        
        }

    public void PlaySequence(List<Point> frameSeqence)
    {
       // currentSequence_ = frameSeqence;
    }

    /// <summary>
    /// Go next frame or don't go next frame if it is not the time.
    /// </summary>
    /// <param name="gameTime">gametime</param>
    public void Update(GameTime gameTime)
    {
        if (lastDrawTime_ >= animationSpeed_ || lastDrawTime_ == 0)
        {

            currentFrame_.X++;

            if (currentFrame_.X == frameCols_ - 1)
            {
                currentFrame_.X = 0;
            }

            lastDrawTime_ = 0;
        }

        lastDrawTime_ += gameTime.ElapsedGameTime.Milliseconds;
    } }
*/
}
