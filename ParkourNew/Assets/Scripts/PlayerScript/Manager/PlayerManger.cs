using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManger : MonoBehaviour
{
    PhotonView PV;
    public Vector3 LeftBound;
    public Vector3 RightBound;
    public Vector3 ChaserPosition;
    private float Zpos;
    private float Xpos;
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        LeftBound = GameObject.Find("LBound").transform.position;
        RightBound = GameObject.Find("RBound").transform.position;
        ChaserPosition = GameObject.Find("MPosition").transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        Debug.Log("Instantiate Player Controller");

        if (PhotonNetwork.IsMasterClient)
        {
            //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MutantPlayer"), Vector3.zero, Quaternion.identity);
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerControllerNeo"), ChaserPosition, Quaternion.identity);

        }
        else
        {
            RandomValueGenerator();
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerControllerNeo"), new Vector3(Xpos,LeftBound.y,Zpos), Quaternion.identity);
            Zpos = 0;
            Zpos = 0;
        }
    }

    // Update is called once per frame
    private void RandomValueGenerator()
    {
         Zpos = Random.Range(Mathf.Min(RightBound.x,LeftBound.x), Mathf.Max(RightBound.x, LeftBound.x));
         Xpos = Random.Range(Mathf.Min(RightBound.y, LeftBound.y), Mathf.Max(RightBound.y, LeftBound.y));
    }
}
