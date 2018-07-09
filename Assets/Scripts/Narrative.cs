using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Narrative
{

	[TextArea (3, 10)]
	public string[] sentences;
	public Texture[] frames;
}
