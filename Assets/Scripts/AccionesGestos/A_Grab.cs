using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Grab : Gesto
{
    [SerializeField] Transform destino;
    public void Activar()
    {

    }    public void Desactivar()
    {

    }

    public void Grab(Transform objetoAgarrado)
    {
        objetoAgarrado.position = destino.position;
    }
}
}
