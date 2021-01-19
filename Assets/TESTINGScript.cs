using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTINGScript : MonoBehaviour
{
    PlayerInventory inventory;
    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            inventory.playerReplenishment.Replenish();
        }
    }
}
