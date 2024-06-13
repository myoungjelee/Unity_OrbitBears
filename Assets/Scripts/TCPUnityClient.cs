using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System;

public class TCPClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        client = new TcpClient("127.0.0.1", 8000);
        stream = client.GetStream();
        Debug.Log("Connected to the server.");

        SendData("Hello Server!");
        ReceiveData();
    }

    void SendData(string message)
    {
        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
        Debug.Log("Data sent: " + message);
    }

    void ReceiveData()
    {
        byte[] receivedData = new byte[1024];
        int bytes = stream.Read(receivedData, 0, receivedData.Length);
        string response = Encoding.ASCII.GetString(receivedData, 0, bytes);
        Debug.Log("Received: " + response);
    }

    private void OnApplicationQuit()
    {
        stream.Close();
        client.Close();
    }
}