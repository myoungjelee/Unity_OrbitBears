using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEditor;
using UnityEngine;
public class TCPserver :MonoBehaviour
{
    private TcpListener tcpListener;

    public TCPserver(string ip, int port)
    {
        IPAddress ipAddress = IPAddress.Parse(ip);
        tcpListener = new TcpListener(ipAddress, port);
    }

    public void Start()
    {
        tcpListener.Start();
        Console.WriteLine("Server started...");

        while (true)
        {
            TcpClient client = tcpListener.AcceptTcpClient();
            Console.WriteLine("Client connected...");
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            int numBytesRead = stream.Read(buffer, 0, buffer.Length);
            string receivedData = Encoding.ASCII.GetString(buffer, 0, numBytesRead);
            Console.WriteLine("Received: " + receivedData);

            // Echo the data back to the client.
            stream.Write(buffer, 0, numBytesRead);

            client.Close();
        }
    }
}