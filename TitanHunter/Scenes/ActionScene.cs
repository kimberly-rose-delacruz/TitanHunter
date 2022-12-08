/*ActionScene.cs
 *     this class will contain all the logic when the game starts. it composes the player, enemy manager, collision manager, and the header component to show all details and updates on what is happening between the player and the enemy.'
 *
 *  Revision History:
 *      Polishing code on December 5, 2022.
 *      Test and Apply some logic for the start of game when ActionScene is enabled to report to the start scene in order to change the begin text to continue on DEcember 4, 2022.
  *     Fixes bugs on showing the player and how it will collide with the enemy projectile and meteor projectile  on December 3, 2022
 *      Created on December 1, 2022 by Kimberly Rose Dela Cruz
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TitanHunter.Controllers;
using TitanHunter.Models;
using TitanHunter.Services;


namespace TitanHunter.Scenes
{

    public class ActionScene : GameScene
    {
        private MainGame mainGame;
        Texture2D actionBackgroundTex;
        Vector2 position;
        private const int X_POSITION = 0;
        private const int Y_POSITION = 0;
        private Player player;        
        public ActionScene(MainGame game) : base(game)
        {
            this.mainGame = game;

            //background loading content and giving its position in the action scene
            position = new Vector2(X_POSITION, Y_POSITION);
            InitializeResources();
            player = new Player(game);
            components.Add(player);
            components.Add(new EnemyManager(mainGame));
            components.Add(new CollisionManager(mainGame, player));
            components.Add(new HeaderComponent(mainGame, player));
        }

        public void InitializeResources()
        {
            actionBackgroundTex = mainGame.Content.Load<Texture2D>("images/DungeonBackground");

        }

        public override void Show()
        {
            //set function isGameStarted to true to let the gameLevelService know that it should report to the other scene that game has started.
            mainGame.gameLevelService.StartGame();
            base.Show();
        }
        public override void Update(GameTime gameTime)
        {
            //if game reset is true when user tries to escape from the game since it is already game over.
            if(mainGame.gameLevelService.IsGameReset() == true)
            {
                Projectile.projectiles.Clear();
                Enemy.enemies.Clear();           
                player.Reset();
            }

            //if game over or game won stop everything.
            if (mainGame.gameLevelService.IsGameOver() || mainGame.gameLevelService.IsGameWon() == true) { return; }

            //update the projectiles within the game 
            foreach (Projectile proj in Projectile.projectiles)
            {
                proj.Update(gameTime);
            }

            //update game for each enemy from the lilst of enemies.
            foreach (Enemy enemy in Enemy.enemies)
            {
                enemy.Update(gameTime);
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Begin();
            //draw the actionBackground in the actionscene
            mainGame._spriteBatch.Draw(actionBackgroundTex, position, Color.White);
            
            //draw the each projectile based from the update
            foreach (Projectile proj in Projectile.projectiles)
            {
                proj.Draw(gameTime);
            }

            //draw each enemy each time it updates.
            foreach (Enemy enemy in Enemy.enemies)
            {
                enemy.Draw(gameTime);
            }
            mainGame._spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
