using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine;
using ShootingRangeMiniGame.Assets.Systems;

namespace ShootingRangeMiniGame.Assets
{
	public class ShootingRangeMiniGameLoader : GameplaySystemsLoader
	{
		public override void AssignSystems(EcsSystems gameplaySystems)
		{
			gameplaySystems
				.Add(new TargetsLoader());
		}
	}
}