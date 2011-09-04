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
        public TriggerArea(Vector2 position)
            : base(position)
        {
            Initialize();
        }

        public TriggerArea(Rectangle rect)
            : base(rect)
        {
            Initialize();
        }

        public TriggerArea(Vector2 position, Texture2D texture)
            : base(position, texture)
        {
            Initialize();
        }

        public TriggerArea(Rectangle rect, Texture2D texture)
            : base(rect, texture)
        {
            Initialize();
        }

        public void addObject (GameObject obj)
        {
            triggerCollider_.addObject((DenseObject)obj);
        }

        public void addObjects(GameObject[] objs)
        {
            triggerCollider_.addObjects(objs);
        }

        public void deleteObject(GameObject obj)
        {
            triggerCollider_.deleteObject((DenseObject)obj);
        }

        public new void Initialize()
        {
            base.Initialize();
            triggerCollider_.addObject(this);
        }

        public IEnumerator GetEnumerator()
        {
            return triggerCollider_.GetEnumerator();
        }

        public void checkCollisions()
        {
            triggerCollider_.checkCollisions();
        }
    
    }

}
