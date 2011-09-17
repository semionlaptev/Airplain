using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Airplane
{
    public interface IDrawable
    {
        Texture2D Image { get; }
        float Width { get; }
        float Height { get; }
        float ImageScale { get; }
        Rectangle SourceRect { get; }
        float ImageRotation { get; }
        Vector2 ImageOrigin { get; }
        Vector2 ImageSize { get; }
        //void donotuse();
    }
}
