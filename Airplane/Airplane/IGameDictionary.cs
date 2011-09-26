using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airplane
{
    interface IGameDictionary
    {
        void AddKeyVal(GameObject keyobj, object valobj);
        void RemoveByKey(GameObject keyobj);
        long Count();
    }
}
