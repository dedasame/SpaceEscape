using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public bool isGo;

    void Update()
    {   
        if(isGo) SetCamera(); //gidiyorsa 
        else Debug.Log("GATE ULASTİ"); //sona geldiyse kamera ->> farklı tarzda kamera açıları?? belki

    }


    public void SetCamera() //kamera kosarkenki
    {
        transform.position = player.transform.position + new Vector3(0,3,-5);
    }

    public void EndCamera() //kamera sona ulasinca
    {

    }

    public void DiedCamera() //kamera palyer olunce
    {

    }

}
