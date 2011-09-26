using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace Airplane
{
    class TextureManager
    {
        Dictionary<string, Texture2D> objectsdict_ = new Dictionary<string, Texture2D>();


        public Texture2D this[string name]
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
    }
}
