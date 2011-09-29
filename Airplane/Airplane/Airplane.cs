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
        PositionedObject farcity, farcityTwin;

        //class that will check collisions between DenseGameObjects that added.
        Collider plain_collider;
        TriggerArea leftScreenTrigger;

        GameAnimation plane_anima;
        GameAnimation stars_anima;

        Random random = new Random();

        Physx physx = new Physx();
        Force gravi = new Force(new Vector2(0,0.5f));

        GameCamera maincamera = new GameCamera(new Vector2(0, 0));

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
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 10;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 10;
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
            TextureManager.Textures["stars2"] = Content.Load<Texture2D>("images/stars");
            TextureManager.Textures["plane"] = Content.Load<Texture2D>("images/letright4");
            TextureManager.Textures["sky"] = Content.Load<Texture2D>("images/sky");
            TextureManager.Textures["backgroundcity"] = Content.Load<Texture2D>("images/backgroundcity");
            TextureManager.Textures["cloud"] = Content.Load<Texture2D>("images/cloud3");
            TextureManager.Textures["house"] = Content.Load<Texture2D>("images/rect3102");
            TextureManager.Textures["stars"] = Content.Load<Texture2D>("images/stars2");
            TextureManager.Textures["mountains"] = Content.Load<Texture2D>("images/smountains");

            InitializeGameObjects(); //shouldnaa't be here
        }

        protected void InitializeGameObjects()
        {
            //ingame object init
            //setting the plane
            planespeed = new Vector2(nspeed(360), 0);
            cloudsArea = new Rectangle(screenWidth, 0, 200, screenHeight / 3);
            stars_anima = new GameAnimation(TextureManager.Textures["stars2"], 3, 6);

            #region Plane setting up

            plane_anima = new GameAnimation(TextureManager.Textures["plane"], 1, 9);
            plane_anima.Animations["normal"] = new AnimationSequence(0, new List<int>() { 0, 0, 0, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 8, 8, 8, 8, 8 });
            plane_anima.Play("normal", LOOP_TYPE.LOOP_PINGPONG);
            PaperPlaneEngine.RegisterAnimation(plane_anima);

            plane = PaperPlaneEngine.CreateDenseImage(
                new Vector2(100, 20),
                plane_anima
                );

            plane.Origin = new Vector2(plane_anima.Width / 3, plane_anima.Height / 3);
            plane.Tag = "Player";


            physx.addForce(plane, gravi);
            plane.Speed = planespeed;

            //tune the plane
            plane.Scale = 0.2f;
            plane.Origin = new Vector2(plane_anima.ImageSize.X / 3, plane_anima.ImageSize.Y / 3);

            #endregion

            //The "infinite" background city is made of two images that run looped each after other
            farcity = PaperPlaneEngine.CreatePositionedImage(new Vector2(0, 5), "backgroundcity", new Vector2(screenWidth, screenHeight));
            farcityTwin = PaperPlaneEngine.CreatePositionedImage(new Vector2(screenWidth, 5), "backgroundcity", new Vector2(screenWidth, screenHeight));

            //set the speed of the background city, better do it by layers
            farcity.Speed = new Vector2(-nspeed(30),0);
            farcityTwin.Speed = farcity.Speed;

            InitializeLayers();
            InitializeColliders();

            GameDrawer.Instance.SetActiveCamera(maincamera);

        }

        protected void InitializeLayers()
        {
            LayersManager.Layers.CreateLayer("clouds3", 0.9f);
            LayersManager.Layers.CreateLayer("houses", 0.86f);
            LayersManager.Layers.CreateLayer("clouds2", 0.85f);
            LayersManager.Layers.CreateLayer("plane", 0.8f);
            LayersManager.Layers.CreateLayer("clouds1", 0.55f);
            LayersManager.Layers.CreateLayer("farcity", 0.5f);
            LayersManager.Layers.CreateLayer("mountains", 0.2f).AddObject(PaperPlaneEngine.CreatePositionedImage(new Vector2(0, 200), "mountains", new Vector2(screenWidth, screenHeight)));
            LayersManager.Layers.CreateLayer("stars", 0.1f).AddObject(PaperPlaneEngine.CreatePositionedImage(new Vector2(0, 5), "stars", new Vector2(screenWidth, screenHeight)));
            LayersManager.Layers.CreateLayer("sky", 0.0f).AddObject(PaperPlaneEngine.CreatePositionedImage(new Vector2(0, 0), "sky"));

            //add objects to the layers
            LayersManager.Layers["plane"].AddObject(plane);
            LayersManager.Layers["farcity"].AddObject(farcity);
            LayersManager.Layers["farcity"].AddObject(farcityTwin);
        }

        protected void InitializeColliders()
        {
            plain_collider = PaperPlaneEngine.CreateCollider(onPlayerHit);
            PaperPlaneEngine.AddToColliderLeft(plane, plain_collider);
            leftScreenTrigger = PaperPlaneEngine.CreateTriggerArea(new Rectangle(-1000, -300, 1000, screenHeight + 300), onObjectInLeftTrigger);

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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
            
            CheckCollisions();

            processKeyboard();

            physx.doPhysix();

            //plane dynamics
            plane.Rotation = (float)Math.PI / 2 - (float)Math.Atan2(plane.Speed.X, plane.Speed.Y);
            maincamera.Position = plane.Position;
            maincamera.Origin = (new Vector2(100,300));

            //-------
            planespeed = plane.Speed;
            plane.Speed = new Vector2(0.0f, planespeed.Y);
            LayersManager.Layers["houses"].Speed = new Vector2(-planespeed.X, 0);
            houserate = starsrate = (int)planespeed.X * 8;
            //---------

            LayersManager.Layers.Move();

            plane.Speed = planespeed;

            //generate houses and clouds
            if (random.Next(1000) <= houserate)
            {
                objectSpawnHouse();
            }
            if (random.Next(1000) <= houserate/10)
            {
                objectSpawnCloud(cloudsArea);
            }

            if (random.Next(1000) <= starsrate)
            {
                objectSpawnStar(cloudsArea);
            }

            if (random.Next(1000) <= starsrate/20)
            {
                spawnSpeedBox(cloudsArea);
            }

            AnimationHandler.Instance.Update(gameTime);
            base.Update(gameTime);

            ObjectReferencesHandler.Checks = 0;
        }

        void processKeyboard()
        {

            KeyboardState kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.Left))
            {
                plane.Speed = Vector2.Transform(plane.Speed, Matrix.CreateRotationZ(-0.05f));
            }

            if (kbState.IsKeyDown(Keys.Right))
            {
                plane.Speed = Vector2.Transform(plane.Speed, Matrix.CreateRotationZ(0.05f));
            }

            if (kbState.IsKeyDown(Keys.Space))
            {
                plane.Speed += plane.Speed/20;
            }
            
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


        /// <summary>
        /// Returns a 2D vector with random X,Y coordinates withing specified rectangle
        /// </summary>
        /// <param name="spawnRect">Rectangle</param>
        /// <returns></returns>


        protected void objectSpawnHouse()
        {
            int houseHeight = screenHeight / 4 + random.Next(screenHeight / 4);
            int houseWidth = 100+random.Next(80);

            DenseObject house = PaperPlaneEngine.CreateDenseImage(new Rectangle(screenWidth, screenHeight - houseHeight, houseWidth, houseHeight), "house", new Vector2(houseWidth, houseHeight));
            house.Tag = "House";
            PaperPlaneEngine.AddToColliderRight(house, plain_collider);
            PaperPlaneEngine.AddToTriggerArea(house, leftScreenTrigger);
            PaperPlaneEngine.AddToLayer(house, "houses");
        }

        protected void spawnSpeedBox(Rectangle spawnRect)
        {
            TriggerArea speedbox = PaperPlaneEngine.CreateTriggerAreaImage(new Rectangle(0, 0, 100, 50), onSpeedBoxHit, "house", new Vector2(100, 50));
            speedbox.Tag = "Speedbox";
            speedbox.Speed = new Vector2(-nspeed( random.Next(60)), 0);

            PaperPlaneEngine.AddToTriggerArea(plane, speedbox);
            PaperPlaneEngine.AddToTriggerArea(speedbox, leftScreenTrigger);
            PaperPlaneEngine.AddToLayer(speedbox, "houses");
           
            PaperPlaneEngine.objectSpawnRandomInRect(speedbox, spawnRect);
        }

        void onSpeedBoxHit(DenseObject self, DenseObject other)
        {
            other.Speed += other.Speed/20;
        }

        protected void objectSpawnStar(Rectangle spawnRect)
        {
            int dice = random.Next(100);
            int row;

            if (dice < 55)
                row = 0;
            else if (dice >= 55 && dice <= 88)
                row = 1;
            else
                row = 2;

            GameAnimation one_star_anim = stars_anima.Copy();
            one_star_anim.Animations["star"] = new AnimationSequence(row, new List<int>() { 0, 1, 2, 3, 4, 5 });
            one_star_anim.AnimationFPS = 12;
            one_star_anim.Play("star", LOOP_TYPE.LOOP_NORMAL);

            DenseObject star = PaperPlaneEngine.CreateDenseImage(new Vector2(0, 0), one_star_anim);
            star.Tag = "Star";
            star.Scale = 0.7f - random.Next(3) * 0.1f;
            star.Speed = new Vector2(-nspeed(30 - random.Next(60)), -nspeed(20 - random.Next(40)));
            PaperPlaneEngine.RegisterAnimation(one_star_anim);
            PaperPlaneEngine.AddToColliderRight(star, plain_collider);
            PaperPlaneEngine.AddToTriggerArea(star, leftScreenTrigger);
            PaperPlaneEngine.AddToLayer(star, "houses");

            PaperPlaneEngine.objectSpawnRandomInRect(star, spawnRect);

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

            DenseObject cloud = PaperPlaneEngine.CreateDenseImage(new Vector2(0, 0), "cloud");

            cloud.Scale = scale + random.Next(50)*0.01f;
            cloud.Speed = new Vector2(-nspeed(120) * cloud.Scale, 0);
            cloud.Tag = "Cloud";

            PaperPlaneEngine.AddToLayer(cloud, LayersManager.Layers[layer]);
            PaperPlaneEngine.AddToTriggerArea(cloud, leftScreenTrigger);

            PaperPlaneEngine.objectSpawnRandomInRect(cloud, spawnRect);

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
                PaperPlaneEngine.DeletePositionedImage(other);
            }
        }

        protected void onObjectInLeftTrigger(DenseObject self, DenseObject other)
        {
            if (self.CollisionRectPositionedScaled.Contains(other.CollisionRectPositionedScaled))
            {
                    Console.WriteLine(other.Tag+" removed.");
                    PaperPlaneEngine.DeletePositionedImage(other);
            }
        }
    }
}
