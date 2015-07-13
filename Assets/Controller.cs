using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
	private Rigidbody rigidBody;

	void Awake()
	{
		this.rigidBody = gameObject.GetComponent<Rigidbody>();
	}
	
	void Start ()
	{

	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			Debug.Log ("Hi");

			// Throw dice in the air and give them a random spin
			this.rigidBody.AddForce (0, 300.0f, 0);
			this.rigidBody.angularVelocity = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
		}
		if (Input.GetKey (KeyCode.UpArrow))
		{
			this.rigidBody.AddForce (gameObject.transform.forward * 100.0f);
		}
	}
}
