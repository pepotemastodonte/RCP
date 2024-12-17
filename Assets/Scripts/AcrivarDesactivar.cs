using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcrivarDesactivar : MonoBehaviour
{
    [SerializeField] BoxCollider BoxCollider;
    [SerializeField] bool Activa;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mano"))
        {
            if (Activa)
            {
                BoxCollider.enabled = true;
            }
            else
            {
                BoxCollider.enabled = false;
            }
        }
    }
}
