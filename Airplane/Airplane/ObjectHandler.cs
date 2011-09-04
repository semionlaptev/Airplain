using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airplane
{
    class ObjectHandler
    {
        Dictionary<GameObject, List<IGameList>> objectslist_ = new Dictionary<GameObject, List<IGameList>>();

        public void addObjectToList(GameObject obj, IGameList list)
        {
            list.addObject(obj); //add to collider/layer/etc...
            if (objectslist_.ContainsKey(obj) == false)
                objectslist_[obj] = new List<IGameList>();
            objectslist_[obj].Add(list);  //save the link
        }
        public void removeObject(GameObject obj)
        {
            foreach (IGameList list in objectslist_[obj])
            {
                list.deleteObject(obj);       
            }
            objectslist_.Remove(obj);
        }
    }
}
