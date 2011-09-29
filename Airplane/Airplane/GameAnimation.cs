using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Airplane
{
    public enum LOOP_TYPE
    {
        LOOP_NORMAL = 0,
        LOOP_PINGPONG = 1,
        LOOP_BACKWARD = 2,
        PLAY_AND_HOLD = 3
    };

    public class GameAnimation: GameImage, IDrawable
    {
        #region Fields

        private Vector2 animationSize_ = Vector2.Zero; //(cols, rows)
        private int animationFPS_;  //default = 2
        private float animationSpeed_;
        private double lastDrawTime_;

        private int sequenceIndex_ = 0; //tmp

        private LOOP_TYPE looptype_ = LOOP_TYPE.LOOP_NORMAL;
        private int playDirection_ = 1;
        private Vector2 currentFrame_ = Vector2.Zero;
        private AnimationSequence currentSequence_ = new AnimationSequence();
        private Dictionary<string, AnimationSequence> animations_ = new Dictionary<string, AnimationSequence>();
        #endregion

        #region Properties
        public int AnimationFPS
        {
            get
            {
                return animationFPS_;
            }
            set
            {
                animationFPS_ = value;
                animationSpeed_ = 1.0f / animationFPS_ * 1000;
            }
        }
        
        public override Rectangle SourceRect
        {
            get
            {
                return new Rectangle(
                    (int)(base.Width * currentFrame_.X),
                    (int)(base.Height * currentFrame_.Y),
                    (int)(base.Width),
                    (int)(base.Height));
            }
        }

        public Dictionary<string, AnimationSequence> Animations { get { return animations_; } }
        #endregion

        #region Init
        public GameAnimation(Texture2D img, int rows, int cols):
            base(img)
        {
            Initialize();

            base.ImageSize = new Vector2(base.ImageSize.X / cols, base.ImageSize.Y/ rows);
            animationSize_ = new Vector2(cols, rows);

            base.SourceRect = new Rectangle(0, 0, (int)base.ImageSize.X, (int)base.ImageSize.Y);
        }

        private void Initialize()
        {
            AnimationFPS = 12;
        }
        #endregion

        #region Methods

        void Play(AnimationSequence seq)
        {
            Play(seq, LOOP_TYPE.LOOP_NORMAL);
        }

        void Play(AnimationSequence seq, LOOP_TYPE loop)
        {
            looptype_ = loop;
            currentSequence_ = seq;
            setupAnimation();
        }

        void setupAnimation()
        {
            if (looptype_ == LOOP_TYPE.LOOP_BACKWARD)
            {
                playDirection_ = -1;
                sequenceIndex_ = currentSequence_.Count() - 1;
            }
            else
            {
                sequenceIndex_ = 0;
                playDirection_ = 1;
            }
        }

        public void Play(string seqname, LOOP_TYPE loop)
        {
            if (animations_.ContainsKey(seqname) == false)
            {
                throw new Exception("The specified animation has not been found.");
            }
            else
            {
                Play(animations_[seqname], loop);
            }
        }

        public GameAnimation Copy()
        {
            return new GameAnimation(Image, (int)animationSize_.Y, (int)animationSize_.X) { };
        }

        public void Update(GameTime gameTime)
        {
            if (lastDrawTime_ >= animationSpeed_ || lastDrawTime_==0)
            {
                goNextFrame();
                lastDrawTime_ = 0;
          }

          lastDrawTime_ += gameTime.ElapsedGameTime.Milliseconds;
        }

        void goNextFrame()
        {
            currentFrame_ = currentSequence_.getFrame(sequenceIndex_);  //starts from the first sequence frame
            if (sequenceIndex_ + playDirection_ == currentSequence_.Count() || sequenceIndex_ + playDirection_ == -1) //if animation sequence bounds exceed
            {
                //choose next frame
                switch (looptype_)
                {
                    case LOOP_TYPE.LOOP_NORMAL:
                        sequenceIndex_ = 0; //go zero frame
                        break;
                    case LOOP_TYPE.LOOP_PINGPONG:   //go backward
                        if (currentSequence_.Count() > 1)
                        {
                            playDirection_ = -playDirection_;
                            sequenceIndex_ += playDirection_;
                        }
                        else
                            sequenceIndex_ = 0;
                        break;
                    case LOOP_TYPE.PLAY_AND_HOLD:   //freeze the last frame
                        playDirection_ = 0;
                        break;
                    case LOOP_TYPE.LOOP_BACKWARD:   //
                        sequenceIndex_ = currentSequence_.Count()-1;
                        break;
                }
            }
            else
            {
                sequenceIndex_ += playDirection_;   //go next
            }
            
        }

        #endregion

    }
}
