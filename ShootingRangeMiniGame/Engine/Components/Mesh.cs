namespace ShootingRangeMiniGame.Engine.Components
{
	public struct Mesh
	{
		public Point[] Points;
		public Brush FillColor;

		public Mesh(Mesh original)
		{
			Points = new Point[original.Points.Length];
			for (int i = 0; i < Points.Length; i++)
			{
				Points[i].X = original.Points[i].X;
				Points[i].Y = original.Points[i].Y;
			}

			FillColor = (Brush)original.FillColor.Clone();
		}
	}
}