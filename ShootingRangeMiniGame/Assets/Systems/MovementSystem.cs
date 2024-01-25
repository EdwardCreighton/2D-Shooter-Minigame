using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Core;
using ShootingRangeMiniGame.Engine.Components;
using ShootingRangeMiniGame.Assets.Components;

namespace ShootingRangeMiniGame.Assets.Systems
{
	public class MovementSystem : IEcsRunSystem
	{
		private App _app;
		private EcsFilter<Movement, Transform> _filter;

		public void Run()
		{
			for (int i = 0; i < _filter.GetEntitiesCount(); i++)
			{
				ref var movement = ref _filter.Get1(i);
				ref var transform = ref _filter.Get2(i);

				transform.Position += movement.Direction * (movement.Speed * _app.UpdateDeltaTime);
			}
		}
	}
}