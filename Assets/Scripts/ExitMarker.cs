//This script determines of the runner has landed on the exit marker and tells the player if they have

using UnityEngine;

public class ExitMarker : MonoBehaviour
{
	[SerializeField] GameObject effects;	//Confetti particles

	//When something enters the collider
	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player")
			return;

		//Get a reference to the PlayerState script and if it exists, call the Won() method
		PlayerState player = other.GetComponent<PlayerState>();
		if (player != null && player.isValidTarget)
			player.Won();

		//Turn effects on
		effects.SetActive(true);
	}
}
