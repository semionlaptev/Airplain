using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Airplane
{
    public static class PaperPlaneEngine
    {

        #region PositionedObject
        public static PositionedObject CreatePositionedImage(Vector2 position, GameImage image)
        {
            PositionedObject posImage = new PositionedObject(position);
            ObjectReferencesHandler.Instance.AddKeyValToDictionary((GameObject)posImage, image, ImageHandler.Instance);
            return posImage;
        }

        public static PositionedObject CreatePositionedImage(Vector2 position, Texture2D texture)
        {
            return CreatePositionedImage(position,new GameImage(texture));
        }

        public static PositionedObject CreatePositionedImage(Vector2 position, string textureName)
        {
            return CreatePositionedImage(position, new GameImage(TextureManager.Textures.GetTexture(textureName)));
        }

        public static PositionedObject CreatePositionedImage(Vector2 position, string textureName, Vector2 imageSize)
        {
            return CreatePositionedImage(position, new GameImage(TextureManager.Textures.GetTexture(textureName), imageSize));
        }
        #endregion

        #region DenseObject
        public static DenseObject CreateDenseImage(Vector2 position, Rectangle collisionRect, GameImage image)
        {
            DenseObject denseImage = new DenseObject(position, collisionRect);
            ObjectReferencesHandler.Instance.AddKeyValToDictionary((GameObject)denseImage, image, ImageHandler.Instance);
            return denseImage;
        }

        public static DenseObject CreateDenseImage(Vector2 position, Rectangle collisionRect, string textureName)
        {
            return CreateDenseImage(position, collisionRect, new GameImage(TextureManager.Textures.GetTexture(textureName)));
        }

        public static DenseObject CreateDenseImage(Vector2 position, GameImage image)
        {
            return CreateDenseImage(position, new Rectangle(0, 0, (int)image.Width, (int)image.Height), image);
        }

        public static DenseObject CreateDenseImage(Vector2 position, string textureName)
        {
            return CreateDenseImage(position,  new GameImage(TextureManager.Textures.GetTexture(textureName)));
        }

        public static DenseObject CreateDenseImage(Rectangle positionedCollisionRect, GameImage image)
        {
            return CreateDenseImage(new Vector2(positionedCollisionRect.X, positionedCollisionRect.Y), new Rectangle(0, 0, positionedCollisionRect.Width, positionedCollisionRect.Height), image);
        }

        public static DenseObject CreateDenseImage(Rectangle positionedCollisionRect, string textureName)
        {
            return CreateDenseImage(positionedCollisionRect, new GameImage(TextureManager.Textures.GetTexture(textureName)));
        }

        public static DenseObject CreateDenseImage(Rectangle positionedCollisionRect, string textureName, Vector2 imageSize)
        {
            return CreateDenseImage(positionedCollisionRect, new GameImage(TextureManager.Textures.GetTexture(textureName), imageSize));
        }

        #endregion

        public static void DeleteObject(GameObject obj)
        {
            ObjectReferencesHandler.Instance.RemoveObjectFromLists(obj);
            ObjectReferencesHandler.Instance.RemoveObjectFromDictonaries(obj);
            obj = null;
        }

        public static void DeleteObjectsImage(PositionedObject obj)
        {
            ObjectReferencesHandler.Instance.RemoveObjectFromLists((GameObject)ImageHandler.Instance.GetImage(obj));  //Delete Animation from AnimationHandler;
            ObjectReferencesHandler.Instance.RemoveObjectFromDictonaries((GameObject)ImageHandler.Instance.GetImage(obj));  //Delete Animation from AnimationHandler;
        }

        public static void DeletePositionedImage(PositionedObject obj)
        {
            DeleteObjectsImage(obj);
            DeleteObject(obj);
        }


        public static void DoUpdates(GameTime gameTime)
        {
            AnimationHandler.Instance.Update(gameTime);
            CollidersManager.Instance.CheckCollisions();
            //physx
            //LayersManager.Layers.Move();
        }

        #region Collider and TriggerArea

        public static Collider CreateCollider(CollisionEventDelegate collisionevent)
        {
            Collider collider = new Collider(collisionevent);
            ObjectReferencesHandler.Instance.AddObjectToList(collider, CollidersManager.Instance);
            return collider;
        }

        public static void AddToColliderLeft(DenseObject obj, Collider collider)
        {
            ObjectReferencesHandler.Instance.AddObjectToList<DenseObject>(obj, collider.LeftCollider, collider.AddToLeftCollider);
        }

        public static void AddToColliderRight(DenseObject obj,Collider collider)
        {
            ObjectReferencesHandler.Instance.AddObjectToList<DenseObject>(obj, collider.RightCollider, collider.AddToRightCollider);
        }

        public static TriggerArea CreateTriggerArea(Rectangle positionsize, CollisionEventDelegate collisionevent)
        {
            TriggerArea triggerArea = new TriggerArea(positionsize, collisionevent);
            ObjectReferencesHandler.Instance.AddObjectToList(triggerArea, CollidersManager.Instance);
            return triggerArea;
        }

        public static TriggerArea CreateTriggerAreaImage(Rectangle positionsize, CollisionEventDelegate collisionevent, GameImage image)
        {
            TriggerArea triggerArea = new TriggerArea(positionsize, collisionevent);
            ObjectReferencesHandler.Instance.AddObjectToList(triggerArea, CollidersManager.Instance);
            ObjectReferencesHandler.Instance.AddKeyValToDictionary((GameObject)triggerArea, image, ImageHandler.Instance);
            return triggerArea;
        }

        public static TriggerArea CreateTriggerAreaImage(Rectangle positionsize, CollisionEventDelegate collisionevent, string textureName)
        {
            return CreateTriggerAreaImage(positionsize, collisionevent, new GameImage(TextureManager.Textures.GetTexture(textureName)));
        }

        public static TriggerArea CreateTriggerAreaImage(Rectangle positionsize, CollisionEventDelegate collisionevent, string textureName, Vector2 imageSize)
        {
            return CreateTriggerAreaImage(positionsize, collisionevent, new GameImage(TextureManager.Textures.GetTexture(textureName), imageSize));
        }

        public static void AddToTriggerArea(DenseObject obj,TriggerArea triggerArea)
        {
            ObjectReferencesHandler.Instance.AddObjectToList(obj, triggerArea);
        }

        #endregion

        public static void RegisterAnimation(GameAnimation animation)
        {
            ObjectReferencesHandler.Instance.AddObjectToList(animation, AnimationHandler.Instance);
        }

        public static void AddToLayer(PositionedObject obj, GameLayer layer)
        {
            ObjectReferencesHandler.Instance.AddObjectToList(obj, layer);
        }

        public static void AddToLayer(PositionedObject obj, string layerName)
        {
            ObjectReferencesHandler.Instance.AddObjectToList(obj, LayersManager.Layers[layerName]);
        }


        public static void objectSpawnRandomInRect(PositionedObject obj, Rectangle spawnRect)
        {
            obj.Position = getRandomCoordsInRect(spawnRect);
        }

        public static Vector2 getRandomCoordsInRect(Rectangle spawnRect)
        {
            Random random = new Random();
            int x = spawnRect.X + random.Next(spawnRect.Width);
            int y = spawnRect.Y + random.Next(spawnRect.Height);
            return new Vector2(x, y);
        }

        /*
        public interface IPositionedObjectsFactory<T> where T : PositionedObject
        {
            T Create(Vector2 position);
        }

         public static T CreatePositionedImage<T>(Vector2 position, Texture2D texture) where T:PositionedObject, new()
        {
            T posImage = new T();
            ObjectReferencesHandler.instance.AddKeyValToDictionary((GameObject)posImage, texture, ImageHandler.Instance);
            return posImage;
        }
        public static class DenseObjectsFactory: IPositionedObjectsFactory<DenseObject>
        {
            public static DenseObject Create(Vector2 position)
            {
                return new DenseObject(new Rectangle((int)position.X, (int)position.Y, 0, 0));
            }
        }*/
    }
}
