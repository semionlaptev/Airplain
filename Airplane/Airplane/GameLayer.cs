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
    /// <summary>
    /// A game layer. It contains GameObjects and its layer depth to draw.
    /// </summary>
    /// TODO: Layer speed support. Proper objects scaling 
    public class GameLayer :  IEnumerable
    {
        List<GameObject> objectlist_ = new List<GameObject>();

        public float Depth { set; get; }
        
        public GameLayer():
            base()
        {
            Initialize();
        }
        
         public GameLayer(Vector2 position)
        {
            Initialize();
        }

        public GameLayer(Rectangle rect)
        {
            Initialize();
        }

        public GameLayer(Vector2 position, Texture2D texture)
        {
            Initialize();
        }

        public GameLayer(Rectangle rect, Texture2D texture)
        {
            Initialize();
        }

        protected void Initialize()
        {
            Depth = 0.0f;
        }

        public void addObject(GameObject obj)
        {
            if (obj == null)
                throw new Exception("Null object.");
            objectlist_.Add(obj);
        }

        public void addObjects(GameObject[] objs)
        {
            if (objs == null)
                throw new Exception("Null array.");
            foreach(GameObject obj in objs)
                objectlist_.Add(obj);
        }

        public void deleteObject(GameObject obj)
        {
            objectlist_.Remove(obj);
        }

        //implementation of IEnumerable interface

        public IEnumerator GetEnumerator()
        {
            return objectlist_.GetEnumerator();
        }

    }
}
