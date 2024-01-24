using System.Numerics;
using Leopotam.Ecs;
using ShootingRangeMiniGame.Engine.Core;
using ShootingRangeMiniGame.Engine.Components;
using ShootingRangeMiniGame.Engine.Markers;

namespace ShootingRangeMiniGame.Engine.Systems
{
	public class PhysicsSystem : IEcsRunSystem
	{
		private App _app;
		private EcsFilter<OnCollision> _collisionEventFilter;
		private EcsFilter<Collider> _filter;

		public void Run()
		{
			ClearOnCollisionEvents();
			
			for (int i = 0; i < _filter.GetEntitiesCount(); i++)
			{
				ref var transform = ref _filter.GetEntity(i).Get<Transform>();
				ref var collider = ref _filter.Get1(i);

				Vector2 collisionPoint = new Vector2();
				if (CheckScreenBoundaries(ref transform, ref collider, ref collisionPoint))
				{
					ref var collisionMarker = ref _filter.GetEntity(i).Get<OnCollision>();
					collisionMarker.OtherEntity = EcsEntity.Null;
					collisionMarker.Point = collisionPoint;
				}

				for (int j = i + 1; j < _filter.GetEntitiesCount(); j++)
				{
					ref var tr2 = ref _filter.GetEntity(j).Get<Transform>();
					ref var cl2 = ref _filter.Get1(j);

					if (AABB(ref transform, ref tr2, ref collider, ref cl2))
					{
						Vector2 cp = (transform.Position + tr2.Position) / 2;

						ref var collisionMarker1 = ref _filter.GetEntity(i).Get<OnCollision>();
						ref var collisionMarker2 = ref _filter.GetEntity(j).Get<OnCollision>();
						
						collisionMarker1.OtherEntity = _filter.GetEntity(j);
						collisionMarker2.OtherEntity = _filter.GetEntity(i);
						
						collisionMarker1.Point = cp;
						collisionMarker2.Point = cp;
					}
				}
			}
		}

		private void ClearOnCollisionEvents()
		{
			int rawCount = _collisionEventFilter.GetEntitiesCount();
			
			for (int i = 0; i < rawCount; i++)
			{
				_collisionEventFilter.GetEntity(i).Del<OnCollision>();
			}
		}
		
		private bool CheckScreenBoundaries(ref Transform transform, ref Collider collider, ref Vector2 collisionPoint)
		{
			bool collisionOccured = false;
			float horizontal = transform.Position.X + collider.BoundingBox[0].X;
			float vertical = transform.Position.Y + collider.BoundingBox[0].Y;
			
			if (horizontal < 0)
			{
				transform.Position.X += -horizontal;
				collisionOccured = true;
				collisionPoint.X = 0;
				collisionPoint.Y = transform.Position.Y;
			}
			if (vertical < 0)
			{
				transform.Position.Y += -vertical;
				collisionOccured = true;
				collisionPoint.X = transform.Position.X;
				collisionPoint.Y = 0;
			}

			horizontal = transform.Position.X + collider.BoundingBox[2].X;
			vertical = transform.Position.Y + collider.BoundingBox[2].Y;
			
			if (horizontal > _app.ScreenSize.Width)
			{
				transform.Position.X -= horizontal - _app.ScreenSize.Width;
				collisionOccured = true;
				collisionPoint.X = _app.ScreenSize.Width;
				collisionPoint.Y = transform.Position.Y;
			}
			if (vertical > _app.ScreenSize.Height)
			{
				transform.Position.Y -= vertical - _app.ScreenSize.Height;
				collisionOccured = true;
				collisionPoint.X = transform.Position.X;
				collisionPoint.Y = _app.ScreenSize.Height;
			}

			return collisionOccured;
		}

		private bool AABB(ref Transform tr1, ref Transform tr2, ref Collider cl1, ref Collider cl2)
		{
			float leftEdge1 = tr1.Position.X + cl1.BoundingBox[0].X;
			float rightEdge1 = tr1.Position.X + cl1.BoundingBox[1].X;
			float topEdge1 = tr1.Position.Y + cl1.BoundingBox[0].Y;
			float bottomEdge1 = tr1.Position.Y + cl1.BoundingBox[3].Y;
			
			float leftEdge2 = tr2.Position.X + cl2.BoundingBox[0].X;
			float rightEdge2 = tr2.Position.X + cl2.BoundingBox[1].X;
			float topEdge2 = tr2.Position.Y + cl2.BoundingBox[0].Y;
			float bottomEdge2 = tr2.Position.Y + cl2.BoundingBox[3].Y;

			return leftEdge1 < rightEdge2
			       && rightEdge1 > leftEdge2
			       && bottomEdge1 > topEdge2
			       && topEdge1 < bottomEdge2;
		}
	}
}