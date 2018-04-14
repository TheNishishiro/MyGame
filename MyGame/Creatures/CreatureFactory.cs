using MyGame.Creatures.Hostile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyGame.Settings;

namespace MyGame.Creatures
{
    class CreatureFactory
    {
        static float posXRange, posYRange;
        public static void AddCreature(List<ICreature> creatures)
        {
            
            posXRange = 50 * GridSize;
            posYRange = 50 * GridSize;

            float posX, posY;

            posX = rnd.Next((int)(_player.GetPosition().X - posXRange), (int)(_player.GetPosition().X + posXRange));
            posY = rnd.Next((int)(_player.GetPosition().Y - posYRange), (int)(_player.GetPosition().Y + posYRange));

            posX = GridSize * (posX / GridSize) - (posX % GridSize);
            posY = GridSize * (posY / GridSize) - (posY % GridSize);

            if (posX < 0)
                posX = 0;
            else if (posX > WorldSizePixels)
                posX = WorldSizePixels;
            if (posY < 0)
                posY = 0;
            else if (posY > WorldSizePixels)
                posY = WorldSizePixels;

            switch (rnd.Next(2))
            {
                case 0:
                    creatures.Add(new Wolf(posX, posY, Textures.WolfTexture));
                    break;
                case 1:
                    creatures.Add(new Rat(posX, posY, Textures.RatTexture));
                    break;
            }
        }

        public static void ClearCreaturesOutsideBounds(List<ICreature> creatures)
        {
            posXRange = 50 * GridSize;
            posYRange = 50 * GridSize;

            for (int i = 0; i < creatures.Count; i++)
            {
                if(!TestRenderBounds(creatures[i].GetPosition(), _player.GetPosition(), 50))
                {
                    creatures.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
