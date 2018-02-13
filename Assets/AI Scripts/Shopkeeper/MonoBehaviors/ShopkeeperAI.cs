﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShopkeeperAI : MonoBehaviour
{
    // General Statemachine Variables
    [SerializeField] private GameObject[] customers;
    private GameObject closestCustomer;
    public GameObject currentCustomer;
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
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentWanderPoint = 0;
        agent.SetDestination(wanderPoints[currentWanderPoint].position);
    }

    private void FixedUpdate()
    {
        // Get Closest Customer
        closestCustomer = GetClosestCustomer();
        currentDistance = Vector3.Distance(closestCustomer.transform.position, transform.position);
        anim.SetFloat("distFromCust", currentDistance);

        // Check where the closest customer is
        checkDirection = closestCustomer.transform.position - transform.position;
        ray = new Ray(transform.position, checkDirection);

        if (Physics.Raycast(ray, out hit, maxDistanceToCheck))
        {
            if (hit.collider.gameObject == closestCustomer)
                anim.SetBool("isCustVisible", true);
            else
                anim.SetBool("isCustVisible", false);
        }
        else
            anim.SetBool("isCustVisible", false);

        // Get the distance to the next wander point
        distanceFromWanderPoint = Vector3.Distance(wanderPoints[currentWanderPoint].position, transform.position);
        anim.SetFloat("distanceFromWanderPoint", distanceFromWanderPoint);
    }

    public GameObject GetClosestCustomer()
    {
        GameObject closestCust = null;
        float dist = 100f;

        foreach(GameObject cust in customers)
        {
            float currentDist = Vector3.Distance(cust.transform.position, transform.position);

            if(currentDist < dist)
            {
                dist = currentDist;
                closestCust = cust;

            }
        }

        return closestCust;
    }

    public void SetNextWanderPoint()
    {
        currentWanderPoint = (int)Random.Range(0, wanderPoints.Length);

        agent.SetDestination(wanderPoints[currentWanderPoint].position);
    }

    public Transform GetCurrentWanderPoint()
    {
        return wanderPoints[currentWanderPoint];
    }

    public GameObject GetCurrentCustomer()
    {
        return closestCustomer;
    }

    public float GetCustDist()
    {
        return currentDistance;
    }
}