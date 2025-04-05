using UnityEngine;

public class SplinePath : MonoBehaviour
{
    public Transform[] pathTransforms; // Array of Transforms for path points

    public Vector3 GetPoint(float t)
    {
        if (pathTransforms == null || pathTransforms.Length < 2) return Vector3.zero;

        // Ensure t is within 0-1 range
        t = Mathf.Clamp01(t);

        int segmentIndex = Mathf.FloorToInt(t * (pathTransforms.Length - 1));
        t = t * (pathTransforms.Length - 1) - segmentIndex;

        // Ensure segmentIndex is within bounds
        segmentIndex = Mathf.Clamp(segmentIndex, 0, pathTransforms.Length - 2);

        Vector3 p0 = pathTransforms[Mathf.Max(0, segmentIndex - 1)].position;
        Vector3 p1 = pathTransforms[segmentIndex].position;
        Vector3 p2 = pathTransforms[segmentIndex + 1].position;
        Vector3 p3 = pathTransforms[Mathf.Min(pathTransforms.Length - 1, segmentIndex + 2)].position;

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
        if (pathTransforms == null || pathTransforms.Length < 2) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < pathTransforms.Length - 1; i++)
        {
            for (float j = 0; j < 1f; j += 0.05f)
            {
                Vector3 p0 = pathTransforms[Mathf.Max(0, i - 1)].position;
                Vector3 p1 = pathTransforms[i].position;
                Vector3 p2 = pathTransforms[i + 1].position;
                Vector3 p3 = pathTransforms[Mathf.Min(pathTransforms.Length - 1, i + 2)].position;

                Gizmos.DrawLine(CalculateCatmullRom(p0, p1, p2, p3, j), CalculateCatmullRom(p0, p1, p2, p3, j + 0.05f));
            }
        }
    }

    public float GetLength()
    {
        if (pathTransforms == null || pathTransforms.Length < 2) return 0f;

        float length = 0f;
        for (int i = 0; i < pathTransforms.Length - 1; i++)
        {
            for (float j = 0; j < 1f; j += 0.05f)
            {
                Vector3 p0 = pathTransforms[Mathf.Max(0, i - 1)].position;
                Vector3 p1 = pathTransforms[i].position;
                Vector3 p2 = pathTransforms[i + 1].position;
                Vector3 p3 = pathTransforms[Mathf.Min(pathTransforms.Length - 1, i + 2)].position;

                length += Vector3.Distance(CalculateCatmullRom(p0, p1, p2, p3, j), CalculateCatmullRom(p0, p1, p2, p3, j + 0.05f));
            }
        }
        return length;
    }
}