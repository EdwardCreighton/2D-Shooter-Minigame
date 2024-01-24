using System.Numerics;
using Leopotam.Ecs;

namespace ShootingRangeMiniGame.Engine.Markers
{
	public struct OnCollision
	{
		public EcsEntity OtherEntity;
		public Vector2 Point;
	}
}