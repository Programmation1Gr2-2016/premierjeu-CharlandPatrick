using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ExerciceTileSheet
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;

        // Fond de tuiles
        GameObjectTile fond;

        KeyboardState keys = new KeyboardState();
        KeyboardState previousKeys = new KeyboardState();
        GameObjectAnime perso;

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

            fond = new GameObjectTile();
            fond.texture = Content.Load<Texture2D>("TileSheet\\pokemon_world.png");

            perso = new GameObjectAnime();
            perso.direction = Vector2.Zero;
            perso.vitesse.X = 2;
            perso.vitesse.Y = 2;
            perso.objetState = GameObjectAnime.etats.attenteDroite;
            perso.position = new Rectangle (62, 80, 52, 83);   //Position initiale de perso
            perso.sprite = Content.Load<Texture2D>("Sprite\\Perso_SpriteSheet.png");

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

            keys = Keyboard.GetState();
            perso.position.X += (int)(perso.vitesse.X * perso.direction.X);
            perso.position.Y += (int)(perso.vitesse.Y * perso.direction.Y);


            if (keys.IsKeyDown(Keys.Right))
            {
                perso.direction.X = 2;
                perso.objetState = GameObjectAnime.etats.runDroite;
            }
            if (keys.IsKeyUp(Keys.Right) && previousKeys.IsKeyDown(Keys.Right))
            {
                perso.direction.X = 0;
                perso.objetState = GameObjectAnime.etats.attenteDroite;
            }
            if (keys.IsKeyDown(Keys.Left))
            {
                perso.direction.X = -2;
                perso.objetState = GameObjectAnime.etats.runGauche;
            }
            if (keys.IsKeyUp(Keys.Left) && previousKeys.IsKeyDown(Keys.Left))
            {
                perso.direction.X = 0;
                perso.objetState = GameObjectAnime.etats.attenteGauche;
            }
            if (keys.IsKeyDown(Keys.Up))
            {
                perso.direction.Y = -2;
                perso.objetState = GameObjectAnime.etats.runHaut;
            }
            if (keys.IsKeyUp(Keys.Up) && previousKeys.IsKeyDown(Keys.Up))
            {
                perso.direction.Y = 0;
                perso.objetState = GameObjectAnime.etats.attenteHaut;
            }
            if (keys.IsKeyDown(Keys.Down))
            {
                perso.direction.Y = 2;
                perso.objetState = GameObjectAnime.etats.runBas;
            }
            if (keys.IsKeyUp(Keys.Down) && previousKeys.IsKeyDown(Keys.Down))
            {
                perso.direction.Y = 0;
                perso.objetState = GameObjectAnime.etats.attenteBas;
            }

            //On appelle la méthode Update de Perso qui permet de gérer les états
            perso.Update(gameTime);
            previousKeys = keys;

            for (int ligne = 0; ligne < fond.map.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < fond.map.GetLength(1); colonne++)
                {
                    Rectangle tuile = new Rectangle();
                    tuile.X = colonne * GameObjectTile.LARGEUR_TUILE - (int)(perso.vitesse.X * perso.direction.X);
                    tuile.Y = ligne * GameObjectTile.HAUTEUR_TUILE - (int)(perso.vitesse.Y * perso.direction.Y);
                    tuile.Width = GameObjectTile.LARGEUR_TUILE;
                    tuile.Height = GameObjectTile.HAUTEUR_TUILE;
                    if (tuile.Intersects(perso.position))
                    {
                        switch (fond.map[ligne, colonne])
                        {
                            case 1: 		 // ne rien faire
                                break;
                            case 2:		// empêcher le mouvement
                                break;
                            case 3:		// faire une autre action...
                                break;
                        }
                    }
                }
            }

            base.Update(gameTime);
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
            fond.Draw(spriteBatch);
            spriteBatch.Draw(perso.sprite, perso.position, perso.spriteAfficher, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
