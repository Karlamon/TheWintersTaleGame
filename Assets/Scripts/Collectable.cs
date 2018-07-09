using UnityEngine;

public class Collectable : MonoBehaviour
{

	public int value;
	public bool isPowerup;

	private GameObject player;
	private GameObject gameController;
	private GameManager gameManager;
	private AudioSource powerupSound;

	private float rotateSpeed = 50f;
	private float asscendSpeed = 5f;
	private bool isCollected = false;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		gameManager = gameController.GetComponent <GameManager> ();
		powerupSound = GetComponent <AudioSource> ();
	}

	void FixedUpdate ()
	{
		transform.Rotate (Vector3.up, rotateSpeed * Time.deltaTime);
		if (isCollected == true) {
			rotateSpeed = 100f;
			transform.Translate (Vector3.up * asscendSpeed * Time.deltaTime);
			Destroy (gameObject, 3);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player && isCollected == false) {
			powerupSound.Play ();
			gameManager.UpdateScore (value);
			if (isPowerup == true) {
				gameManager.EnableSpeedup ();
			}
			isCollected = true;
		}
	}
}
