using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airplane
{
    class CollidersManager:IGameList
    {
        List<ICollider> objectslist_ = new List<ICollider>();

        //private readonly static AnimationHandler instance_ = new AnimationHandler();

        public void AddCollider(ICollider collider)
        {
            objectslist_.Add(collider);
        }

        public void RemoveCollider(ICollider collider)
        {
            objectslist_.Remove(collider);
        }

        //IGameList impl.

        public void AddObject(GameObject obj)
        {
            AddCollider((ICollider)obj);
        }

        public void RemoveObject(GameObject obj)
        {
            RemoveCollider((ICollider)obj);
        }

        public void AddObjects(GameObject[] objs)
        {
            foreach (GameObject obj in objs)
            {
                AddCollider((ICollider)obj);
            }
        }

        public long Count()
        {
            return objectslist_.Count();
        }

        public void CheckCollisions()
        {
            foreach(ICollider collider in objectslist_)
            {
                collider.CheckCollisions();
            }
        }

        private static CollidersManager instance_ = null;
        private CollidersManager() { }
        public static CollidersManager Instance
        {
            get
            {
                if (instance_ == null)
                    instance_ = new CollidersManager();
                return instance_;
            }
        }

    }
}
