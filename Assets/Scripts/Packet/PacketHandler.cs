using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DummyClient;
using ServerCore;
using UnityEngine;
using Debug = UnityEngine.Debug;


class PacketHandler
{
    public static void S_ChatHandler(PacketSession session, IPacket packet)
    {
        S_Chat chatPacket = packet as S_Chat;
        ServerSession serverSession = session as ServerSession;

        if(chatPacket.playerID == 1)
            Debug.Log(chatPacket.chat);
        
    }

}

