using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDeactivate : MonoBehaviour
{
    [SerializeField] Canvas canvasToDeactivate;
    CustomerAI cust;
    ShopkeeperAI keep;

	// Use this for initialization
	void Start ()
    {
        cust = GetComponent<CustomerAI>();
        if (cust == null) { Debug.LogError("cust not found", this); }
        
        canvasToDeactivate.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject temp = cust.GetCurrentShopkeep();

        if (temp != null)
        {
            keep = temp.GetComponent<ShopkeeperAI>();

            if (this.gameObject == keep.GetCurrentCustomer())
                canvasToDeactivate.enabled = true;
            else
                canvasToDeactivate.enabled = false;
        }
    }
}
