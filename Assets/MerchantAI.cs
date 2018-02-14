using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MerchantAI : MonoBehaviour {


    private GameObject customer;
    private Animator animator;
    private Ray ray;
    private RaycastHit hit;
    private float maxDistanceToCheck = 6.0f;
    private float currentDistance;
    private Vector3 checkDirection;

    // Wait/Patrol state variables
    public Transform pointA;
    public Transform pointB;
    public NavMeshAgent navMeshAgent;
    private int currentTarget;
    private float distanceFromTarget;
    private Transform[] waypoints = null;


    private void Awake()
    {
        customer = GameObject.FindWithTag("Customer");
        animator = gameObject.GetComponent<Animator>();
        pointA = GameObject.Find("p1").transform;
        pointB = GameObject.Find("p2").transform;
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        waypoints = new Transform[2] 
        {
            pointA,
            pointB
        };

        currentTarget = 0;
        navMeshAgent.SetDestination(waypoints[currentTarget].position);

   
    }
    
    private void FixedUpdate()
    {
        //dist from customer

        currentDistance = Vector3.Distance(customer.transform.position, transform.position);
        animator.SetFloat("distanceFromCustomer", currentDistance);

        //check visible
        checkDirection = customer.transform.position - transform.position;
        ray = new Ray(transform.position, checkDirection);
        if (Physics.Raycast(ray, out hit, maxDistanceToCheck))
        {
            if(hit.collider.gameObject == customer)
            {
                animator.SetBool("isCustomerVisible", true);
            }
            else
            {
                animator.SetBool("isCustomerVisible", false);
            }
        }
        else
        {
            animator.SetBool("isCustomerVisible", false);
        }

        //get distance to next waypoint

        distanceFromTarget = Vector3.Distance(waypoints[currentTarget].position, transform.position);
        animator.SetFloat("distanceFromWaypoint", distanceFromTarget);   
    }
    public void setNextPoint()
    {
        switch(currentTarget)
        {
            case 0:
                currentTarget = 1;
                break;
            case 1:
                currentTarget = 0;
                break;

        }
        navMeshAgent.SetDestination(waypoints[currentTarget].position);
    }
}
