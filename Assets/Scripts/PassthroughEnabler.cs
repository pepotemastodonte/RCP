using UnityEngine;

public class PassthroughEnabler : MonoBehaviour
{
    void Start()
    {
        if (OVRManager.isHmdPresent)
        {
            // Activa el Passthrough
            OVRManager.instance.isInsightPassthroughEnabled = true;
            Debug.Log("Passthrough activado.");
        }
        else
        {
            Debug.LogError("No se detectó un visor Oculus.");
        }
    }
}
