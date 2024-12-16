using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutGraph : MonoBehaviour
{
    [Header("Input Range")]
    public float Value;
    public float minInput = 0f; // Límite inferior del rango de entrada
    public float maxInput = 60f; // Límite superior del rango de entrada

    [Header("Rotation Range")]
    public float minRotationZ = 0f; // Rotación mínima en Z (en grados)
    public float maxRotationZ = 180f; // Rotación máxima en Z (en grados)

    [SerializeField] Transform objectTransform;

    public void ChangeValue(float value)
    {
        Value = value;
        if (value < 1)
        {
            value = 0f;
        }
        // Mapea el valor de inputValue al rango de rotación en Z
        float clampedValue = Mathf.Clamp(value, minInput, maxInput); // Limita el valor de entrada
        float normalizedValue = Mathf.InverseLerp(minInput, maxInput, clampedValue);

        // Calcula la rotación interpolada solo en el eje Z
        float mappedRotationZ = Mathf.Lerp(minRotationZ, maxRotationZ, normalizedValue);

        // Aplica la rotación al objeto solo en Z, manteniendo las rotaciones de X e Y
        objectTransform.rotation = Quaternion.Euler(objectTransform.rotation.eulerAngles.x, objectTransform.rotation.eulerAngles.y, mappedRotationZ);

        // Debug para verificar el resultado
        Debug.Log($"Mapped Rotation Z: {mappedRotationZ}");
    }
}
