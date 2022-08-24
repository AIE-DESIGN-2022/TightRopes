using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius; 
    [Range(0f, 360f)] public float angle;

    public GameObject cam;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeeObj;

    public GameObject ObjectA;
    public GameObject ObjectC;
    public Transform target;
     // Start is called before the first frame update
    void Start()
    {
        cam = this.gameObject;
        StartCoroutine(FOVRoutine());
    }
    IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
        yield return wait;
            FieldOfViewCheck();

        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length != 0)
        {
            target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle/2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeeObj = true;
                    ObjectA = rangeChecks[0].gameObject;

                }
                else
                    canSeeObj = false;
            }
            else
                canSeeObj = false;
        }
        else if (canSeeObj)
            canSeeObj = false;
            ObjectA.SetActive(canSeeObj);
    }

    private void Deactivate()
    {
        while(canSeeObj && ObjectA != null)
        {
            
        }
    }
    }

