namespace Intersection3D
{
	class MatematicalOperationOnVectorsAndSegments
	{
		public static double GetScalarMultiplication(Vector3D vector1, Vector3D vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
		}

		public static Vector3D GetVectorMultiplication(Vector3D vector1, Vector3D vector2)
		{
			return new Vector3D(
				vector1.Y * vector2.Z - vector1.Z * vector2.Y,
				vector1.Z * vector2.X - vector1.X * vector2.Z,
				vector1.X * vector2.Y - vector1.Y * vector2.X
			);
		}


	}
}
