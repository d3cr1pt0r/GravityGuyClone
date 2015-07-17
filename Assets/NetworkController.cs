using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System.Net.Sockets;

public class NetworkController : MonoBehaviour {

	public InputField inputFieldAddress;

	void Start() {
		inputFieldAddress.text = "192.168.1.104";
	}

	public void OnButtonStartServerClicked() {
		Debug.Log ("Starting server...");
		NetworkConnectionError status = Network.InitializeServer(4, 9000);
		Debug.Log (status);
	}

	public void OnButtonConnectClick() {
		string address = inputFieldAddress.text;
		Debug.Log ("Connecting to server: " + address);
		NetworkConnectionError status = Network.Connect (address, 9000);
		Debug.Log (status);
	}

	void OnConnectedToServer() {
		Debug.Log ("OnConnectedToServer");
	}

	void OnStartServer() {
		Debug.Log ("OnStartServer");
	}

	void OnStartClient() {
		Debug.Log ("OnStartClient");
	}
}
