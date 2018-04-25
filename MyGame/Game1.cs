using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Creatures;
using MyGame.GridElements;
using MyGame.Items;
using MyGame.UI;
using NFramework;
using System;
using System.Collections.Generic;
using System.IO;
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
        bool debug = false;
        long MemoryUsageAtStart = 0, MemoryUsageDifference = 0, CurrentMemoryUsage = 0;


        Thread GCInfo, DebugConsole;


        private void GetGCInfo()
        {
            while (true)
            {
                CurrentMemoryUsage = System.GC.GetTotalMemory(true);
                MemoryUsageDifference = CurrentMemoryUsage - MemoryUsageAtStart;
                Thread.Sleep(2000);
            }
        }

        private void Debug()
        {
            while (true)
            {
                string command = "";
                Console.Write("> ");
                command = Console.ReadLine();
                string[] options = command.Split(' ');
                try
                {
                    switch (options[0].ToLower())
                    {
                        case "tp":
                            _player.Position = new Vector2(float.Parse(options[1]) * GridSize, float.Parse(options[2]) * GridSize);
                            break;
                        case "setbase":
                            _player.baseStats[options[1]] = int.Parse(options[2]);
                            break;
                        case "togglecheatmenu":
                            _player.ShowCheatMenu = !_player.ShowCheatMenu;
                            break;
                        case "giveitem":
                            try
                            {
                                if (options.Length <= 1)
                                    _player.AddItem();
                                else
                                    _player.Inventory.Add(Textures.ItemTemplates[options[1]].CreateCopy());
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                            break;
                        case "reload":
                            Textures.LoadTextures();
                            Console.WriteLine("reloading done.");
                            break;
                        case "spawnenemy":
                            if (options.Length == 1)
                                CreatureFactory.AddCreature(creatures, _player.Position.X, _player.Position.Y);
                            else
                                CreatureFactory.AddCreature(creatures, _player.Position.X, _player.Position.Y, options[1]);
                            break;
                        case "spawnobject":
                            if (options.Length == 1)
                                AdditionFactory.SpawnAddition(_player.Position);
                            else if (options.Length == 2)
                                AdditionFactory.SpawnAddition(_player.Position, options[1]);
                            else
                                AdditionFactory.SpawnAddition(_player.Position, options[1], options[2]);
                            break;
                        case "giveallitems":
                            foreach(KeyValuePair<string, IItems> entry in Textures.ItemTemplates)
                            {
                                Console.Write($"Spawning {entry.Key}... ");
                                _player.Inventory.Add(entry.Value.CreateCopy());
                                Console.WriteLine("done");
                            }
                            break;
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            DebugConsole = new Thread(Debug);
            DebugConsole.Start();
        }

        protected override void Initialize()
        {
            if (Directory.Exists(".\\Data"))
                base.Initialize();
            else
            {
                Console.WriteLine("Data folder could not be found!");
                Console.ReadLine();
            }

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _Content = Content;
            _GraphicsDevice = GraphicsDevice;
            System.Diagnostics.Stopwatch sw2 = new System.Diagnostics.Stopwatch();
            sw2.Start();
            cursor = new Cursor(Content.Load<Texture2D>("Light_Blue"));
            Textures.LoadTextures();

            font = Content.Load<SpriteFont>("font");
            font2 = Content.Load<SpriteFont>("font2");
            font3 = Content.Load<SpriteFont>("font3");

            font = font2 = font3;
            Console.WriteLine("Generating world...");
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            grid = new Grid(Content, WorldSizeBlocks, WorldSizeBlocks);
            sw.Stop();
            Console.WriteLine($"World created in {sw.Elapsed.TotalSeconds} sec");
            _player = new Player(WorldSizePixels/2, WorldSizePixels / 2, Content.Load<Texture2D>("player"));
            _player._UI = new MainUI(_player);
            creatures = new List<ICreature>();
            for (int i = 0; i < CreatureLimit; i++)
            {
                CreatureFactory.AddCreature(creatures);
            }

            graphics.PreferredBackBufferWidth = (int)(graphics.PreferredBackBufferWidth * 1.3);
            graphics.PreferredBackBufferHeight = (int)(graphics.PreferredBackBufferHeight * 1.3);
            graphics.HardwareModeSwitch = false; // Causes window to shrink when fullscreened and windowed then moved

            
            graphics.ApplyChanges();
            NCamera.Camera_CreateViewport(GraphicsDevice.Viewport, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height + 120);
            sw2.Stop();
            Console.WriteLine($"All done in {sw2.Elapsed.TotalSeconds} sec");
        }

        protected override void UnloadContent()
        {

            if (GCInfo != null)
            {
                try
                {
                    if (GCInfo.ThreadState == ThreadState.Suspended)
                    {
                        GCInfo.Resume();
                        GCInfo.Abort();
                    }
                    else
                    {
                        GCInfo.Abort();
                    }
                }
                catch { } // Useless catch
            }
            DebugConsole.Abort();
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

            if(!GotFirstGC && debug == true)
            {
                MemoryUsageAtStart = System.GC.GetTotalMemory(true);
                GotFirstGC = true;
                GCInfo = new Thread(GetGCInfo);
                GCInfo.Start();
            }
            
            NControls._NewKeyState();
                KeyboardShortcuts();
            NControls._OldKeyState();

            base.Update(gameTime);
        }

        protected void KeyboardShortcuts()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (NControls.GetSingleKeyPress(Keys.L))
            {
                debug = !debug;
                if (GCInfo != null)
                {
                    if (GCInfo.ThreadState == ThreadState.Suspended)
                        GCInfo.Resume();
                    else
                        GCInfo.Suspend();
                }
            }
            if (NControls.GetSingleKeyPress(Keys.P))
            {
                graphics.ToggleFullScreen();
                graphics.ApplyChanges();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            NCamera.Camera_Use(ref spriteBatch, SpriteSortMode.FrontToBack);
            _player.Draw(ref spriteBatch);
            _player._UI.Draw(ref spriteBatch);
            for (int i = 0; i < creatures.Count; i++)
            {
                if (TestRenderBounds(creatures[i].GetPosition(), _player.Position))
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
            grid.Draw(ref spriteBatch, _player.Position, Settings.RenderDistance);

            cursor.Draw(ref spriteBatch);


            spriteBatch.End();

            spriteBatch.Begin();



            NDrawing.FPS_Draw(new Vector2(0, 0), Color.Red, gameTime, Settings.font, ref spriteBatch);

            if (debug)
                spriteBatch.DrawString(font, $"" +
                    $"X:{(Mouse.GetState().X - graphics.PreferredBackBufferWidth / 2) + _player.Position.X}\n" +
                    $"Y:{(Mouse.GetState().Y - graphics.PreferredBackBufferHeight / 2) + _player.Position.Y}\n" +
                    $"Width: {GraphicsDevice.Viewport.Width}\n" +
                    $"Heigth: {GraphicsDevice.Viewport.Height}\n" +
                    $"Creatures: {creatures.Count}\n" +
                    $"Tiles: {grid.map.Length}\n" +
                    $"Memory usage before full GC {((float)System.GC.GetTotalMemory(false) / 1024.0 / 1024.0).ToString("0.00")} MB\n" +
                    $"Memory usage after full GC: {((float)CurrentMemoryUsage / 1024.0 / 1024.0).ToString("0.00")} MB\n" +
                    $"Memory usage change: {MemoryUsageDifference} bytes\n" +
                    $"Mouse over layer: {highestLayerTarget}",
                    new Vector2(5, 100), Color.White);

            spriteBatch.End();

            Settings.highestLayerTarget = 0;
            base.Draw(gameTime);
        }
    }
}
