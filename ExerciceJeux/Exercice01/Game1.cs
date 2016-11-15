﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        Texture2D backgroundTexture;

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
            projectile.estVivant = true;
            projectile.vitesse = 10;
            projectile.sprite = Content.Load<Texture2D>("Bullet_Bill.png");
            projectile.position = projectile.sprite.Bounds;
            projectile.position.X = ennemi.position.X;
            projectile.position.Y = ennemi.position.Y;

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

            //Ennemis move

            ennemi.position.Y += ennemi.vitesse;

            if (ennemi.position.Y <= fenetre.Top || ennemi.position.Y >= fenetre.Bottom - ennemi.sprite.Bounds.Height)
            {
                ennemi.vitesse *= -1;
            }

            //Projectile move
            
            projectile.position.X -= projectile.vitesse;
            if(projectile.position.X < fenetre.Left - 500)
            {
                projectile.estVivant = false;
                projectile.position.X = ennemi.position.X;
                projectile.position.Y = ennemi.position.Y;
                projectile.estVivant = true;
            }

            UpdateHeros();

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

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            this.spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height), Color.White);

     
            spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            spriteBatch.Draw(projectile.sprite, projectile.position, Color.White);
            spriteBatch.Draw(ennemi.sprite, ennemi.position, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
