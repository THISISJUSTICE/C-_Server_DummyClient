using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PacketQueue
{
    public static PacketQueue Inst {get;} = new PacketQueue();

    Queue<IPacket> packetQueue_ = new Queue<IPacket>();
    object lock_ = new object();

    public void Push(IPacket packet){
        lock(lock_){
            packetQueue_.Enqueue(packet);

        }
    }

    public IPacket Pop(){
        lock(lock_){
            if(packetQueue_.Count <= 0) return null;
            else return packetQueue_.Dequeue();
        }

    }

    public List<IPacket> PopAll(){
        List<IPacket> list = new List<IPacket>();

        lock(lock_){
            while(packetQueue_.Count>0){
                list.Add(packetQueue_.Dequeue());
            }
        }

        return list;
    }

}
