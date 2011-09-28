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
        #region Fields
        private Vector2 position_ = Vector2.Zero;
        private float scale_ = 1.0f;
        private float rotation_ = 0.0f;
        private Vector2 speed_ = Vector2.Zero;
        private Vector2 origin_ = Vector2.Zero;
        #endregion

        #region Properties
        public Vector2 Position { get { return position_; } set { position_ = value; } }
        public float Scale { get { return scale_; } set { scale_ = value; } }
        public float Rotation { get { return rotation_; } set { rotation_ = value; } }
        public Vector2 Speed { get { return speed_; } set { speed_ = value; } }
        public Vector2 Origin { get { return origin_; } set { origin_ = value; } }
        //public Vector2 Origin { get { return new Vector2(origin_.X * Scale, origin_.Y*Scale); } set { origin_ = value; } } //TODO: test it with scale
        #endregion

        #region Initialization

        protected PositionedObject()
        {
        }

        public PositionedObject(Vector2 position)
        {
            Position = position;
        }

        #endregion

        #region Methods
        public virtual void Move()
        {
            Position += Speed;
        }
        public virtual void Move(Vector2 parent_speed)
        {
            Position += Speed+parent_speed;
        }
        #endregion

    }
}

