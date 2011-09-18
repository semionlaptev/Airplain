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
    public class TriggerArea : DenseObject, IGameList, IEnumerable
    {
        Collider triggerCollider_ = new Collider();
        //friend
        TriggerArea()
            : base()
        {

        }

        public TriggerArea(Rectangle rect)
            : base(rect)
        {
            Initialize();
        }

        public void AddObject (GameObject obj)
        {
            triggerCollider_.AddObject((DenseObject)obj);
        }

        public void AddObjects(GameObject[] objs)
        {
            triggerCollider_.AddObjects(objs);
        }

        public void RemoveObject(GameObject obj)
        {
            triggerCollider_.RemoveObject((DenseObject)obj);
        }

        public new void Initialize()
        {
            base.Initialize();
            triggerCollider_.AddObject(this);
        }

        public IEnumerator GetEnumerator()
        {
            return triggerCollider_.GetEnumerator();
        }

        public void checkCollisions()
        {
            triggerCollider_.checkCollisions();
        }

        public double Count()
        {
            return triggerCollider_.Count();
        }
    }

}
