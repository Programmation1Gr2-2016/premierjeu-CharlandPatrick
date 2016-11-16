using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

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
        GameObject ennemi;
        GameObject projectile;
        GameObject fireball;
        Texture2D backgroundTexture;

        int nombreEnnemis = 2;
        int pointVie = 3;
        int pointVieEnnemi = 3;

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

            this.graphics.ToggleFullScreen();

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

            heros = new GameObject();
            heros.estVivant = true;
            heros.vitesse = 5;
            heros.sprite = Content.Load<Texture2D>("Mario.png");
            heros.position = heros.sprite.Bounds;

            ennemi = new GameObject();
            ennemi.estVivant = true;
            ennemi.vitesse = 7;
            ennemi.sprite = Content.Load<Texture2D>("Bowserjr_MP9.png");
            ennemi.position = ennemi.sprite.Bounds;
            ennemi.position.X = fenetre.Right - 300;
            ennemi.position.Y = fenetre.Top + 20;

            projectile = new GameObject();
            projectile.estVivant = false;
            projectile.vitesse = 10;
            projectile.sprite = Content.Load<Texture2D>("Bullet_Bill.png");
            projectile.position = projectile.sprite.Bounds;

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
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W)) //Up
                {
                    heros.position.Y -= heros.vitesse;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A)) //Gauche
                {
                    heros.position.X -= heros.vitesse;
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
            if (ennemi.estVivant) //Ennemis move
            {
                ennemi.position.Y += ennemi.vitesse;

                if (ennemi.position.Y <= fenetre.Top || ennemi.position.Y >= fenetre.Bottom - ennemi.sprite.Bounds.Height)
                {
                    ennemi.vitesse *= -1;
                }
            }
        }

        protected void UpdateProjectiles()
        {
            //Projectile move
            if (heros.estVivant && ennemi.estVivant)
            {
                projectile.estVivant = true;
            }
            if (projectile.estVivant && ennemi.estVivant && heros.estVivant)
            {
                jetFlyby.Play();
                projectile.position.X -= projectile.vitesse;
                if (projectile.position.X < fenetre.Left - 500)
                {
                    projectile.position.X = ennemi.position.X;
                    projectile.position.Y = ennemi.position.Y;
                    gun.Play();
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
            if (projectile.estVivant && heros.estVivant && projectile.position.Intersects(heros.position))
            {
                explosion.Play();
                pointVie--;
                projectile.estVivant = false;
                projectile.position.X = ennemi.position.X;
                projectile.position.Y = ennemi.position.Y;
                projectile.estVivant = true;
                if(pointVie == 0)
                {
                    death.Play();
                    heros.estVivant = false;
                    projectile.estVivant = false;
                }
            }
            if (fireball.estVivant && fireball.position.Intersects(ennemi.position))
            {
                explosion.Play();
                pointVieEnnemi--;
                fireball.estVivant = false;
                fireball.position.X = heros.position.X;
                fireball.position.Y = heros.position.Y;
                if (pointVieEnnemi == 0)
                {
                    fanfare.Play();
                    ennemi.estVivant = false;
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

            if(heros.estVivant)
            {
                spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            }
            if(projectile.estVivant && ennemi.estVivant)
            {
                spriteBatch.Draw(projectile.sprite, projectile.position, Color.White);
            }
            if(ennemi.estVivant)
            {
                spriteBatch.Draw(ennemi.sprite, ennemi.position, Color.White);
            }
            if (fireball.estVivant)
            {
                spriteBatch.Draw(fireball.sprite, fireball.position, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
