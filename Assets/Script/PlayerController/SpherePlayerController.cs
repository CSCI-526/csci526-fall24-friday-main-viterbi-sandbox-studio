using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePlayerController : PlayerController
{
    public float sphereDistanceToGround = 0f;

    protected override float distanceToGround => sphereDistanceToGround;

    protected override bool IsGrounded()
    {
        float sphereRadius = transform.localScale.x / 2;
        return Physics.SphereCast(
            transform.position,
            sphereRadius,
            Vector3.down,
            out RaycastHit hit,
            distanceToGround + 0.1f,
            combinedLayerMask
        );
    }
}
