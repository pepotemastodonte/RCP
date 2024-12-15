using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gesto : MonoBehaviour
{
    public virtual void AccionNormal()
    {
        Debug.Log("Acción normal en Gesto");
    }
    public virtual void AccionConObjeto(GameObject objeto)
    {

    }
    public virtual void Desactivar()
    {

    }
    /*
    public void Grab(Transform objeto, Transform destino)
    {

    }
    */

}
