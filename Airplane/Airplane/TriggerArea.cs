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
    public class TriggerArea : DenseObject, IGameList, ICollider
    {
        #region Fields
        private Collider triggerCollider_;
        private Dictionary<DenseObject, bool> isInArea_ = new Dictionary<DenseObject, bool>();
        #endregion

        #region Properties
        public CollisionEventDelegate CollisionEvent { set { triggerCollider_.CollisionEvent = value; } get { return triggerCollider_.CollisionEvent; } }
        #endregion

        #region Initalization

        public TriggerArea(Rectangle rect, CollisionEventDelegate colliderevent)
            : base(rect)
        {
            triggerCollider_ = new Collider(colliderevent);
            Initialize();
        }

        public void Initialize()
        {
            triggerCollider_.LeftCollider.AddObject(this);
        }

        #endregion

        #region Methods
        public void AddDenseObject(DenseObject obj)
        {
            triggerCollider_.RightCollider.AddObject(obj);
            isInArea_[obj] = triggerCollider_.DoIntersect(this, obj);    //remember current state
        }

        public void RemoveDenseObject(DenseObject obj)
        {
            triggerCollider_.RightCollider.RemoveObject(obj);
        }

        #endregion

        #region ICollider implementation
        public void CheckCollisions()
        {
            triggerCollider_.CheckCollisions();
        }

        /*void AddToLeftCollider(DenseObject obj) //private
        {
            throw new NotSupportedException();
        }

        public void AddToRightCollider(DenseObject obj)
        {
            AddDenseObject(obj);
        }*/
        #endregion

        #region IGameList Implementation
        public void AddObject (GameObject obj)
        {
            AddDenseObject((DenseObject)obj);
        }

        public void AddObjects(GameObject[] objs)
        {
            foreach(GameObject obj in objs)
                AddDenseObject((DenseObject)obj);
        }

        public void RemoveObject(GameObject obj)
        {
            RemoveDenseObject((DenseObject)obj);
        }

        public long Count()
        {
            return triggerCollider_.Count();
        }

        #endregion

    }

}
