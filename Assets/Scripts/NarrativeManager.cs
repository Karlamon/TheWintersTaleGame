using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{

	public Transform canvas;
	public Narrative narrative;
	public Text narrativeText;
	public RawImage frame;
	public Animator animator;

	private Animator fadeAnim;
	private Queue<string> sentences;
	private int frameIndex = 0;
	private AudioSource menuPressAudio;
	private Scene currentScene;
	private string sceneName;

	// Use this for initialization
	void Start ()
	{
		sentences = new Queue<string> ();
		frame = GetComponent<RawImage> ();
		menuPressAudio = GetComponent<AudioSource> ();
		fadeAnim = canvas.GetComponent<Animator> ();
		StartNarrative (narrative);
		currentScene = SceneManager.GetActiveScene ();
		sceneName = currentScene.name;
	}

	public void StartNarrative (Narrative narrative)
	{
		animator.SetBool ("IsOpen", true);

		sentences.Clear ();

		foreach (string sentence in narrative.sentences) {
			sentences.Enqueue (sentence);
		}
		DisplayNextSentence ();
	}

	public void DisplayNextSentence ()
	{
		menuPressAudio.Play ();
		if (sentences.Count == 0) {
			EndNarrative ();
			return;
		}

		frame.texture = narrative.frames [frameIndex];
		string sentence = sentences.Dequeue ();
		StopAllCoroutines ();
		StartCoroutine (TypeSentence (sentence));
		++frameIndex;
	}

	IEnumerator TypeSentence (string sentence)
	{
		narrativeText.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			narrativeText.text += letter;
			yield return null;
		}
	}

	public void EndNarrative ()
	{
		menuPressAudio.Play ();
		StartCoroutine (NextScene ());
	}

	IEnumerator NextScene ()
	{
		animator.SetBool ("IsOpen", false);
		fadeAnim.SetBool ("IsFadingOut", true);
		yield return new WaitForSeconds (1f);
		if (sceneName == "Intro") {
			SceneManager.LoadScene ("Forest Beach");
		} else {
			SceneManager.LoadScene ("Main Menu");
		}
	}
}
