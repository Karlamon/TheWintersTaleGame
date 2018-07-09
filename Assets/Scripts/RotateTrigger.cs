using UnityEngine;

public class RotateTrigger : MonoBehaviour
{

	public bool hasTriggered;

	private GameObject player;
	private PlayerMovement playerMovement;
	private Camera cam;
	private CameraFollow cameraFollow;

	private float rotation;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerMovement = player.GetComponent <PlayerMovement> ();
		cam = Camera.main;
		cameraFollow = cam.GetComponent <CameraFollow> ();
	}

	void FixedUpdate ()
	{
		if (playerMovement.turnStage == 1) {
			rotation = -90f;
		} else if (playerMovement.turnStage == 2) {
			rotation = -180f;
		} else if (playerMovement.turnStage == 3) {
			rotation = -270f;
		} else if (playerMovement.turnStage == 4) {
			rotation = -360f;
		}

		if (hasTriggered == true) {
			if (cameraFollow.currentX > rotation) {
				--cameraFollow.currentX;
				if (cameraFollow.currentX == rotation) {
					++playerMovement.turnStage;
					if (playerMovement.turnStage > 4) {
						playerMovement.turnStage = 1;
						cameraFollow.currentX = 0f;
					}
					Destroy (gameObject);
				}
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player) {
			hasTriggered = true;
		}
	}
}
