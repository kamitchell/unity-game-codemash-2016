//This script allows the player to tap on the ground and get the runner to move to the chosen spot

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public GameObject navMarkerPrefab;	//Waypoint marker model

	GameObject navMarker;				//Reference to an instantiated version of the waypoint marker model
	NavMeshAgent navAgent;				//Refernece to nav mesh agent component
	Animator anim;						//Reference to animator component


	void Start()
	{
		//Get our references
		navAgent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();		//We will uncomment this during the workshop

		//Instantiate a waypoint marker
		navMarker = (GameObject)Instantiate(navMarkerPrefab);
		navMarker.SetActive(false);
	}

	void Update()
	{
		CheckForInput();

		if(navAgent.hasPath && anim != null)
			UpdateAnimation();
	}

	void CheckForInput()
	{
		//"Fire1" is left mouse click, left control key, or screen tap
		if (Input.GetButtonDown("Fire1"))
		{
			//Generate a world space (x, y, z) "Ray" from the camera based on where we clicked in screen space (x,y)
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit rayHit;

			//Does our Ray hit anything?
			if (Physics.Raycast(ray, out rayHit, 200f))
			{
				//If we hit the "floor" move the waypoint marker there and set it as the runner's destination
				if (rayHit.transform.tag == "Floor")
				{
					navMarker.transform.position = rayHit.point;
					navMarker.SetActive(true);

					navAgent.SetDestination(rayHit.point);
				}
			}
		}
	}

	void UpdateAnimation()
	{
		//If we haven't reached the destination calculated the vector values to pass into the animator
		if (navAgent.remainingDistance > navAgent.stoppingDistance)
		{
			Vector3 move = navAgent.desiredVelocity;

			if (move.magnitude > 1f)
				move.Normalize();

			//Convert our normalized vector from world space to local space (not the world's "forward", but "my" "forward")
			move = transform.InverseTransformDirection(move);

			float m_TurnAmount = Mathf.Atan2(move.x, move.z);
			float m_ForwardAmount = move.z;

			anim.SetFloat("Speed", Mathf.Abs(m_ForwardAmount));
			//anim.SetFloat("Turn", m_TurnAmount);	//This MAY be commented our during the workshop if there is time to set up an animation blend for turning

			//Debug.Log("Player Running...");	//Not necessarily to be uncommented, I just wanted you to see how to output to the console window
		}
		else
		{
			//When we reach our destination, hide the waypoint marker
			navMarker.SetActive(false);

			anim.SetFloat("Speed", 0f);
			//anim.SetFloat("Turn", 0f);	//This MAY be commented our during the workshop if there is time to set up an animation blend for turning

			//Debug.Log("Player Stopping...");	//Not necessarily to be uncommented, I just wanted you to see how to output to the console window
		}
    }
}
