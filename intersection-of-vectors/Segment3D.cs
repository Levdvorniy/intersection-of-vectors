namespace Intersection3D
{

	public class Segment3D
	{

		public Vector3D Start { get; set; }
		public Vector3D End { get; set; }

		public Segment3D(Vector3D start, Vector3D end)
		{
			Start = start;
			End = end;
		}

		public double GetLengthOfSegment()
		{
			return End.DistanceTo(Start);
		}

		public override string ToString()
		{
			return $"Segment3D({Start} -> {End})";
		}

	}

}