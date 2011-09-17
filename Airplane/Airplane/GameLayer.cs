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
    /// TODO: Layer postions support. Proper objects scaling 
    public class GameLayer : PositionedObject, IGameList, IEnumerable
    {
        List<PositionedObject> objectlist_ = new List<PositionedObject>();

        public float Depth { set; get; }

        public GameLayer(float depth)
            : base()
        {
            Initialize();
            Depth = depth;
        }
        
        protected new void Initialize()
        {
            Depth = 0;
        }

        public void AddObject(GameObject obj)
        {
            if (obj == null)
                throw new Exception("Null object.");
            objectlist_.Add((PositionedObject)obj);
        }

        public void AddObjects(GameObject[] objs)
        {
            if (objs == null)
                throw new Exception("Null array.");
            foreach (GameObject obj in objs)
                objectlist_.Add((PositionedObject)obj);
        }

        public void RemoveObject(GameObject obj)
        {
            objectlist_.Remove((PositionedObject)obj);
        }

        //implementation of IEnumerable interface

        public IEnumerator GetEnumerator()
        {
            return objectlist_.GetEnumerator();
        }

        public override void Move(Vector2 parent_speed)
        {
            //base.Move(parent_speed);
            foreach (PositionedObject obj in objectlist_)
            {
                obj.Move(this.Speed+parent_speed);
            }
        }

        public override void Move()
        {
            //base.Move();
            foreach(PositionedObject obj in objectlist_)
            {
                obj.Move(this.Speed);
            }
        }

    }
}
