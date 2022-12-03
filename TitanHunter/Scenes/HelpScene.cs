using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;

namespace TitanHunter.Scenes
{
    public class HelpScene : GameScene
    {
        private MainGame mainGame;
        private Texture2D helpSceneTexture;
        private Vector2 backgroundPosition;
        private const int X_POSITION = 0;
        private const int Y_POSITION = 0;
        private const int ADJUSTEDTEXT_X_POSITION = 20;
        private const int GAP_Y_POSITION = 20;
        private const int GAP_X_POSITION = 30;

        private const int PURPOSE_X_POSITION = 60;
        private const int PURPOSE_Y_POSITION = 195;
        private const int OBJECTIVE_X_POSITION = 120;
        private const int OBJECTIVE_Y_POSITION = 280;
        private const int ACTIONKEY_Y_POSITION = 350;


        private string helpTitle = "Help";
        private string purposeText = "To protect humanity from perishing, we must fight all titans \n and regain our freedom.";

        private string objectiveText = "Objective: Kill all titans in the field to win the game. Destroy many meteors as much as \n you can but remember the longer titan lives the harder it will to kill them all. \nHigher number of meteors and titans killed will be the high score.";

        private string actionKeyInstruction = "Use up, down, left, and right to move the player.";
        private string projectileInstruction = "Use spacebar to throw shurikens towards enemies to kill them.";
        private string escapeInstruction = "Use escape to return to home menu.";

        //texture for the icons
        Texture2D actionKeyInstructionTexture;
        Texture2D projectileInstructionTexture;
        Texture2D escapeInstructionTexture;

        Vector2 helpTitlePosition;
        Vector2 purposeTextPosition;
        Vector2 objectiveTextPosition;
        Vector2 actionKeyPosition;
        Vector2 actionKeyTextPosition;

        SpriteFont helpTitleFont;
        SpriteFont purposeTextFont;
        SpriteFont objectiveTextFont;
        SpriteFont actionKeyTextFont;

        private Color textColor = Color.Wheat;

        public HelpScene(MainGame game) : base(game)
        {
            mainGame = game;
            InitializeTexture();
            backgroundPosition = new Vector2(X_POSITION, Y_POSITION);
            helpTitlePosition = new Vector2(ADJUSTEDTEXT_X_POSITION, Y_POSITION);
            purposeTextPosition = new Vector2(PURPOSE_X_POSITION, PURPOSE_Y_POSITION);
            objectiveTextPosition = new Vector2(OBJECTIVE_X_POSITION, OBJECTIVE_Y_POSITION);
            actionKeyPosition = new Vector2(PURPOSE_X_POSITION, ACTIONKEY_Y_POSITION + GAP_Y_POSITION);
            actionKeyTextPosition =new Vector2(actionKeyInstructionTexture.Width + PURPOSE_X_POSITION, ACTIONKEY_Y_POSITION + GAP_Y_POSITION);
        }

        public void InitializeTexture()
        {
            helpSceneTexture = mainGame.Content.Load<Texture2D>("images/HelpBackground");
            helpTitleFont = mainGame.Content.Load<SpriteFont>("fonts/GameTitleFont");
            purposeTextFont = mainGame.Content.Load<SpriteFont>("fonts/medium");
            objectiveTextFont = mainGame.Content.Load<SpriteFont>("fonts/small");
            actionKeyTextFont = mainGame.Content.Load<SpriteFont>("fonts/small");

            actionKeyInstructionTexture = mainGame.Content.Load<Texture2D>("images/keyboard");
            projectileInstructionTexture = mainGame.Content.Load<Texture2D>("images/spacebar");
            escapeInstructionTexture = mainGame.Content.Load<Texture2D>("images/escapeKey");
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Begin();
            mainGame._spriteBatch.Draw(helpSceneTexture, backgroundPosition, Color.White);
            mainGame._spriteBatch.DrawString(helpTitleFont, helpTitle, helpTitlePosition, textColor);
            mainGame._spriteBatch.DrawString(purposeTextFont, purposeText, purposeTextPosition, textColor);
            mainGame._spriteBatch.DrawString(objectiveTextFont, objectiveText, objectiveTextPosition, textColor);
            mainGame._spriteBatch.Draw(actionKeyInstructionTexture, actionKeyPosition, Color.White);
            mainGame._spriteBatch.DrawString(actionKeyTextFont, actionKeyInstruction, actionKeyTextPosition, textColor);

            mainGame._spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
