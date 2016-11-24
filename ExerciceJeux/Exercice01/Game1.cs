using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;

namespace Exercice01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        GameObject heros;
        GameObject fireball;
        Texture2D backgroundTexture;

        SpriteFont font; //Font
        string gameOver = "GAME OVER";
        string win = "WIN";

        bool imageInverse;
        bool gameWin = false;
        bool gameTimeOn = true;
        Random r;


        int ennemyStartingPosX = 300;
        int ennemyStartingPosY = 20;

        int nombreEnnemis = 2;
        int ennemyMort = 0;
        int nombreEnnemisCount = 0;
        int pointVie = 4;
        string totalGameTime = "";

        GameObject[] tabEnnemis;
        GameObject[] tabProjectiles;
        int[] tabPointVieEnnemi;

        SoundEffect sonExplosion;
        SoundEffect sonFanfare;
        SoundEffect sonGun;
        SoundEffect sonJet;
        SoundEffect sonDeath;
        SoundEffect sonPhaser;
        SoundEffectInstance explosion;
        SoundEffectInstance fanfare;
        SoundEffectInstance gun;
        SoundEffectInstance jetFlyby;
        SoundEffectInstance death;
        SoundEffectInstance phaser;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;

            Window.Position = new Point(0, 0);

            this.graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            fenetre.Width = graphics.GraphicsDevice.DisplayMode.Width;
            fenetre.Height = graphics.GraphicsDevice.DisplayMode.Height;

            this.backgroundTexture = this.Content.Load<Texture2D>("resized_daf_12685_super_mario_bros.jpg");
            // font

            font = Content.Load<SpriteFont>("Font\\Font");

            // sounds

            sonExplosion = Content.Load<SoundEffect>("Sounds\\explosion_x");
            explosion = sonExplosion.CreateInstance();
            sonDeath = Content.Load<SoundEffect>("Sounds\\pacman_dies_y");
            death = sonDeath.CreateInstance();
            sonFanfare = Content.Load<SoundEffect>("Sounds\\fanfare3");
            fanfare = sonFanfare.CreateInstance();
            sonGun = Content.Load<SoundEffect>("Sounds\\gun_44mag_11");
            gun = sonGun.CreateInstance();
            sonJet = Content.Load<SoundEffect>("Sounds\\jet_doppler2");
            jetFlyby = sonJet.CreateInstance();
            sonPhaser = Content.Load<SoundEffect>("Sounds\\phasers3");
            phaser = sonPhaser.CreateInstance();

            //Music
            Song song = Content.Load<Song>("Sounds\\Nowhere Land");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);

            //Ennemi array
            tabEnnemis = new GameObject[nombreEnnemis];
            tabProjectiles = new GameObject[nombreEnnemis];
            tabPointVieEnnemi = new int[nombreEnnemis];

            heros = new GameObject();
            heros.estVivant = true;
            heros.vitesse = 5;
            heros.sprite = Content.Load<Texture2D>("Mario.png");
            heros.position = heros.sprite.Bounds;

            r = new Random(); //Random for random direction

            for(int i = 0; i<nombreEnnemis; i++)
            {
                tabEnnemis[i] = new GameObject();
                tabEnnemis[i].estVivant = false;
                tabPointVieEnnemi[i] = 3;
                tabEnnemis[i].vitesse = 2;
                tabEnnemis[i].sprite = Content.Load<Texture2D>("Bowserjr_MP9.png");
                tabEnnemis[i].position = tabEnnemis[i].sprite.Bounds;
                tabEnnemis[i].position.X = fenetre.Right - ennemyStartingPosX;
                tabEnnemis[i].position.Y = fenetre.Top + ennemyStartingPosY;
                ennemyStartingPosX += 300;
                ennemyStartingPosY += 600;
                tabEnnemis[i].direction.X = r.Next(-4, 5);
                tabEnnemis[i].direction.Y = r.Next(-4, 5);
            }
            for (int i = 0; i < nombreEnnemis; i++)
            {
                tabProjectiles[i] = new GameObject();
                tabProjectiles[i].estVivant = false;
                tabProjectiles[i].vitesse = 10;
                tabProjectiles[i].sprite = Content.Load<Texture2D>("Bullet_Bill.png");
                tabProjectiles[i].position = tabProjectiles[i].sprite.Bounds;
            }
                
            fireball = new GameObject();
            fireball.estVivant = false;
            fireball.vitesse = 25;
            fireball.sprite = Content.Load<Texture2D>("Mario-Fireball.png");
            fireball.position = fireball.sprite.Bounds;
            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            
            if(heros.estVivant)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D)) //Droite
                {
                    heros.position.X += heros.vitesse;
                    imageInverse = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W)) //Up
                {
                    heros.position.Y -= heros.vitesse;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A)) //Gauche
                {
                    heros.position.X -= heros.vitesse;
                    imageInverse = false;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S)) //Down
                {
                    heros.position.Y += heros.vitesse;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space)) //Shoot
                {
                    fireball.position.X = heros.position.X;
                    fireball.position.Y = heros.position.Y;
                    phaser.Play();
                    fireball.estVivant = true;
                }
            }
                if (nombreEnnemisCount < tabEnnemis.Length)
                {
                    if (nombreEnnemisCount * 10 < gameTime.TotalGameTime.Seconds)
                    {
                    tabEnnemis[nombreEnnemisCount].estVivant = true;
                    nombreEnnemisCount++;
                    }
                }

            UpdateHeros();
            UpdateEnnemis();
            UpdateProjectiles();
            Collision();

            base.Update(gameTime);
        }

        protected void UpdateHeros()//Check game limit (frame)
        {
            if (heros.position.X < fenetre.Left)
            {
                heros.position.X = fenetre.Left;
            }
            if (heros.position.X + heros.sprite.Bounds.Width > fenetre.Right)
            {
                heros.position.X = fenetre.Right - heros.sprite.Bounds.Width;
            }
            if (heros.position.Y < fenetre.Top)
            {
                heros.position.Y = fenetre.Top;
            }
            if (heros.position.Y + heros.sprite.Bounds.Height > fenetre.Bottom)
            {
                heros.position.Y = fenetre.Bottom - heros.sprite.Bounds.Height;
            }
        }

        protected void UpdateEnnemis()
        {
            for (int i = 0; i < nombreEnnemis; i++)
            {
                if (tabEnnemis[i].estVivant) //Ennemis move
                {
                    tabEnnemis[i].position.X += (int)tabEnnemis[i].direction.X;
                    tabEnnemis[i].position.Y += (int)tabEnnemis[i].direction.Y;
                    if (tabEnnemis[i].position.X < fenetre.Left)
                    {
                        tabEnnemis[i].direction.X *= -1;
                    }
                    if (tabEnnemis[i].position.X + tabEnnemis[i].sprite.Bounds.Width > fenetre.Right)
                    {
                        tabEnnemis[i].direction.X *= -1;
                    }
                    if (tabEnnemis[i].position.Y < fenetre.Top)
                    {
                        tabEnnemis[i].direction.Y *= -1;
                    }
                    if (tabEnnemis[i].position.Y + tabEnnemis[i].sprite.Bounds.Height > fenetre.Bottom)
                    {
                        tabEnnemis[i].direction.Y *= -1;
                    }
                }
            }
                
        }

        protected void UpdateProjectiles()
        {
            //Projectile move
            for (int i = 0; i < nombreEnnemis; i++)
            {
                if (heros.estVivant && tabEnnemis[i].estVivant)
                {
                    if(tabProjectiles[i].estVivant==false)
                    {
                        gun.Play();
                        jetFlyby.Play();
                    }
                    tabProjectiles[i].estVivant = true;
                }
                if (tabProjectiles[i].estVivant && tabEnnemis[i].estVivant)
                {
                    tabProjectiles[i].position.X -= tabProjectiles[i].vitesse;
                    if (tabProjectiles[i].position.X < fenetre.Left - 500)
                    {
                        tabProjectiles[i].estVivant = false;
                        tabProjectiles[i].position.X = tabEnnemis[i].position.X;
                        tabProjectiles[i].position.Y = tabEnnemis[i].position.Y;
                    }
                }
            }
            

            //Fireball move

            if (fireball.estVivant && heros.estVivant)
            {
                fireball.position.X += fireball.vitesse;
                if (fireball.position.X > fenetre.Right + 500)
                {
                    fireball.estVivant = false;
                    fireball.position.X = heros.position.X;
                    fireball.position.Y = heros.position.Y;
                }
            }
        }

        protected void Collision()
        {
            for (int i = 0; i < nombreEnnemis; i++)
            {
                if (tabProjectiles[i].estVivant && heros.estVivant && tabProjectiles[i].position.Intersects(heros.position))
                {
                    tabProjectiles[i].estVivant = false;
                    explosion.Play();
                    pointVie--;
                    tabProjectiles[i].position.X = tabEnnemis[i].position.X;
                    tabProjectiles[i].position.Y = tabEnnemis[i].position.Y;
                    if (pointVie == 0)
                    {
                        death.Play();
                        heros.estVivant = false;
                        tabProjectiles[i].estVivant = false;
                    }
                }
                if (fireball.estVivant && fireball.position.Intersects(tabEnnemis[i].position))
                {
                    explosion.Play();
                    tabPointVieEnnemi[i]--;
                    fireball.estVivant = false;
                    fireball.position.X = heros.position.X;
                    fireball.position.Y = heros.position.Y;
                    if (tabPointVieEnnemi[i] == 0)
                    {
                        ennemyMort++;
                        fanfare.Play();
                        tabEnnemis[i].estVivant = false;
                        tabEnnemis[i].position.X = fenetre.Right + 1000;
                        tabEnnemis[i].position.Y = fenetre.Top + 1000;
                        tabProjectiles[i].position.X = tabEnnemis[i].position.X;
                        tabProjectiles[i].position.Y = tabEnnemis[i].position.Y;
                        if(ennemyMort==2)
                        {
                            {
                                gameWin = true;
                            }
                        }
                    }
                }
            }
           
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            this.spriteBatch.Draw(backgroundTexture, fenetre, Color.White);

            if (heros.estVivant)
            {
                if (imageInverse)
                {
                    spriteBatch.Draw(heros.sprite, heros.position, Color.White);
                }
                else
                {
                    spriteBatch.Draw(heros.sprite, heros.position, Color.White);
                }    
            }
            for (int i = 0; i < nombreEnnemis; i++)
            {
                if (tabProjectiles[i].estVivant && tabEnnemis[i].estVivant)
                {
                    spriteBatch.Draw(tabProjectiles[i].sprite, tabProjectiles[i].position, Color.White);
                }
                if (tabEnnemis[i].estVivant)
                {
                    spriteBatch.Draw(tabEnnemis[i].sprite, tabEnnemis[i].position, Color.White);
                }
            }
           
            if (fireball.estVivant)
            {
                spriteBatch.Draw(fireball.sprite, fireball.position, Color.White);
            }

            if (heros.estVivant==false)
            {
                if (gameTimeOn)
                {
                    totalGameTime = "Time: " + gameTime.TotalGameTime.Seconds + "sec";
                    gameTimeOn = false;
                }
                spriteBatch.DrawString(font, gameOver, new Vector2((fenetre.Width/3 + font.MeasureString(gameOver).X), fenetre.Height/3), Color.Black);           
                spriteBatch.DrawString(font, totalGameTime, new Vector2((fenetre.Width/2 - font.MeasureString(totalGameTime).X + 120), fenetre.Height / 2 - 120), Color.Black);
            }
            if(gameWin)
            {
                if (gameTimeOn)
                {
                    totalGameTime = "Time: " + gameTime.TotalGameTime.Seconds + "sec";
                    gameTimeOn = false;
                }
                spriteBatch.DrawString(font, win, new Vector2(fenetre.Width/2, fenetre.Height / 3), Color.Black);
                spriteBatch.DrawString(font, totalGameTime, new Vector2((fenetre.Width / 2 - font.MeasureString(totalGameTime).X + 150), fenetre.Height / 2 - 120), Color.Black);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
