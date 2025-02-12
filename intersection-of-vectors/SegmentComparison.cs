namespace Intersection3D
{

	public class SegmentComparison
	{

		public static Vector3D? Intersect(Segment3D seg1, Segment3D seg2)
		{
			const double EPS = 1e-9;

			Vector3D p = seg1.Start;
			Vector3D q = seg2.Start;
			Vector3D d1 = seg1.End - seg1.Start;
			Vector3D d2 = seg2.End - seg2.Start;

			Vector3D cross = d1.GetVectorMultiplication(d2);
			double crossLenSq = cross.GetScalarMultiplication(cross);

			if (crossLenSq < EPS)
			{
				if ((q - p).GetVectorMultiplication(d1).GetLengthOfVector() < EPS)
				{
					double d1LenSq = d1.GetScalarMultiplication(d1);
					if (d1LenSq < EPS)
					{
						if (p.DistanceTo(q) < EPS)
							return p;
						else
							return null;
					}

					double t0 = (q - p).GetScalarMultiplication(d1) / d1LenSq;
					double t1 = ((q + d2) - p).GetScalarMultiplication(d1) / d1LenSq;

					if (t0 > t1)
					{
						double temp = t0;
						t0 = t1;
						t1 = temp;
					}

					if (t1 < 0 || t0 > 1)
						return null;

					double tStart = Math.Max(t0, 0);
					double tEnd = Math.Min(t1, 1);
					if (Math.Abs(tStart - tEnd) < EPS)
					{
						return p + d1 * tStart;
					}

					return null;
				}
				else
				{
					return null;
				}
			}
			else
			{
				Vector3D r = q - p;
				double t = r.GetVectorMultiplication(d2).GetScalarMultiplication(cross) / crossLenSq;
				double s = r.GetVectorMultiplication(d1).GetScalarMultiplication(cross) / crossLenSq;

				Vector3D pointOnL1 = p + d1 * t;
				Vector3D pointOnL2 = q + d2 * s;

				if (pointOnL1.DistanceTo(pointOnL2) > EPS)
					return null;

				if (t < -EPS || t > 1 + EPS || s < -EPS || s > 1 + EPS)
					return null;

				return pointOnL1;
			}
		}

	}

}