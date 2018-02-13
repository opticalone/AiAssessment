using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    [SerializeField] GameObject[] allShopKeeps;
    [SerializeField] GameObject currentShop;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentShop = GetClosestShopKeep();
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
