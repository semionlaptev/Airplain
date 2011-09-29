using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airplane
{
    class GameList<T> : IGameList where T:GameObject
    {
        #region Fields
        private List<T> list_ = new List<T>();
        #endregion

        #region Init
        public GameList()
        {

        }
        #endregion

        #region Methods
        public T this[int i]
        {
            get { return list_[i]; }
        }

        void AddObject(T obj)   //private?
        {
            if (obj == null)
                throw new Exception("Null object.");
            list_.Add(obj);
        }

        public void RemoveObject(T obj)
        {
            list_.Remove(obj);
        }

        #endregion

        #region IGameList implementation
        public void AddObject(GameObject obj)
        {
            AddObject((T)obj);
        }

        public void AddObjects(GameObject[] objs)
        {
            if (objs == null)
                throw new Exception("Null array.");
            foreach (GameObject obj in objs)
                AddObject((T)obj);
        }

        public void RemoveObject(GameObject obj)
        {
            RemoveObject((T)obj);
        }

        public long Count()
        {
            return list_.Count();
        }
        #endregion

    }
}
