﻿using Microsoft.Xna.Framework;
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
        private const int GAP_X_POSITION = 20;
        private const int GAP_Y_POSITION = 20;
        private const int PURPOSE_X_POSITION = 60;
        private const int PURPOSE_Y_POSITION = 195;
        private const int OBJECTIVE_X_POSITION = 120;
        private const int OBJECTIVE_Y_POSITION = 280;
        private const int ACTIONKEY_Y_POSITION = 350;
        private const int PROJECTILEKEY_Y_POSITION = 450;
        private const int ESCAPEKEY_X_POSITION = 80;
        private const int ESCAPEKEY_Y_POSITION = 500;

        private string helpTitle = "Help";
        private string purposeText = "To protect humanity from perishing, we must fight all titans \n and regain our freedom.";

        private string objectiveText = "Objective: Kill all titans in the field to win the game. Destroy many meteors as much as \n you can but remember the longer titan lives the harder to kill them all. \nHigher number of meteors and titans killed will be the high score.";

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
        Vector2 projectileKeyPosition;
        Vector2 projectileTextPosition;
        Vector2 escapeKeyPosition;
        Vector2 escapeTextPosition;

        SpriteFont helpTitleFont;
        SpriteFont instructionFont;


        private Color textColor = Color.White;

        public HelpScene(MainGame game) : base(game)
        {
            mainGame = game;
            InitializeResources();
            backgroundPosition = new Vector2(X_POSITION, Y_POSITION);
            helpTitlePosition = new Vector2(GAP_X_POSITION, Y_POSITION);
            purposeTextPosition = new Vector2(OBJECTIVE_X_POSITION, PURPOSE_Y_POSITION);
            objectiveTextPosition = new Vector2(OBJECTIVE_X_POSITION, OBJECTIVE_Y_POSITION);
            actionKeyPosition = new Vector2(PURPOSE_X_POSITION, ACTIONKEY_Y_POSITION + GAP_Y_POSITION);
            actionKeyTextPosition =new Vector2(projectileInstructionTexture.Width + OBJECTIVE_X_POSITION, Shared.stage.Y/2 + actionKeyInstructionTexture.Height);
            projectileKeyPosition = new Vector2(PURPOSE_X_POSITION, PROJECTILEKEY_Y_POSITION);
            projectileTextPosition = new Vector2(projectileInstructionTexture.Width + OBJECTIVE_X_POSITION, PROJECTILEKEY_Y_POSITION);
            escapeKeyPosition = new Vector2(ESCAPEKEY_X_POSITION, ESCAPEKEY_Y_POSITION);
            escapeTextPosition = new Vector2(projectileInstructionTexture.Width + OBJECTIVE_X_POSITION, ESCAPEKEY_Y_POSITION);
        }

        public void InitializeResources()
        {
            helpSceneTexture = mainGame.Content.Load<Texture2D>("images/HelpBackground");
            helpTitleFont = mainGame.Content.Load<SpriteFont>("fonts/GameTitleFont");
            instructionFont = mainGame.Content.Load<SpriteFont>("fonts/small");
            actionKeyInstructionTexture = mainGame.Content.Load<Texture2D>("images/keyboard");
            projectileInstructionTexture = mainGame.Content.Load<Texture2D>("images/spacebar");
            escapeInstructionTexture = mainGame.Content.Load<Texture2D>("images/escapeKey");
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Begin();
            mainGame._spriteBatch.Draw(helpSceneTexture, backgroundPosition, textColor);
            mainGame._spriteBatch.DrawString(helpTitleFont, helpTitle, helpTitlePosition, textColor);
            mainGame._spriteBatch.DrawString(instructionFont, purposeText, purposeTextPosition, textColor);
            mainGame._spriteBatch.DrawString(instructionFont, objectiveText, objectiveTextPosition, textColor);
            mainGame._spriteBatch.Draw(actionKeyInstructionTexture, actionKeyPosition, textColor);
            mainGame._spriteBatch.DrawString(instructionFont, actionKeyInstruction, actionKeyTextPosition, textColor);
            mainGame._spriteBatch.Draw(projectileInstructionTexture, projectileKeyPosition, textColor);
            mainGame._spriteBatch.DrawString(instructionFont, projectileInstruction, projectileTextPosition, textColor);
            mainGame._spriteBatch.Draw(escapeInstructionTexture, escapeKeyPosition, textColor);
            mainGame._spriteBatch.DrawString(instructionFont, escapeInstruction, escapeTextPosition, textColor);

            mainGame._spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
