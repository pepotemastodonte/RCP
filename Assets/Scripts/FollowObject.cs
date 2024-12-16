using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] Transform referncia;
    [SerializeField] bool FollowRotation;

    private void Update()
    {
        transform.position = referncia.position;
        if (FollowRotation)
        {
            transform.rotation = referncia.rotation;
        }
    }
}
