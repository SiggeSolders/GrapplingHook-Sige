using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement pm;
    public Transform cam;
    public Transform guntip;
    public LayerMask WhatIsGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    public float MaxGrappleDistance;
    public float GrappleDelayTime;
    public float overshootYAxis;

    private Vector3 GrapplePoint;

    [Header("CoolDown")]
    public float GrapplingCd;
    private float GrapplingCdTimer;

    [Header("Input")]
    public KeyCode GrappleKey = KeyCode.Mouse1;

    private bool IsGrappling;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(GrappleKey)) StartGrapple();

        if(GrapplingCdTimer > 0)
        {
            GrapplingCdTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (IsGrappling)
        {
            lr.SetPosition(0, guntip.position);
        }
    }
    private void StartGrapple()
    {
        if (GrapplingCdTimer > 0) return;

        IsGrappling = true;

        pm.freeze = true;

        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, MaxGrappleDistance, WhatIsGrappleable))
        {
            GrapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), GrappleDelayTime);
        }
        else
        {
            GrapplePoint = cam.position + cam.forward * MaxGrappleDistance;

            Invoke(nameof(StopGrapple), GrappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, GrapplePoint);
    }

    private void ExecuteGrapple()
    {
        pm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = GrapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;
        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        pm.JumpToPosition(GrapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        pm.freeze = false;
        
        IsGrappling = false;

        GrapplingCdTimer = GrapplingCd;

        lr.enabled = false;
    }
}
