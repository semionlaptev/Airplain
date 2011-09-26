using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airplane
{
    static class Colisions
    {
        static public bool CheckCollision(DenseObject objOne, DenseObject objTwo)
        {
            return objOne.CollisionRectPositionedScaled.Intersects(objTwo.CollisionRectPositionedScaled);
        }

        static public List<DenseObject> CheckCollisions(DenseObject objOne, List<DenseObject> objects)
        {
            if (objects != null)
            {
                List<DenseObject> tempDenceObjects = new List<DenseObject>();
                foreach (DenseObject obj in objects)
                {
                    if (CheckCollision(objOne, obj))
                    {
                        tempDenceObjects.Add(obj);
                    }
                }

                if (tempDenceObjects.Count > 0)
                {
                    return tempDenceObjects;
                }
            }
                return null;
        }
    }
}
