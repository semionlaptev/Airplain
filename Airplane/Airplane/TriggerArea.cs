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
    public class TriggerArea : DenseGameObject, ICollider
    {
        Collider triggerCollider_ = new Collider();

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

        public void addObject (DenseGameObject obj)
        {
             triggerCollider_.addObject(obj);
        }

        public void addObjects(DenseGameObject[] objs)
        {
            triggerCollider_.addObjects(objs);
        }

        public void deleteObject(DenseGameObject obj)
        {
            triggerCollider_.deleteObject(obj);
        }

        public void checkCollisions()
        {
            triggerCollider_.checkCollisions();
        }
    
    }

}
