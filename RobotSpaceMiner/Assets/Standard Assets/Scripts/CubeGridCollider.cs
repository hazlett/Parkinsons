using UnityEngine;
using System.Collections;

public class CubeGridCollider : MonoBehaviour {

    private Color transparent = new Color(1, 1, 1, 0);
    private Color indicatorOn = new Color(0, 0, 1, 0.4f);
    private Color indicatorHit = new Color(0, 1, 0, 0.4f);
    private Color indicatorMissed = new Color(1, 0, 0, 0.4f);

	// Use this for initialization
	void Start () {

        this.renderer.material.color = transparent;
	}

    void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "Hand")
        {
            if (this.renderer.material.color == indicatorOn)
            {
                Deactivate();
            }
        }
    }

    internal void Activate()
    {
        this.renderer.material.color = indicatorOn;
    }

    void Deactivate()
    {
        this.renderer.material.color = transparent;

        GameObject attackFireball;
        attackFireball = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Iceball"));
        attackFireball.transform.position = this.transform.position;
    }

    internal void DeactivateHit()
    {
        this.renderer.material.color = transparent;
    }
}
