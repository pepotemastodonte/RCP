using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesacativarVIsualManos : MonoBehaviour
{
    public List<SkinnedMeshRenderer> renderers;

    private void Start()
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    private void Update()
    {
        foreach (var renderer in renderers)
        {
            if (renderer.enabled)
            {
                renderer.enabled = false;
            }
        }
    }
}
