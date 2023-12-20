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

    private void Start() {
        string host = Dns.GetHostName();
        IPHostEntry ipHost = Dns.GetHostEntry(host);
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

        Connector connector = new Connector();

        connector.Connect(endPoint, () => { return session_; }, 1);
        StartCoroutine(CoSendPacket());
    }

    private void Update() {

        IPacket packet = PacketQueue.Inst.Pop();
        if(packet != null){
            PacketManager.Inst.HandlePacket(session_, packet);
        }
    }

    IEnumerator CoSendPacket(){
        while(true){
            yield return new WaitForSeconds(3);

            C_Chat chatPacket = new C_Chat();
            chatPacket.chat = "Hello Unity!";
            ArraySegment<byte> segment = chatPacket.Write();

            session_.Send(segment);
        }
    }
}
