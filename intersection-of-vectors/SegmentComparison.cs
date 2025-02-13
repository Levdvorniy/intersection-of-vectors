using Microsoft.VisualBasic;

namespace Intersection3D
{

	public class SegmentComparison
	{

		// This method determines the intersection point(if it exists and is the only one) of two segments of the Segment3D type
		// through representation of segments in the form of parametric equations:
		// L1: P = p + t*d1, t in [0,1]
		// L2: Q = q + s *d2, s in [0,1]
		// Input parameters: Segment3D seg1, seg2 -- two segments,
		// double EPS -- error for comparing numbers of the double type
		// Output data: a point of the Vector3D type is the intersection point of two specified segments, if the segments intersect,
		// Null -- otherwise
		public static Vector3D? Intersect(Segment3D seg1, Segment3D seg2, double EPS)
		{

			Vector3D p = seg1.Start;
			Vector3D q = seg2.Start;
			Vector3D d1 = seg1.End - seg1.Start;
			Vector3D d2 = seg2.End - seg2.Start;

			Vector3D cross = MatematicalOperationOnVectorsAndSegments.GetVectorMultiplication(d1, d2);
			double crossLengthSquared = MatematicalOperationOnVectorsAndSegments.GetScalarMultiplication(cross, cross);

			if (crossLengthSquared < EPS)
			{
				// If the cross is almost zero, the segments are parallel
				if (MatematicalOperationOnVectorsAndSegments.GetVectorMultiplication(q - p, d1).GetLengthOfVector() < EPS)
				{

					double d1LengthSquared = MatematicalOperationOnVectorsAndSegments.GetScalarMultiplication(d1, d1);
					if (d1LengthSquared < EPS)
					{
						// seg1 is a degenerate segment (point)
						if (p.DistanceTo(q) < EPS)
							return p;
						else
							return null;
					}

					double t0 = MatematicalOperationOnVectorsAndSegments.GetScalarMultiplication(q - p, d1) / d1LengthSquared;
					double t1 = MatematicalOperationOnVectorsAndSegments.GetScalarMultiplication((q + d2) - p, d1) / d1LengthSquared;

					if (t0 > t1)
					{
						double temp = t0;
						t0 = t1;
						t1 = temp;
					}

					// If the intervals [t0, t1] and [0,1] do not intersect, there is no intersection
					if (t1 < 0 || t0 > 1)
						return null;

					// If the intersection is reduced to a single point
					double tStart = Math.Max(t0, 0);
					double tEnd = Math.Min(t1, 1);
					if (Math.Abs(tStart - tEnd) < EPS)
					{
						return p + d1 * tStart;
					}

					// The intersection is a segment – ambiguous
					return null;
				}
				else
				{
					// They are parallel, but not collinear – there is no intersection.
					return null;
				}
			}
			else
			{
				// The segments are not parallel. Let's try to find the parameters t and s,
				// for which p + t*d1 = q + s*d2 holds.
				Vector3D r = q - p;
				
				double t = MatematicalOperationOnVectorsAndSegments.GetScalarMultiplication(
				MatematicalOperationOnVectorsAndSegments.GetVectorMultiplication(r, d2), cross) / crossLengthSquared;
				
				double s = MatematicalOperationOnVectorsAndSegments.GetScalarMultiplication(
				MatematicalOperationOnVectorsAndSegments.GetVectorMultiplication(r, d1), cross) / crossLengthSquared;

				// We calculate the points on both segments based on the found parameters
				Vector3D pointOnL1 = p + d1 * t;
				Vector3D pointOnL2 = q + d2 * s;

				// If the distance between the points is too large, the segments are skew (don't intersect)
				if (pointOnL1.DistanceTo(pointOnL2) > EPS)
					return null;

				// We check whether the found parameters lie within [0,1] – that is on the segments
				if (t < -EPS || t > 1 + EPS || s < -EPS || s > 1 + EPS)
					return null;

				return pointOnL1;
			}

		}

	}

}