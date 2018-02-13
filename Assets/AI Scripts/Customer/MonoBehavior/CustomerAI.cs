using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour 
{

    //state machine variables
    [SerializeField] private GameObject[] shopKeeps;
    private GameObject closestShopKeep;
    public GameObject CurrentShopKeep;


    private Animator anim;
    private Ray ray;
    private RaycastHit hit;
    private float maxDistanceToCheck = 4f;
    [SerializeField] private float currentDistance;
    private Vector3 checkDirection;

    // Wander State Variables
    [SerializeField] private Transform[] wanderPoints;
    [SerializeField] private float distanceFromWanderPoint;
    [SerializeField] private int currentWanderPoint;
    [SerializeField] private NavMeshAgent agent;

    private void Awake()
    {
        anim = GetComponent<Animator>();        //............................//attaches animator
        agent = GetComponent<NavMeshAgent>();   //...........................//attatches navmesh agent to our gameObject

        currentWanderPoint = (int)Random.Range(0, wanderPoints.Length);  //..//sets current point to 0
        agent.SetDestination(wanderPoints[currentWanderPoint].position);//..//sets the destination to the curerent wanderpoint

    }

    public GameObject GetClosestShopKeep()
    {
        GameObject closestKeep = null;
        float dist = 100f;

        foreach(GameObject keep in shopKeeps)
        {
            float currentDist = Vector3.Distance(keep.transform.position, transform.position);

            if(currentDist < dist)
            {
                dist = currentDist;
                closestKeep = keep;

            }
        }

        return closestKeep;
    }

    public void SetNextWanderPoint()
    {
        currentWanderPoint = (int)Random.Range(0, wanderPoints.Length);

        agent.SetDestination(wanderPoints[currentWanderPoint].position);
    }

    private void FixedUpdate()
    {
        distanceFromWanderPoint = Vector3.Distance(wanderPoints[currentWanderPoint].position, transform.position);
        anim.SetFloat("distFromWanderPoint", distanceFromWanderPoint); 

    }




}
