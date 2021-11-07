using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KnifeSpawner : XRSocketInteractor
{
    [SerializeField] private ThrowingBehaviour KnifePrefab;

    private ThrowingBehaviour m_instance;

    protected override void OnHoverEntering(HoverEnterEventArgs args)
    {
        base.OnHoverEntering(args);
    }

    protected override void OnHoverExiting(HoverExitEventArgs args)
    {
        base.OnHoverExiting(args);
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
    }


    private ThrowingBehaviour InstanciateNewKnife(Vector3 pos)
    {
        ThrowingBehaviour instantiate = Instantiate(KnifePrefab, pos, Quaternion.identity);
        Rigidbody rb = instantiate.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;
        return instantiate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_instance) return;
        var transform = this.transform;
        Vector3 pos = transform.position + transform.right * 0.15f;
        m_instance = InstanciateNewKnife(pos);
    }

    private void OnTriggerExit(Collider other)
    {
        Invoke(nameof(CleanSpawner), 2f);
    }

    private void CleanSpawner()
    {
        if (m_instance && !m_instance.HasBeenUsed) Destroy(m_instance.gameObject);
        m_instance = null;
    }
}
