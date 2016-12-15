using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciceTileSheet
{
    class GameObjectAnime
    {
        public bool estVivant;
        public Texture2D sprite;
        public Vector2 vitesse;
        public Vector2 direction;
        public Rectangle position;
        public Rectangle spriteAfficher;
        public enum etats { attenteDroite, attenteGauche, attenteHaut, attenteBas, runDroite, runGauche, runHaut, runBas };
        public etats objetState;


        //Compteur qui changera le sprite affiché
        private int cpt = 0;

        //GESTION DES TABLEAUX DE SPRITES (chaque sprite est un rectangle dans le tableau)
        int runState = 0; //État de départ

        int nbEtatRun = 4; //Combien il y a de rectangles pour l’état “courrir”
        public Rectangle[] tabRunDroite = {
            new Rectangle(1, 80, 52, 83),
            new Rectangle(62, 80, 52, 83),
            new Rectangle(130, 80, 52, 83),
            new Rectangle(188, 80, 52, 83)};

        public Rectangle[] tabRunGauche = {
            new Rectangle(1, 166, 52, 84),
            new Rectangle(61, 166, 52, 84),
            new Rectangle(129, 166, 52, 84),
            new Rectangle(188, 166, 52, 84) };

        public Rectangle[] tabRunHaut = {
            new Rectangle(1, 256, 57, 73),
            new Rectangle(67, 253, 57, 73),
            new Rectangle(130, 256, 57, 73),
            new Rectangle(194, 253, 57, 73)};

        public Rectangle[] tabRunBas = {
            new Rectangle(1, 3, 57, 73),
            new Rectangle(67, 1, 57, 73),
            new Rectangle(130, 3, 57, 73),
            new Rectangle(194, 1, 57, 73) };

        int waitState = 0;
        public Rectangle[] tabAttenteDroite = {
            new Rectangle(62, 80, 52, 83) };

        public Rectangle[] tabAttenteGauche = {
            new Rectangle(130, 166, 52, 83) };

        public Rectangle[] tabAttenteHaut = {
            new Rectangle(67, 253, 57, 73) };

        public Rectangle[] tabAttenteBas = {
            new Rectangle(67, 1, 57, 73) };

        public virtual void Update(GameTime gameTime)
        {
            if (objetState == etats.attenteDroite)
            {
                spriteAfficher = tabAttenteDroite[waitState];
            }
            if (objetState == etats.attenteGauche)
            {
                spriteAfficher = tabAttenteGauche[waitState];
            }
            if (objetState == etats.attenteHaut)
            {
                spriteAfficher = tabAttenteHaut[waitState];
            }
            if (objetState == etats.attenteBas)
            {
                spriteAfficher = tabAttenteBas[waitState];
            }
            if (objetState == etats.runDroite)
            {
                spriteAfficher = tabRunDroite[runState];
            }
            if (objetState == etats.runGauche)
            {
                spriteAfficher = tabRunGauche[runState];
            }
            if (objetState == etats.runHaut)
            {
                spriteAfficher = tabRunHaut[runState];
            }
            if (objetState == etats.runBas)
            {
                spriteAfficher = tabRunBas[runState];
            }
            //Compteur permettant de gérer le changement d'images
            cpt++;
            if (cpt == 6) //Vitesse défilement
            {
                //Gestion de la course
                runState++;
                if (runState == nbEtatRun)
                {
                    runState = 0;
                }
                cpt = 0;
            }
        }
    }
}

