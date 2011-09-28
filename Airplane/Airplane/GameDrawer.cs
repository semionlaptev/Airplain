using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Airplane
{
    class GameDrawer
    {

        #region Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            DrawScene(spriteBatch);
            spriteBatch.End();
        }

        /// <summary>
        /// goes through GameLayers list and calls DrawLayer for each layer
        /// </summary>
        void DrawScene(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<string, GameLayer> layer in LayersManager.Instance)
            {
                DrawLayer(layer.Value, spriteBatch);
            }
        }

        protected void DrawLayer(GameLayer layer, SpriteBatch spriteBatch)
        {
            foreach (PositionedObject gameObject in layer)
            {
                if (gameObject.Position.X < -2000)
                {
                    Console.WriteLine("WTF?");
                }
                if (ImageHandler.Instance.HasImage(gameObject))
                {
                    IDrawable gameImage = ImageHandler.Instance.GetImage(gameObject);
                    spriteBatch.Draw(
                        gameImage.Image,       //an object texture
                        new Rectangle(          //an object size and position
                            (int)(gameObject.Position.X),
                            (int)(gameObject.Position.Y),
                            (int)(gameImage.ImageSize.X * gameObject.Scale * gameImage.ImageScale), //dont forget about the scale
                            (int)(gameImage.ImageSize.Y * gameObject.Scale * gameImage.ImageScale)),
                         gameImage.SourceRect,  //animation works here
                         Color.White,
                        //Color.LightSeaGreen,
                        //new Color(255,252,219),
                         gameObject.Rotation + gameImage.ImageRotation,
                         gameObject.Origin,  //how to sum tow rotations? no matter
                         SpriteEffects.None,
                         layer.Depth);  //draw depth
                }
            }
        }
        #endregion

        #region Singleton
        private static GameDrawer instance_ = null;
        private GameDrawer() { }
        public static GameDrawer Instance
        {
            get
            {
                if (instance_ == null)
                    instance_ = new GameDrawer();
                return instance_;
            }
        }
        #endregion

    }
}
