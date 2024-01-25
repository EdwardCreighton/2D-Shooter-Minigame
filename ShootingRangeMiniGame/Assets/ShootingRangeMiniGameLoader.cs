using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine;
using ShootingRangeMiniGame.Assets.Systems;

namespace ShootingRangeMiniGame.Assets
{
	public class ShootingRangeMiniGameLoader : IGameplaySystemsLoader
	{
		public void AssignSystems(EcsSystems gameplaySystems)
		{
			gameplaySystems
				.Add(new PlayerLoader())
				.Add(new TargetsLoader())
				
				.Add(new PlayerRotationSystem())
				.Add(new PlayerShootSystem())
				.Add(new TargetsOnCollisionResolver())
				
				.Add(new SpawnProjectileSystem())
				.Add(new MovementSystem());
		}
	}
}