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

    public class Collider : GameObject, ICollider
    {
        #region Fields
        
        private GameList<DenseObject> leftList_ = new GameList<DenseObject>();
        private GameList<DenseObject> rightList_ = new GameList<DenseObject>();
        private CollisionEventDelegate collisionEvent_ = null;

        #endregion

        #region Properties

        public IGameList LeftCollider { get { return leftList_; } }
        public IGameList RightCollider { get { return rightList_; } }
        public CollisionEventDelegate CollisionEvent { get { return collisionEvent_; } set { collisionEvent_ = value; } }

        #endregion

        #region Methods

        /// <summary>
        /// Checks collisions between object in the lists
        /// </summary>
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

        /// <summary>
        /// Check if two objects intersect each other and calls CollisionEvent if true.
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        protected void checkCollisionBetween(DenseObject obj1, DenseObject obj2)
        {
            ObjectReferencesHandler.Checks++;
            if (DoIntersect(obj1,obj2))
            {
                //Rectangle rect = Rectangle.Intersect(obj1.CollisionRectPositionedScaled, obj2.CollisionRectPositionedScaled);
                if (CollisionEvent != null)
                    CollisionEvent(obj1, obj2);
            }
        }

        /// <summary>
        /// Returns true if two objects specified intersect each other
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public bool DoIntersect(DenseObject obj1, DenseObject obj2)
        {
            return Rectangle.Intersect(obj1.CollisionRectPositionedScaled, obj2.CollisionRectPositionedScaled) != Rectangle.Empty;
        }

        public long Count()
        {
            return rightList_.Count();
        }
        #endregion

    }
}
