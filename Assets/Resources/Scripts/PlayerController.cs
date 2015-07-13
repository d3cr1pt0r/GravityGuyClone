using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	void Start ()
	{
		
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			gameObject.GetComponent<Rigidbody2D>().gravityScale *= -1;
			gameObject.transform.Rotate(0, 180, 180);
		}
	}
}
