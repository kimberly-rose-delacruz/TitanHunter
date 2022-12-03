using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TitanHunter.Models;
using TitanHunter.Services;
using SharpDX.Direct3D9;

namespace TitanHunter.Controllers
{
    public class EnemyManager : GameComponent
    {
        static Random rand = new Random();
        private MainGame mainGame;
        private GameLevelService gameLevelService;

        public EnemyManager(MainGame game) : base(game)
        {
            this.mainGame = game;
            this.gameLevelService = game.gameLevelService;
        }

        public override void Update(GameTime gameTime)
        {
            //all update will stop when player is dead.
            if (mainGame.gameLevelService.IsGameOver()) { return; }

            if (gameLevelService.ThrowMeteors(gameTime) == true)
            {
                Projectile.projectiles.Add(new MeteorProjectile(mainGame, new Vector2(Shared.stage.X, rand.Next(Shared.SHARED_HEIGHT, (int)Shared.stage.Y))));

            }

            if(gameLevelService.SpawnEnemy(gameTime) == true)
            {
                Enemy.enemies.Add(new Enemy(mainGame, new Vector2(rand.Next((int)(Shared.stage.X * 0.6f), (int)(Shared.stage.X * 0.9f)), rand.Next(Shared.SHARED_HEIGHT, (int)Shared.stage.Y))));

            }

        }
    }
}
