using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using ServerCore;
using DummyClient;
using System;

public class NetworkManager : MonoBehaviour
{
    ServerSession session_ = new ServerSession();

    public void Send(ArraySegment<byte> sendBuff){
        session_.Send(sendBuff);
    }

    private void Start() {
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

        Connector connector = new Connector();

        connector.Connect(endPoint, () => { return session_; }, 1);
    }

    private void Update() {
        List<IPacket> list = PacketQueue.Inst.PopAll();
        foreach(IPacket packet in list){
            PacketManager.Inst.HandlePacket(session_, packet);
        }
        
    }

    
}
