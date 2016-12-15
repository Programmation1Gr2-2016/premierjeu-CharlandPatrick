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
        public const int LIGNE = 7;
        //Le nombre total de tuiles par colonne dans un écran
        public const int COLONNE = 11;
        //Dimensions d'une tuile
        public const int LARGEUR_TUILE = 175;
        public const int HAUTEUR_TUILE = 175;

        //La texture tileLayer
        public Texture2D texture;
        public Vector2 position;

        // 1 = pierre, 2 = gazon 3 = sable (voir image tiles1.jpg)        
        // Vous pouvez aussi faire un tableau de tuiles si vous en avez plusieurs
        public Rectangle rectPierre = new Rectangle(175, 175, 175, 175);                //1
        public Rectangle rectPierreCoinUpGauche = new Rectangle(875, 745, 175, 175);  //2
        public Rectangle rectPierreCoinUpGNext = new Rectangle(1050, 920, 175, 175); //19
        public Rectangle rectPierreCoinUpDroite = new Rectangle(1565, 750, 175, 175);  //3
        public Rectangle rectPierreCoinDownGauche = new Rectangle(875, 1095, 175, 175);  //4
        public Rectangle rectPierreCoinDownDroite = new Rectangle(1570, 1095, 175, 175); //5
        public Rectangle rectPierreUp = new Rectangle(1050, 750, 175, 175);            //6
        public Rectangle rectPierreDown = new Rectangle(1050, 1100, 175, 175);            //7
        public Rectangle rectPierreCurveUpGauche = new Rectangle(1230, 405, 175, 175);  //8
        public Rectangle rectPierreCurveUpDroite = new Rectangle(1395, 400, 175, 175);  //9
        public Rectangle rectPierreCurveDownGauche = new Rectangle(1225, 570, 175, 175);  //10
        public Rectangle rectPierreCurveDownDroite = new Rectangle(1395, 570, 175, 175);  //11
        public Rectangle rectPierreGauche = new Rectangle(875, 920, 175, 175);            //17
        public Rectangle rectPierreDroite = new Rectangle(1565, 920, 175, 175);            //18
        public Rectangle rectFin = new Rectangle(1565, 920, 175, 175); //20
        public Rectangle rectPorte = new Rectangle(1400, 2145, 175, 175); //21
        public Rectangle rectBlessure = new Rectangle(1050, 2145, 175, 175); //22

        //La carte qui est affichée
        public int[,] map1 = {                      
                            {2,6,3,2,6,6,6,6,6,6,3},
                            {17,19,18,17,19,8,7,7,7,7,21},
                            {17,19,18,17,22,10,6,6,6,3,1},
                            {17,19,18,17,19,8,7,9,19,18,1},
                            {17,19,10,11,19,18,1,17,19,18,1},
                            {4,9,19,8,7,5,1,17,19,18,1},
                            {1,17,21,18,1,1,1,17,21,18,1},
                            };
        public int[,] map2 = {
                            {1,17,21,18,1,1,1,17,21,18,1},
                            {1,17,19,18,1,1,1,17,19,18,1},
                            {1,17,19,10,6,6,6,11,6,3,1},
                            {1,17,19,8,7,7,7,9,19,18,1},
                            {1,17,10,11,19,18,1,17,19,18,1},
                            {4,9,19,8,7,5,1,17,19,18,21},
                            {4,17,21,18,1,1,1,17,21,18,1},
                            };
        public int[,] map3 = {
                            {2,6,3,2,6,6,6,6,6,6,3},
                            {17,19,18,17,15,8,7,7,7,7,21},
                            {17,19,18,17,22,10,6,6,6,3,1},
                            {17,19,18,17,19,8,7,9,19,18,1},
                            {17,19,10,11,19,18,1,17,19,18,1},
                            {4,9,19,8,7,5,1,17,19,18,1},
                            {1,17,21,18,1,1,1,17,21,18,1},
                            };
        public int[,] map4 = {
                            {2,6,3,2,6,6,6,6,6,6,3},
                            {17,19,18,17,15,8,7,7,7,7,21},
                            {17,19,18,17,22,10,6,6,6,3,1},
                            {17,19,18,17,19,8,7,9,19,18,1},
                            {17,19,10,11,19,18,1,17,19,18,1},
                            {4,9,19,8,7,5,1,17,19,18,1},
                            {1,17,21,18,1,1,1,17,21,18,1},
                            };

        public void Draw(SpriteBatch spriteBatch) // Draw board
        {
            for (int i = 0; i < LIGNE; i++)
            {
                position.Y = (i * HAUTEUR_TUILE);
                for (int j = 0; j < COLONNE; j++)
                {
                    position.X = (j * LARGEUR_TUILE);
                    switch (map1[i, j])
                    {
                        case 1:
                            spriteBatch.Draw(texture, position, rectPierre, Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(texture, position, rectPierreCoinUpGauche, Color.White);
                            break;
                        case 3:
                            spriteBatch.Draw(texture, position, rectPierreCoinUpDroite, Color.White);
                            break;
                        case 4:
                            spriteBatch.Draw(texture, position, rectPierreCoinDownGauche, Color.White);
                            break;
                        case 5:
                            spriteBatch.Draw(texture, position, rectPierreCoinDownDroite, Color.White);
                            break;
                        case 6:
                            spriteBatch.Draw(texture, position, rectPierreUp, Color.White);
                            break;
                        case 7:
                            spriteBatch.Draw(texture, position, rectPierreDown, Color.White);
                            break;
                        case 8:
                            spriteBatch.Draw(texture, position, rectPierreCurveUpGauche, Color.White);
                            break;
                        case 9:
                            spriteBatch.Draw(texture, position, rectPierreCurveUpDroite, Color.White);
                            break;
                        case 10:
                            spriteBatch.Draw(texture, position, rectPierreCurveDownGauche, Color.White);
                            break;
                        case 11:
                            spriteBatch.Draw(texture, position, rectPierreCurveDownDroite, Color.White);
                            break;
                        case 17:
                            spriteBatch.Draw(texture, position, rectPierreGauche, Color.White);
                            break;
                        case 18:
                            spriteBatch.Draw(texture, position, rectPierreDroite, Color.White);
                            break;
                        case 19:
                            spriteBatch.Draw(texture, position, rectPierreCoinUpGNext, Color.White);
                            break;
                        case 20:
                            spriteBatch.Draw(texture, position, rectFin, Color.White);
                            break;
                        case 21:
                            spriteBatch.Draw(texture, position, rectPorte, Color.White);
                            break;
                        case 22:
                            spriteBatch.Draw(texture, position, rectBlessure, Color.White);
                            break;
                    }
                }
            }
        }

    }
}
