using System.Numerics;

namespace ShootingRangeMiniGame.Engine
{
	public struct BoundingBox
	{
		private static readonly Exception WrongIndex = new("Index can't be less than 0 or greater than 3!");
		
		public Vector2 this[int i]
		{
			get
			{
				if (i < 0 || i > 3)
					throw WrongIndex;
				return _points[i];
			}
			set
			{
				if (i < 0 || i > 3)
					throw WrongIndex;
				_points[i] = value;
			}
		}
		
		private Vector2[] _points;

		public BoundingBox(float x, float y, float width, float height)
		{
			_points = new[]
			{
				new Vector2(x, y),
				new Vector2(x + width, y),
				new Vector2(x + width, y + height),
				new Vector2(x, y + height),
			};
		}
	}
}