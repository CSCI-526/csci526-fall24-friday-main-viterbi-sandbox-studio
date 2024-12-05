using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlayerController : PlayerController
{
    public float boxDistanceToGround = 1.6f;

    protected override float distanceToGround => boxDistanceToGround;

    protected override bool IsGrounded()
    {
        Vector3 boxSize = new Vector3(transform.localScale.x / 2, 0.1f, transform.localScale.z / 2);
        return Physics.BoxCast(
            transform.position,
            boxSize,
            Vector3.down,
            Quaternion.identity,
            distanceToGround + 0.1f,
            combinedLayerMask
        );
    }
}
