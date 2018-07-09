using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

	public Transform canvas;
	public Canvas helpCanvas;
	public Canvas creditsCanvas;

	private Animator anim;
	private AudioSource menuPressAudio;

	void Awake ()
	{
		anim = canvas.GetComponent<Animator> ();
		menuPressAudio = GetComponent<AudioSource> ();
	}

	public void StartGame ()
	{
		menuPressAudio.Play ();
		StartCoroutine (Loading ());
	}

	public void ToggleHelpScreen ()
	{
		menuPressAudio.Play ();
		if (helpCanvas.gameObject.activeInHierarchy == false) {
			helpCanvas.gameObject.SetActive (true);
		} else {
			helpCanvas.gameObject.SetActive (false);
		}
	}

	public void ToggleCredits ()
	{
		menuPressAudio.Play ();
		if (creditsCanvas.gameObject.activeInHierarchy == false) {
			creditsCanvas.gameObject.SetActive (true);
		} else {
			creditsCanvas.gameObject.SetActive (false);
		}
	}

	public void ExitGame ()
	{
		menuPressAudio.Play ();
		StartCoroutine (Quitting ());
	}

	IEnumerator Loading ()
	{
		anim.SetBool ("IsFadingOut", true);
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene ("Intro");
	}

	IEnumerator Quitting ()
	{
		anim.SetBool ("IsFadingOut", true);
		yield return new WaitForSeconds (1f);
		Application.Quit ();
	}
}
