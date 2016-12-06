using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciceTileSheet
{
    class GameObjectTile
    {
        //Nombre total de tuiles pour les lignes qui entrent dans l'écran
        public const int LIGNE = 10;
        //Le nombre total de tuiles par colonne dans un écran
        public const int COLONNE = 17;
        //Dimensions d'une tuile
        public const int LARGEUR_TUILE = 56;
        public const int HAUTEUR_TUILE = 56;

        //La texture tileLayer
        public Texture2D texture;
        public Vector2 position;

        // 1 = pierre, 2 = gazon 3 = sable (voir image tiles1.jpg)        
        // Vous pouvez aussi faire un tableau de tuiles si vous en avez plusieurs
        public Rectangle rectPierre = new Rectangle(877, 116, 56, 56);
        public Rectangle rectGazon = new Rectangle(164, 280, 56, 56);
        public Rectangle rectSable = new Rectangle(1067, 195, 16, 16);

        //La carte qui est affichée
        public int[,] map = {
                            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            };

        public void Draw(SpriteBatch spriteBatch) // Draw board
        {
            for (int i = 0; i < LIGNE; i++)
            {
                position.Y = (i * HAUTEUR_TUILE);
                for (int j = 0; j < COLONNE; j++)
                {
                    position.X = (j * LARGEUR_TUILE);
                    switch (map[i, j])
                    {
                        case 1:
                            spriteBatch.Draw(texture, position, rectPierre, Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(texture, position, rectGazon, Color.White);
                            break;
                        case 3:
                            spriteBatch.Draw(texture, position, rectSable, Color.White);
                            break;
                    }
                }
            }
        }

    }
}
