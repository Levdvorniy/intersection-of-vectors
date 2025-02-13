namespace Intersection3D
{

	public class SegmentComparison
	{

		// Данный метод определяет точку пересечения (если она существует и единственна) двух отрезков типа Segment3D через
		// представление отрезков в виде параметрических уравнений:
		// L1: P = p + t*d1, t in [0,1]
		// L2: Q = q + s*d2, s in [0,1]
		// Входные параметры: Segment3D seg1, seg2 -- два отрезка, 
		// double EPS -- погрешность для сравнения чисел типа double
		// Выходные данные: точка типа Vector3D -- точка пересечения двух заданных отрезков, если отрезки пересекаются,
		// Null -- иначе
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
				// Если cross почти нулевой – отрезки параллельны
				if (MatematicalOperationOnVectorsAndSegments.GetVectorMultiplication(q - p, d1).GetLengthOfVector() < EPS)
				{

					double d1LengthSquared = MatematicalOperationOnVectorsAndSegments.GetScalarMultiplication(d1, d1);
					if (d1LengthSquared < EPS)
					{
						// seg1 – вырожденный отрезок (точка)
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

					// Если интервалы [t0, t1] и [0,1] не пересекаются – нет пересечения
					if (t1 < 0 || t0 > 1)
						return null;

					// Если пересечение сведено к одной точке
					double tStart = Math.Max(t0, 0);
					double tEnd = Math.Min(t1, 1);
					if (Math.Abs(tStart - tEnd) < EPS)
					{
						return p + d1 * tStart;
					}

					// Пересечение представляет собой отрезок – неоднозначно
					return null;
				}
				else
				{
					// Параллельны, но не коллинеарны – пересечения нет
					return null;
				}
			}
			else
			{
				// Линии не параллельны. Попытаемся найти параметры t и s,
				// для которых выполняется p + t*d1 = q + s*d2.
				Vector3D r = q - p;
				
				double t = MatematicalOperationOnVectorsAndSegments.GetScalarMultiplication(
				MatematicalOperationOnVectorsAndSegments.GetVectorMultiplication(r, d2), cross) / crossLengthSquared;
				
				double s = MatematicalOperationOnVectorsAndSegments.GetScalarMultiplication(
				MatematicalOperationOnVectorsAndSegments.GetVectorMultiplication(r, d1), cross) / crossLengthSquared;

				// Вычисляем точки на обеих прямых по найденным параметрам
				Vector3D pointOnL1 = p + d1 * t;
				Vector3D pointOnL2 = q + d2 * s;

				// Если расстояние между точками слишком велико – линии скошие (не пересекаются)
				if (pointOnL1.DistanceTo(pointOnL2) > EPS)
					return null;

				// Проверяем, лежат ли найденные параметры в пределах [0,1] – то есть на отрезках
				if (t < -EPS || t > 1 + EPS || s < -EPS || s > 1 + EPS)
					return null;

				return pointOnL1;
			}

		}

	}

}