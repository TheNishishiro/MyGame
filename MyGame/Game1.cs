using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Creatures;
using MyGame.Creatures.Hostile;
using MyGame.GridElements;
using NFramework;
using System;
using System.Collections.Generic;
using static MyGame.Settings;

namespace MyGame
{
    public class Game1 : Game
    {
        public static GraphicsDevice _GraphicsDevice;
        public static ContentManager _Content;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        
        List<ICreature> creatures;
        List<FightingManager> fights = new List<FightingManager>();
        
        
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
            font = Content.Load<SpriteFont>("font");
            grid = new Grid(Content, Settings.WorldSizeBlocks, Settings.WorldSizeBlocks);

            _player = new Player(32 * 5, 32 * 5, Content.Load<Texture2D>("player"));
            cursor = new Cursor(Content.Load<Texture2D>("Light_Blue"));
            
            creatures = new List<ICreature>()
            {
                new Wolf(32 * 8, 32 * 5, Content.Load<Texture2D>("wolf")),
                new Rat(32 * 8, 32 * 5, Content.Load<Texture2D>("rat")),
                new Wolf(32 * 6, 32 * 5, Content.Load<Texture2D>("wolf"))
            };
            
            NCamera.Camera_CreateViewport(GraphicsDevice.Viewport, 800, 600);
        }

        protected override void UnloadContent()
        {

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

            _player.Update();
            cursor.Update();
            NCamera.Camera_Bound(_player.GetBounds());
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            NCamera.Camera_Use(ref spriteBatch);
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
            spriteBatch.End();

            spriteBatch.Begin();

            _player._UI.Draw(ref spriteBatch);
            NDrawing.FPS_Draw(new Vector2(5, 200), Color.Red, gameTime, Settings.font, ref spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
