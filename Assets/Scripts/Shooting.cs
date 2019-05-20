using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
  public GameObject shot;
  private Transform playerPos;

  void Start(){
    playerPos = GetComponent<Transform>();
  }

  void Update(){
    if(Input.GetMouseButtonDown(0)){
      Instantiate(shot, playerPos.position, Quaternion.identity);
    //MouseButtonは0が左クリック、1が右クリック、3がマウスホイールのクリック
    //マウスを左クリックすると、shotというゲームオブジェクトがプレーヤーの位置(playerPos)から出てくる。
    }
  }

}
