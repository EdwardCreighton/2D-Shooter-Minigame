using System.Numerics;
using Leopotam.Ecs;

namespace ShootingRangeMiniGame.Engine.Markers
{
	public struct OnCollision : IEcsIgnoreInFilter
	{
		public EcsEntity? OtherEntity;
		public Vector2 Point;
	}
}