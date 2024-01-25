using System.Numerics;
using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine;
using ShootingRangeMiniGame.Engine.Core;
using ShootingRangeMiniGame.Engine.Components;
using ShootingRangeMiniGame.Assets.Markers;

namespace ShootingRangeMiniGame.Assets.Systems
{
	public class PlayerRotationSystem : IEcsRunSystem
	{
		private EcsFilter<PlayerMarker> _filter;
		
		private App _app;
		
		public void Run()
		{
			ref var transform = ref _filter.GetEntity(0).Get<Transform>();
			
			Vector2 aimDirection = _app.MousePosition - transform.Position;
			aimDirection = Vector2.Normalize(aimDirection);
			transform.Rotation = aimDirection.Angle();
		}
	}
}