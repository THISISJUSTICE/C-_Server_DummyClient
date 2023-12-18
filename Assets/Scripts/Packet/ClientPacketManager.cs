using System;
using System.Collections.Generic;
using ServerCore;

class PacketManager
{
    #region Singleton
    static PacketManager instance_ = new PacketManager();
    public static PacketManager Inst { get {return instance_;} }
    #endregion

    PacketManager() {
        Register();
    }

    Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>> onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>>();
    Dictionary<ushort, Action<PacketSession, IPacket>> handler_ = new Dictionary<ushort, Action<PacketSession, IPacket>>();

    public void Register() {
      
        onRecv.Add((ushort)PacketID.S_Chat, MakePacket<S_Chat>);
        handler_.Add((ushort)PacketID.S_Chat, PacketHandler.S_ChatHandler);

    }

    public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer) {
        ushort count = 0;
        ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
        count += 2;
        ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
        count += 2;

        Action<PacketSession, ArraySegment<byte>> action = null;
        if (onRecv.TryGetValue(id, out action)) {
            action.Invoke(session, buffer);
        }

    }

    void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket, new() {
        T pkt = new T();
        pkt.Read(buffer);

        Action<PacketSession, IPacket> action = null;
        if (handler_.TryGetValue(pkt.Protocol, out action)) {
            action.Invoke(session, pkt);
        }
    }
}
