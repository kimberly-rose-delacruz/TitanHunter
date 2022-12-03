using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TitanHunter.Scenes;
using TitanHunter.Services;
using TitanHunter;

namespace TitanHunter
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        //declaring all scenes here.
        public StartScene startScene;
        public ActionScene actionScene;
        public GameLevelService gameLevelService;
        public HelpScene helpscene;


        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 620;
            gameLevelService = new GameLevelService();
        }

        protected override void Initialize()
        {

            // TODO: Add your initialization logic here


            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth,
                 _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //startScene
            startScene = new StartScene(this);
            this.Components.Add(startScene);
            startScene.Show();

            //actione scene
            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);

            //helpscene
            helpscene = new HelpScene(this);
            this.Components.Add(helpscene);

        }


        //hiding alls scenes
        private void HideAllScenes()
        {
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    GameScene scene = (GameScene)item;
                    scene.Hide();
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            int selectedIndex = 0;
            KeyboardState keyboardState = Keyboard.GetState();
            // TODO: Add your update logic here

            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.selectedIndex;
                if (selectedIndex == 0 && keyboardState.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    actionScene.Show();
                }
                else if (selectedIndex == 1 && keyboardState.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    helpscene.Show();
                }
                else if (selectedIndex == 4 && keyboardState.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }



            //any of the scene is enabled can do escape from the current scene and return back to the startscene to show it.
            if (actionScene.Enabled || helpscene.Enabled)
            {
                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    HideAllScenes();
                    startScene.Show();
                }
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

    }
}