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
        public const int LIGNE = 12;
        //Le nombre total de tuiles par colonne dans un écran
        public const int COLONNE = 22;
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
        public Rectangle rectPierreDown = new Rectangle(1050, 1095, 175, 175);            //7
        public Rectangle rectPierreCurveUpGauche = new Rectangle(1230, 400, 175, 175);  //8
        public Rectangle rectPierreCurveUpDroite = new Rectangle(1395, 400, 175, 175);  //9
        public Rectangle rectPierreCurveDownGauche = new Rectangle(1225, 570, 175, 175);  //10
        public Rectangle rectPierreCurveDownDroite = new Rectangle(1395, 570, 175, 175);  //11
        public Rectangle rectWaterCoinDownDroite = new Rectangle(700, 525, 175, 175); //12
        public Rectangle rectWaterUp = new Rectangle(175, 350, 175, 175);            //13
        public Rectangle rectWaterSpecial = new Rectangle(1060, 0, 175, 175);            //14
        public Rectangle rectSable = new Rectangle(1225, 920, 175, 175);            //15
        public Rectangle rectGazon = new Rectangle(1050, 2100, 175, 175);            //16
        public Rectangle rectPierreGauche = new Rectangle(875, 920, 175, 175);            //17
        public Rectangle rectPierreDroite = new Rectangle(1565, 920, 175, 175);            //18


        //La carte qui est affichée
        public int[,] map = {                      //Switch here
                            {2,6,3,2,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,3},
                            {17,19,18,17,15,8,7,7,7,7,7,7,7,7,1,1,1,1,1,1,1,18},
                            {17,19,18,17,19,10,6,6,6,3,1,6,7,7,7,7,1,1,1,1,1,18},
                            {17,19,18,17,19,8,7,9,19,18,1,1,1,1,1,1,1,1,1,1,1,18},
                            {17,19,10,11,19,18,1,17,19,18,1,1,1,1,1,1,1,1,1,1,1,18},
                            {4,9,19,8,7,5,1,17,19,18,1,1,1,1,1,1,1,1,1,1,1,18},//switch here
                            {1,17,19,18,1,1,1,17,19,18,1,1,1,1,1,1,1,1,1,1,1,18},
                            {17,19,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,18},
                            {17,19,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,18},
                            {17,19,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,18},
                            {17,19,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,18},
                            {4,7,7,7,7,7,7,7,7,5,6,7,7,7,7,7,7,7,7,7,7,5},
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
                        case 12:
                            spriteBatch.Draw(texture, position, rectWaterCoinDownDroite, Color.White);
                            break;
                        case 13:
                            spriteBatch.Draw(texture, position, rectWaterUp, Color.White);
                            break;
                        case 14:
                            spriteBatch.Draw(texture, position, rectWaterSpecial, Color.White);
                            break;
                        case 15:
                            spriteBatch.Draw(texture, position, rectSable, Color.White);
                            break;
                        case 16:
                            spriteBatch.Draw(texture, position, rectGazon, Color.White);
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
                    }
                }
            }
        }

    }
}
