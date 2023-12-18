using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using ServerCore;
using DummyClient;

public class NetworkManager : MonoBehaviour
{
    ServerSession session_ = new ServerSession();

    private void Start() {
        string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            Connector connector = new Connector();

            connector.Connect(endPoint, () => { return session_; }, 1);
    }
}
