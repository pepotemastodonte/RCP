using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class A_Grab : Gesto
{
    [SerializeField] Transform grabPosition;
    [SerializeField] ObjectDetector ObjectDetector;
    List<Transform> objects = new List<Transform>();

    [SerializeField] Grabbable grabbedObj = new Grabbable();
    bool ManoOcupada;

    public override void AccionNormal()
    {
        if (ManoOcupada)
        {
            Debug.Log("Mano ocupada");
            return;
        }
        //Debug.Log("Esta en A_grab");
        objects = ObjectDetector.objects;
        Transform obj = GetClosestTransform(grabPosition.position, objects);
        if (obj == null)
        {
            return;
        }
        Grab(obj);
    }

    public override void Desactivar()
    {
        if (ManoOcupada)
        {
            Soltar();
        }      
    }
    void Grab(Transform obj)
    {
        grabbedObj = obj.GetComponent<Grabbable>();
        grabbedObj.Agarrar(grabPosition);
        ManoOcupada = true;
        Debug.Log("Agarrar");


    }
    void Soltar()
    {
        if (grabbedObj == null)
        {
            return;
        }
        grabbedObj.Soltar();
        grabbedObj = null;
        ManoOcupada = false;
        Debug.Log("Soltar");
        
    }
    public static Transform GetClosestTransform(Vector3 referencePoint, List<Transform> objects)
    {
        if (objects == null || objects.Count == 0)
        {
            //Debug.LogError("La lista está vacía o es nula.");
            return null;
        }

        // Busca el Transform más cercano
        Transform closest = objects
            .OrderBy(obj => Vector3.Distance(referencePoint, obj.position))
            .First();

        return closest;
    }
}

public class GrabbedObj
{
    public Transform obj;
    public Rigidbody rb;
    public bool Kinematic;

}
