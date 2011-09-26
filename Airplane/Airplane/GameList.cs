using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airplane
{
    class GameList : IGameList
    {
        List<GameObject> list_ = new List<GameObject>();

        public GameList()
        {

        }

        public GameObject this[int i]
        {
            get { return list_[i]; }
        }

        public void AddObject(GameObject obj)
        {
            if (obj == null)
                throw new Exception("Null object.");
            list_.Add(obj);
        }

        public void AddObjects(GameObject[] objs)
        {
            if (objs == null)
                throw new Exception("Null array.");
            foreach (GameObject obj in objs)
                list_.Add(obj);
        }

        public void RemoveObject(GameObject obj)
        {
            list_.Remove(obj);
        }
        public long Count()
        {
            return list_.Count();
        }
    }
}
