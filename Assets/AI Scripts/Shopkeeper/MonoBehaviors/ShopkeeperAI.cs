using System.Collections;
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
    private float maxDistanceToCheck = 6f;
    [SerializeField] private float currentDistance;
    private Vector3 checkDirection;
    [SerializeField] GameObject eyeImage;

    private FollowOrGetStockShopKeeper shop;


    // Wander State Variables
    [SerializeField] private Transform[] wanderPoints;
    [SerializeField] private float distanceFromWanderPoint;
    [SerializeField] private int currentWanderPoint;
    [SerializeField] private NavMeshAgent agent;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        shop = GetComponent<FollowOrGetStockShopKeeper>();
        currentWanderPoint = 0;
        agent.SetDestination(wanderPoints[currentWanderPoint].position);
        eyeImage.SetActive(false);
        
    }

    private void Update()
    {
        if(currentCustomer != null)
        {
            if(this.gameObject == 
                currentCustomer.gameObject.GetComponent<CustomerAI>().GetCurrentShopkeep())
            {
                eyeImage.SetActive(true);
            }
            else
            {
                if (eyeImage.activeSelf)
                    eyeImage.SetActive(false);
            }
        }
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
           // if (hit.collider.gameObject == closestCustomer)
            if(hit.collider.tag == "customer")
        
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
        shop.SetIsOccupied(false);
        
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
