using System.Collections;
using UnityEngine;

public class Bridge : MonoBehaviour
{

	private GameObject enemy;
	private EnemyMovement enemyMovement;

	public bool isHolding;

	void Awake ()
	{
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
		enemyMovement = enemy.GetComponent<EnemyMovement> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<Rigidbody> ().isKinematic = isHolding;
	}

	public void TriggerCollaspe ()
	{
		StartCoroutine (Collaspe ());
		isHolding = false;
	}

	IEnumerator Collaspe ()
	{
		enemyMovement.GetComponent<Collider> ().isTrigger = false;
		yield return new WaitForSeconds (0.1f);
		enemyMovement.Fall ();
		isHolding = false;
	}
}
