using System.Collections.Generic;
using UnityEngine;

public class LineGraph : MonoBehaviour {
    public RectTransform graphContainer;
    public GameObject pointPrefab; // Prefab que representa el punto visual del gráfico
    public int maxPoints = 50; // Número máximo de puntos a mostrar en el eje X
    public float graphHeight = 200f; // Altura del gráfico
    public float graphWidth = 300f; // Ancho del gráfico
    public float maxYValue = 255f; // Valor máximo del eje Y
    public float minYValue = 0f; // Valor mínimo del eje Y
    [SerializeField] FrecuenciaCalculator calculator;

    private List<float> valueList = new List<float>();
    private List<GameObject> points = new List<GameObject>();
    private LineRenderer lineRenderer;


    private void Awake()
    {
        if (gameObject.TryGetComponent<LineRenderer>(out LineRenderer line))
        {
            lineRenderer = line;
        }
        else
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        } 
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        /*
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        */
        lineRenderer.positionCount = 0; // Al principio no hay puntos en la línea
    }

    public void AddValue(float value)
    {
        // Limitar el número de puntos en el gráfico
        if (valueList.Count >= maxPoints)
        {
            // Eliminar el punto más antiguo si hemos alcanzado el número máximo
            valueList.RemoveAt(0);
        }

        // Añadir el nuevo valor
        valueList.Add(value);

        // Actualizar el gráfico
        DrawGraph();
        calculator.EstadoCambioCheck(value);
    }

    private void DrawGraph()
    {
        // Limpiar los puntos visuales existentes
        
        foreach (var point in points)
        {
            Destroy(point);
        }
        points.Clear();

        // Actualizar el LineRenderer con la cantidad correcta de puntos
        lineRenderer.positionCount = valueList.Count;

        // Dibujar los nuevos puntos y actualizar la línea
        for (int i = 0; i < valueList.Count; i++)
        {
            // Calcular la posición en el eje X (espaciado entre puntos)
            float xPosition = i * (graphWidth / maxPoints);

            // Normalizar y escalar la posición en el eje Y
            float normalizedValue = Mathf.InverseLerp(minYValue, maxYValue, valueList[i]);
            float yPosition = normalizedValue * graphHeight;

            // Instanciar un nuevo punto en el gráfico
            GameObject point = Instantiate(pointPrefab, graphContainer);
            point.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, yPosition);
            points.Add(point);

            // Establecer la posición en el LineRenderer
            lineRenderer.SetPosition(i, new Vector3(xPosition, yPosition, 0));
        }
    }

    // Métodos para actualizar configuración dinámica
    public void SetMaxPoints(int newMaxPoints)
    {
        maxPoints = Mathf.Max(1, newMaxPoints); // Asegura que siempre haya al menos un punto
        DrawGraph();
    }

    public void SetYRange(float minY, float maxY)
    {
        minYValue = minY;
        maxYValue = maxY;
        DrawGraph();
    }
}
