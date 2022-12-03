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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Taskbar;


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
            actionBackgroundTex = mainGame.Content.Load<Texture2D>("images/DungeonBackground");
            position = new Vector2(X_POSITION, Y_POSITION);

            player = new Player(game);
            components.Add(player);
            components.Add(new EnemyManager(mainGame));
            components.Add(new CollisionManager(mainGame, player));
            components.Add(new HeaderComponent(mainGame));
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

            if (mainGame.gameLevelService.IsGameOver()) { return; }

            foreach (Projectile proj in Projectile.projectiles)
            {
                proj.Update(gameTime);
            }

            foreach (Enemy enemy in Enemy.enemies)
            {
                enemy.Update(gameTime);
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Begin();
            mainGame._spriteBatch.Draw(actionBackgroundTex, position, Color.White);
            foreach (Projectile proj in Projectile.projectiles)
            {
                //spriteBatch.Draw(shuriken, new Vector2(proj.Position.X, proj.Position.Y), Color.White);
                proj.Draw(gameTime);
            }

            foreach (Enemy enemy in Enemy.enemies)
            {
                enemy.Draw(gameTime);
            }


            mainGame._spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
