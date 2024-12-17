using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ConsoleToWorldSpaceCanvas : MonoBehaviour {
    // Referencia al objeto de texto en el Canvas
    public TextMeshProUGUI consoleText;

    // Lista para almacenar los mensajes de la consola
    private List<string> consoleMessages = new List<string>();

    // N�mero m�ximo de mensajes a mostrar
    public int maxMessages = 10;

    // Se ejecuta cuando el script se inicia
    private void OnEnable()
    {
        // Suscribirse al evento de la consola
        Application.logMessageReceived += HandleLog;
    }

    // Se ejecuta cuando el script se desactiva
    private void OnDisable()
    {
        // Desuscribirse del evento de la consola
        Application.logMessageReceived -= HandleLog;
    }

    // M�todo que maneja los mensajes de la consola
    private void HandleLog(string logString, string stackTrace, LogType logType)
    {
        // A�adir el mensaje a la lista
        consoleMessages.Add(logString);

        // Limitar la cantidad de mensajes a mostrar
        if (consoleMessages.Count > maxMessages)
        {
            consoleMessages.RemoveAt(0); // Eliminar el mensaje m�s antiguo
        }

        // Actualizar el texto que se mostrar� en la pantalla
        UpdateConsoleText();
    }

    // M�todo para actualizar el texto del Canvas con los mensajes de la consola
    private void UpdateConsoleText()
    {
        // Limpiar el texto actual
        consoleText.text = "";

        // Concatenar todos los mensajes de la lista y agregarlos al texto del Canvas
        foreach (string message in consoleMessages)
        {
            consoleText.text += message + "\n";
        }
    }
}
