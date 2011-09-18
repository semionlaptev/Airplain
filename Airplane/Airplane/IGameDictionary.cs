using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airplane
{
    interface IGameDictionary
    {
        void AddKeyVal(GameObject keyobj, GameObject valobj);
        void RemoveByKey(GameObject keyobj);
        double Count();
    }
}
