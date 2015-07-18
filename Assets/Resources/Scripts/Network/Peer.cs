using System;
using UnityEngine;
using UnityEngine.Networking;

public class Peer : MonoBehaviour
{
	public event ConnectionHandler onConnectionEstablished;
	public event ConnectionHandler onConnectionReceived;
	public event ConnectionHandler onDisconnect;
	public event ConnectionHandler onPeerDisconnected;

	public event DataReceivedHandler onDataReceived;
	public event DataReceivedHandler onBroadcastReceived;

	public delegate void ConnectionHandler(int connectionId);
	public delegate void DataReceivedHandler(string message);

	public bool socketAlive;

	private int hostId;
	private HostTopology hostTopology;
	private int recHostId;
	private int connectionId;
	private int channelId;
	private int dataSize;
	private byte[] buffer;
	private byte error;
	
	public Peer (int port) {
		buffer = new byte[1024];
		socketAlive = createSocket(port);
	}
	
	public void CheckForNetworkEvents() {
		NetworkEventType recData = NetworkTransport.Receive (out recHostId, out connectionId, out channelId, buffer, 1024, out dataSize, out error);
		
		switch(recData) {
			case NetworkEventType.Nothing:
				break;
			case NetworkEventType.ConnectEvent:
				if (onConnectionReceived != null)
					onConnectionReceived(connectionId);
				break;
			case NetworkEventType.DisconnectEvent:
				if (onPeerDisconnected != null)
					onPeerDisconnected(connectionId);
				break;
			case NetworkEventType.DataEvent:
				if (onDataReceived != null)
					onDataReceived(System.Text.Encoding.ASCII.GetString (buffer));
				break;
			case NetworkEventType.BroadcastEvent:
				if (onBroadcastReceived != null)
					onBroadcastReceived("NOT YET IMPLEMENTED!");
				break;
			default:
				break;
		}
	}

	bool createSocket(int port) {
		initNetworkConfig();
		
		hostId = NetworkTransport.AddHost (hostTopology, port);
		return hostId != -1;
	}

	public void sendString(string str, int channelId = 1) {
		byte error;
		byte[] buffer = System.Text.Encoding.ASCII.GetBytes (str);
		NetworkTransport.Send(hostId, connectionId, channelId, buffer, buffer.Length, out error);
	}

	public bool connectSocket(string address, int port) {
		byte error;
		connectionId = NetworkTransport.Connect (hostId, address, port, 0, out error);

		if (error == (byte)NetworkError.Ok) {
			if (onConnectionEstablished != null)
				onConnectionEstablished(connectionId);
			return true;
		}
		return false;
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

