using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airplane
{
    class GameList : IGameList//, new()
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
        public double Count()
        {
            return list_.Count();
        }
    }

    class GameList<T>// : IGameList<T> where T : GameObject//, new()
    {
        List<T> list_ = new List<T>();

        public GameList()
        {

        }

        public T this[int i]
        {
            get { return list_[i]; }
        }

        public void AddObject(T obj)
        {
            if (obj == null)
                throw new Exception("Null object.");
            list_.Add(obj);
        }

        public void AddObjects(T[] objs)
        {
            if (objs == null)
                throw new Exception("Null array.");
            foreach (T obj in objs)
                list_.Add(obj);
        }

        public void RemoveObject(T obj)
        {
            list_.Remove(obj);
        }
        public double Count()
        {
            return list_.Count();
        }
    }
}
