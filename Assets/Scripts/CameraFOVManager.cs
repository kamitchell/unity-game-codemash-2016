//This script changes the FOV (zoom in / out) of the camera based off of the aspect ratio of the screen

using UnityEngine;

public class CameraFOVManager : MonoBehaviour
{
	const float ASPECT4x3 = 1.33f;		//4:3 ratio
	const float ASPECT16x10 = 1.59f;	//16:10 ratio
	const float ASPECT16x9 = 1.77f;		//16:9 ratio

	//These may need to be changed based on your maze size and camera position
	//Change them in editor, changing here won't do anything
	[SerializeField] float FOV4x3 = 70f;
	[SerializeField] float FOV16x10 = 60f;
	[SerializeField] float FOV16x9 = 55f;

	Camera localCamera;	//reference to local camera component

	void Start ()
	{
		//Get reference to local camera and then get its aspect
		localCamera = GetComponent<Camera>();
		float aspect = localCamera.aspect;

		//Based on aspect, set FOV of camera
		if (aspect >= ASPECT16x9)
			localCamera.fieldOfView = FOV16x9;
		else if (aspect >= ASPECT16x10)
			localCamera.fieldOfView = FOV16x10;
		else if (aspect >= ASPECT4x3)
			localCamera.fieldOfView = FOV4x3;

	}
}
