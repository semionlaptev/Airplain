using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airplane
{
    /// <summary>
    /// singletone class with Positioned Object List
    /// </summary>
    class DenseObjectsList : IGameList
    {
        List<DenseObject> denseobjectlist = new List<DenseObject>();

        /// <summary>
        /// Add DenseObject into the DenseObjectList
        /// </summary>
        /// <param name="obj">DenseObject</param>
        public void AddObject(DenseObject obj)
        {
            if (obj == null)
                throw new Exception("Null object.");
            denseobjectlist.Add(obj);
        }
        public void AddObjects(DenseObject[] objs)
        {
            foreach (DenseObject obj in objs)
            {
                this.AddObject(obj);
            }
        }

        /// <summary>
        /// Метод позволяет получить обект по заданому тэгу, если такой объект есть, регистр тэга не учитывается.
        /// </summary>
        /// <param name="Tag">Тэг для поиска</param>
        /// <returns>Если объект с тэгом == Tag существует, возвращает объект с самым младшим ID, если объекта не существует возвращает null</returns>
        public DenseObject GetObjectByTag(string Tag)
        {
            foreach (DenseObject pobj in denseobjectlist)
            {
                if (pobj.Tag.ToUpper() == Tag.ToUpper())
                    return pobj;
            }
            return null;
        }

        /// <summary>
        /// Метод позволяет получить список обектов по заданому тэгу, если такой объект есть, регистр тэга не учитывается.
        /// </summary>
        /// <param name="Tag">Тэг для поиска</param>
        /// <returns>Если объекты с заданным Тоэгом существуют, то возвращается Список объектов, иначе null</returns>
        public List<DenseObject> GetObjectsByTag(string Tag)
        {
            List<DenseObject> tempDenseObj = new List<DenseObject>();
            foreach (DenseObject obj in denseobjectlist)
            {
                if (obj.Tag.ToUpper() == Tag.ToUpper())
                {
                    tempDenseObj.Add(obj);
                }
            }
            if (tempDenseObj.Count > 0)
            {
                return tempDenseObj;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Метод позволяет получить список обектов по заданным тэгам, если такие есть, регистр тэгов не учитывается.
        /// </summary>
        /// <param name="Tags">Тэги для поиска</param>
        /// <returns>Если объекты с заданными Тоэгом существуют, то возвращается список объектов, иначе null</returns>
        public List<DenseObject> GetObjectsByTags(string[] Tags)
        {
            List<DenseObject> tempDenseObj = new List<DenseObject>();
            foreach (string tag in Tags)
            {
                if (this.GetObjectsByTag(tag) != null)
                    tempDenseObj.AddRange(GetObjectsByTag(tag));
            }
            if (tempDenseObj.Count > 0)
            {
                return tempDenseObj;
            }
            else
            {
                return null;
            }
        }

        #region GetObjectsById
        /// <summary>
        /// Method return DenseObject from Global DenseObject list
        /// </summary>
        /// <param name="ID">Object ID from DenseObjectList[ID]</param>
        /// <returns> List{DenseObject}[ID] </returns>
        public DenseObject GetObjectById(int ID)
        {
            if (ID < 0 || ID > denseobjectlist.Count - 1)
            {
                throw new Exception("Wrong id " + ID.ToString());
            }
            return denseobjectlist[ID];
        }
        /// <summary>
        /// Method return DenseObjects from Global DenseObject list
        /// </summary>
        /// <param name="IDS">ID's of object for return</param>
        /// <returns>List{DenseObject}[ID]</returns>
        public List<DenseObject> GetObjectsById(int[] IDS)
        {
            List<DenseObject> tempPosObjList = new List<DenseObject>();
            foreach (int id in IDS)
            {
                tempPosObjList.Add(GetObjectById(id));
            }
            return tempPosObjList;
        }
        #endregion

        #region IGameListRealisation
        public void AddObject(GameObject obj)
        {
            if (obj == null)
                throw new Exception("Null object.");
            denseobjectlist.Add((DenseObject)obj);
        }

        public void AddObjects(GameObject[] objs)
        {
            if (objs == null)
                throw new Exception("Null array.");
            foreach (GameObject obj in objs)
                denseobjectlist.Add((DenseObject)obj);
        }

        public void RemoveObject(GameObject obj)
        {
            denseobjectlist.Remove((DenseObject)obj);
        }
        public long Count()
        {
            return denseobjectlist.Count;
        }
        #endregion

        #region Singleton
        private sealed class ListCreator
        {
            private static readonly DenseObjectsList instance = new DenseObjectsList();
            public static DenseObjectsList Instance { get { return instance; } }
        }
        public static DenseObjectsList Instance
        {
            get { return ListCreator.Instance; }
        }
        protected DenseObjectsList() {}
        #endregion
    }
}
