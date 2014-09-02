using UnityEngine;
using System.Collections;

public class CaveBehavior : MonoBehaviour {

    void Start()
    {
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
