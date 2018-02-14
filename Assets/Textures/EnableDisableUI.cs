using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableDisableUI : MonoBehaviour {
    
    [SerializeField]
    GameObject Exclaim;

    ShopkeeperAI shop;

    CustomerAI cust;
    [SerializeField]
    GameObject guy;


    void Start()
    {
        cust = GetComponent<CustomerAI>();     
        shop = guy.GetComponent<ShopkeeperAI>();
        Exclaim.SetActive(false);
    }

    void Update()
    {
        guy = cust.CurrentShopKeep;
        if (shop.GetCurrentCustomer() == guy)
        {
            Exclaim.SetActive(true);
        }
        else
        {
            Exclaim.SetActive(false);

        }

    }
     

}
