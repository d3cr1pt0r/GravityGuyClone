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
		
	}

	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
	}

	void Start() {
        UDPListener listener = new UDPListener(1337);
        listener.Listen();
	}

	void FixedUpdate() {

	}

	void OnLevelWasLoaded(int level) {
		GameObject localPlayer = Instantiate (Player, new Vector3 (-4, 0, 0), Quaternion.Euler(0, 180, 0)) as GameObject;
		GameObject peerPlayer = Instantiate (Player, new Vector3 (-2, 0, 0), Quaternion.Euler(0, 180, 0)) as GameObject;
	}

}
