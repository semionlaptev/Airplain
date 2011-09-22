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
    public class TriggerArea : DenseObject, IGameList//, IEnumerable
    {
        Collider triggerCollider_ = new Collider();

        public CollisionEventDelegate CollisionEvent { set { triggerCollider_.CollisionEvent = value; } get { return triggerCollider_.CollisionEvent; } }

        TriggerArea()
            : base()
        {
            Initialize();
        }

        public TriggerArea(Rectangle rect)
            : base(rect)
        {
            Initialize();
        }

        public void AddObject (GameObject obj)
        {
            triggerCollider_.RightCollider.AddObject((DenseObject)obj);
        }

        public void AddObjects(GameObject[] objs)
        {
            triggerCollider_.RightCollider.AddObjects(objs);
        }

        public void RemoveObject(GameObject obj)
        {
            triggerCollider_.RightCollider.RemoveObject((DenseObject)obj);
        }

        public new void Initialize()
        {
            base.Initialize();
            triggerCollider_.LeftCollider.AddObject(this);
        }

        /*public IEnumerator GetEnumerator()
        {
            return triggerCollider_.GetEnumerator();
        }*/

        public void CheckCollisions()
        {
            triggerCollider_.CheckCollisions();
        }

        public double Count()
        {
            return triggerCollider_.Count();
        }
    }

}
