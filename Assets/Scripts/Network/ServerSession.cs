using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using ServerCore;
using Debug = UnityEngine.Debug;

namespace DummyClient
{


	class ServerSession : PacketSession
    {
        public override void OnConnected(EndPoint endPoint)
        {
            Debug.Log($"Onconnected : {endPoint}");

        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Debug.Log($"OnDisconnected : {endPoint}");
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            PacketManager.Inst.OnRecvPacket(this, buffer, (s, p) => PacketQueue.Inst.Push(p));
        }

        public override void OnSend(int numOfBytes)
        {
            //Console.WriteLine($"Transferred bytes: {numOfBytes}");
        }
    }
}
