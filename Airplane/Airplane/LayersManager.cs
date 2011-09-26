using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airplane
{
    class LayersManager :  IEnumerable
    {
        Dictionary<string, GameLayer> objectsdict_ = new Dictionary<string, GameLayer>();

        public GameLayer CreateLayer(string name, float depth)
        {
            if (objectsdict_.ContainsKey(name) == false)
            {
                objectsdict_[name] = new GameLayer(depth);
            }

            return objectsdict_[name];
        }

        public void AddToLayer(string name, PositionedObject obj)
        {
            if (objectsdict_.ContainsKey(name))
            {
                objectsdict_[name].AddObject(obj);
            }
            else
            {
                throw new Exception("Wrong layer name.");
            }
        }

        public GameLayer this[string name]
        {
            get { return objectsdict_[name]; }
            set { objectsdict_[name] = value; }
        }

        public IEnumerator GetEnumerator()
        {
            return objectsdict_.GetEnumerator();
        }

        public long Count()
        {
            return objectsdict_.Count();
        }

        private static LayersManager instance;
        private LayersManager() { }
        public static LayersManager Instance
        {
             get 
            {
                if (instance == null)
                 {
                     instance = new LayersManager();
                 }
                 return instance;
            }
        }

    }
}
