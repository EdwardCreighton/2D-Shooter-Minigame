using System.Numerics;
using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Components;
using ShootingRangeMiniGame.Engine.Markers;
using ShootingRangeMiniGame.Assets.Components;
using ShootingRangeMiniGame.Assets.Markers;

namespace ShootingRangeMiniGame.Assets.Systems
{
	public class TargetsOnCollisionResolver : IEcsRunSystem
	{
		private EcsFilter<TargetMarker, OnCollision> _filter;

		public void Run()
		{
			for (int i = 0; i < _filter.GetEntitiesCount(); i++)
			{
				ref var collisionInfo = ref _filter.Get2(i);

				if (!collisionInfo.OtherEntity.IsNull() && !collisionInfo.OtherEntity.Has<TargetMarker>())
					continue;
				
				ref var movement = ref _filter.GetEntity(i).Get<Movement>();
				ref var transform = ref _filter.GetEntity(i).Get<Transform>();
				
				Vector2 directionToHitPoint = collisionInfo.Point - transform.Position;
				
				if (Math.Abs(directionToHitPoint.X) > Math.Abs(directionToHitPoint.Y))
				{
					movement.Direction.X *= -1;
				}
				else
				{
					movement.Direction.Y *= -1;
				}
			}
		}
	}
}