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

	private int hostId;
	private HostTopology hostTopology;
	private int connectionId;

	void Start() {
		inputFieldAddress.text = "192.168.1.104";
		hostId = 1;
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			byte error;
			byte[] buffer = System.Text.Encoding.ASCII.GetBytes ("Hello world");
			
			int channelId = 1;
			NetworkTransport.Send (hostId, connectionId, channelId, buffer, buffer.Length, out error);

			Debug.Log (error);
		}
	}

	public void OnButtonStartServerClicked() {
		initNetworkConfig();

		hostId = NetworkTransport.AddHost (hostTopology, 8888);
		Debug.Log ("Host ID: " + hostId.ToString());
	
		StartCoroutine (CoroutineExample.CoroutineReceiveRequest ());

		// Connect to host
		/*
		byte error;
		int connectionId = NetworkTransport.Connect (hostId, "127.0.0.1", 6000, 0, out error);
		Debug.Log (error);

		if (error != (byte)NetworkError.Ok) {
			NetworkError nerror = (NetworkError)error;
			Debug.Log ("Connection error: " + nerror.ToString());
		}

		byte[] buffer = new byte[1024];
		Stream stream = new MemoryStream (buffer);
		BinaryFormatter formatter = new BinaryFormatter ();
		formatter.Serialize (stream, "MyMessage");

		int channelId = 1;
		NetworkTransport.Send (hostId, connectionId, channelId, buffer, (int)stream.Position, out error);
		*/
	}

	public void OnButtonConnectClick() {
		string address = inputFieldAddress.text;

		// Connect to host
		byte error;
		connectionId = NetworkTransport.Connect (hostId, address, 8888, 0, out error);
		Debug.Log (error);

		if (error != (byte)NetworkError.Ok) {
			NetworkError nerror = (NetworkError)error;
			Debug.Log ("Connection error: " + nerror.ToString());
		}
	}

	void initNetworkConfig() {
		// Create global config
		GlobalConfig globalConfig = new GlobalConfig ();
		globalConfig.ReactorModel = ReactorModel.FixRateReactor;
		globalConfig.ThreadAwakeTimeout = 1;
		
		// Create connection config
		ConnectionConfig connectionConfig = new ConnectionConfig ();
		byte channelReliable = connectionConfig.AddChannel (QosType.Reliable);
		byte channelUnreliable = connectionConfig.AddChannel (QosType.Unreliable);
		
		// Create host config
		hostTopology = new HostTopology (connectionConfig, 10);
		
		// Init network
		NetworkTransport.Init (globalConfig);
	}
}
