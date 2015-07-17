using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float runSpeed;
	public float jumpSpeed;
	
	private bool isJumping = true;
	private NetworkIdentity networkIdentity;

	void Start ()
	{
		networkIdentity = gameObject.GetComponent<NetworkIdentity>();
	}

	void Update ()
	{
		if (!isJumping && Input.GetKeyDown (KeyCode.Space))
		{
			Debug.Log ("JUMP!");
			gameObject.transform.Rotate(0, 180, 180);
			jumpSpeed *= -1;
			isJumping = true;
		}
	}

	void FixedUpdate()
	{
		if (isJumping) {
			gameObject.transform.position -= new Vector3(0, jumpSpeed, 0);
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (isJumping && (collider.tag == "Floor" || collider.tag == "Player")) {
			// TODO: Adjust player position to floor position
			isJumping = false;
		}
	}
}
