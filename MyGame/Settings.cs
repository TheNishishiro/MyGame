using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Creatures;
using MyGame.GridElements;
using MyGame.Items;
using MyGame.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Settings
    {
        public const float
            entityLayer = 0.5f,
            tileLayer = 0.05f,
            UILayer = 0.9f,
            MainUILayer = 0.95f,
            tileAdditionTopLayer = 0.65f,
            tileAdditionBottomLayer = 0.4f;

        public const string
            Material_Wood = "Wood",
            Material_Stone = "Stone",
            Material_Gold = "Gold";

        public const string
            Sword = "Sword",
            Fist = "Fisting",
            Axe = "Axe",
            Mace = "Mace",
            Defence = "Armor",
            Mining = "Mining",
            SkillLevel = "level",
            SkillLevelPoints = "levelpoints",
            SkillLevelPointsNeeded = "levelpointsneeded";

        public const string
            Strength = "Strength",
            Inteligence = "Inteligence",
            Dexterity = "Dexterity",
            Vitality = "Vitality";

        public const int VirtualHeight = 624;
        public const int VirtualWidth = 1040;

        public const int GridSize = 32;
        public static int CreatureLimit = 100;
        public const int RenderDistance = 22;
        public const int WorldSizeBlocks = 1000;
        public const int WorldSizePixels = WorldSizeBlocks * 32;

        public static Random rnd = new Random();

        public static Cursor cursor;
        public static SpriteFont font, font2, font3;
        public static Grid grid;
        public static Player _player;

        public static int TextButtonScaling = 9;

        public static float highestLayerTarget = 0;

        public static bool TestRenderBounds(Vector2 entity, Vector2 player, int RenderDistance = RenderDistance)
        {
            return entity.X >= player.X - RenderDistance * 32 &&
                    entity.X <= player.X + RenderDistance * 32 &&
                    entity.Y >= player.Y - RenderDistance * 32 &&
                    entity.Y <= player.Y + RenderDistance * 32;
        }

    }

    class Textures
    {
        public static Texture2D Button;

        public static Texture2D ItemBackground;
        public static Texture2D ItemBorder;

        public static List<Texture2D> Swords;

        public static Dictionary<string, Texture2D> EnemyTextures;
        public static Dictionary<string, Texture2D> AdditionTextures;
        public static Dictionary<string, Texture2D> ItemTextures;

        public static Dictionary<string, ICreature> EnemyTemplates;
        public static Dictionary<string, IItems> ItemTemplates;
        public static Dictionary<string, Addition> GeneratorAdditionTemplates;
        public static Dictionary<string, Addition> SpawnableAdditionTemplates;

        public static Texture2D UITargetTexture;
        public static Texture2D UIAvatarRingTexture;
        public static Texture2D UIBarBackgroundTexture;
        public static Texture2D UIBarBorderTexture;
        public static Texture2D UIBarTexture;
        public static Texture2D UITopCornerTexture;
        public static Texture2D UIMainSideBar;
        public static Texture2D UIEqIcons;
        public static Texture2D UIPlusIcon;
             
        public static Texture2D Avatar;

        public static void LoadTextures()
        {
            EnemyTextures = new Dictionary<string, Texture2D>();
            AdditionTextures = new Dictionary<string, Texture2D>();
            ItemTextures = new Dictionary<string, Texture2D>();

            EnemyTemplates = new Dictionary<string, ICreature>();
            ItemTemplates = new Dictionary<string, IItems>();
            GeneratorAdditionTemplates = new Dictionary<string, Addition>();
            SpawnableAdditionTemplates = new Dictionary<string, Addition>();
            EnemyTextures = LoadTexturesFromFile(".\\Data\\EnemyTextures.xml");
            AdditionTextures = LoadTexturesFromFile(".\\Data\\GridAdditionTextures.xml");
            ItemTextures = LoadTexturesFromFile(".\\Data\\ItemTextures.xml");
            LoadObjectTemplates();


            Button = Game1._Content.Load<Texture2D>("button");


            UIAvatarRingTexture = Game1._Content.Load<Texture2D>("UI/AvatarRing");
            UIBarBackgroundTexture = Game1._Content.Load<Texture2D>("UI/Bar_background");
            UIBarBorderTexture = Game1._Content.Load<Texture2D>("UI/Bar_border");
            UIBarTexture = Game1._Content.Load<Texture2D>("UI/HP_bar");
            UITopCornerTexture = Game1._Content.Load<Texture2D>("UI/TopCorner");
            Avatar = Game1._Content.Load<Texture2D>("UI/Avatars/Avatar1");
            UITargetTexture = Game1._Content.Load<Texture2D>("UI/Target");
            UIMainSideBar = Game1._Content.Load<Texture2D>("UI/mainSideBar");
            UIEqIcons = Game1._Content.Load<Texture2D>("UI/EqIcons");
            UIPlusIcon = Game1._Content.Load<Texture2D>("UI/plusButton");

            ItemBackground = Game1._Content.Load<Texture2D>("UI/item_background");
            ItemBorder = Game1._Content.Load<Texture2D>("UI/item_border");

            Swords = new List<Texture2D>();
            
            string[] files = Directory.GetFiles("./Content/Items/");
            foreach (string file in files)
            {
                Swords.Add(Game1._Content.Load<Texture2D>("Items/" + file.Split('/')[3].Split('.')[0]));
            }
        }
        public static void LoadObjectTemplates()
        {
            AdditionFactory.CreateAdditionTemplate(GeneratorAdditionTemplates, "Generatable");
            AdditionFactory.CreateAdditionTemplate(SpawnableAdditionTemplates, "Spawnable");
            CreatureFactory.LoadCreatureTemplate(EnemyTemplates);
            ItemFactory.CreateItemTemplate(ItemTemplates);
        }

        public static Dictionary<string, Texture2D> LoadTexturesFromFile(string path)
        {
            Dictionary<string, Texture2D> dic = new Dictionary<string, Texture2D>();
            Console.WriteLine("Loading textures from " + path + "...");
            try
            {
                String[] lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string nospace_line = line.Replace(" ", String.Empty);
                    string[] components = nospace_line.Split(',');
                    string id = "";
                    string texture = "";
                    foreach (string component in components)
                    {

                        string[] property = component.Split(':');
                        if (property[0].ToLower() == "id")
                            id = property[1].Replace("\"", String.Empty);
                        if (property[0].ToLower() == "name")
                            texture = property[1].Replace("\"", String.Empty);
                    }
                    FileStream fileStream = new FileStream(".\\Data\\"+texture, FileMode.Open);
                    Texture2D spriteAtlas = Texture2D.FromStream(Game1._GraphicsDevice, fileStream);

                    if (!dic.ContainsKey(id))
                    {
                        dic.Add(id, spriteAtlas);
                        Console.WriteLine("\tLoaded: " + line);
                    }
                    else
                    {
                        Console.WriteLine("Didn't load: " + line + ", ID have already been found");
                    }
                    fileStream.Dispose();

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(path + "\n" + ex.Message);
            }
            return dic;
        }

        
    }
}
