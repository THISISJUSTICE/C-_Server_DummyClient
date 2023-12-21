using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    MyPlayer myplayer_;
    Dictionary<int, Player> players_ = new Dictionary<int, Player>();

    public static PlayerManager Inst {get;} = new PlayerManager();

    public void Add(S_PlayerList packet){
        Object obj = Resources.Load("Player");

        foreach (S_PlayerList.Player p in packet.players){
            Debug.Log($"PlayerManager.Add: p.isSelf={p.isSelf}, p.playerID={p.playerID}");
            GameObject go = Object.Instantiate(obj) as GameObject;
            if(p.isSelf){
                MyPlayer myPlayer = go.AddComponent<MyPlayer>();
                myPlayer.playerID = p.playerID;
                myPlayer.transform.position = new Vector3(p.posX, p.posY, p.posZ);
                myplayer_ = myPlayer;
            }
            else{
                Player player = go.AddComponent<Player>();
                player.playerID = p.playerID;
                player.transform.position = new Vector3(p.posX, p.posY, p.posZ);
                if(!players_.ContainsKey(player.playerID))
                    players_.Add(player.playerID, player);
            }
        }
    }

    public void EnterGame(S_BroadCastEnterGame packet){
        if(packet.playerID == myplayer_.playerID) return;
        Object obj = Resources.Load("Player");
        GameObject go = Object.Instantiate(obj) as GameObject;

        Player player = go.AddComponent<Player>();
        players_.Add(packet.playerID, player);
        player.transform.position = new Vector3(packet.posX, packet.posY, packet.posZ);
    }

    public void LeaveGame(S_BroadCastLeaveGame packet){
        if(myplayer_.playerID == packet.playerID){
            GameObject.Destroy(myplayer_.gameObject);
            myplayer_ = null;
        }
        else{
            Player player= null;
            if(players_.TryGetValue(packet.playerID, out player)){
                GameObject.Destroy(player);
                players_.Remove(packet.playerID);
            }
        }
    }

    public void Move(S_BroadCastMove packet){        
        if(myplayer_.playerID == packet.playerID){
            myplayer_.transform.position = new Vector3(packet.posX, packet.posY, packet.posZ);
        }
        else{
            Player player = null;
            if(players_.TryGetValue(packet.playerID, out player)){
                player.transform.position = new Vector3(packet.posX, packet.posY, packet.posZ);
            }
        }
    }

}
