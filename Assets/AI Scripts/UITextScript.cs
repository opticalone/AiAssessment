using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextScript : MonoBehaviour {

    [SerializeField] GameObject Arissa;

    private ShopkeeperAI Ari;

    [SerializeField] 
    Text cust;
	void Start()
    {
        Ari = Arissa.GetComponent<ShopkeeperAI>();

    }
	// Update is called once per frame
	void Update ()

    {
     cust.text = "Arissa's Customer: " + Ari.currentCustomer.name.ToString();

	}
}
