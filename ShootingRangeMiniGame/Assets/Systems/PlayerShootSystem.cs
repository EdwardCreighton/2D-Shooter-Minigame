using System.Numerics;
using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Core;
using ShootingRangeMiniGame.Engine.Components;
using ShootingRangeMiniGame.Assets.Markers;

namespace ShootingRangeMiniGame.Assets.Systems
{
	public class PlayerShootSystem : IEcsRunSystem
	{
		private EcsFilter<PlayerMarker> _filter;
		private App _app;
		
		public void Run()
		{
			if (_app.LeftMouseButtonPressed)
			{
				ref var transform = ref _filter.GetEntity(0).Get<Transform>();
			
				Vector2 aimDirection = _app.MousePosition - transform.Position;
				aimDirection = Vector2.Normalize(aimDirection);
				
				ref var onSpawnProjectile = ref _filter.GetEntity(0).Get<OnSpawnProjectile>();
				onSpawnProjectile.Position = transform.Position;
				onSpawnProjectile.Direction = aimDirection;
			}
		}
	}
}