//This script shoots at the player

using UnityEngine;
using System.Collections;

public class TurretShooting : MonoBehaviour
{
	[SerializeField] Transform target;				//The transform of the runner
	[SerializeField] GameObject turretFireEffect;	//The particle effect object on the barrel of the gun
	[SerializeField] float fireTime = 2f;			//How long should the shooting effect run
	[SerializeField] float fireSpeed = -1000f;		//How fast should the barrel spin?


	void Update()
	{
		//Cast a ray trying to hit the player
		RaycastHit rayHit;
		if (Physics.Raycast(transform.position, transform.forward, out rayHit, 50f))
		{
			//Did we hit the player? (Comparing transforms is "lighter" than string comparisons with tags)
			if (rayHit.transform == target)
			{
				//Get the player state, and if the player is valid, kill them!
				PlayerState player = rayHit.transform.GetComponent<PlayerState>();
				if (player.isValidTarget)
				{
					player.Died();
					StartCoroutine(Fire());
				}
			}
		}
	}

	//Firing effect
	IEnumerator Fire()
	{
		//Turn effects on and then off at end of this method
		turretFireEffect.SetActive(true);

		float time = 0f;

		//While within the time range, rotate barrel and accumulate time
		while (time < fireTime)
		{
			transform.Rotate(0f, 0f, fireSpeed * Time.deltaTime);
			time += Time.deltaTime;
			yield return null;
		}

		turretFireEffect.SetActive(false);
	}
}
