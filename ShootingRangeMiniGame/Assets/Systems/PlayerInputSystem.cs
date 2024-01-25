using System.Numerics;
using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine;
using ShootingRangeMiniGame.Engine.Core;
using ShootingRangeMiniGame.Engine.Components;
using ShootingRangeMiniGame.Assets.Markers;

namespace ShootingRangeMiniGame.Assets.Systems
{
	public class PlayerInputSystem : IEcsRunSystem
	{
		private App _app;
		private EcsFilter<PlayerMarker> _filter;

		public void Run()
		{
			ref var transform = ref _filter.GetEntity(0).Get<Transform>();
			
			Vector2 aimDirection = _app.MousePosition - transform.Position;
			aimDirection = Vector2.Normalize(aimDirection);
			transform.Rotation = aimDirection.Angle();

			if (_app.LeftMouseButtonPressed)
			{
				ref var onSpawnProjectile = ref _filter.GetEntity(0).Get<OnSpawnProjectile>();
				onSpawnProjectile.Position = transform.Position;
				onSpawnProjectile.Direction = aimDirection;
			}
		}
	}
}