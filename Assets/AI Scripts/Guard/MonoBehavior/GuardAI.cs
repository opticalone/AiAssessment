using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    [SerializeField] Transform[] patPoints;
    [SerializeField] float distanceFromPatPoint;
    [SerializeField] int currentPatPoint;
    NavMeshAgent agent;
    Animator anim;

    // Debug Variables
    [SerializeField] GameObject[] allShopKeeps;
    [SerializeField] GameObject closestShopKeep;
    [SerializeField] GameObject currentShopkeep;
    private float currentDistance;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentPatPoint = (int)Random.Range(0, patPoints.Length);
        agent.SetDestination(patPoints[currentPatPoint].position);
    }

    private void FixedUpdate()
    {
        distanceFromPatPoint = Vector3.Distance(patPoints[currentPatPoint].position, transform.position);
        anim.SetFloat("distFromPatrolPoint", distanceFromPatPoint);

        closestShopKeep = GetClosestShopKeep();

        //currentDistance = Vector3.Distance(closestShopKeep.transform.position, transform.position);
    }

    public void SetNextPatrolPoint()
    {
        currentPatPoint = (int)Random.Range(0, patPoints.Length);
        currentShopkeep = GetCurrentShopkeep();
        agent.SetDestination(patPoints[currentPatPoint].position);
    }

    public GameObject GetClosestShopKeep()
    {
        GameObject closestShop = null;
        float dist = 100f;

        foreach(GameObject keep in allShopKeeps)
        {
            float currentDist = Vector3.Distance(keep.transform.position, transform.position);

            if(currentDist < dist)
            {
                dist = currentDist;
                closestShop = keep;
            }
        }

        return closestShop;
    }

    public GameObject GetCurrentShopkeep()
    {
        return closestShopKeep;
    }
}
