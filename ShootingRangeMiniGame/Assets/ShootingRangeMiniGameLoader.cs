using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine;
using ShootingRangeMiniGame.Assets.Data;
using ShootingRangeMiniGame.Assets.Systems;

namespace ShootingRangeMiniGame.Assets
{
	public class ShootingRangeMiniGameLoader : IGameplaySystemsLoader
	{
		private DataProvider _dataProvider;
		
		public ShootingRangeMiniGameLoader()
		{
			_dataProvider = new DataProvider();
		}
		
		public void AssignSystems(EcsSystems gameplaySystems)
		{
			gameplaySystems
				.Add(new PlayerLoader())
				.Add(new TargetsLoader())

				.Add(new PlayerRotationSystem())
				.Add(new PlayerShootSystem())
				.Add(new SpawnProjectileSystem())

				.Add(new TargetsOnCollisionResolver())
				.Add(new ProjectilesOnCollisionResolver())

				.Add(new MovementSystem())

				.Inject(_dataProvider);
		}
	}
}