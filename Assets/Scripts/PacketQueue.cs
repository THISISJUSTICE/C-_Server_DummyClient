using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PacketQueue
{
    public static PacketQueue Inst {get;} = new PacketQueue();

    Queue<IPacket> packetQueue_ = new Queue<IPacket>();
    object lock_ = new object();

    public void Push(IPacket pakcet){
        lock(lock_){
            packetQueue_.Enqueue(pakcet);

        }
    }

    public IPacket Pop(){
        lock(lock_){
            if(packetQueue_.Count <= 0) return null;
            else return packetQueue_.Dequeue();
        }

    }
}
