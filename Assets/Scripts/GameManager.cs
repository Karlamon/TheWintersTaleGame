using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

	public Transform canvas;
	public bool isAlive = true;
	public bool hasWon = false;
	public Text score;
	public Text time;

	private GameObject player;
	private PlayerMovement playerMovement;
	private Camera cam;
	private CameraFollow cameraFollow;
	private GameObject music;
	private AudioSource musicSource;

	public int playerScore = 0;
	private float startTime;
	public int minutes;
	public int seconds;

	private int scoreCounter = 0;
	private int minutesCounter = 0;
	private int secondsCounter = 0;
	private bool countUp = false;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerMovement = player.GetComponent <PlayerMovement> ();
		cam = Camera.main;
		cameraFollow = cam.GetComponent <CameraFollow> ();
		startTime = Time.time;
		music = GameObject.FindGameObjectWithTag ("Audio");
		musicSource = music.GetComponent <AudioSource> ();
	}

	void Update ()
	{
		if (isAlive == true && hasWon == false) {
			float t = Time.time - startTime;
			minutes = ((int)t / 60);
			seconds = ((int)t % 60);
		}

		if (countUp == true) {
			score.text = "Score: " + scoreCounter;
			time.text = "Time: " + string.Format ("{0:0}:{1:00}", minutesCounter, secondsCounter);
			if (scoreCounter < playerScore) {
				++scoreCounter;
			}
			if (minutesCounter < minutes) {
				++minutesCounter;
			}
			if (secondsCounter < seconds) {
				++secondsCounter;
			}
		}
		if (hasWon == true) {
			musicSource.Stop ();
		}
	}

	public void UpdateScore (int value)
	{
		playerScore += value;
	}

	public void EnableSpeedup ()
	{
		StartCoroutine (Speedup ());
	}

	IEnumerator Speedup ()
	{
		playerMovement.moveSpeed += 10;
		yield return new WaitForSeconds (2f);
		playerMovement.moveSpeed -= 10;
	}

	public void KillPlayer ()
	{
		musicSource.Stop ();
		isAlive = false;
		playerMovement.enabled = false;
		playerMovement.anim.SetBool ("IsRunning", false);
		cameraFollow.enabled = false;
		Cursor.visible = true;
		if (canvas != null) {
			canvas.gameObject.SetActive (true);
		}
		countUp = true;
	}
}
