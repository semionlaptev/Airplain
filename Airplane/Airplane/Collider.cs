using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Airplane
{
    public delegate void CollisionEventDelegate(DenseObject obj1, DenseObject obj2);

    public class Collider : GameObject, ICollider // check this huita
    {
        GameList leftList_ = new GameList();
        GameList rightList_ = new GameList();

        public IGameList LeftCollider { get { return leftList_; } }
        public IGameList RightCollider { get { return rightList_; } }

        public CollisionEventDelegate CollisionEvent { set; get; }

        public void CheckCollisions()
        {
            //check each with each objects not more than one time
            for (int i = 0; i < leftList_.Count(); i++)
            {
                for (int j = i + 1; j < leftList_.Count(); j++) //check in leftList
                {
                    checkCollisionBetween((DenseObject)leftList_[i], (DenseObject)leftList_[j]);
                }
                for (int j = 0; j < rightList_.Count(); j++)    //check in rightlist
                {
                    checkCollisionBetween((DenseObject)leftList_[i], (DenseObject)rightList_[j]);
                }
            }
        }

        protected void checkCollisionBetween(DenseObject obj1, DenseObject obj2)
        {
            ObjectHandler.Checks++;
            if (Rectangle.Intersect(obj1.CollisionRectPositionedScaled, obj2.CollisionRectPositionedScaled) != Rectangle.Empty)
            {
                if (CollisionEvent != null)
                    CollisionEvent(obj1, obj2);
            }
        }

        public long Count()
        {
            return rightList_.Count();
        }
    }
}
