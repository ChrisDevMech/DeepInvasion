using UnityEngine;

public class SplinePath : MonoBehaviour
{
    public Vector3[] pathPoints;

    public Vector3 GetPoint(float t)
    {
        if (pathPoints == null || pathPoints.Length < 2) return Vector3.zero;

        // Ensure t is within 0-1 range
        t = Mathf.Clamp01(t);

        int segmentIndex = Mathf.FloorToInt(t * (pathPoints.Length - 1));
        t = t * (pathPoints.Length - 1) - segmentIndex;

        // Ensure segmentIndex is within bounds
        segmentIndex = Mathf.Clamp(segmentIndex, 0, pathPoints.Length - 2);

        Vector3 p0 = pathPoints[Mathf.Max(0, segmentIndex - 1)];
        Vector3 p1 = pathPoints[segmentIndex];
        Vector3 p2 = pathPoints[segmentIndex + 1];
        Vector3 p3 = pathPoints[Mathf.Min(pathPoints.Length - 1, segmentIndex + 2)];

        return CalculateCatmullRom(p0, p1, p2, p3, t);
    }

    Vector3 CalculateCatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 a = 2f * p1;
        Vector3 b = p2 - p0;
        Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
        Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

        return 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));
    }


    void OnDrawGizmos()
    {
        if (pathPoints == null || pathPoints.Length < 2) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < pathPoints.Length - 1; i++)
        {
            for (float j = 0; j < 1f; j += 0.05f)
            {
                Vector3 p0 = pathPoints[Mathf.Max(0, i - 1)];
                Vector3 p1 = pathPoints[i];
                Vector3 p2 = pathPoints[i + 1];
                Vector3 p3 = pathPoints[Mathf.Min(pathPoints.Length - 1, i + 2)];

                Gizmos.DrawLine(CalculateCatmullRom(p0, p1, p2, p3, j), CalculateCatmullRom(p0, p1, p2, p3, j + 0.05f));
            }
        }
    }
    public float GetLength()
    {
        if (pathPoints == null || pathPoints.Length < 2) return 0f;

        float length = 0f;
        for (int i = 0; i < pathPoints.Length - 1; i++)
        {
            for (float j = 0; j < 1f; j += 0.05f)
            {
                Vector3 p0 = pathPoints[Mathf.Max(0, i - 1)];
                Vector3 p1 = pathPoints[i];
                Vector3 p2 = pathPoints[i + 1];
                Vector3 p3 = pathPoints[Mathf.Min(pathPoints.Length - 1, i + 2)];

                length += Vector3.Distance(CalculateCatmullRom(p0, p1, p2, p3, j), CalculateCatmullRom(p0, p1, p2, p3, j + 0.05f));
            }
        }
        return length;
    }
}