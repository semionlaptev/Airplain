using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airplane
{
    public class ImageHandler : IGameDictionary
    {
        #region Fields
        private Dictionary<PositionedObject, IDrawable> objectslist_ = new Dictionary<PositionedObject, IDrawable>();
        #endregion

        #region Methods
        public void LinkObjectAndImage(PositionedObject obj,GameImage image)
        {
            objectslist_[obj] = image;
        }

        public bool HasImage(PositionedObject obj)
        {
            return objectslist_.ContainsKey(obj);
        }

        public IDrawable GetImage(PositionedObject obj)
        {
            return objectslist_[obj];
        }

        public void RemoveObject(PositionedObject obj)
        {
            objectslist_.Remove(obj);
        }
        #endregion

        #region Singleton
        private static ImageHandler instance;
        private ImageHandler() { }
        public static ImageHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ImageHandler();
                }
                return instance;
            }
        }
        #endregion

        #region IGameDictionary implementation
        public void AddKeyVal(GameObject key, object val)
        {
            LinkObjectAndImage((PositionedObject)key,(GameImage)val); //baaad
        }

        public void RemoveByKey(GameObject key)
        {
            RemoveObject((PositionedObject)key);
        }

        public long Count()
        {
            return objectslist_.Count();
        }
        #endregion

    }
}
