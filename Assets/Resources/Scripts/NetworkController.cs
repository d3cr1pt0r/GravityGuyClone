using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Threading;
using System.IO;

public class NetworkController : MonoBehaviour {

	public InputField inputFieldAddress;

	private Peer peer;

	const int PORT = 8888;

	public void OnButtonConnectClick() {
		if (peer.connectSocket(inputFieldAddress.text, PORT)) {
			Debug.Log("Connected to peer");
		}
	}

	void Start() {
		inputFieldAddress.text = "127.0.0.1";

		peer = new Peer(PORT);
		peer.onConnectionReceived += new Peer.ConnectionHandler(onPeerConnected);
		peer.onDataReceived += new Peer.DataReceivedHandler(onPeerDataReceived);

		Debug.Log ("Socket alive: " + peer.socketAlive.ToString());
	}

	void Update() {

		if (Input.GetKeyDown (KeyCode.Space)) {
			peer.sendString("fool");
		}
	}

	void FixedUpdate() {
		peer.CheckForNetworkEvents();
	}

	void onPeerConnected(int connectionId) {
		Debug.Log ("Peer connected: " + connectionId.ToString());
	}

	void onPeerDataReceived(string message) {
		Debug.Log ("Peer message: " + message);
	}
	
}
