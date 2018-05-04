using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Creatures;
using MyGame.GridElements;
using MyGame.Items;
using MyGame.Spells;
using MyGame.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Names
    {
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
            Vitality = "Vitality",
            Luck = "Luck",
            Greed = "Greed",
            Survivability = "Survivability",
            Faith = "Faith",
            Resistance = "Resistance",
            Endurance = "Endurance";

        public const string
            Necklace = "Necklace",
            Shield = "Shield",
            Weapon = "Weapon",
            Armor = "Armor",
            Ring = "Ring",
            Book = "Book",
            Scroll = "Scroll",
            Consumable = "Consumable",
            Misc = "Misc";

        public const string
            BiomGrass = "Grass",
            BiomSand = "Sand",
            BiomSwamp = "Swamp";
    }

    class Global
    {
        public static List<SpellCell> spellCells = new List<SpellCell>();
    }

    class Settings
    {
        public const float
            entityLayer = 0.5f,
            tileLayer = 0.05f,
            UILayer = 0.9f,
            MainUILayer = 0.95f,
            tileAdditionTopLayer = 0.65f,
            tileAdditionBottomLayer = 0.4f;

        public const int MaxRandomValue = 10000;

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

        public static Texture2D Grass;
        public static Texture2D Water;
        public static Texture2D Sand;
        public static Texture2D Swamp;
        public static Texture2D[] WaterSides = new Texture2D[4*3];

        public static Dictionary<string, Texture2D> EnemyTextures;
        public static Dictionary<string, Texture2D> AdditionTextures;
        public static Dictionary<string, Texture2D> ItemTextures;
        public static Dictionary<string, Texture2D> SpellTextures;

        public static Dictionary<string, ICreature> EnemyTemplates;
        public static Dictionary<string, IItems> ItemTemplates;
        public static Dictionary<string, ITileAddition> GeneratorAdditionTemplates;
        public static Dictionary<string, ITileAddition> SpawnableAdditionTemplates;
        public static Dictionary<string, ITileAddition> ScriptedAdditions;
        public static Dictionary<string, ISpell> SpellTemplates;

        public static List<Biom> bioms = new List<Biom>();

        public static Texture2D UITargetTexture;
        public static Texture2D UIAvatarRingTexture;
        public static Texture2D UIBarBackgroundTexture;
        public static Texture2D UIBarBorderTexture;
        public static Texture2D UIBarTexture;
        public static Texture2D UITopCornerTexture;
        public static Texture2D UIMainSideBar;
        public static Texture2D UIEqIcons;
        public static Texture2D UIPlusIcon;
        public static Texture2D UIMessageBoxTexture;
        public static Texture2D UIArrowLeft, UIArrowRight, UIArrowUp, UIArrowDown;
        public static List<Texture2D> UIButtons;
        public static Texture2D Avatar;

        public static void LoadTextures()
        {
            Grass = Game1._Content.Load<Texture2D>("grass");
            Water = Game1._Content.Load<Texture2D>("water");
            Sand = Game1._Content.Load<Texture2D>("sand");
            Swamp = Game1._Content.Load<Texture2D>("swamp");

            WaterSides[0] = Game1._Content.Load<Texture2D>("waterL");
            WaterSides[1] = Game1._Content.Load<Texture2D>("waterU");
            WaterSides[2] = Game1._Content.Load<Texture2D>("waterR");
            WaterSides[3] = Game1._Content.Load<Texture2D>("waterD");
            WaterSides[4] = Game1._Content.Load<Texture2D>("waterSwampL");
            WaterSides[5] = Game1._Content.Load<Texture2D>("waterSwampU");
            WaterSides[6] = Game1._Content.Load<Texture2D>("waterSwampR");
            WaterSides[7] = Game1._Content.Load<Texture2D>("waterSwampD");
            WaterSides[8] = Game1._Content.Load<Texture2D>("waterSandL");
            WaterSides[9] = Game1._Content.Load<Texture2D>("waterSandU");
            WaterSides[10] = Game1._Content.Load<Texture2D>("waterSandR");
            WaterSides[11] = Game1._Content.Load<Texture2D>("waterSandD");

            
            bioms.Add(new Biom(Names.BiomGrass, Textures.Grass, Textures.WaterSides[0], Textures.WaterSides[1], Textures.WaterSides[2], Textures.WaterSides[3]));
            bioms.Add(new Biom(Names.BiomSwamp, Textures.Swamp, Textures.WaterSides[4], Textures.WaterSides[5], Textures.WaterSides[6], Textures.WaterSides[7]));
            bioms.Add(new Biom(Names.BiomSand, Textures.Sand, Textures.WaterSides[8], Textures.WaterSides[9], Textures.WaterSides[10], Textures.WaterSides[11]));

            EnemyTextures = new Dictionary<string, Texture2D>();
            AdditionTextures = new Dictionary<string, Texture2D>();
            ItemTextures = new Dictionary<string, Texture2D>();
            SpellTextures = new Dictionary<string, Texture2D>();

            EnemyTemplates = new Dictionary<string, ICreature>();
            ItemTemplates = new Dictionary<string, IItems>();
            GeneratorAdditionTemplates = new Dictionary<string, ITileAddition>();
            SpawnableAdditionTemplates = new Dictionary<string, ITileAddition>();
            ScriptedAdditions = new Dictionary<string, ITileAddition>();
            SpellTemplates = new Dictionary<string, ISpell>();

            CheckDataFolder();

            EnemyTextures = LoadTexturesFromFile(".\\Data\\EnemyTextures.xml");
            AdditionTextures = LoadTexturesFromFile(".\\Data\\GridAdditionTextures.xml");
            ItemTextures = LoadTexturesFromFile(".\\Data\\ItemTextures.xml");
            SpellTextures = LoadTexturesFromFile(".\\Data\\SpellTextures.xml");
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
            UIMessageBoxTexture = Game1._Content.Load<Texture2D>("UI/messageBox");
            UIArrowLeft = Game1._Content.Load<Texture2D>("UI/UIArrowLeft");
            UIArrowRight = Game1._Content.Load<Texture2D>("UI/UIArrowRight");
            UIArrowUp = Game1._Content.Load<Texture2D>("UI/UIArrowUp");
            UIArrowDown = Game1._Content.Load<Texture2D>("UI/UIArrowDown");

            UIButtons = new List<Texture2D>();
            UIButtons.Add(Game1._Content.Load<Texture2D>("UI/UIBackpack"));
            UIButtons.Add(Game1._Content.Load<Texture2D>("UI/UISkills"));
            UIButtons.Add(Game1._Content.Load<Texture2D>("UI/UIStats"));
            UIButtons.Add(Game1._Content.Load<Texture2D>("UI/UIResources"));
            UIButtons.Add(Game1._Content.Load<Texture2D>("UI/UISpells"));
            UIButtons.Add(Game1._Content.Load<Texture2D>("UI/UICraft"));
            UIButtons.Add(Game1._Content.Load<Texture2D>("UI/UIMenu"));

            ItemBackground = Game1._Content.Load<Texture2D>("UI/item_background");
            ItemBorder = Game1._Content.Load<Texture2D>("UI/item_border");
        }
        public static void LoadObjectTemplates()
        {
            AdditionFactory.CreateAdditionTemplate(GeneratorAdditionTemplates, "Generatable");
            AdditionFactory.CreateAdditionTemplate(SpawnableAdditionTemplates, "Spawnable");
            AdditionFactory.CreateAdditionTemplate(ScriptedAdditions, "Scripted");
            CreatureFactory.LoadCreatureTemplate(EnemyTemplates);
            ItemFactory.CreateItemTemplate(ItemTemplates);
            SpellFactory.CreateSpellTemplate(SpellTemplates);
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

        
        public static void CheckDataFolder()
        {
            if (Directory.Exists(".\\Data"))
            {

            }
            else
            {
                Console.WriteLine("Couldn't find Data folder... Attemtping to repair");
                Directory.CreateDirectory(".\\Data");
                Directory.CreateDirectory(".\\Data\\Additions");
                Directory.CreateDirectory(".\\Data\\Additions\\Generatable");
                Directory.CreateDirectory(".\\Data\\Additions\\Scripted");
                Directory.CreateDirectory(".\\Data\\Additions\\Spawnable");
                Directory.CreateDirectory(".\\Data\\Enemies");
                Directory.CreateDirectory(".\\Data\\EnemyTextures");
                Directory.CreateDirectory(".\\Data\\GridAdditionTextures");
                Directory.CreateDirectory(".\\Data\\Items");
                Directory.CreateDirectory(".\\Data\\ItemTextures");
                Directory.CreateDirectory(".\\Data\\Spells");
                Directory.CreateDirectory(".\\Data\\SpellTextures");
                File.Create(".\\Data\\Crafting.xml");
                File.Create(".\\Data\\EnemyTextures.xml");
                File.Create(".\\Data\\GridAdditionTextures.xml");
                File.Create(".\\Data\\ItemTextures.xml");
                File.Create(".\\Data\\SpellTextures.xml");
            }
        }
    }
}
