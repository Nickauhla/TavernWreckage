using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CookingTurkey : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.DOLocalRotate(Vector3.forward * 360, 10f, RotateMode.LocalAxisAdd);
        transform.DORestart();
    }
}
