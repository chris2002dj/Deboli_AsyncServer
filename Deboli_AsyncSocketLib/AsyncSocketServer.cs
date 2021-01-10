using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;


namespace Deboli_AsyncSocketLib
{
    public class AsyncSocketServer
    {
        IPAddress mIP;
        int mPort;
        TcpListener mServer;

        /// <summary>
        /// Server inizia ad ascoltare
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        public async void InAscolto(IPAddress ipaddress = null, int port = 23000)
        {
            // Controlli generali
            if (ipaddress == null)
                ipaddress = IPAddress.Any;

            if (port < 0 || port > 65535)
                port = 23000;
            
            mIP = ipaddress;
            mPort = port;
            
            mServer = new TcpListener(mIP, mPort);
            Debug.WriteLine("Server in ascolto su IP: {0} - Porta: {1}", mIP.ToString(), mPort.ToString());
            mServer.Start(); // Avvia il server
            
            Debug.WriteLine("Server avviato.");
            TcpClient client = await mServer.AcceptTcpClientAsync();

            Debug.WriteLine("Client Connesso: " + client.Client.RemoteEndPoint);
        }
    }
}
