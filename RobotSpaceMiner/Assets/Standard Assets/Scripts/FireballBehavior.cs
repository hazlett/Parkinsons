using UnityEngine;
using System.Collections;

public class FireballBehavior : MonoBehaviour {

    private float explosionDistance = 2.5f;
    private int cubeNumber;
    public int CubeNumber { set {cubeNumber = value;} }
    private GameObject cart;
    private CubeGridCollider topLeft, topCenter, topRight, middleLeft, middleCenter, middleRight;
	// Use this for initialization
	void Start () {

        cart = GameObject.Find("Cart");

        topLeft = GameObject.Find("TopLeft").GetComponent<CubeGridCollider>();
        topCenter = GameObject.Find("TopCenter").GetComponent<CubeGridCollider>();
        topRight = GameObject.Find("TopRight").GetComponent<CubeGridCollider>();
        middleLeft = GameObject.Find("MiddleLeft").GetComponent<CubeGridCollider>();
        middleCenter = GameObject.Find("MiddleCenter").GetComponent<CubeGridCollider>();
        middleRight = GameObject.Find("MiddleRight").GetComponent<CubeGridCollider>();
	}
	
	// Update is called once per frame
	void Update () {

        if (cart.transform.rotation.z > 0.001f || cart.transform.rotation.z < -0.001f)
        {
            DeactivateCube();
            GameObject.Destroy(this.gameObject);
        }
        if (this.transform.position.x - cart.transform.position.x < explosionDistance)
        {
            GameObject explosion = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Explosion"));
            explosion.transform.position = this.transform.position;

            cart.rigidbody.AddForce(new Vector3(-25000, 0, 0));

            DeactivateCube();

            GameObject.Destroy(this.gameObject);
        }
	}

    void DeactivateCube()
    {
        switch (cubeNumber)
        {
            case 0: topLeft.DeactivateHit();
                break;
            case 1: topCenter.DeactivateHit();
                break;
            case 2: topRight.DeactivateHit();
                break;
            case 3: middleLeft.DeactivateHit();
                break;
            case 4: middleCenter.DeactivateHit();
                break;
            case 5: middleRight.DeactivateHit();
                break;
        }
    }
}
