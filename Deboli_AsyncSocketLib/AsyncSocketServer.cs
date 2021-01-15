using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.IO;

namespace Deboli_AsyncSocketLib
{
    public class AsyncSocketServer
    {
        IPAddress mIP;
        int mPort;
        TcpListener mServer;
        List<TcpClient> mClients;

        public AsyncSocketServer()
        {
            mClients = new List<TcpClient>();
        }

        /// <summary>
        /// Metodo che permette al server di ascoltare
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
            

            while (true)
            {
                TcpClient client = await mServer.AcceptTcpClientAsync();
                mClients.Add(client);
                Debug.WriteLine("Client connessi: {0}. Client Connesso {1}", mClients.Count, client.Client.RemoteEndPoint);
                RiceviMessaggio(client);
            }
        }

        public async void RiceviMessaggio (TcpClient client)
        {
            NetworkStream stream = null; // Contenuto del file
            StreamReader reader = null; // Legge e manipola i dati

            try
            {
                stream = client.GetStream();
                reader = new StreamReader(stream);
                char[] buffer = new char[512];
                int nBytes = 0; // N byte che ricevo

                while (true)
                {
                    Debug.WriteLine("In attesa di un messaggio");

                    // Ricezione messaggio asincrono
                    nBytes = await reader.ReadAsync(buffer, 0, buffer.Length);

                    if (nBytes == 0)
                    {
                        Debug.WriteLine("Client disconnesso");
                        break;
                    }
                    string recvText = new string(buffer);
                    Debug.WriteLine("N° byte: {0}. Messaggio: {1}", nBytes + recvText);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Errore " + ex.Message);
            }
        }
    }
}
