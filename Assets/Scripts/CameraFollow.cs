using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform lookAt;
	public Transform camTransform;

	public float currentX;
	public float currentY;

	private float distance = 10f;

	private void Start ()
	{
		camTransform = transform;
	}

	private void LateUpdate ()
	{
		Vector3 dir = new Vector3 (0, 3, -distance);
		Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);
		camTransform.position = lookAt.position + rotation * dir;
		camTransform.LookAt (lookAt.position);
	}
}
