using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{

    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1);

    private void Update(){

        //Debug.Log(OwnerClientId + "; rand: " + randomNumber.Value);

        if(!IsOwner) return;

        if(Input.GetKeyDown(KeyCode.T)){
            //randomNumber.Value = Random.Range(0, 100);
            TestServerRpc(new ServerRpcParams());
        }
        Vector3 moveDir = new Vector3(0,0,0);

        if(Input.GetKey(KeyCode.W)) moveDir.z = +1f;
        if(Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        if(Input.GetKey(KeyCode.A)) moveDir.x = +1f;
        if(Input.GetKey(KeyCode.D)) moveDir.x = -1f;

        float moveSpeed = 3f;
        transform.position += moveDir*moveSpeed*Time.deltaTime;

    }

    [ServerRpc]
    private void TestServerRpc(ServerRpcParams serverRpcParams){
        Debug.Log("TestServerRpc Id: " + OwnerClientId + " param: " + serverRpcParams.Receive.SenderClientId);
    }

}
