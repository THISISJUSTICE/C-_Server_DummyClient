using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : Player
{
    NetworkManager network_;
    private void Start() {
        network_ = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        StartCoroutine(CoSendPacket());
    }

    IEnumerator CoSendPacket(){
        while(true){
            yield return new WaitForSeconds(0.25f);

            C_Move movePacket = new C_Move();
            movePacket.posX = Random.Range(-50, 50);
            movePacket.posY = 0;
            movePacket.posZ = Random.Range(-50, 50);

            network_.Send(movePacket.Write());
        }
    }
}
