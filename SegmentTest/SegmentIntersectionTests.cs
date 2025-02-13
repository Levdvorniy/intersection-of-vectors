using Intersection3D;

namespace SegmentTest
{

	public class SegmentIntersectionTests
	{
		const double tolerance = 1e-9;

		[Fact]
		public void IntersectionTwoSegmentsTest_True()
		{
			// Arrange
			var a = new Vector3D(0, 0, 0);
			var b = new Vector3D(1, 1, 1);
			var c = new Vector3D(0, 1, 0);
			var d = new Vector3D(1, 0, 1);

			var seg1 = new Segment3D(a, b);
			var seg2 = new Segment3D(c, d);

			// Act
			Vector3D? intersection = SegmentComparison.Intersect(seg1, seg2, tolerance);

			// Assert
			Assert.NotNull(intersection);
			var expected = new Vector3D(0.5, 0.5, 0.5);

			Assert.InRange(intersection.X, expected.X - tolerance, expected.X + tolerance);
			Assert.InRange(intersection.Y, expected.Y - tolerance, expected.Y + tolerance);
			Assert.InRange(intersection.Z, expected.Z - tolerance, expected.Z + tolerance);
		}

		[Fact]
		public void IntersectionTwoSegmentsLengthZeroTest_True()
		{
			// Arrange
			var a = new Vector3D(1, 1, 1);
			var b = new Vector3D(1, 1, 1);
			var c = new Vector3D(1, 1, 1);
			var d = new Vector3D(1, 1, 1);

			var seg1 = new Segment3D(a, b);
			var seg2 = new Segment3D(c, d);

			// Act
			Vector3D? intersection = SegmentComparison.Intersect(seg1, seg2, tolerance);

			// Assert
			Assert.NotNull(intersection);
			var expected = new Vector3D(1, 1, 1);

			Assert.InRange(intersection.X, expected.X - tolerance, expected.X + tolerance);
			Assert.InRange(intersection.Y, expected.Y - tolerance, expected.Y + tolerance);
			Assert.InRange(intersection.Z, expected.Z - tolerance, expected.Z + tolerance);
		}

		[Fact]
		public void IntersectionTwoParallelSegmentsTest_False()
		{
			// Arrange
			var a = new Vector3D(0, 0, 0);
			var b = new Vector3D(0, 0, 1);
			var c = new Vector3D(1, 0, 0);
			var d = new Vector3D(1, 0, 1);

			var seg1 = new Segment3D(a, b);
			var seg2 = new Segment3D(c, d);

			// Act
			Vector3D? intersection = SegmentComparison.Intersect(seg1, seg2, tolerance);

			// Assert
			Assert.Null(intersection);
		}

		[Fact]
		public void IntersectionTwoSegmentsOnOneLineTest_False()
		{
			// Arrange
			var a = new Vector3D(0, 0, 0);
			var b = new Vector3D(0, 0, 1);
			var c = new Vector3D(0, 0, 2);
			var d = new Vector3D(0, 0, 4);

			var seg1 = new Segment3D(a, b);
			var seg2 = new Segment3D(c, d);

			// Act
			Vector3D? intersection = SegmentComparison.Intersect(seg1, seg2, tolerance);

			// Assert
			Assert.Null(intersection);
		}
	}

}