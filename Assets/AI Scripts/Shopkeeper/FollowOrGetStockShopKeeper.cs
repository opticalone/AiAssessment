using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class FollowOrGetStockShopKeeper : MonoBehaviour
{
    private NavMeshAgent agent;
    private ShopkeeperAI shopAI;
    private Animator anim;
    [SerializeField] float distToStock;
    [SerializeField] Transform stockArea;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        shopAI = GetComponent<ShopkeeperAI>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        distToStock = Vector3.Distance(stockArea.transform.position, transform.position);
    }

    public void Follow()
    { 
        if(shopAI.GetCustDist() < 5f && anim.GetBool("isCustVisible"))
        {
            agent.SetDestination(shopAI.GetCurrentCustomer().transform.position);
            agent.stoppingDistance = 3f;
        }
    }

    public void GetStock()
    {
        distToStock = Vector3.Distance(stockArea.transform.position, transform.position);
        anim.SetFloat("distToStock", distToStock);
        agent.SetDestination(stockArea.position);
    }

    public void PickupStock()
    {
        StartCoroutine("PickupStockTimer");
    }

    IEnumerator PickupStockTimer()
    {
        float pickupTimer = 0f;
        float maxTime = Random.Range(10f, 30f);
        while (pickupTimer < maxTime)
        {
            pickupTimer++;

            yield return new WaitForSeconds(1f);
        }
        
        anim.SetBool("haveStock", true);
        
    }

    public void WalkToCustomer()
    {
        agent.SetDestination(shopAI.GetCurrentCustomer().transform.position);
        agent.stoppingDistance = 5f;

        if(anim.GetBool("haveStock") && shopAI.GetCustDist() < 2f)
        {
            anim.SetBool("haveStock", false);
        }
    }
}
