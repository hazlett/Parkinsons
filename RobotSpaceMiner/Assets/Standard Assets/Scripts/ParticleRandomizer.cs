using UnityEngine;
using System.Collections;

public class ParticleRandomizer : MonoBehaviour {

    private ParticleSystem popUp;
    private bool newLocation = false;
    private float newX, newY, newZ, startX, startY, startZ;
    private int moveCount;

	// Use this for initialization
	void Start () {

        popUp = this.particleSystem;

        moveCount = 0;

        startX = this.popUp.transform.localPosition.x;
        startY = this.popUp.transform.localPosition.y;
        startZ = this.popUp.transform.localPosition.z;
    }
	
	// Update is called once per frame
	void Update () {

        if (popUp.isPlaying)
        {
            newLocation = false;
            moveCount = 0;
        }
        if (popUp.isStopped)
        {
            newLocation = true;
        }

        MovePopup();
	}

    void MovePopup()
    {
        newX = Random.Range(-0.5f, 0.5f);
        newY = Random.Range(-0.5f, 0.5f);
        newZ = Random.Range(-0.5f, 0.5f);

        if (newLocation && moveCount == 0)
        {
            this.popUp.transform.localPosition = new Vector3(startX + newX, startY + newY, startZ + newZ);
            moveCount++;
        }
    }
}
