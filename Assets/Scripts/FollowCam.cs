using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.12f;
    public float lookAtSpeed = .003f;
    public Vector3 offset;
    public Transform currentTarget;
    public Transform nextTarget;
  

    public float transitionProgress;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPostion = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPostion, smoothSpeed);
        transform.position = smoothedPosition;

        //transform.LookAt(target);

        transitionProgress += smoothSpeed;
        transitionProgress = Mathf.Clamp01(transitionProgress);
        Vector3 lookAtPosition = Vector3.Lerp(currentTarget.position, nextTarget.position, lookAtSpeed);
        transform.LookAt(lookAtPosition);
    }

    public void LookAtThisOne(Transform transform1)
    {
        transform.LookAt(transform1);
    }
}
