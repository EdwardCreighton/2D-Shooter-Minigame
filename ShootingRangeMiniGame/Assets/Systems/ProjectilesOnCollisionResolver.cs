using System.Numerics;
using Leopotam.Ecs;
using ShootingRangeMiniGame.Assets.Components;
using ShootingRangeMiniGame.Engine.Components;
using ShootingRangeMiniGame.Assets.Markers;

namespace ShootingRangeMiniGame.Assets.Systems
{
	public class ProjectilesOnCollisionResolver : IEcsRunSystem
	{
		private EcsFilter<Projectile, OnCollision> _filter;

		public void Run()
		{
			int initialCount = _filter.GetEntitiesCount();
			
			for (int i = 0; i < initialCount; i++)
			{
				ref var collisionInfo = ref _filter.Get2(i);

				if (collisionInfo.OtherEntity.IsNull())
				{
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

					_filter.Get1(i).Durability -= 1;
					
					if (_filter.Get1(i).Durability <= 0)
						_filter.GetEntity(i).Destroy();
				}
				else if (collisionInfo.OtherEntity.Has<TargetMarker>())
				{
					collisionInfo.OtherEntity.Destroy();
					_filter.GetEntity(i).Destroy();
				}
			}
		}
	}
}