using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

	private GameObject player;
	private GameObject gameController;
	private GameManager gameManager;
	private NavMeshAgent nav;
	private AudioSource roarSound;
	private AudioSource playerSound;

	private bool hasRoarSoundPlayed = false;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		gameManager = gameController.GetComponent <GameManager> ();
		nav = GetComponent <NavMeshAgent> ();
		roarSound = GetComponent <AudioSource> ();
		playerSound = player.GetComponent <AudioSource> ();
	}

	void FixedUpdate ()
	{
		
		if (nav.isOnNavMesh) {
			nav.SetDestination (player.transform.position);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player) {
			playerSound.Stop ();
			gameManager.KillPlayer ();
			if (hasRoarSoundPlayed == false) {
				roarSound.Play ();
				hasRoarSoundPlayed = true;
			}
		}
	}

	public void Fall ()
	{
		nav.Stop ();
		nav.enabled = false;
		if (hasRoarSoundPlayed == false) {
			roarSound.Play ();
			hasRoarSoundPlayed = true;
		}
	}
}
