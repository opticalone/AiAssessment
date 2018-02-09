﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class FollowOrGetStockShopKeeper : MonoBehaviour
{
    private NavMeshAgent agent;
    private ShopkeeperAI shopAI;
    private Animator anim;
    [SerializeField] float distToStock;
    [SerializeField] float maxCustomerDist;
    [SerializeField] bool givenStock = true;
    [SerializeField] Transform stockArea;
    [SerializeField] float debugGetStockCount = 0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        shopAI = GetComponent<ShopkeeperAI>();
        anim = GetComponent<Animator>();
        maxCustomerDist = 5f;
    }

    private void FixedUpdate()
    {
        distToStock = Vector3.Distance(stockArea.transform.position, transform.position);
    }

    public void Follow()
    {
        if (anim.GetBool("haveStock"))
            anim.SetBool("haveStock", false);

        shopAI.currentCustomer = shopAI.GetCurrentCustomer();

        if(shopAI.GetCustDist() < maxCustomerDist && anim.GetBool("isCustVisible") && givenStock)
        {
            agent.SetDestination(shopAI.currentCustomer.transform.position);
            agent.stoppingDistance = 3f;
        }

        if (shopAI.GetCustDist() > maxCustomerDist && anim.GetBool("isCustVisible"))
            anim.SetBool("isCustVisible", false);

        if (maxCustomerDist != 5f && !anim.GetBool("isCustVisible"))
            maxCustomerDist = 5f;
    }

    public void GetStock()
    {
        
        distToStock = Vector3.Distance(stockArea.transform.position, transform.position);
        anim.SetFloat("distToStock", distToStock);
        agent.SetDestination(stockArea.position);
        givenStock = false;
    }

    public void PickupStock()
    {
        debugGetStockCount++;
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
        shopAI.SetNextWanderPoint();
        
    }

    public void WalkToCustomer()
    {
        agent.SetDestination(shopAI.currentCustomer.transform.position);
        agent.stoppingDistance = 5f;

        maxCustomerDist = 4f;

        if (anim.GetBool("haveStock"))
        {           
            StartCoroutine("FollowDelay");
            
        }
    }

    IEnumerator FollowDelay()
    {
        float delay = 0f;

        shopAI.SetNextWanderPoint();
        //transform.LookAt(shopAI.GetCurrentWanderPoint());

        while (delay < 5f)
        {
            delay++;

            yield return new WaitForSeconds(1f);
        }
        givenStock = true;
        anim.SetBool("haveStock", false);
    }
}
