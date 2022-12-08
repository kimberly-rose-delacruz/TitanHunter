/*MainGame.cs
 *      this is the mainGame class composing the entire structure of the game. I instantiate here the stage size and also handles the enabling and disabled of each scene based on player's navigation using keyboard keys. I also added the resetting of game resources when the game is over or even when the game is already ended. 
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TitanHunter.Scenes;
using TitanHunter.Services;
using TitanHunter;
using Microsoft.Xna.Framework.Media;
using MobsHunterGame.Scenes;

namespace TitanHunter
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        //declaring all scenes here.
        public StartScene startScene;
        public ActionScene actionScene;
        public GameManager gameLevelService;
        public HelpScene helpscene;
        public HighScoreScene highSCoreScene;
        public AboutScene aboutScene;
        private KeyboardState oldKeyboardState = Keyboard.GetState();


        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 633;
            gameLevelService = new GameManager();
        }

        protected override void Initialize()
        {

            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth,
                 _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

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

            highSCoreScene = new HighScoreScene(this);
            this.Components.Add(highSCoreScene);

            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            
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

            if (startScene.Enabled)
            {
                //if the startscene is enabled
                selectedIndex = startScene.Menu.selectedIndex;
                if (selectedIndex == 0 && (keyboardState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter)))
                {
                    //when actionscene is enabled and selected it will be validated if the game is over or game is won true
                    if (gameLevelService.IsGameOver() == true || gameLevelService.IsGameWon() == true)
                    {
                        //then the game will be reset.
                        gameLevelService.Reset();
                    }

                    //then hide all scenes and show the action scene.
                    HideAllScenes();
                    actionScene.Show();
                }
                else if (selectedIndex == 1 && keyboardState.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    helpscene.Show();
                }
                else if(selectedIndex == 2 && keyboardState.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    highSCoreScene.Show();
                }
                else if(selectedIndex == 3 && keyboardState.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    aboutScene.Show();
                }
                else if (selectedIndex == 4 && keyboardState.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }

            //any of the scene is enabled can do escape from the current scene and return back to the startscene to show it.
            else if (actionScene.Enabled)
            {
                if (keyboardState.IsKeyDown(Keys.Escape) || 
                    (keyboardState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter)))
                {
                    //when action scene is enable and user attempts to click on escale or enter
                    //it will check if there is a new highscore if yes
                    if (gameLevelService.HasNewHighScore == true)
                    {
                        //it will redirect the user to the highscore
                        HideAllScenes();
                        highSCoreScene.Show();
                    }
                    else
                    {
                        //else just show the startscene
                        HideAllScenes();
                        startScene.Show();
                    }    

                }
            }

            else if(helpscene.Enabled || highSCoreScene.Enabled || aboutScene.Enabled)
            {
                if (keyboardState.IsKeyDown(Keys.Escape)) 
                {
                    HideAllScenes();
                    startScene.Show();
                }
            }

            oldKeyboardState = keyboardState;

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