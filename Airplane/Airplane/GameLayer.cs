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

        #region Fields
        private List<PositionedObject> objectlist_ = new List<PositionedObject>();
        private float depth_ = 0.0f;
        #endregion

        #region Properties
        public float Depth { get { return depth_; } set { depth_ = value; } }
        #endregion

        #region Init
        public GameLayer(float depth)
        {
            Depth = depth;
        }
        #endregion

        #region Methods

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

        public void AddObject(PositionedObject obj)
        {
            if (obj == null)
                throw new Exception("Null object.");
            objectlist_.Add(obj);
        }

        public void RemoveObject(PositionedObject obj)
        {
            objectlist_.Remove(obj);
        }

        #endregion

        #region IGameList implementation

        public void AddObject(GameObject obj)
        {
            AddObject((PositionedObject)obj);
        }

        public void AddObjects(GameObject[] objs)
        {
            foreach (GameObject obj in objs)
                AddObject((PositionedObject)obj);
        }

        public void RemoveObject(GameObject obj)
        {
            RemoveObject((PositionedObject)obj);
        }

        //implementation of IEnumerable interface

        public IEnumerator GetEnumerator()
        {
            return objectlist_.GetEnumerator();
        }

        public long Count()
        {
            return objectlist_.Count();
        }

        #endregion

    }
}
