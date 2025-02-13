namespace Intersection3D
{

	public class SegmentComparison
	{

		// ������ ����� ���������� ����� ����������� (���� ��� ���������� � �����������) ���� �������� ���� Segment3D �����
		// ������������� �������� � ���� ��������������� ���������:
		// L1: P = p + t*d1, t in [0,1]
		// L2: Q = q + s*d2, s in [0,1]
		// ������� ���������: Segment3D seg1, seg2 -- ��� �������, 
		// double EPS -- ����������� ��� ��������� ����� ���� double
		// �������� ������: ����� ���� Vector3D -- ����� ����������� ���� �������� ��������, ���� ������� ������������,
		// Null -- �����
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
				// ���� cross ����� ������� � ������� �����������
				if (MatematicalOperationOnVectorsAndSegments.GetVectorMultiplication(q - p, d1).GetLengthOfVector() < EPS)
				{

					double d1LengthSquared = MatematicalOperationOnVectorsAndSegments.GetScalarMultiplication(d1, d1);
					if (d1LengthSquared < EPS)
					{
						// seg1 � ����������� ������� (�����)
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

					// ���� ��������� [t0, t1] � [0,1] �� ������������ � ��� �����������
					if (t1 < 0 || t0 > 1)
						return null;

					// ���� ����������� ������� � ����� �����
					double tStart = Math.Max(t0, 0);
					double tEnd = Math.Min(t1, 1);
					if (Math.Abs(tStart - tEnd) < EPS)
					{
						return p + d1 * tStart;
					}

					// ����������� ������������ ����� ������� � ������������
					return null;
				}
				else
				{
					// �����������, �� �� ����������� � ����������� ���
					return null;
				}
			}
			else
			{
				// ����� �� �����������. ���������� ����� ��������� t � s,
				// ��� ������� ����������� p + t*d1 = q + s*d2.
				Vector3D r = q - p;
				
				double t = MatematicalOperationOnVectorsAndSegments.GetScalarMultiplication(
				MatematicalOperationOnVectorsAndSegments.GetVectorMultiplication(r, d2), cross) / crossLengthSquared;
				
				double s = MatematicalOperationOnVectorsAndSegments.GetScalarMultiplication(
				MatematicalOperationOnVectorsAndSegments.GetVectorMultiplication(r, d1), cross) / crossLengthSquared;

				// ��������� ����� �� ����� ������ �� ��������� ����������
				Vector3D pointOnL1 = p + d1 * t;
				Vector3D pointOnL2 = q + d2 * s;

				// ���� ���������� ����� ������� ������� ������ � ����� ������ (�� ������������)
				if (pointOnL1.DistanceTo(pointOnL2) > EPS)
					return null;

				// ���������, ����� �� ��������� ��������� � �������� [0,1] � �� ���� �� ��������
				if (t < -EPS || t > 1 + EPS || s < -EPS || s > 1 + EPS)
					return null;

				return pointOnL1;
			}

		}

	}

}