using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderOrShop : MonoBehaviour
{

    private NavMeshAgent agent;
    private CustomerAI custAI;
    private Animator anim;

    FollowOrGetStockShopKeeper GettingStock;


    [SerializeField] GameObject[] ShopKeeps;
    [SerializeField] GameObject currentShopKeep;
    [SerializeField] float timeToAnger;
    [SerializeField] float maxShopKeepDist;

    private float maxWaitTime;
    private float waitTimer;

    [SerializeField ]bool recievedStock = false;
    [SerializeField] bool talkingToShopKeep = false;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        custAI = GetComponent<CustomerAI>();
        anim = GetComponent<Animator>();
    }

    IEnumerator CustomerWaitTimer()
    {
        float waitTimer = 0f;
        float maxWaitTime = Random.Range(10f, 30f);
        for (; waitTimer < maxWaitTime + 1; waitTimer++)
        {
            yield return new WaitForSeconds(1f);
        }


        if (waitTimer > maxWaitTime)
            StealStock();

        anim.SetBool("haveStock", true);
        custAI.SetNextWanderPoint();

    }

    public void walkToShopKeep()
    {
        float dist = Vector3.Distance(GetCurrentShopKeep().transform.position, transform.position);
        anim.SetFloat("distFromShopkeep", dist);
        

        if (anim.GetBool("shopkeepOccupied") )
        {
            anim.SetBool("ShopkeepGettingStock", true);
            
        }
        else
        {
            anim.SetBool("ShopkeepGettingStock", false);
 
        }

        if(dist > 3)
            agent.SetDestination(currentShopKeep.transform.position);
        else
            agent.SetDestination(transform.position);


    }

    public void waitForGoods()
    {
        StartCoroutine("CustomerWaitTimer");
    }

    public void StealStock()
    {


    }
   
    public GameObject GetCurrentShopKeep()
    {
        currentShopKeep = custAI.GetClosestShopKeep();
        return currentShopKeep;
    }
    public bool updateBuy()
    {
        int updateBuyState = (int)Random.Range(0, 2);

        bool buy = false;

        switch(updateBuyState)
        {
            case 0:
                buy = false;
                break;
            case 1:
                buy = true;
                break;
            default:
                Debug.Log("something went wrong");
                break;
        }
        return buy;

    }
}


































































//    private void WantToBuy()
//    {
//        bool wantToBuy;
//        wantToBuy = false;


//        int buyState = (int)Random.Range(0, 2);

//        switch(buyState)
//        {
//            case 0:
//                wantToBuy = false;
//                anim.SetBool("WantToBuy", false);
//                break;
//            case 1:
//                wantToBuy = true;
//                anim.SetBool("WantToBuy", true);
//                break;
//            default:
//                Debug.Log("Something went wrong!");
//                break;
//        }
//    }

//    public IEnumerator CheckBuyStatus()
//    {
//        float counter = 0f;

//        while (counter < 4)
//        {
//            counter++;
//            yield return new WaitForSeconds(1);
//        }

//        WantToBuy();
//    }

//    public void RunBuyCheck()
//    {
//        StartCoroutine("CheckBuyStatus");
//    }
//}
