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
			float angleRad = (float)Math.Atan2(aimDirection.Y, aimDirection.X);
			transform.Rotation = 360f + angleRad * MathE.Rad2Deg;
		}
	}
}