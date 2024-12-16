using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrecuenciaCalculator : MonoBehaviour
{
    [SerializeField] List<float> floats = new List<float>();
    [SerializeField] int PuntosCalculados;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    public EstadoPresion estadoPresion;
    float LastPointPos = 0;
    [SerializeField] DonutGraph donutGraph;
    [SerializeField] float timeGeneral = 0;
    [SerializeField] float timeEntrePulsos = 0;

    [SerializeField] float TiempoEntreCalculos;

    [SerializeField] float Pulsaciones;
    [SerializeField] float PulsacionesTotales;
    [SerializeField] float valor;

    private void Update()
    {       
        timeGeneral += Time.deltaTime;
        timeEntrePulsos += Time.deltaTime;
        if (timeGeneral > TiempoEntreCalculos)
        {           
            float num = 0;
            if (floats.Count > 0)
            {
                foreach (float f in floats)
                {
                    num += f;
                }
                float b = num / floats.Count;
                float c = 60 / b;
                donutGraph.ChangeValue(c);
                floats.Clear();
            }
            else
            {
                donutGraph.ChangeValue(0);
            }
            
            
            timeGeneral = 0;
            Pulsaciones = 0;
            
        }
    }
    public void EstadoCambioCheck(float point)
    {
        if (estadoPresion != StatusCalculator(point))
        {
            if (estadoPresion == EstadoPresion.Subiendo)
            {
                NewPoint(point);
            }          
        }
    }
    public EstadoPresion StatusCalculator(float point)
    {
        if (point > LastPointPos)
        {
            CambiarEstado(EstadoPresion.Subiendo);
            LastPointPos = point;
            return (EstadoPresion.Subiendo);
        }
        else
        {
            CambiarEstado(EstadoPresion.Bajando);
            LastPointPos = point;
            return (EstadoPresion.Bajando);
        }
    }
    void NewPoint(float point)
    {
        floats.Add(timeEntrePulsos);
        timeEntrePulsos = 0;
        valor = point;
        Pulsaciones++;
        PulsacionesTotales++;

        /*
        if (floats.Count >= PuntosCalculados)
        {
            floats.RemoveAt(0);         
        }
        
        floats.Add(point);
        */
        //textMeshProUGUI.text = CalcularMedia().ToString();
        //donutGraph.ChangeValue(point);
    }

    float CalcularMedia()
    {
        float a;

        a = 0;
        foreach (var f in floats)
        {
            a += f;
        }

        return (a / PuntosCalculados);
    }

    void CambiarEstado(EstadoPresion st)
    {
        estadoPresion = st;
    }
}

public enum EstadoPresion
{
    Bajando,
    Subiendo,
    Ninguno

}
