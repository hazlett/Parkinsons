using UnityEngine;
using System.Collections;

public class AutoRunState : MonoBehaviour {
	
	public GameObject cubeGrid;
	private CubeGridCollider[] cubes;
	private BasicMovement cart;
	private float timer;
	void Start() {
		timer = 0;
		cubes = cubeGrid.GetComponentsInChildren<CubeGridCollider> ();
	}

	void Update () {
		timer += Time.deltaTime;
		if (!cart.downhill.enabled)
		{
			if (timer > 1)
			{
				cart.Move();
				timer = 0;
			}
		}
		else {
			if (timer > 2.5f)
			{
				FireballCheck ();
				timer = 0;
			}
		}
	}

	private void FireballCheck()
	{
		foreach (CubeGridCollider cube in cubes)
		{
            cube.Deactivate();
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (!this.enabled)
			return;
		switch (collider.tag){
		case "roadblock":
			if (cart.currentTrack == BasicMovement.trackNumber.LEFT)
				cart.TriggerHopRight();
			else if (cart.currentTrack == BasicMovement.trackNumber.RIGHT)
				cart.TriggerHopLeft();
			else {
				int random = Random.Range(0,2);
				if (random == 1)
					cart.TriggerHopLeft();
				else
					cart.TriggerHopRight();
			}
			break;

		}
	}
	public void Initialize(BasicMovement cart)
	{
		this.cart = cart;
	}
}