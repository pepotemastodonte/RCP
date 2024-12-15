using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    public List<Transform> objects = new List<Transform>();
    public string GrabbableTag;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (!other.CompareTag(GrabbableTag))
        {
            return;
        }
        var obj = other.gameObject.transform;
        if (!objects.Contains(obj))
        {
            objects.Add(obj);
        }       
    }
    private void OnTriggerExit(Collider other)
    {

        var obj = other.gameObject.transform;
        if (objects.Contains(obj))
        {
            objects.Remove(obj);
        }      
    }
    
}
