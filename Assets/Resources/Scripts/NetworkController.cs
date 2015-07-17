using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Threading;
using System.IO;

public class CoroutineExample : MonoBehaviour {
	public static NetworkEventType serverEvent;

	public static IEnumerator CoroutineReceiveRequest() {
		int recHostId;
		int connectionId;
		int channelId;
		int dataSize;
		byte[] buffer = new byte[1024];
		byte error;

		while (true) {
			NetworkEventType recData = NetworkTransport.Receive (out recHostId, out connectionId, out channelId, buffer, 1024, out dataSize, out error);
			switch(recData) {
				case NetworkEventType.Nothing:
					break;
				case NetworkEventType.ConnectEvent:
					Debug.Log ("Connection received...");
					break;
				case NetworkEventType.DisconnectEvent:
					Debug.Log ("DisconnectEvent");
					break;
				case NetworkEventType.DataEvent:
					string data = System.Text.Encoding.ASCII.GetString (buffer);
					Debug.Log ("DataEvent: " + data);
					break;
				case NetworkEventType.BroadcastEvent:
					Debug.Log ("BroadcastEvent");
					break;
				default:
					break;
			}
			yield return null;
		}
	}
}

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
		Debug.Log (connectionId);
	}

	public void OnButtonConnectClick() {
		if (peer.connectSocket(inputFieldAddress.text, PORT)) {
			Debug.Log("connected");
		}
	}


}
