namespace Intersection3D
{
	
	class Program
	{
		static void Main(string[] args)
		{
			// Пример 1: два отрезка, пересекающиеся в одной точке
			Vector3D a = new Vector3D(0, 0, 0);
			Vector3D b = new Vector3D(1, 1, 1);
			Vector3D c = new Vector3D(0, 1, 0);
			Vector3D d = new Vector3D(1, 0, 1);

			Segment3D seg1 = new Segment3D(a, b);
			Segment3D seg2 = new Segment3D(c, d);

			Vector3D? intersection = SegmentComparison.Intersect(seg1, seg2);
			if (intersection != null)
			{
				Console.WriteLine("Intersection point: " + seg1 + seg2 + intersection);
			}
			else
			{
				Console.WriteLine("No unique intersection found.");
			}

		}

	}
	
}
