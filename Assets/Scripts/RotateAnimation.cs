using System;
using DG.Tweening;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    public float speed = 2f;      // How fast it oscillates
    public float angle = 30f;     // Max rotation in degrees

    void Update()
    {
        float zRotation = Mathf.Sin(Time.time * speed) * angle;

        transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
    }
}
