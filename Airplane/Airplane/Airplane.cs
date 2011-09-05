using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//using mat

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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;

        int screenWidth, screenHeight;

        int houserate = 9;
        //layers
        //List that will store game object layers. Each layer has a string name
        Dictionary<string, GameLayer> Layers = new Dictionary<string, GameLayer>();

        //object textures

        Texture2D planeImage;
        Texture2D skyImage;
        Texture2D cityImage;
        Texture2D cloudImage;
        Texture2D houseImage;
        Texture2D starsImage;

        //ingame objects
        DenseObject plane;
        Rectangle cloudsArea;

        //decorative objects
        PositionedObject sky, stars;
        PositionedObject farcity, farcityTwin;

        //class that will check collisions between DenseGameObjects that added.
        Collider collider;
        TriggerArea leftScreenTrigger;
        ObjectHandler handler;

        Random random = new Random();

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
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 600;
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
            planeImage = Content.Load<Texture2D>("images/plane2");
            skyImage = Content.Load<Texture2D>("images/sky");
            cityImage = Content.Load<Texture2D>("images/farcity");
            cloudImage = Content.Load<Texture2D>("images/cloud3");
            houseImage = Content.Load<Texture2D>("images/rect3102");
            starsImage = Content.Load<Texture2D>("images/stars2");

            InitializeGameObjects(); //shouldnaa't be here
        }

        protected void InitializeGameObjects()
        {
            //ingame object init

            plane = new DenseObject(new Vector2(100,100), planeImage);
            cloudsArea = new Rectangle(screenWidth, 0, 200, screenHeight / 3);

            plane.Tag = "Player";

            //tune the plane
            plane.Scale = 0.5f;

            //decorations
            sky = new RealObject(new Rectangle(0, 0, screenWidth, screenHeight), skyImage);
            stars = new RealObject(new Rectangle(0, 5, screenWidth, screenHeight + 5), starsImage);

            //The "infinite" background city is made of two images that run looped each after other
            farcity = new RealObject(new Rectangle(0, 0, screenWidth, screenHeight + 5), cityImage);
            farcityTwin = new RealObject(new Rectangle(screenWidth, 5, screenWidth, screenHeight + 5), cityImage);

            //set the speed of the background city
            farcity.Speed = new Vector2(-nspeed(30),0);
            farcityTwin.Speed = new Vector2(-nspeed(30), 0);

            handler = new ObjectHandler();

            InitializeLayers();
            InitializeColliders();

           
        }

        protected void InitializeLayers()
        {
            //layers init
            Layers.Add("plane", new GameLayer(0.8f));
            Layers.Add("sky", new GameLayer(0.0f));
            Layers.Add("farcity", new GameLayer(0.5f));
            Layers.Add("stars", new GameLayer(0.1f));
            Layers.Add("clouds1", new GameLayer(0.55f));
            Layers.Add("clouds2", new GameLayer(0.85f));
            Layers.Add("clouds3", new GameLayer(0.9f));
            Layers.Add("houses", new GameLayer(0.86f));
           // //set the draw depth for the game layers
           // Layers["plane"].Depth = 0.8f;
           // Layers["farcity"].Depth = 0.5f;
           // Layers["sky"].Depth = 0.0f;
           // Layers["stars"].Depth = 0.1f;
           // Layers["clouds1"].Depth = 0.55f;
           // Layers["clouds2"].Depth = 0.85f;
           // Layers["clouds3"].Depth = 0.9f;

            //add objects to the layers
            Layers["plane"].addObject(plane);

            Layers["sky"].addObject(sky);
            Layers["stars"].addObject(stars);
            Layers["farcity"].addObject(farcity);
            Layers["farcity"].addObject(farcityTwin);

            Layers["houses"].Speed = new Vector2(-nspeed(90), 0);
        }

        protected void InitializeColliders()
        {
            collider = new Collider();

            //add dense objects to the collider
            collider.addObject(plane);
            plane.CollisionEvent = onPlayerHit;

            leftScreenTrigger = new TriggerArea(new Rectangle(-500, 0, 500, screenHeight));

            leftScreenTrigger.CollisionEvent = onObjectInArea;

        }

        protected void onObjectInArea(DenseObject self, DenseObject other)
        {
            if (self.Position.X + self.SizeScaled.X > other.Position.X + other.SizeScaled.X)
            {
                if (other.Tag == "House")
                {
                    Console.WriteLine("House removed.");
                    handler.removeObject(other);
                }
                else if (other.Tag == "Cloud")
                {
                    Console.WriteLine("Cloud removed.");
                    handler.removeObject(other);
                }
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected void MoveObjects()
        {
            //to move objects in the world coordinates add summ of its speed and layer's speed to the object's position
            foreach (KeyValuePair<string, GameLayer> layer in Layers)
            {
                //foreach (PositionedObject gameObject in layer.Value)
                //{
                //    gameObject.Position += gameObject.Speed;
                //}
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
            collider.checkCollisions();
            leftScreenTrigger.checkCollisions();
        }

        protected override void Update(GameTime gameTime)
        {
            float fps;
            if (gameTime.ElapsedGameTime.Milliseconds > 0)
                fps = 1000.0f / gameTime.ElapsedGameTime.Milliseconds;

            bool up = false;
            bool sp = false;

            KeyboardState kbState = Keyboard.GetState();
            {
                if (kbState.IsKeyDown(Keys.Up))
                {
                    up = true;
                }

                if (kbState.IsKeyUp(Keys.Up))
                {
                    up = false;
                }

                if (kbState.IsKeyDown(Keys.Space))
                {
                    sp = true;
                }

                if (kbState.IsKeyUp(Keys.Space))
                {
                    sp = false;
                }

            }

            if (up)
            {
                //plane.Speed = new Vector2(plane.Speed.X, -nspeed(100));
                
            }
            else
            {
                //plane.Speed = new Vector2(plane.Speed.X, nspeed(50));
                Layers["houses"].Speed = new Vector2(-nspeed(90), 0);
                
            }

            Vector2 cityspeed = new Vector2(-nspeed(360), 0);

            if (sp)
            {
                //plane.Speed = new Vector2(nspeed(1000), plane.Speed.Y);
                
                houserate = 45;
                if (plane.Rotation > -1.5f)
                    plane.Rotation -= 0.01f;
                //Layers["houses"].Speed = new Vector2(-nspeed(360), 0);
                
                //Vector2 newspeed = new Vector2();
            }
            else
            {
                //houserate = 9;
                //plane.Speed = new Vector2(-nspeed(0), plane.Speed.Y);
                if (plane.Rotation < 1.5f)
                    plane.Rotation += 0.01f;
            }

            plane.Speed = new Vector2(0, -(float) (cityspeed.X*Math.Sin((double) plane.Rotation)));
            cityspeed = new Vector2((float)(cityspeed.X*Math.Cos((double)plane.Rotation)),0);

            Layers["houses"].Speed = cityspeed;

            MoveObjects();
            CheckCollisions();

            int dice = random.Next(1000);
            if (dice <= houserate)
            {
                objectSpawnHouse();
            }
            dice = random.Next(1000);
            if (dice <= 12)
            {
                objectSpawnCloud(cloudsArea);
            }

            base.Update(gameTime);
        }

        protected void DrawLayer(GameLayer layer)
        {
            foreach (RealObject gameObject in layer)
            {
                if (gameObject.IsVisible)
                {
                    //don't sure if this float to int conversion is right
                    spriteBatch.Draw(
                        gameObject.Image,       //an object texture
                        new Rectangle(          //an object size and position
                            (int)(gameObject.Position.X), 
                            (int)(gameObject.Position.Y),
                            (int)(gameObject.Size.X*gameObject.Scale), //dont forget about the scale
                            (int)(gameObject.Size.Y*gameObject.Scale)),
                         null,  //?
                         Color.White,   
                         gameObject.Rotation,
                         Vector2.Zero,  //?
                         SpriteEffects.None,
                         layer.Depth);  //draw depth
                }
            }
        }

        protected void DrawObject(PositionedObject obj)
        {

        }

        /// <summary>
        /// goes through layers list and calls DrawLayer for each one
        /// </summary>
        protected void DrawScene()
        {
            
            foreach (KeyValuePair<string, GameLayer> layer in Layers)
            {
                DrawLayer(layer.Value);
            }
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            DrawScene();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected float nspeed(int speed) { return speed / 60.0f; }

        /// <summary>
        /// Moves an object withing the specified rectangle.
        /// </summary>
        /// <param name="obj">An object to spawn</param>
        /// <param name="spawnRect">A region to spawn in.</param>
        protected void objectSpawnRandom(PositionedObject obj, Rectangle spawnRect)
        {
            obj.Position = objectSpawnRandomCoords(spawnRect);
        }

        /// <summary>
        /// Returns a 2D vector with random X,Y coordinates withing specified rectangle
        /// </summary>
        /// <param name="spawnRect">Rectangle</param>
        /// <returns></returns>
        protected Vector2 objectSpawnRandomCoords(Rectangle spawnRect)
        {
            int x = spawnRect.X + random.Next(spawnRect.Width);
            int y = spawnRect.Y + random.Next(spawnRect.Height);
            return new Vector2(x,y);
        }

        protected void objectSpawnHouse()
        {
            int houseHeight = screenHeight / 4 + random.Next(screenHeight / 4);
            int houseWidth = 100+random.Next(80);
            DenseObject house = new DenseObject(new Rectangle(screenWidth,screenHeight-houseHeight,houseWidth, houseHeight),houseImage);
            house.Tag = "House";
            //house.Speed = new Vector2(-nspeed(80), 0);

            handler.addObjectToList(house, collider);
            handler.addObjectToList(house, leftScreenTrigger);
            handler.addObjectToList(house, Layers["houses"]);
        }

        protected void objectSpawnCloud(Rectangle spawnRect)
        {
            int dice = random.Next(100);
            float scale = 1.0f;
            string layer;
            if (dice < 55)
            {
                scale = 0.1f;
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

            DenseObject cloud = new DenseObject(new Vector2(0, 0), cloudImage);
            cloud.Scale = scale + random.Next(50)*0.01f;
            cloud.Speed = new Vector2(-nspeed(120) * cloud.Scale, 0);
            cloud.Tag = "Cloud";

            //Layers[layer].addObject(cloud);
            //leftScreenTrigger.addObject(cloud);

            handler.addObjectToList(cloud, Layers[layer]);
            handler.addObjectToList(cloud, leftScreenTrigger);
            objectSpawnRandom(cloud, spawnRect);

        }

        void onPlayerHit(DenseObject self,DenseObject other)
        {
            if (other.Tag == "House")
            {
                self.Position = new Vector2(self.Position.X - 150, self.Position.Y);
            }
        }
    }
}
