using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Airplane
{
    
    public class Airplane : Microsoft.Xna.Framework.Game
    {
        Vector2 planespeed;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;

        int screenWidth, screenHeight;

        int houserate = 9;
        int starsrate = 40;
        float fps = 0;
        int stars_collected = 0;
        //layers

        //ingame objects
        DenseObject plane;
        Rectangle cloudsArea;

        //decorative objects
        PositionedObject sky, stars;
        PositionedObject farcity, farcityTwin;
        PositionedObject mountains;

        SpriteFont font;
        //class that will check collisions between DenseGameObjects that added.
        Collider plain_collider;
        TriggerArea leftScreenTrigger;
        

        GameAnimation plane_anima;
        GameAnimation stars_anima;

        Dictionary<string, AnimationSequence> plane_animations = new Dictionary<string, AnimationSequence>();
        Dictionary<string, AnimationSequence> stars_animations = new Dictionary<string, AnimationSequence>();

        Random random = new Random();

        Physx physx = new Physx();
        Force gravi = new Force(new Vector2(0,0.5f));
        Force antigravi = new Force(new Vector2(0, -1));

        public Airplane()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //graphics init
            this.IsMouseVisible = true;
            device = graphics.GraphicsDevice;
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 900;
            Window.Title = "Paper Plane";
            graphics.ApplyChanges();
            
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;

            //
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load textures
            TextureManager.Instance["stars2"] = Content.Load<Texture2D>("images/stars");
            TextureManager.Instance["plane"] = Content.Load<Texture2D>("images/letright4");
            TextureManager.Instance["sky"] = Content.Load<Texture2D>("images/sky");
            TextureManager.Instance["backgroundcity"] = Content.Load<Texture2D>("images/backgroundcity");
            TextureManager.Instance["cloud"] = Content.Load<Texture2D>("images/cloud3");
            TextureManager.Instance["house"] = Content.Load<Texture2D>("images/rect3102");
            TextureManager.Instance["stars"] = Content.Load<Texture2D>("images/stars2");
            TextureManager.Instance["mountains"] = Content.Load<Texture2D>("images/smountains");

            font = Content.Load<SpriteFont>("fonts/TextFont");
            InitializeGameObjects(); //shouldnaa't be here
        }

        protected void InitializeGameObjects()
        {
            //ingame object init
            //setting the plane
            planespeed = new Vector2(nspeed(360), 0);

            plane_animations["normal"] = new AnimationSequence(0, new List<int>() { 0,0,0,0,0,0, 1, 2, 3, 4, 5, 6, 7, 8,8,8,8,8,8 });
            plane_anima = new GameAnimation(TextureManager.Instance["plane"], 1, 9);
            plane_anima.Play(plane_animations["normal"], LOOP_TYPE.LOOP_PINGPONG);
            plane_anima.AnimationFPS = 12;

            ObjectHandler.instance.AddObjectToList(plane_anima, AnimationHandler.Instance);

            plane = new DenseObject(
                new Rectangle(100, 
                    100, 
                (int)(plane_anima.ImageSize.X - plane_anima.ImageSize.X/2), 
                (int)(plane_anima.ImageSize.Y- 1.5*plane_anima.ImageSize.Y/2))
                );

            plane.Origin = new Vector2(plane_anima.Width / 3, plane_anima.Height / 3);
            ImageHandler.Instance.LinkObjectAndImage(plane, plane_anima);
            plane.Tag = "Player";

            //antigravy = new Vector2(0,-gravy.Y);
            physx.addForce(plane, gravi);
            physx.addForce(plane, antigravi);

            plane.Speed = planespeed;

            //tune the plane
            plane.Scale = 0.2f;
            plane.Origin = new Vector2(plane_anima.ImageSize.X / 3, plane_anima.ImageSize.Y / 3);

            stars_anima = new GameAnimation(TextureManager.Instance["stars2"], 3, 6);
            stars_animations["star1"] = new AnimationSequence(0, new List<int>() { 0, 1, 2, 3, 4, 5 });
            stars_animations["star2"] = new AnimationSequence(1, new List<int>() { 0, 1, 2, 3, 4, 5 });
            stars_animations["star3"] = new AnimationSequence(2, new List<int>() { 0, 1, 2, 3, 4, 5 });

            //game decorations (sky + stars + background city))
            sky = new PositionedObject(new Vector2(0,0));
            ImageHandler.Instance.LinkObjectAndImage(sky, new GameImage(TextureManager.Instance["sky"], new Vector2(screenWidth, screenHeight)));

            stars = new PositionedObject(new Vector2(0, 5));
            ImageHandler.Instance.LinkObjectAndImage(stars, new GameImage(TextureManager.Instance["stars"], new Vector2(screenWidth, screenHeight)));

            //The "infinite" background city is made of two images that run looped each after other
            farcity = new PositionedObject(new Vector2(0, 5));
            ImageHandler.Instance.LinkObjectAndImage(farcity, new GameImage(TextureManager.Instance["backgroundcity"], new Vector2(screenWidth, screenHeight)));

            farcityTwin = new PositionedObject(new Vector2(screenWidth, 5));
            ImageHandler.Instance.LinkObjectAndImage(farcityTwin, new GameImage(TextureManager.Instance["backgroundcity"], new Vector2(screenWidth, screenHeight)));

            //set the speed of the background city, better do it by layers
            farcity.Speed = new Vector2(-nspeed(30),0);
            farcityTwin.Speed = new Vector2(-nspeed(30), 0);

            mountains = new PositionedObject(new Vector2(0,200));
            ImageHandler.Instance.LinkObjectAndImage(mountains, new GameImage(TextureManager.Instance["mountains"]));
            //mountains.Speed = new Vector2(-nspeed(20),0);

            InitializeLayers();
            InitializeColliders();           
        }

        protected void InitializeLayers()
        {
            cloudsArea = new Rectangle(screenWidth, 0, 200, screenHeight / 3);

            LayersManager.Instance.CreateLayer("clouds3", 0.9f);
            LayersManager.Instance.CreateLayer("houses", 0.86f);
            LayersManager.Instance.CreateLayer("clouds2", 0.85f);
            LayersManager.Instance.CreateLayer("plane", 0.8f);
            LayersManager.Instance.CreateLayer("clouds1", 0.55f);
            LayersManager.Instance.CreateLayer("farcity", 0.5f);
            LayersManager.Instance.CreateLayer("mountains", 0.2f);
            LayersManager.Instance.CreateLayer("stars", 0.1f);
            LayersManager.Instance.CreateLayer("sky", 0.0f);

            //add objects to the layers
            LayersManager.Instance["plane"].AddObject(plane);
            LayersManager.Instance["sky"].AddObject(sky);
            LayersManager.Instance["stars"].AddObject(stars);
            LayersManager.Instance["farcity"].AddObject(farcity);
            LayersManager.Instance["farcity"].AddObject(farcityTwin);
            LayersManager.Instance["mountains"].AddObject(mountains);

        }

        protected void InitializeColliders()
        {
            plain_collider = new Collider();

            //add dense objects to the collider
            ObjectHandler.instance.AddObjectToList(plane, plain_collider.LeftCollider);
            plain_collider.CollisionEvent = onPlayerHit;

            leftScreenTrigger = new TriggerArea(new Rectangle(-1000, -300, 1000, screenHeight+300));
            leftScreenTrigger.CollisionEvent = onObjectInArea;

            CollidersManager.Instance.AddCollider(plain_collider);
            CollidersManager.Instance.AddCollider(leftScreenTrigger);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected void MoveObjects()
        {
            foreach (KeyValuePair<string, GameLayer> layer in LayersManager.Instance)
            {
                layer.Value.Move();
            }
        }
        protected void CheckCollisions()
        {
            //loop the background city
            if (farcity.Position.X + screenWidth < farcity.Speed.X)
            {
                farcity.Position = new Vector2(farcity.Position.X + screenWidth, 5);
                farcityTwin.Position = new Vector2(farcityTwin.Position.X + screenWidth, 5);
            }

            //check it check
            CollidersManager.Instance.CheckCollisions();
        }

        protected override void Update(GameTime gameTime)
        {
            if (gameTime.ElapsedGameTime.Milliseconds > 0)
                fps = 1000.0f / gameTime.ElapsedGameTime.Milliseconds;

            bool sp = false;

            KeyboardState kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.Space))
                sp = true;

            if (kbState.IsKeyUp(Keys.Space))
                sp = false;
            
            if (sp)
            {
                physx.addImpulse(plane, new Vector2(0,-0.3f));
                plane.Rotation -= 0.07f;    
            }
            else
            {
                if (Math.Cos(plane.Rotation) >= 0)
                    plane.Rotation += 0.01f;
                else
                    plane.Rotation -= 0.05f;
            }

            //antigravy.X = 0;
            antigravi.Value = new Vector2(0, -gravi.Value.Y * (float)Math.Cos(plane.Rotation));

            physx.doPhysix();
            planespeed = plane.Speed;
            plane.Speed = new Vector2(0, planespeed.Y);
            LayersManager.Instance["houses"].Speed = new Vector2(-planespeed.X, 0);
            
            //plane.Speed = new Vector2(0, -(float) (cityspeed.X*Math.Sin((double) plane.Rotation)));
            //LayersManager.Instance["houses"].Speed = new Vector2((float)(cityspeed.X * Math.Cos((double)plane.Rotation)), 0);

            MoveObjects();
            CheckCollisions();

            plane.Speed = planespeed;

            //generate houses and clouds
            if (random.Next(1000) <= houserate)
            {
                objectSpawnHouse();
            }
            if (random.Next(1000) <= houserate/3)
            {
                objectSpawnCloud(cloudsArea);
            }

            if (random.Next(1000) <= starsrate)
            {
                objectSpawnStar(cloudsArea);
            }

            AnimationHandler.Instance.Update(gameTime);
            base.Update(gameTime);

            ObjectHandler.Checks = 0;
        }

        /*private void DrawText()
        {
            spriteBatch.DrawString(font, "stared: "+stars_collected, new Vector2(screenWidth-100, 20), Color.Red);
        }*/

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            GameDrawer.Instance.Draw(spriteBatch);
            base.Draw(gameTime);
        }

        protected float nspeed(int speed) { return speed / 60.0f; }

        /// <summary>
        /// Moves an object withing the specified rectangle.
        /// </summary>
        /// <param name="obj">An object to spawn</param>
        /// <param name="spawnRect">A region to spawn in.</param>
        protected void objectSpawnRandomInRect(PositionedObject obj, Rectangle spawnRect)
        {
            obj.Position = getRandomCoordsInRect(spawnRect);
        }

        /// <summary>
        /// Returns a 2D vector with random X,Y coordinates withing specified rectangle
        /// </summary>
        /// <param name="spawnRect">Rectangle</param>
        /// <returns></returns>
        protected Vector2 getRandomCoordsInRect(Rectangle spawnRect)
        {
            int x = spawnRect.X + random.Next(spawnRect.Width);
            int y = spawnRect.Y + random.Next(spawnRect.Height);
            return new Vector2(x, y);
        }

        protected void objectSpawnHouse()
        {
            int houseHeight = screenHeight / 4 + random.Next(screenHeight / 4);
            int houseWidth = 100+random.Next(80);

            DenseObject house = new DenseObject(new Rectangle(screenWidth, screenHeight - houseHeight, houseWidth, houseHeight));
            ObjectHandler.instance.AddKeyValToDictionary(house, new GameImage(TextureManager.Instance["house"], new Vector2(houseWidth, houseHeight)), ImageHandler.Instance, ImageHandler.Instance.AddKeyVal);

            house.Tag = "House";

            ObjectHandler.instance.AddObjectToList(house, plain_collider.RightCollider);
            ObjectHandler.instance.AddObjectToList(house, leftScreenTrigger);
            ObjectHandler.instance.AddObjectToList(house, LayersManager.Instance["houses"]);
        }

        protected void objectSpawnStar(Rectangle spawnRect)
        {
            int dice = random.Next(100);
            string anim;

            if (dice < 55)
                anim = "star1";
            else if (dice >= 55 && dice <= 88)
                anim = "star2";
            else
                anim = "star3";

            DenseObject star = new DenseObject(new Rectangle(0, 0, (int)stars_anima.Width, (int)stars_anima.Height));
            star.Tag = "Star";
            star.Scale = 0.7f-random.Next(3)*0.1f;

            star.Speed = new Vector2(-nspeed(30 - random.Next(60)), -nspeed(20 - random.Next(40)));

            GameAnimation one_star_anim = stars_anima.Copy();

            ObjectHandler.instance.AddKeyValToDictionary(star, one_star_anim, ImageHandler.Instance);
            ObjectHandler.instance.AddObjectToList(one_star_anim, AnimationHandler.Instance);
            ObjectHandler.instance.AddObjectToList(star, plain_collider.RightCollider);
            ObjectHandler.instance.AddObjectToList(star, leftScreenTrigger);
            ObjectHandler.instance.AddObjectToList(star, LayersManager.Instance["houses"]);

            one_star_anim.AnimationFPS = 12;
            one_star_anim.Play(stars_animations[anim], LOOP_TYPE.LOOP_NORMAL);

            objectSpawnRandomInRect(star, spawnRect);

        }

        protected void objectSpawnCloud(Rectangle spawnRect)
        {
            int dice = random.Next(100);
            float scale = 1.0f;
            string layer;
            if (dice < 55)
            {
                scale = 1.1f;
                layer = "clouds1";
            }
            else if (dice >= 55 && dice <= 88)
            {
                scale = 0.8f;
                layer = "clouds2";
            }
            else
            {
                scale = 1.4f;
                layer = "clouds3";
            }

            DenseObject cloud = new DenseObject(new Rectangle(0, 0, TextureManager.Instance["cloud"].Width, TextureManager.Instance["cloud"].Height));
            ObjectHandler.instance.AddKeyValToDictionary(cloud, new GameImage(TextureManager.Instance["cloud"]), ImageHandler.Instance);

            cloud.Scale = scale + random.Next(50)*0.01f;
            cloud.Speed = new Vector2(-nspeed(120) * cloud.Scale, 0);
            cloud.Tag = "Cloud";

            ObjectHandler.instance.AddObjectToList(cloud, LayersManager.Instance[layer]);
            ObjectHandler.instance.AddObjectToList(cloud, leftScreenTrigger);
            objectSpawnRandomInRect(cloud, spawnRect);

        }

        void onPlayerHit(DenseObject self, DenseObject other)
        {
            if (other.Tag == "House")
            {
                self.Position = new Vector2(self.Position.X, self.Position.Y - 150);
            }
            else if (other.Tag == "Star")
            {
                stars_collected++;
                //other = null;
                ObjectHandler.instance.RemoveObjectFromList(other);
                ObjectHandler.instance.RemoveObjectFromList((GameObject)ImageHandler.Instance.GetImage(other));
                ObjectHandler.instance.RemoveObjectFromDictonary(other);
            }
        }

        protected void onObjectInArea(DenseObject self, DenseObject other)
        {
            if (self.CollisionRectPositionedScaled.Contains(other.CollisionRectPositionedScaled))
            {
                if (other.Tag == "House")
                {
                    Console.WriteLine("House removed.");
                    ObjectHandler.instance.RemoveObjectFromList(other);
                    ObjectHandler.instance.RemoveObjectFromDictonary(other);
                    other = null;
                }
                else if (other.Tag == "Cloud")
                {
                    Console.WriteLine("Cloud removed.");
                    ObjectHandler.instance.RemoveObjectFromList(other);
                    ObjectHandler.instance.RemoveObjectFromDictonary(other);
                    other = null;
                }
                else if (other.Tag == "Star")
                {
                    Console.WriteLine("Star removed.");
                    ObjectHandler.instance.RemoveObjectFromList(other);
                    ObjectHandler.instance.RemoveObjectFromList((GameObject)ImageHandler.Instance.GetImage(other));
                    ObjectHandler.instance.RemoveObjectFromDictonary(other);
                    other = null;
                }
            }
        }
    }
}
