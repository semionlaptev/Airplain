﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airplane
{
    public interface ICollider
    {
        void CheckCollisions();
        //void AddToLeftCollider(DenseObject obj);
        //void AddToRightCollider(DenseObject obj);
    }
}
