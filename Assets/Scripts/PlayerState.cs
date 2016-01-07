//This script manages the state of the runner

using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))] //If an animator doesn't exist on the object, this will add one (though it won't be set up). Mostly I just wanted you to see this
public class PlayerState : MonoBehaviour
{
	[HideInInspector] public bool isValidTarget = true;	//A winning player or a dying player is not a valid target to shoot at

	[SerializeField] GameObject restartDialog;			//Game Object containing the UI text the player will see when the game is done being played

	PlayerMovement playerMovement;						//Reference to PlayerMovement script as a component
	Animator anim;										//Reference to animator component

	void Start()
	{
		playerMovement = GetComponent<PlayerMovement>();
		anim = GetComponent<Animator>();
	}

	public void Won()
	{
		//Tell the animator that we won
		anim.SetTrigger("Won");
		GameOver();
	}

	public void Died()
	{
		//Tell the animator that we died
		anim.SetTrigger("Died");
		GameOver();
	}

	void GameOver()
	{
		//If the restart dialog exists, show it
		if (restartDialog)
			restartDialog.SetActive(true);

		//The runner isn't valid and can't move
		isValidTarget = false;
		playerMovement.enabled = false;
	}

	//Public method called by the UI to reload the scene
	public void Reload()
	{
		SceneManager.LoadScene(0);
    }
}
