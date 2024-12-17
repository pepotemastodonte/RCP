using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.OpenXR.Features.Meta;

public class PlaneDetection : MonoBehaviour {

    public ARPlaneManager planeManager;

    private void Awake()
    {
        SubscribeToPlanesChanged();
    }
    public void OnPlanesChanged(ARPlanesChangedEventArgs changes)
    {
        foreach (var plane in changes.added)
        {
            // handle added planes
        }

        foreach (var plane in changes.updated)
        {
            // handle updated planes
        }

        foreach (var plane in changes.removed)
        {
            // handle removed planes
        }
    }
    void SubscribeToPlanesChanged()
    {
        // This is inefficient. You should re-use a saved reference instead.
        planeManager.planesChanged += OnPlanesChanged;
    }
}
