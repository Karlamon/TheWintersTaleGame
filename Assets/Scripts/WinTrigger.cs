using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{

	public Transform canvas;
	public Text score;
	public Text time;
	public AudioClip bridgeFallSound;
	public AudioClip winSound;

	private AudioSource sound;
	private GameObject player;
	private GameObject bridge;
	private Bridge bridgeTrigger;
	private PlayerMovement playerMovement;
	private GameObject gameController;
	private GameManager gameManager;

	private int scoreCounter = 0;
	private int minutesCounter = 0;
	private int secondsCounter = 0;
	private bool countUp = false;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		bridge = GameObject.FindGameObjectWithTag ("Collaspable");
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		bridgeTrigger = bridge.GetComponent <Bridge> ();
		playerMovement = player.GetComponent <PlayerMovement> ();
		gameManager = gameController.GetComponent <GameManager> ();
		sound = GetComponent <AudioSource> ();
	}

	void Update ()
	{
		if (countUp == true) {
			score.text = "Score: " + scoreCounter;
			time.text = "Time: " + string.Format ("{0:0}:{1:00}", minutesCounter, secondsCounter);
			if (scoreCounter < gameManager.playerScore) {
				++scoreCounter;
			}
			if (minutesCounter < gameManager.minutes) {
				++minutesCounter;
			}
			if (secondsCounter < gameManager.seconds) {
				++secondsCounter;
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player) {
			sound.clip = bridgeFallSound;
			sound.Play ();
			gameManager.hasWon = true;
			bridgeTrigger.TriggerCollaspe ();
			StartCoroutine (ShowWinScreen ());
		}
	}

	IEnumerator ShowWinScreen ()
	{
		yield return new WaitForSeconds (3);
		{
			sound.clip = winSound;
			sound.Play ();
			playerMovement.sound.Stop ();
			playerMovement.anim.SetBool ("IsRunning", false);
			playerMovement.enabled = false;
			canvas.gameObject.SetActive (true);
			Cursor.visible = true;
			countUp = true;
		}
	}
}
