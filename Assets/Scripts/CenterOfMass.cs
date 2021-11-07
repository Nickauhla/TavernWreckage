using System;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Rigidbody))]
public class CenterOfMass : MonoBehaviour
{
    public Transform m_centerOfMassPosition;

    private void OnEnable()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = m_centerOfMassPosition.position;
    }
}
