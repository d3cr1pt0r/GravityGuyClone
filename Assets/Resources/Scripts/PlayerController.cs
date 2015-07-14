using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float runSpeed;
	public float jumpSpeed;

	private bool isJumping = true;
	private int collisionNum = 0;

	void Start ()
	{
		
	}

	void Update ()
	{
		if (!isJumping && Input.GetKeyDown (KeyCode.Space))
		{
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
		if (isJumping && collider.tag == "Floor") {
			collisionNum++;
			isJumping = false;

			Debug.Log (collisionNum);
			Debug.Log (isJumping);
		}
	}
}
