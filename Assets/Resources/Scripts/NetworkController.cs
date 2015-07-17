using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Threading;
using System.IO;

public class NetworkController : MonoBehaviour {

	public InputField inputFieldAddress;

	private Peer peer;

	const int PORT = 8888;

	void Start() {
		inputFieldAddress.text = "192.168.0.100";

		peer = new Peer(PORT);
		peer.onConnected += new Peer.NetworkEvent(onPeerConnected);

		Debug.Log (peer.socketAlive);
	}

	void Update() {

		if (Input.GetKeyDown (KeyCode.Space)) {
			peer.sendString("fool");
		}
	}

	void onPeerConnected(Peer p, int connectionId) {
		Debug.Log ("Peer connected: " + connectionId.ToString());
	}

	public void OnButtonConnectClick() {
		if (peer.connectSocket(inputFieldAddress.text, PORT)) {
			Debug.Log("Connected to peer");
		}
	}


}
