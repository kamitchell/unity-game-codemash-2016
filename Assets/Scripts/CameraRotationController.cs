//This script rotates a game object either abruplty or smoothly

using UnityEngine;

public class CameraRotationController : MonoBehaviour
{
	[SerializeField] float rotationIncrement = 90f;	//Angle of rotation, 90 degrees
	[SerializeField] float rotationFudge = .1f;		//How close do we need to be to our target rotation before we "teleport" to it
	[SerializeField] float speed = 5f;				//How fast do we rotate?

	Quaternion currentRotation;						//Our current rotation
	Quaternion desiredRotation;						//Our desired rotation
	bool isRotating = false;						//Are we currently rotating?

	void Start()
	{
		//Our "desired" rotation starts as the rotation we already have
		desiredRotation = transform.rotation;
	}

	public void RotateCW()
	{
		transform.Rotate(0f, -rotationIncrement, 0f);	//Instant rotation, will be commented out
		//StartRotation(-rotationIncrement);			//Smooth rotation, will be uncommented
    }

	public void RotateCCW()
	{
		transform.Rotate(0f, rotationIncrement, 0f);	//Instant rotation, will be commented out
		//StartRotation(rotationIncrement);				//Smooth rotation, will be uncommented
    }

	void StartRotation(float angle)
	{
		Vector3 eulerRotation = desiredRotation.eulerAngles;	//Get euler (x, y, z) version of our current desired rotation
		eulerRotation.y += angle;								//Add angle to desired rotation
		desiredRotation = Quaternion.Euler(eulerRotation);		//Convert back into a quaternion (x, y, z, w) rotation

		isRotating = true;										//Signify we want to begin rotating
	}

	void Update()
	{
		if (!isRotating)
			return;

		//Get our current rotation
		currentRotation = transform.rotation;
		//"Linearly Interpolate" from our current angle to our desired angle over time
		currentRotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * speed);

		//If we are within "fudge" distance, snap to desired angle
		if (Quaternion.Angle(currentRotation, desiredRotation) <= rotationFudge)
		{
			currentRotation = desiredRotation;
			isRotating = false;
		}

		//Set calculated position back to object
		transform.rotation = currentRotation;
	}
}
