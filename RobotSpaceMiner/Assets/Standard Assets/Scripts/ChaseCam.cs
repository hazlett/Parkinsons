using UnityEngine;
using System.Collections;

public class ChaseCam : MonoBehaviour {

    public GameObject mineCart;

    private float xOffset = -10, yOffset = 9;
	
	// Update is called once per frame
	void Update () {

        this.transform.position = new Vector3(mineCart.transform.position.x + xOffset, mineCart.transform.position.y + yOffset, 0);
	}
}
