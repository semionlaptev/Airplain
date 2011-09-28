using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace Airplane
{
    class TextureManager//:IGameList (?)
    {
        #region Fields
        private Dictionary<string, Texture2D> objectsdict_ = new Dictionary<string, Texture2D>();
        #endregion

        #region Methods

        public Texture2D this[string name]
        {
            get
            {
                return GetTexture(name);
            }
            set
            {
                AddTexture(name, value);
            }
        }

        public void AddTexture(string name, Texture2D texture)
        {
            if (texture != null)
                objectsdict_[name] = texture;
            else
                throw new Exception("No texture loaded.");
        }
        public Texture2D GetTexture(string name)
        {
            if (objectsdict_.ContainsKey(name))
            {
                return objectsdict_[name];
            }
            else
            {
                throw new Exception("Wrong texture name.");
            }
        }

        public IEnumerator GetEnumerator()
        {
            return objectsdict_.GetEnumerator();
        }

        public long Count()
        {
            return objectsdict_.Count();
        }

        #endregion

        #region Singleton
        private static TextureManager instance;
        private TextureManager() { }
        public static TextureManager Instance
        {
             get 
            {
                if (instance == null)
                 {
                     instance = new TextureManager();
                 }
                 return instance;
            }
        }
        #endregion
    }
}
