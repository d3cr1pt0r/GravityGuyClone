using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Threading;
using System.IO;

public class NetworkController : MonoBehaviour {

	public GameObject Player;
	public InputField InputFieldAddress;
	public Text ConnectionStatusText;

	private Peer peer;

	const int PORT = 8888;

	public void OnButtonConnectClick() {
		if (peer.ConnectSocket(InputFieldAddress.text, PORT)) {

		}
	}

	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
	}

	void Start() {
		InputFieldAddress.text = "127.0.0.1";

		peer = new Peer(PORT);
		peer.onConnectionReceived += new Peer.ConnectionHandler(onPeerConnected);

		Debug.Log ("Socket alive: " + peer.socketAlive.ToString());
	}

	void FixedUpdate() {
		peer.CheckForNetworkEvents();
	}

	void OnLevelWasLoaded(int level) {
		GameObject localPlayer = Instantiate (Player, new Vector3 (-4, 0, 0), Quaternion.Euler(0, 180, 0)) as GameObject;
		GameObject peerPlayer = Instantiate (Player, new Vector3 (-2, 0, 0), Quaternion.Euler(0, 180, 0)) as GameObject;

		localPlayer.GetComponent<PlayerController> ().SetIsLocalPlayer (true);
		localPlayer.GetComponent<PlayerController> ().SetPeer (peer);

		peerPlayer.GetComponent<PlayerController> ().SetIsLocalPlayer (false);
		peerPlayer.GetComponent<PlayerController> ().SetPeer (peer);
	}

	void onPeerConnected(int connectionId) {
		Debug.Log ("Peer connected: " + connectionId.ToString());
		ConnectionStatusText.text = "Connected";
		ConnectionStatusText.color = Color.green;

		Application.LoadLevel ("level_1");
	}	
}
