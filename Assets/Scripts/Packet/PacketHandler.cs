using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DummyClient;
using ServerCore;
using UnityEngine;


class PacketHandler
{
    public static void S_ChatHandler(PacketSession session, IPacket packet)
    {
        S_Chat chatPacket = packet as S_Chat;
        ServerSession serverSession = session as ServerSession;

        Debug.Log(chatPacket.chat);

        GameObject go = GameObject.Find("Player");
        if(go == null){
            Debug.Log("Player Not found");
        }
        else Debug.Log("Player found");
    }

}

