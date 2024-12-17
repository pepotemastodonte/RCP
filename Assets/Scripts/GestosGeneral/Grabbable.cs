using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRPlugin;

public class Grabbable : MonoBehaviour {

    public bool siendoAgarrado = false;
    public bool KinematicAlAgarrar = true;
    public bool snapAlAgarrar = true;

    public bool MoverEjeX = true;
    public bool MoverEjeY = true;
    public bool MoverEjeZ = true;

    private bool rotarConLaMano = true;

    // Variables p�blicas para los ejes
    public bool RotacionEjeX = true;
    public bool RotacionEjeY = true;
    public bool RotacionEjeZ = true;

    private Rigidbody rb;
    private Transform grabObj;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (siendoAgarrado)
        {
            if (snapAlAgarrar)
            {
                if (MoverEjeX && MoverEjeY && MoverEjeZ)
                {
                    SeguimientoCompleto(grabObj);
                }
                else
                {
                    MoverCadaEje();
                }
                
            }
            if (rotarConLaMano)
            {
                RotarCadaEje();
            }

        }
    }
    public void Agarrar(Transform grab)
    {
        grabObj = grab;
        siendoAgarrado = true;
    }
    public void Soltar()
    {
        grabObj = null;
        siendoAgarrado = false;
    }
    void SeguimientoCompleto(Transform destino)
    {
        transform.position = destino.position;
    }
    void MoverCadaEje()
    {
        Vector3 nuevaPosicion = transform.position;

        // Obt�n la posici�n del objetivo
        Vector3 objetivoPos = grabObj.transform.position;

        // Mueve solo a lo largo del eje especificado
        if (MoverEjeX)
        {
            nuevaPosicion.x = objetivoPos.x; // Mantiene la misma Y y Z, solo cambia la X
        }
        if (MoverEjeY)
        {
            nuevaPosicion.y = objetivoPos.y; // Mantiene la misma X y Z, solo cambia la Y
        }
        if (MoverEjeZ)
        {
            nuevaPosicion.z = objetivoPos.z; // Mantiene la misma X y Y, solo cambia la Z
        }
        // Aplica la nueva posici�n
        transform.position = nuevaPosicion;
    }
    void RotarCadaEje()
    {
        // Obt�n la rotaci�n del objetivo
        Quaternion objetivoRot = grabObj.transform.rotation;

        // Crear una rotaci�n combinada que solo se aplica en los ejes que est�n activados
        Vector3 rotacionEuler = objetivoRot.eulerAngles;

        // Aplica la rotaci�n solo en los ejes que est�n habilitados
        if (!RotacionEjeX)
        {
            rotacionEuler.x = transform.rotation.eulerAngles.x;  // Mant�n la rotaci�n actual en X si est� deshabilitada
        }
        if (!RotacionEjeY)
        {
            rotacionEuler.y = transform.rotation.eulerAngles.y;  // Mant�n la rotaci�n actual en Y si est� deshabilitada
        }
        if (!RotacionEjeZ)
        {
            rotacionEuler.z = transform.rotation.eulerAngles.z;  // Mant�n la rotaci�n actual en Z si est� deshabilitada
        }

        // Aplicar la rotaci�n al objeto
        transform.rotation = Quaternion.Euler(rotacionEuler);
    }

    void ConfigurarRb()
    {
        if (KinematicAlAgarrar)
        {
            rb.isKinematic = true;
        }
        /*
        if (RotarConLaMano)
        {
            // L�gica adicional si es necesario
            if (EjeX)
            {

            }
            if (EjeY)
            {

            }
            if (EjeZ)
            {

            }

        }
        */
    }

    public bool SnapAlAgarrar
    {
        get => snapAlAgarrar;
        set
        {
            snapAlAgarrar = value;

            // Si se desactiva RotarConLaMano, reiniciar los ejes
            if (!rotarConLaMano)
            {
                MoverEjeX = false;
                MoverEjeY = false;
                MoverEjeZ = false;
            }
        }
    }
    public bool RotarConLaMano
    {
        get => rotarConLaMano;
        set
        {
            rotarConLaMano = value;

            // Si se desactiva RotarConLaMano, reiniciar los ejes
            if (!rotarConLaMano)
            {
                RotacionEjeX = false;
                RotacionEjeY = false;
                RotacionEjeZ = false;
            }
        }
    }
}
public enum Eje {
    X, // Eje horizontal (izquierda-derecha)
    Y, // Eje vertical (arriba-abajo)
    Z  // Eje de profundidad (adelante-atr�s)
}
