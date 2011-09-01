﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Airplane
{
    /// <summary>
    /// A game layer. It stores GameObjects and its layer depth to draw.
    /// </summary>
    public class GameLayer : System.Collections.CollectionBase
    {
        public bool isVisible { set; get; }
        public float Rotation { set; get; }
        public Vector2 Position { set; get; }
        public float Depth { set; get; }
        public float Scale { set; get; }
        public Vector2 Speed { set; get; }

        public GameLayer()
        {
            Initialize();
        }

        protected void Initialize()
        {
            isVisible = true;
            Rotation = 0.0f;
            Position = new Vector2(0, 0);
            Depth = 0.0f;
            Scale = 1.0f;
            Speed = new Vector2(0,0);
        }

        public void addObject(GameObject obj)
        {
            List.Add(obj);
        }

        public void addObjects(GameObject[] objs)
        {
            foreach(GameObject obj in objs)
                List.Add(obj);
        }
    }
}
