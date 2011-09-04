﻿using System;
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
    interface IGameList
    {
        void addObject(GameObject obj);
        void addObjects(GameObject[] objs);
        //void checkCollisions();
        void deleteObject(GameObject obj);
        //void checkCollisionBetween(DenseGameObject obj1, DenseGameObject obj2);
        //bool checkRectanglesCollision(Rectangle rect1, Rectangle rect2);
        //bool isPointInRectangle(Vector2 point, Rectangle rect);
    }
}