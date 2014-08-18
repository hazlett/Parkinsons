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
            Deactivate();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Hand")
        {
            this.transform.renderer.material.color = indicatorMissed;
        }
    }

    internal void Activate()
    {
        this.renderer.material.color = indicatorOn;
    }

    void Deactivate()
    {
        this.renderer.material.color = transparent;
    }
}
