using UnityEngine;
using System.Collections;

public class BarrierCollision : MonoBehaviour {

    public Rigidbody mineCart;
    public GUIScript screenFlash;

    private bool hit;

    void Start() {
        hit = false;
    }

    void OnTriggerEnter(Collider robot)
    {
        mineCart.velocity = new Vector3(0, mineCart.velocity.y, mineCart.velocity.z);
        mineCart.AddForce(new Vector3(-1000, 0, 0));
        hit = true;

        screenFlash.DamageFlash();
    }

    void Update()
    {
        changeColor(hit);
    }

    void changeColor(bool collided) {
        if(collided) {
            this.transform.renderer.material.color = new Color(1, 0, 0);
        }
        else {
            this.transform.renderer.material.color = new Color(0, 1, 0);
        }
     }
}
