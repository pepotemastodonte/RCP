using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LinearGraph : MonoBehaviour
{
    [SerializeField] float Ymax;
    [SerializeField] float Ymin;

    private List<float> pointsList = new List<float>();

    [SerializeField] RectTransform yMaxObj;
    [SerializeField] RectTransform yMinObj;

    [SerializeField] RectTransform Marcador;
    [SerializeField] FrecuenciaCalculator calculator;

    [SerializeField] float Max;
    [SerializeField] float Min;





    public float valor;

    [SerializeField] float PresionMedia;

    private void Start()
    {
        Ymax = yMaxObj.localPosition.y;
        Ymin = yMinObj.localPosition.y;      
    }

    public void AddValue(float value)
    {
        float mappedValue = Map(value, Min, Max, Ymin, Ymax);
        valor = mappedValue;
        MoverMarcador(mappedValue);
        pointsList.Add(value);
        calculator.EstadoCambioCheck(value);
    }

    void MoverMarcador(float value)
    {

        CambiarAltura(Marcador, value);
    }

    void CambiarAltura(RectTransform trans, float value)
    {
        trans.localPosition = new Vector3(trans.localPosition.x, value, trans.localPosition.z);
    }

    float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return Mathf.Lerp(outMin, outMax, Mathf.InverseLerp(inMin, inMax, value));
    }

    float CalcularMedia()
    {
        float a = 0f;
        foreach (float num in pointsList)
        {
            a += num;
        }
        float b = a / pointsList.Count;
        var c = b - 33;
        return c;
    }
    private void OnDisable()
    {
        PresionMedia = CalcularMedia();
    }
}
