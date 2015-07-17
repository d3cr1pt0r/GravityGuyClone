using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float runSpeed;
	public float jumpSpeed;
	
	private bool isJumping = true;

	void Start ()
	{

	}

	void Update ()
	{
		if (!isJumping && Input.GetKeyDown (KeyCode.Space))
		{
			Jump();
		}
	}
	
	void Jump()
	{
		Debug.Log ("JUMP!");
		gameObject.transform.Rotate(0, 180, 180);
		jumpSpeed *= -1;
		isJumping = true;
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
