﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airplane
{
    public delegate void ObjectHandlerDictionaryDelegate<T,K>(T key, K val);
    class ObjectHandler //: IGameDictionary
    {

        private ObjectHandler() {}
        private static ObjectHandler instance_ = null;

        public static int Checks {set;get;}

        Dictionary<GameObject, List<IGameList>> objectslists_ = new Dictionary<GameObject, List<IGameList>>();
        Dictionary<GameObject, List<IGameDictionary>> objectsdicts_ = new Dictionary<GameObject, List<IGameDictionary>>();

        public void AddObjectToList(GameObject obj, IGameList list)
        {
            list.AddObject(obj); //add to collider/layer/etc...
            if (objectslists_.ContainsKey(obj) == false)
                objectslists_[obj] = new List<IGameList>();
            objectslists_[obj].Add(list);  //save the link
        }
        public void RemoveObjectFromList(GameObject obj)
        {
            foreach (IGameList list in objectslists_[obj])
            {
                list.RemoveObject(obj);       
            }
            objectslists_.Remove(obj);
        }

        public void AddKeyValToDictionary(GameObject objkey, object objval, IGameDictionary dict)
        {
            dict.AddKeyVal(objkey, objval); //add to IGameDictionary game object (ImageHandler)
            if (objectsdicts_.ContainsKey(objkey) == false)
                objectsdicts_[objkey] = new List<IGameDictionary>();
            objectsdicts_[objkey].Add(dict);
        }

        /// <summary>
        /// Add to dictionary over the specified method.
        /// </summary>
        /// <typeparam name="T">Key type (must be derived from GameObject).</typeparam>
        /// <typeparam name="K">Value type.</typeparam>
        /// <param name="objkey">Key</param>
        /// <param name="objval">Value</param>
        /// <param name="dict">Dictionary object to add in (must implement IGameDictionary)</param>
        /// <param name="method">The dictionary method to use for inserting.</param>
        public void AddKeyValToDictionary<T,K>(T objkey, K objval, IGameDictionary dict, ObjectHandlerDictionaryDelegate<T,K> method) where T: GameObject
        {
            method(objkey, objval); //add to IGameDictionary game object (ImageHandler)
            if (objectsdicts_.ContainsKey(objkey) == false)
                objectsdicts_[objkey] = new List<IGameDictionary>();
            objectsdicts_[objkey].Add(dict);
        }

        public void RemoveObjectFromDictonary(GameObject obj)
        {
            foreach (IGameDictionary dict in objectsdicts_[obj])
            {
                dict.RemoveByKey(obj);
            }
            objectsdicts_.Remove(obj);
        }

        public static ObjectHandler instance
        {
            get
            {
                if (instance_ == null)
                {
                    instance_ = new ObjectHandler();
                }
                return instance_;
            }
        }
    }
}
