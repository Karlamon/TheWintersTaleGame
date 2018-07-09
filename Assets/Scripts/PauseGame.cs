using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{

	public Transform canvas;
	public Canvas lose;
	public Canvas win;

	private GameManager gameManager;
	private AudioSource menuPressAudio;
	private GameObject music;
	private AudioSource musicSource;
	private GameObject fader;
	private Animator anim;
	private Animator loseAnim;
	private Animator winAnim;

	private bool isFading = false;
	private string ending;

	void Awake ()
	{
		gameManager = GetComponent <GameManager> ();
		menuPressAudio = GetComponent<AudioSource> ();
		Cursor.visible = false;
		fader = GameObject.FindGameObjectWithTag ("Fader");
		anim = fader.GetComponent <Animator> ();
		loseAnim = lose.GetComponent <Animator> ();
		winAnim = win.GetComponent <Animator> ();
		music = GameObject.FindGameObjectWithTag ("Audio");
		musicSource = music.GetComponent <AudioSource> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && gameManager.isAlive == true && gameManager.hasWon == false) {
			Pause ();
		}
	}

	public void Pause ()
	{
		if (isFading == false) {
			if (canvas.gameObject.activeInHierarchy == false) {
				menuPressAudio.Play ();
				canvas.gameObject.SetActive (true);
				Time.timeScale = 0;
				musicSource.Pause ();
				Cursor.visible = true;
			} else {
				menuPressAudio.Play ();
				canvas.gameObject.SetActive (false);
				Time.timeScale = 1;
				musicSource.UnPause ();
				Cursor.visible = false;
			}
		}
	}

	public void RestartLevel ()
	{
		menuPressAudio.Play ();
		canvas.gameObject.SetActive (false);
		Time.timeScale = 1;
		StartCoroutine (Restarting ());
	}

	public void Surrender ()
	{
		menuPressAudio.Play ();
		canvas.gameObject.SetActive (false);
		Time.timeScale = 1;
		ending = "Ending Bad";
		StartCoroutine (Ending ());
	}

	public void Succeed ()
	{
		menuPressAudio.Play ();
		canvas.gameObject.SetActive (false);
		Time.timeScale = 1;
		ending = "Ending Good";
		if (gameManager.isAlive == true) {
			StartCoroutine (Ending ());
		} else {
			SceneManager.LoadScene (ending);
		}
	}

	IEnumerator Restarting ()
	{
		isFading = true;
		anim.SetBool ("IsFadingOut", true);
		if (loseAnim.isActiveAndEnabled == true) {
			loseAnim.SetBool ("IsFadingOut", true);
		}
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	IEnumerator Ending ()
	{
		isFading = true;
		gameManager.canvas = null;
		anim.SetBool ("IsFadingOut", true);
		if (winAnim.isActiveAndEnabled == true) {
			winAnim.SetBool ("IsFadingOut", true);
		} else if (loseAnim.isActiveAndEnabled == true) {
			loseAnim.SetBool ("IsFadingOut", true);
		}
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (ending);
	}
}
