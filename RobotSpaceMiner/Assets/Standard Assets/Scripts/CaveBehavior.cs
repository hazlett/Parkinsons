using UnityEngine;
using System.Collections;

public class CaveBehavior : MonoBehaviour {

    //public GameObject cart;

   // private float roadBlockDistance;

    void Start()
    {
        //roadBlockDistance = 50;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "cart")
        {
            StateManager.Instance.Cave = true; 
            StateManager.Instance.Roadblocks = false;
            StateManager.Instance.FireHazards = true;
            Debug.Log("Roadblocks off");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "cart")
        {
            StateManager.Instance.Cave = false;
            StateManager.Instance.Roadblocks = true;
            StateManager.Instance.FireHazards = false;
        }
    }
}
