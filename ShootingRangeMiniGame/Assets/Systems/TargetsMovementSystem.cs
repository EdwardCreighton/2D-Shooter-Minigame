using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Core;
using ShootingRangeMiniGame.Assets.Components;
using ShootingRangeMiniGame.Assets.Markers;
using ShootingRangeMiniGame.Engine.Components;

namespace ShootingRangeMiniGame.Assets.Systems
{
	public class TargetsMovementSystem : IEcsRunSystem
	{
		private App _app;
		private EcsFilter<TargetMarker, Movement> _filter;

		public void Run()
		{
			for (int i = 0; i < _filter.GetEntitiesCount(); i++)
			{
				ref var transform = ref _filter.GetEntity(i).Get<Transform>();
				ref var movement = ref _filter.Get2(i);

				transform.Position += movement.Direction * (movement.Speed * _app.UpdateDeltaTime);
			}
		}
	}
}