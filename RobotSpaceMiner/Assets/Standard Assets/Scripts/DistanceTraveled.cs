using UnityEngine;
using System.Collections;

public class DistanceTraveled : MonoBehaviour {

    private float initialDistance, currentDistance, maxDistance;

	void Start () {
        initialDistance = currentDistance = 0;
	}

    void Update() {
        currentDistance = this.transform.position.x;
    }

    public int Distance() {
        if (maxDistance < (currentDistance - initialDistance))
        {
            maxDistance = currentDistance - initialDistance;
        }
        return (int)maxDistance;
    }
}
