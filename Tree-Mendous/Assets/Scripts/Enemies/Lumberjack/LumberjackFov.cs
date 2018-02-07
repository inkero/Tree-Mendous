using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackFov : MonoBehaviour {

    [SerializeField]
    private Lumberjack lumberjack;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public Collider2D[] targetsInViewRadius;
    public List<Transform> visibleTargets = new List<Transform>();

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    //[HideInInspector]
    //public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            
                yield return new WaitForSeconds(delay);
                FindVisibleTargets();
            
        }
    }

    void FindVisibleTargets()
    {
        if (!lumberjack.blinded)
        {
            visibleTargets.Clear();
            targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

            for(int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2 || Vector3.Angle(-transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if(!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        visibleTargets.Add(target);
                    }
                }
            }

            if (visibleTargets.Count > 0)
            {
                lumberjack.Target = visibleTargets[0].gameObject;
            }
            else
            {
                lumberjack.Target = null;
            }
        } else
        {
            lumberjack.Target = null;
        }
    }

    

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        //angleInDegrees += 90;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

}
