using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Airplane
{
    public class AnimationSequence
    {
        #region Fields
        private List<Vector2> framesSequence_ = new List<Vector2>() {Vector2.Zero}; //?
        #endregion

        #region Init

        public AnimationSequence()
        {

        }

        public AnimationSequence(int row, List<int> cols)
        {
            framesSequence_ = new List<Vector2>();
            foreach (int col in cols)
            {
                framesSequence_.Add(new Vector2(col, row));
            }
        }
        public AnimationSequence(List<int> rows, int col)
        {
            framesSequence_ = new List<Vector2>();
            foreach (int row in rows)
            {
                framesSequence_.Add(new Vector2(col, row));
            }
        }

        public AnimationSequence(List<int> rows, List<int> cols)
        {
            if (rows.Count() != cols.Count())
            {
                throw new Exception("Lists have different length.");    //an exception in a constructor?
            }

            framesSequence_ = new List<Vector2>();
            int i = 0;
            foreach (int row in rows)
            {
                framesSequence_.Add(new Vector2(cols[i++], row));
            }
        }

        public AnimationSequence(List<Vector2> seq)
        {
            framesSequence_ = new List<Vector2>();
            foreach(Vector2 frame in seq)
                framesSequence_.Add(new Vector2(frame.Y,frame.X));
        }

        #endregion

        #region Methods
        public Vector2 getFrame(int i)
        {
            if (i >= Count() || i<0)
                throw new Exception("bad index.");
            return framesSequence_[i];
        }

        public int Count()
        {
            return framesSequence_.Count();
        }

        #endregion
    }
}
