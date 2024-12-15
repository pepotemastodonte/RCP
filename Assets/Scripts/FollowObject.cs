using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] Transform referncia;

    private void Update()
    {
        transform.position = referncia.position;
    }
}
