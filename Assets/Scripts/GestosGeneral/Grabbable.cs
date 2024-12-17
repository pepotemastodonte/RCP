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

    // Variables públicas para los ejes
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

        // Obtén la posición del objetivo
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
        // Aplica la nueva posición
        transform.position = nuevaPosicion;
    }
    void RotarCadaEje()
    {
        // Obtén la rotación del objetivo
        Quaternion objetivoRot = grabObj.transform.rotation;

        // Crear una rotación combinada que solo se aplica en los ejes que están activados
        Vector3 rotacionEuler = objetivoRot.eulerAngles;

        // Aplica la rotación solo en los ejes que estén habilitados
        if (!RotacionEjeX)
        {
            rotacionEuler.x = transform.rotation.eulerAngles.x;  // Mantén la rotación actual en X si está deshabilitada
        }
        if (!RotacionEjeY)
        {
            rotacionEuler.y = transform.rotation.eulerAngles.y;  // Mantén la rotación actual en Y si está deshabilitada
        }
        if (!RotacionEjeZ)
        {
            rotacionEuler.z = transform.rotation.eulerAngles.z;  // Mantén la rotación actual en Z si está deshabilitada
        }

        // Aplicar la rotación al objeto
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
            // Lógica adicional si es necesario
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
    Z  // Eje de profundidad (adelante-atrás)
}
