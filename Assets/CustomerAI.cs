using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour {

    private GameObject merchant;
    private Animator animator;
    private Ray ray;
    private RaycastHit hit;
    private float maxDistanceToCheck = 6.0f;
    private float currentDistance;
    private Vector3 checkDirection;

    // Wait/Patrol state variables
    public Transform shopA;
    public Transform shopB;
    public Transform shopC;
    public NavMeshAgent navMeshAgent;
    private int currentTarget;
    private float distanceFromTarget;
    private Transform[] waypoints = null;


    private void Awake()
    {
        merchant = GameObject.FindWithTag("Merchant");
        animator = gameObject.GetComponent<Animator>();
        shopA = GameObject.Find("p1").transform;
        shopB = GameObject.Find("p2").transform;
        shopC = GameObject.Find("p3").transform;
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        waypoints = new Transform[3]
        {
            shopA,
            shopB,
            shopC
        };

        currentTarget = 0;
        navMeshAgent.SetDestination(waypoints[currentTarget].position);


    }

    private void FixedUpdate()
    {
        //dist from customer

        currentDistance = Vector3.Distance(merchant.transform.position, transform.position);
        animator.SetFloat("distanceFromPlayer", currentDistance);

        //check visible
        checkDirection = merchant.transform.position - transform.position;
        ray = new Ray(transform.position, checkDirection);
        if (Physics.Raycast(ray, out hit, maxDistanceToCheck))
        {
            if (hit.collider.gameObject == merchant)
            {
                animator.SetBool("isMerchantVisible", true);
            }
            else
            {
                animator.SetBool("isMerchantVisible", false);
            }
        }
        else
        {
            animator.SetBool("isMerchantVisible", false);
        }

        //get distance to next waypoint

        distanceFromTarget = Vector3.Distance(waypoints[currentTarget].position, transform.position);
        animator.SetFloat("distanceFromWaypoint", distanceFromTarget);
    }

    public void setNextPoint()
    {
        switch (currentTarget)
        {
            case 0:
                currentTarget = 1;
                break;
            case 1:
                currentTarget = 2;
                break;
            case 2:
                currentTarget = 0;
                break;

            

        }
        navMeshAgent.SetDestination(waypoints[currentTarget].position);
    }
}
