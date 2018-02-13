using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAI : MonoBehaviour
{
    [SerializeField] Transform[] wanderPoints;
    [SerializeField] float distanceFromWanderPoint;
    [SerializeField] int currentWanderPoint;
    Vector3 checkDirection;
    Ray ray;
    RaycastHit hit;
    float maxDistanceToCheck = 6f;
    NavMeshAgent agent;
    Animator anim;

    // Debug Variables
    [SerializeField] GameObject[] allShopKeeps;
    [SerializeField] GameObject closestShopKeep;
    [SerializeField] GameObject currentShopkeep;
    [SerializeField] private float currentDistance;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentWanderPoint = (int)Random.Range(0, wanderPoints.Length);
        agent.SetDestination(wanderPoints[currentWanderPoint].position);
    }

    private void FixedUpdate()
    {
        // Get The Closest Shopkeep
        closestShopKeep = GetClosestShopKeep();
        currentDistance = Vector3.Distance(closestShopKeep.transform.position, transform.position);
        anim.SetFloat("distFromKeep", currentDistance);

        // Check where the closest keep is
        checkDirection = closestShopKeep.transform.position - transform.position;
        ray = new Ray(transform.position, checkDirection);

        if (Physics.Raycast(ray, out hit, maxDistanceToCheck))
        {
            if (hit.collider.gameObject == closestShopKeep)
                anim.SetBool("isKeepVisible", true);
            else
                anim.SetBool("isKeepVisible", false);
        }
        else
            anim.SetBool("isKeepVisible", false);

        // Get the distance to the next wander point
        distanceFromWanderPoint = Vector3.Distance(wanderPoints[currentWanderPoint].position, transform.position);
        anim.SetFloat("distanceFromWanderPoint", distanceFromWanderPoint);
    }

    public void SetNextWanderPoint()
    {
        currentWanderPoint = (int)Random.Range(0, wanderPoints.Length);
        currentShopkeep = GetCurrentShopkeep();

        if (anim.GetBool("receivedStock"))
            anim.SetBool("receivedStock", false);

        agent.SetDestination(wanderPoints[currentWanderPoint].position);
    }

    public GameObject GetClosestShopKeep()
    {
        GameObject closestShop = null;
        float dist = 100f;

        foreach (GameObject keep in allShopKeeps)
        {
            float currentDist = Vector3.Distance(keep.transform.position, transform.position);

            if (currentDist < dist)
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

    public bool GetCurrentKeepOccupied()
    {
        return GetCurrentShopkeep().GetComponent<FollowOrGetStockShopKeeper>().GetShopkeepOccupied();
    }

    public void Wait()
    {
        if(GetCurrentShopkeep().GetComponent<ShopkeeperAI>().GetCurrentCustomer() == this.gameObject && GetCurrentKeepOccupied())
        {
            agent.SetDestination(transform.position);
        }
    }

    public bool KeepGettingStock()
    {
        return GetCurrentShopkeep().GetComponent<FollowOrGetStockShopKeeper>().GettingStockState();
    }
}
