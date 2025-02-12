namespace Intersection3D
{

	public class Vector3D
	{

		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public Vector3D(double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static Vector3D operator +(Vector3D a, Vector3D b)
		{
			return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public static Vector3D operator -(Vector3D a, Vector3D b)
		{
			return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		public static Vector3D operator *(Vector3D a, double s)
		{
			return new Vector3D(a.X * s, a.Y * s, a.Z * s);
		}

		public double GetScalarMultiplication(Vector3D other)
		{
			return X * other.X + Y * other.Y + Z * other.Z;
		}

		public Vector3D GetVectorMultiplication(Vector3D other)
		{
			return new Vector3D(
				Y * other.Z - Z * other.Y,
				Z * other.X - X * other.Z,
				X * other.Y - Y * other.X
			);
		}

		public double GetLengthOfVector()
		{
			return Math.Sqrt(X * X + Y * Y + Z * Z);
		}

		public double DistanceTo(Vector3D other)
		{
			return (this - other).GetLengthOfVector();
		}

		public override string ToString()
		{
			return $"({X}, {Y}, {Z})";
		}

	}

}
