using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DebugScript : MonoBehaviour
{
    [SerializeField] GameObject[] allShopKeeps;
    [SerializeField] GameObject currentShop;

    private NavMeshAgent agent;

	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentShop = GetClosestShopKeep();

        if (currentShop.GetComponent<FollowOrGetStockShopKeeper>().GetShopkeepOccupied())
            agent.SetDestination(transform.position);
        else
            agent.SetDestination(currentShop.transform.position);
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
}
