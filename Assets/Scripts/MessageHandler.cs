using UnityEngine;

public class MessageHandler : MonoBehaviour {
    public float numericValue;
    public LineGraph lineGraph;
    public LinearGraph linearGraph;
    public float margenDeError;

    public void ProcessIncomingMessage(string message)
    {
        if (IsNumeric(message) && float.TryParse(message, out float result))
        {
            var a = Mathf.Abs(numericValue - result);
            if (a > margenDeError)
            {
                numericValue = result;
            }          
            Debug.Log("Valor numérico recibido y almacenado: " + numericValue);

            // Añade el valor al gráfico
            if (lineGraph != null)
            {
                lineGraph.AddValue(numericValue);
            }
            if (linearGraph != null)
            {
                linearGraph.AddValue(numericValue);
            }
        }
        else
        {
            Debug.Log("El mensaje recibido no es numérico: " + message);
        }
    }

    private bool IsNumeric(string message)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(message, @"^[0-9]*\.?[0-9]+$");
    }
}
