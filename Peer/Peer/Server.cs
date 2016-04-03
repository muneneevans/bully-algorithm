
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Peer
{
    public class Server
    {
        private string serverip = "127.0.0.1";
        public int port;
        TcpListener listener;
        public void Run(int port )
        {
            try
            {

            this.port = port;
            listener = new TcpListener(port);
            listener.Start();


            while (true)
            {

                using (TcpClient client = listener.AcceptTcpClient())
                {
                    try
                    {
                        string text = ReadMessage(client);
                        MessageBox.Show(text);                        
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine(e);
                    }
                }
            }

            }
            catch (Exception)
            {                
            }
        }

        public string ReadMessage(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            BinaryReader reader = new BinaryReader(ns);

            string i = "";



            using (var stream = new MemoryStream())
            {
                byte[] buffer = new byte[2048]; // read in chunks of 2KB
                int bytesRead;
                while ((bytesRead = ns.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, bytesRead);
                }
                stream.Position = 0;
                i = Deserialize<string>(stream);

                UnpackMessage(i);
                // TODO: do something with the result
            }

            ns.Close();
            return i;
        }

        public void UnpackMessage(string message)
        {
            try
            {
                Container c = JsonConvert.DeserializeObject<Container>(message);
                switch (c.Header)
                {
                    case Constants.Me:
                        MessageBox.Show("Message from " + c.peer.id);
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate
                        {
                            

                            if (!(((App)System.Windows.Application.Current).vm.ProcessExisits(c.peer)))
                            {
                                ((App)System.Windows.Application.Current).vm.AddProcess(c.peer);


                                Container nc = new Container();
                                nc.Header = Constants.Me;
                                nc.peer = new Process() { id = port, port = port };
                                SendData(c.peer.port, JsonConvert.SerializeObject(nc));
                            }
                            
                        }));
                        
                        break;
                    case Constants.Message:
                        MessageBox.Show(c.peer.id + "has joined network");
                        break;
                }
            }
            catch (Exception)
            {

                
            }
        }

        public T Deserialize<T>(MemoryStream ms)
        {
            BinaryFormatter fm = new BinaryFormatter();
            object data = fm.Deserialize(ms);

            return (T)data;
        }

        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        public void SendData(int sendingport , string message)
        {
            try
            {
                using (TcpClient client = new TcpClient(serverip, sendingport))
                {
                    MemoryStream ms = new MemoryStream();
                    //client.Connect(ip);
                    NetworkStream ns = client.GetStream();
                    var data = Serialize(message, ms);
                    ns.Write(data, 0, data.Length);
                }
            }
            catch (Exception)
            {
                
            }
        }

        public void FindProcesses()
        {
            try
            {
                for(int i = 100; i <= 120; i++)
                {
                    Container c = new Container() ;
                    c.Header = Constants.Me;
                    c.peer = new Process() { id = port, port = port };
                    SendData(i , JsonConvert.SerializeObject(c));
                }
            }
            catch (Exception)
            {

                
            }
        }

        public byte[] Serialize(object data, MemoryStream ms)
        {
            BinaryFormatter fm = new BinaryFormatter();
            fm.Serialize(ms, data);
            return ms.ToArray();
        }
    }
}
