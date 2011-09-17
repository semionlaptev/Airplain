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

    class GameAnimation: GameImage, IDrawable
    {
        
        private Vector2 animationSize_; //(cols, rows)
        private int animationFPS_;
        private float animationSpeed_;
        private double lastDrawTime_;

        private int sequenceIndex_; //tmp
        private LOOP_TYPE looptype_;
        private int playDirection_;
        private Vector2 currentFrame_;
        public AnimationSequence currentSequence_;

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

        public GameAnimation(Texture2D img, int rows, int cols):
            base(img)
        {
            Initialize();

            base.ImageSize = new Vector2(base.ImageSize.X / cols, base.ImageSize.Y/ rows);
            animationSize_ = new Vector2(cols, rows);

            base.SourceRect = new Rectangle(0, 0, (int)base.ImageSize.X, (int)base.ImageSize.Y);
        }

        public void Play(AnimationSequence seq)
        {
            Play(seq, LOOP_TYPE.LOOP_NORMAL);
        }

        public void Play(AnimationSequence seq, LOOP_TYPE loop)
        {
            looptype_ = loop;
            currentSequence_ = seq;
            sequenceIndex_ = 0;
            playDirection_ = 1;

            if (loop == LOOP_TYPE.LOOP_BACKWARD)
            {
                playDirection_ = -1;
                sequenceIndex_ = seq.Count() - 1;
            }
        }

        public GameAnimation Copy()
        {
            return new GameAnimation(Image, (int)animationSize_.Y, (int)animationSize_.X) { };
        }

        public void Update(GameTime gameTime)
        {
            if (lastDrawTime_ >= animationSpeed_ || lastDrawTime_ == 0)
            {
                currentFrame_ = currentSequence_.getFrame(sequenceIndex_);
                sequenceIndex_ += playDirection_;

                if (sequenceIndex_ == currentSequence_.Count() || sequenceIndex_== -1) //animation sequence bounds reached
                {
                    switch (looptype_)
                    {
                        case LOOP_TYPE.LOOP_NORMAL:
                            sequenceIndex_ = 0;
                            break;
                        case LOOP_TYPE.LOOP_PINGPONG:
                            playDirection_ = -playDirection_;
                            sequenceIndex_ += playDirection_ + playDirection_;
                            break;
                        case LOOP_TYPE.PLAY_AND_HOLD:
                            sequenceIndex_ -= playDirection_;
                            playDirection_ = 0;
                            break;
                        case LOOP_TYPE.LOOP_BACKWARD:
                            sequenceIndex_ = currentSequence_.Count() - 1;
                            break;
                    }  
                }            

                lastDrawTime_ = 0;
          }

          lastDrawTime_ += gameTime.ElapsedGameTime.Milliseconds;
        }

        private void Initialize()
        {
            currentFrame_ = Vector2.Zero;
            animationSize_ = Vector2.Zero;
            currentSequence_ = new AnimationSequence();
            
            AnimationFPS = 2;

            playDirection_ = 1;
            sequenceIndex_ = 0;
            looptype_ = LOOP_TYPE.LOOP_NORMAL;
        }
    }
}
