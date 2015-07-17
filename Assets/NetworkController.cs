using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkController : MonoBehaviour{

	void Start() {

	}

	public void OnButtonStartServerClicked() {
		Debug.Log ("Starting server...");
		NetworkConnectionError status = Network.InitializeServer(2, 9000);
		Debug.Log (status);
	}

	public void OnButtonConnectClick() {
		Debug.Log ("Connecting to server...");
		NetworkConnectionError status = Network.Connect ("localhost", 9000);
		Debug.Log (status);
	}

	void OnConnectedToServer() {
		Debug.Log ("OnConnectedToServer");
	}

	void OnStartClient() {
		Debug.Log ("OnStartClient");
	}
}
