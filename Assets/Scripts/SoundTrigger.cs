using System.Collections;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{

	private GameObject player;
	private PlayerMovement playerMovement;

	// Use this for initialization
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerMovement = player.GetComponent <PlayerMovement> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player) {
			if (playerMovement.isInForest == true) {
				playerMovement.isInForest = false;
			} else {
				playerMovement.isInForest = true;
			}
		}
	}
}
