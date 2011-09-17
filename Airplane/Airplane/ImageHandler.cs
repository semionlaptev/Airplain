using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airplane
{
    public class ImageHandler : IGameDictionary
    {

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

        Dictionary<PositionedObject, IDrawable> objectslist_ = new Dictionary<PositionedObject, IDrawable>();

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


        //IGameDictionary interface implementation
        public void AddKeyVal(GameObject key, GameObject val)
        {
            LinkObjectAndImage((PositionedObject)key,(GameImage)val); //baaad
        }

        public void RemoveByKey(GameObject key)
        {
            RemoveObject((PositionedObject)key);
        }
    }
}
