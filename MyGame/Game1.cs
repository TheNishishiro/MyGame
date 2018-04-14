using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Creatures;
using MyGame.Creatures.Hostile;
using MyGame.GridElements;
using MyGame.UI;
using NFramework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static MyGame.Settings;

namespace MyGame
{
    
    public class Game1 : Game
    {
        public static GraphicsDevice _GraphicsDevice;
        public static ContentManager _Content;
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        List<ICreature> creatures;
        List<FightingManager> fights = new List<FightingManager>();
        bool GotFirstGC = false;
        long MemoryUsageAtStart = 0, MemoryUsageDifference = 0, CurrentMemoryUsage = 0;

        Thread GCInfo;


        private void GetGCInfo()
        {
            while (true)
            {
                CurrentMemoryUsage = System.GC.GetTotalMemory(true);
                MemoryUsageDifference = CurrentMemoryUsage - MemoryUsageAtStart;
                Thread.Sleep(2000);
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _Content = Content;
            _GraphicsDevice = GraphicsDevice;

            cursor = new Cursor(Content.Load<Texture2D>("Light_Blue"));
            Textures.LoadTextures();

            font = Content.Load<SpriteFont>("font");
            grid = new Grid(Content, Settings.WorldSizeBlocks, Settings.WorldSizeBlocks);

            _player = new Player(WorldSizePixels/2, WorldSizePixels / 2, Content.Load<Texture2D>("player"));
            

            creatures = new List<ICreature>();
            for (int i = 0; i < CreatureLimit; i++)
            {
                CreatureFactory.AddCreature(creatures);
            }

            graphics.PreferredBackBufferWidth = (int)(graphics.PreferredBackBufferWidth * 1.3);
            graphics.PreferredBackBufferHeight = (int)(graphics.PreferredBackBufferHeight * 1.3);
            
            graphics.HardwareModeSwitch = false;
         //   graphics.ToggleFullScreen();
            graphics.ApplyChanges();
            NCamera.Camera_CreateViewport(GraphicsDevice.Viewport, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height + 120);
        }

        protected override void UnloadContent()
        {
            //GCInfo.Abort();
        }


        protected override void Update(GameTime gameTime)
        {
            for (int i = 0; i < fights.Count; i++)
            {
                if(!fights[0].enemy2.GetFightingState() || fights[0].enemy2.GetHealth() <= 0)
                {
                    fights.RemoveAt(i);
                    i--;
                }
                else
                {
                    fights[i].Evaluate();
                }
            }
            if(creatures.Count < CreatureLimit)
            {
                CreatureFactory.AddCreature(creatures);
            }
            CreatureFactory.ClearCreaturesOutsideBounds(creatures);

            _player.Update();
            cursor.Update();
            NCamera.Camera_Bound(_player.GetBounds());

            if(!GotFirstGC)
            {
                MemoryUsageAtStart = System.GC.GetTotalMemory(true);
                GotFirstGC = true;
                //GCInfo = new Thread(GetGCInfo);
                //GCInfo.Start();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            NCamera.Camera_Use(ref spriteBatch, SpriteSortMode.FrontToBack);
            grid.Draw(ref spriteBatch, _player.Position, Settings.RenderDistance);

            _player.Draw(ref spriteBatch);

            for(int i = 0; i < creatures.Count; i++)
            {
                if(TestRenderBounds(creatures[i].GetPosition(), _player.Position))
                {
                    creatures[i].Update();
                    creatures[i].Draw(ref spriteBatch);
                    if (creatures[i].GetHealth() <= 0)
                    {
                        creatures[i].Die(_player);
                        creatures.RemoveAt(i);
                        i--;
                        continue;
                    }
                    if (creatures[i].GetFightingState() == true && fights.Count == 0)
                    {
                        fights.Add(new FightingManager(_player, creatures[i]));
                    }

                }
                
            }
            
            
            grid.DrawAdditions(ref spriteBatch, _player.Position, Settings.RenderDistance);
            cursor.Draw(ref spriteBatch);
            _player._UI.Draw(ref spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();

            
            
            NDrawing.FPS_Draw(new Vector2(0, 0), Color.Red, gameTime, Settings.font, ref spriteBatch);
            //spriteBatch.DrawString(font, $"" +
            //    $"X:{(Mouse.GetState().X - graphics.PreferredBackBufferWidth / 2) + _player.Position.X}\n" +
            //    $"Y:{(Mouse.GetState().Y - graphics.PreferredBackBufferHeight / 2) + _player.Position.Y}\n" +
            //    $"Width: {GraphicsDevice.Viewport.Width}\n" +
            //    $"Heigth: {GraphicsDevice.Viewport.Height}\n" +
            //    $"Creatures: {creatures.Count}\n" +
            //    $"Tiles: {grid.map.Length}\n" +
            //    $"Memory usage before full GC {((float)System.GC.GetTotalMemory(false) / 1024.0 / 1024.0).ToString("0.00")} MB\n" +
            //    $"Memory usage after full GC: {((float)CurrentMemoryUsage / 1024.0 / 1024.0).ToString("0.00")} MB\n" +
            //    $"Memory usage change: {MemoryUsageDifference} bytes",
            //    new Vector2(5, 100), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
