using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float runSpeed;
	public float jumpSpeed;
	
	private bool isLocalPlayer = false;
	private bool isJumping = true;
	private Peer peer = null;

	void Start ()
	{
		
	}

	void Update ()
	{
		if (isLocalPlayer && Input.GetMouseButtonDown(0)) {
			Jump ();
			peer.SendString("j");
		}
	}

	void Jump()
	{
		if (!isJumping) {
			gameObject.transform.Rotate (0, 180, 180);
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
	
	void onPeerDataReceived(string message) {
		Debug.Log ("Message received: " + message);
		Debug.Log (isLocalPlayer);

		if (message == "j")
			Jump ();
	}

	public void SetPeer(Peer peer) {
		this.peer = peer;
		peer.onDataReceived += new Peer.DataReceivedHandler (onPeerDataReceived);
	}

	public void SetIsLocalPlayer(bool isLocalPlayer) {
		this.isLocalPlayer = isLocalPlayer;
	}
}
