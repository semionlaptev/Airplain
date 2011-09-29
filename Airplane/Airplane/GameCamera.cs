using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Airplane
{
    class GameCamera:PositionedObject
    {
        public Matrix Transform { get { return getTransform(); } }

        Matrix getTransform()
        {
            Matrix transform = Matrix.Identity *
                    Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                    Matrix.CreateRotationZ(Rotation) *
                    Matrix.CreateTranslation(Origin.X, Origin.Y, 0) *
                    Matrix.CreateScale(new Vector3(Scale, Scale, Scale));

            return transform;
        }

        public GameCamera(Vector2 position)
            :base(position)
        {

        }
    }
}
