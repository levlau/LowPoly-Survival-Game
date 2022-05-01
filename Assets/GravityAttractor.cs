using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -18f;

    public void GetAttracted(Transform ownTransform)
    {
        Vector3 ownVelocityDir = (transform.position - ownTransform.position).normalized;

        ownTransform.rotation = Quaternion.FromToRotation(ownTransform.up, -ownVelocityDir) * ownTransform.rotation;
        ownTransform.GetComponent<Rigidbody>().AddForce(ownVelocityDir * gravity);
    }
}
