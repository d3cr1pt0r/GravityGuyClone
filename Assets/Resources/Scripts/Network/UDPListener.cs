using UnityEngine;

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPListener {

    private int listen_port;
    private bool done;
    private UdpClient listener;
    private IPEndPoint groupEP;
    private string received_data;
    private byte[] receive_byte_array;

    private Thread listen_thread;

    public UDPListener(int listen_port)
    {
        this.listen_port = listen_port;
        done = false;
        listener = new UdpClient(listen_port);
        groupEP = new IPEndPoint(IPAddress.Any, listen_port);
    }

    public void Listen()
    {
        listen_thread = new Thread(new ThreadStart(listenThread));
        listen_thread.Start();
    }

    private void listenThread()
    {
        try
        {
            while (!done)
            {
                Debug.Log("Waiting for broadcast...");
                receive_byte_array = listener.Receive(ref groupEP);
                Debug.Log("Received a broadcast from " + groupEP.ToString());
                Debug.Log("Recieved data: " + BitConverter.ToString(receive_byte_array, 0, 1));
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

}
