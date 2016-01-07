//This script makes the turret follow the runner and handles being "shut down"

using UnityEngine;
using System.Collections;

public class TurretMovement : MonoBehaviour
{
	[SerializeField] Transform target;					//The runner's transform
	[SerializeField] Transform turretRotator;			//The transform of the turret that handles turning the gun
	[SerializeField] Transform turretArm;				//The transform of the turret that handles looking up and down
	[SerializeField] float turretRotationSpeed = 5f;	//How fast the turret rotates
	[SerializeField] float poweredDownAngle = 25f;		//How far down does the turret look when unpowered

	AudioSource audioSource;							//Reference to audio source component
	bool isPowered = true;								//Is the turret currently powered?
	TurretShooting turretShooting;						//Reference to the TurretShooting script on the barrel of the gun

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		turretShooting = GetComponentInChildren<TurretShooting> ();
	}

	void Update()
	{
		if (!isPowered)
			return;

		//Get the vector between the turret and the runner, calculate the rotation needed to "look at" it, and "Linerally Interpolate" from the current rotation to the desired one
		Vector3 relativePos = target.position - turretRotator.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		turretRotator.rotation = Quaternion.Lerp(turretRotator.rotation, rotation, Time.deltaTime * turretRotationSpeed);
	}

	//Public method called by the TurretPowerMarker
	public void TurnOff()
	{
		if (!isPowered)
			return;

		//Flag the turret as "off", disable the shooting capability, and begin rotating the gun downward
		isPowered = false;
		turretShooting.enabled = false;
		StartCoroutine(PowerDown());
	}

	IEnumerator PowerDown()
	{
		audioSource.Play();

		//Rotate the turret arm downward (over time) until the desired angle is reached (making it look like it is powering down)
		while (turretArm.localEulerAngles.x < poweredDownAngle)
		{
			turretArm.Rotate(Time.deltaTime * turretRotationSpeed * 4f, 0f, 0f);

			yield return null;
		}
	}
}
