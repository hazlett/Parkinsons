using UnityEngine;
using System.Collections;

public class CubeGridCollider : MonoBehaviour {

    public GameObject fireText;

    private Color transparent = new Color(1, 1, 1, 0);
    internal int onCount;
	// Use this for initialization
	void Start () {

        this.renderer.material.color = transparent;
	}

    // If the cube collides with the hand...
    void OnTriggerEnter(Collider collider)
    {
		if (collider.tag == "Hand")
		{
			CheckCollision();
		}
    }

    // Turn off the cube indicator...
	private void CheckCollision()
	{
		if (fireText.renderer.enabled)
		{
			Deactivate();
		}
	}
	internal void DebugCheckCollision()
	{
		CheckCollision ();
	}

    // Turn cube indicator on
    internal void Activate()
    {
        onCount++;
        fireText.renderer.enabled = true;
    }

    // And shoot an iceball out of the cube
    internal void Deactivate()
    {
        onCount--;
        if (onCount == 0)
        {
            fireText.renderer.enabled = false;
        }
        GameObject iceball;
        iceball = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/Iceball"));
        iceball.transform.position = this.transform.position;
    }

    internal void DeactivateHit()
    {
        fireText.renderer.enabled = false;
        onCount = 0;
    }
}
