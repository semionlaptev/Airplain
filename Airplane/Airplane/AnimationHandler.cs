using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Airplane
{
    class AnimationHandler : IGameList
    {
        #region Fields
        private List<GameAnimation> objectslist_ = new List<GameAnimation>();
        private static AnimationHandler instance_ = null;
        #endregion

        #region Singleton
        private AnimationHandler() { }
        public static AnimationHandler Instance
        {
            get
            {
                if (instance_ == null)
                    instance_ = new AnimationHandler();
                return instance_;
            }
        }
        #endregion

        #region Methods
        void AddAnimation(GameAnimation anim)
        {
            if(objectslist_.Contains(anim) == false)
                objectslist_.Add(anim);
            else
                throw new Exception("Already in list.");
        }

        void RemoveAnimation(GameAnimation anim)
        {
            objectslist_.Remove(anim);
        }

        public void Update(GameTime gameTime)
        {
            foreach (GameAnimation anim in objectslist_)
            {
                anim.Update(gameTime);
            }
        }
        #endregion

        #region IGameList implementation

        public void AddObject(GameObject obj)
        {
            AddAnimation((GameAnimation)obj);
        }

        public void RemoveObject(GameObject obj)
        {
            RemoveAnimation((GameAnimation)obj);
        }

        public void AddObjects(GameObject[] objs)
        {
            foreach (GameObject obj in objs)
            {
                AddAnimation((GameAnimation)obj);
            }
        }

        public long Count()
        {
            return objectslist_.Count();
        }
        #endregion

    }
}
