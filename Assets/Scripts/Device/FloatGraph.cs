using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatGraph : MonoBehaviour
{
    public bool showDetectedPoitns = false;

    public List<double> dataPoints; // Lista wartości double
    public List<int> detectedPointsX; // Lista wartości double
    public List<double> detectedPointsY; // Lista wartości double

    [SerializeField] float graphWidth = 485f; // Szerokość wykresu
    [SerializeField] float graphHeight = 50f; // Wysokość wykresu
    [SerializeField] float graphDotsRadius = 5f; // Wysokość wykresu

    // Metoda do przekazywania wartości z innego skryptu
    public void SetDataPoints(List<double> newDataPoints)
    {
        dataPoints = newDataPoints;
    }
    public void SetDetectedPoints(List<int> newDataPointsX, List<double> newDataPointsY)
    {
        detectedPointsX = newDataPointsX;
        detectedPointsY = newDataPointsY;
    }

    private void OnDrawGizmos()
    {
        if (dataPoints == null || dataPoints.Count == 0)
            return;

        double maxValue = FindMaxValue();

        float xStep = graphWidth / (dataPoints.Count - 1);
        float yScale = graphHeight / (float)maxValue;

        int j = 0;
        for (int i = 0; i < dataPoints.Count - 1; i++)
        {
            float x1 = transform.position.x + i * xStep;
            float y1 = transform.position.y + (float)dataPoints[i] * yScale;
            float x2 = transform.position.x + (i + 1) * xStep;
            float y2 = transform.position.y + (float)dataPoints[i + 1] * yScale;

            if (showDetectedPoitns)
            {
                if (j < detectedPointsX.Count)
                    if (detectedPointsX[j] == i)
                    {
                        float xx1 = transform.position.x + i * xStep;
                        float yy1 = transform.position.y + (float)detectedPointsY[j] * yScale;
                        Gizmos.color = Color.red;
                        Gizmos.DrawSphere(new Vector3(xx1, yy1, 0f), graphDotsRadius);
                        ++j;
                    }
            }

            Gizmos.color = Color.black;

            Gizmos.DrawLine(new Vector3(x1, y1, 0f), new Vector3(x2, y2, 0f));
        }
    }

    private void OnDrawGizmosSelected()
    {
        OnDrawGizmos();
    }

    private double FindMaxValue()
    {
        double maxValue = double.MinValue;
        foreach (double value in dataPoints)
        {
            if (value > maxValue)
                maxValue = value;
        }
        return maxValue;
    }
}