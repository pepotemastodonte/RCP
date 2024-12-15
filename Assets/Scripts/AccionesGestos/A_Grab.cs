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

    GrabbedObj grabbedObj = new GrabbedObj();
    bool ManoOcupada;

    public override void AccionNormal()
    {
        if (ManoOcupada)
        {
            Debug.Log("Mano ocupada");
            return;
        }
        Debug.Log("Esta en A_grab");
        objects = ObjectDetector.objects;
        Transform obj = GetClosestTransform(grabPosition.position, objects);
        if (obj == null)
        {
            return;
        }
        ConfigurarGrabbaed(obj);
        Grab(obj, grabPosition);
    }

    public override void Desactivar()
    {
        Debug.Log("A_grabSuelta");
        if (ManoOcupada)
        {
            Soltar();
        }      
    }
    void Grab(Transform objeto, Transform destino)
    {
        Transform obj = objeto;
        if (!grabbedObj.Kinematic)
        {
            GrabbedChangeRb(true);
        }
        obj.position = destino.position;
        obj.SetParent(destino);
        ManoOcupada = true;
    }
    void Soltar()
    {
        if (grabbedObj == null)
        {
            return;
        }
        if (!grabbedObj.Kinematic)
        {
            GrabbedChangeRb(false);
            Debug.Log("AAAAAAAAAAA");
        }
        grabbedObj.obj.SetParent(null);
        ManoOcupada = false;
        
    }
    public static Transform GetClosestTransform(Vector3 referencePoint, List<Transform> objects)
    {
        if (objects == null || objects.Count == 0)
        {
            Debug.LogError("La lista está vacía o es nula.");
            return null;
        }

        // Busca el Transform más cercano
        Transform closest = objects
            .OrderBy(obj => Vector3.Distance(referencePoint, obj.position))
            .First();

        return closest;
    }

    void ConfigurarGrabbaed(Transform obj)
    {
        grabbedObj.obj = obj;
        RbConf(obj);
        
    }

    void GrabbedChangeRb(bool state)
    {
        grabbedObj.rb.isKinematic = state;
    }

    void RbConf(Transform obj)
    {
        if (obj.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            grabbedObj.rb = rb;
            if (rb.isKinematic)
            {
                grabbedObj.Kinematic = true;
            }
            else
            {
                grabbedObj.Kinematic = false;
            } 
        }
    }
}

public class GrabbedObj
{
    public Transform obj;
    public Rigidbody rb;
    public bool Kinematic;

}
