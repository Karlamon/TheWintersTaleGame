using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public float moveSpeed;
	public float jumpForce;
	public int turnStage = 1;
	public bool isInForest;
	public Animator anim;

	private GameObject player;
	private GameObject gameController;
	private Vector3 movement;
	private Rigidbody playerRigidbody;
	private GameManager gameManager;

	public AudioClip footstepsBeachAudio;
	public AudioClip footstepsForestAudio;
	public AudioClip jumpAudio;
	public AudioClip splashAudio;
	public AudioSource sound;

	private bool isGrounded = true;
	private bool isRunning = false;
	private bool isSliding = false;
	private bool isFootstepsAudioOn = false;
	private bool hasSplashSoundPlayed = false;

	void Awake ()
	{
		playerRigidbody = GetComponent<Rigidbody> ();
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		gameManager = gameController.GetComponent <GameManager> ();
		sound = GetComponent<AudioSource> ();
		anim = GetComponent <Animator> ();
	}

	void FixedUpdate ()
	{
		float h = Input.GetAxisRaw	("Horizontal");
		float v = Input.GetAxisRaw	("Vertical");
		if (turnStage == 1) {
			Move (h, v);
		} else if (turnStage == 2) {
			Move (-v, h);
		} else if (turnStage == 3) {
			Move (-h, -v);
		} else if (turnStage == 4) {
			Move (v, -h);
		}
		if ((Input.GetKeyDown (KeyCode.LeftControl) || Input.GetKeyDown (KeyCode.RightControl)) && isGrounded == true && isSliding == false && isRunning == true) {
			StartCoroutine (Slide ());
		}
		if (transform.position.y > 2) {
			PlayRunningSound ();
		} else {
			Drown ();
		}
		if (h != 0 || v != 0) {
			isRunning = true;

		} else {
			isRunning = false;
		}
		Turn ();
		Animating (h, v);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded == true) {
			Jump ();
		}
	}

	void Move (float h, float v)
	{ 	
		movement.Set (h, 0f, v);
		movement = movement.normalized * moveSpeed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Jump ()
	{
		sound.loop = false;
		sound.clip = jumpAudio;
		sound.Play ();
		playerRigidbody.AddForce (Vector3.up * jumpForce/* * Time.deltaTime*/, ForceMode.Impulse);
		anim.SetTrigger ("Jump");
	}

	void PlayRunningSound ()
	{
		if (isRunning == true && isGrounded == true && isFootstepsAudioOn == false) {
			if (isInForest == true) {
				sound.clip = footstepsForestAudio;
			} else {
				sound.clip = footstepsBeachAudio;
			}
			sound.loop = true;
			sound.Play ();
			isFootstepsAudioOn = true;
		}
		if (isRunning == false && isGrounded == true && isFootstepsAudioOn == true) {
			sound.clip = null;
			isFootstepsAudioOn = false;
		}
		if (isGrounded == false) {
			isFootstepsAudioOn = false;
			sound.clip = jumpAudio;
		}
	}

	IEnumerator Slide ()
	{
		isSliding = true;
		float angle = -90;
		float duration = 1.0f;
		Quaternion from = transform.rotation; 
		Quaternion to = transform.rotation;
		to *= Quaternion.Euler (Vector3.right * angle);
		float elapsed = 0.0f;
		while (elapsed < duration) {
			transform.rotation = Quaternion.Slerp (from, to, elapsed / duration);
			elapsed += 3 * Time.deltaTime;
			yield return null;
		}
		transform.rotation = to;
		elapsed = 0.0f;
		while (elapsed < duration) {
			transform.rotation = Quaternion.Slerp (to, from, elapsed / duration);
			elapsed += 3 * Time.deltaTime;
			yield return null;
		}
		transform.rotation = from;
		isSliding = false;
	}

	void Turn ()
	{
		if (movement != Vector3.zero) {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (movement), Time.deltaTime * 5f);
		}
	}

	void Drown ()
	{
		
		sound.loop = false;
		sound.clip = splashAudio;
		if (hasSplashSoundPlayed == false) {
			sound.Play ();
			hasSplashSoundPlayed = true;
		}
		gameManager.KillPlayer ();
	}

	void OnCollisionStay (Collision col)
	{
		if (col.gameObject.CompareTag ("Ground") || col.gameObject.CompareTag ("Collaspable")) {
			isGrounded = true;
		}
	}

	void OnCollisionExit (Collision col)
	{
		if (col.gameObject.CompareTag ("Ground") || col.gameObject.CompareTag ("Collaspable")) {
			anim.ResetTrigger ("Jump");
			isGrounded = false;
		}
	}

	void Animating (float h, float v)
	{
		bool running = h != 0f || v != 0;
		anim.SetBool ("IsRunning", running);
	}
}
