using UnityEngine;
using System.Collections;

public class CubeGridCollider : MonoBehaviour {

    private Color transparent = new Color(1, 1, 1, 0);
    private Color indicatorOn = new Color(0, 0, 1, 0.55f);
    private Color indicatorHit = new Color(0, 1, 0, 0.55f);
    private Color indicatorMissed = new Color(1, 0, 0, 0.55f);

	// Use this for initialization
	void Start () {

        this.renderer.material.color = transparent;
	}

    void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "Hand")
        {
            this.transform.renderer.material.color = indicatorHit;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        this.transform.renderer.material.color = transparent;
    }
}
