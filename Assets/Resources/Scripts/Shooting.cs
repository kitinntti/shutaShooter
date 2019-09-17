using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
  public GameObject shot;
    public GameObject shot2;
    public GameObject shot3;
    private Transform playerPos;
  float time = 0;

    //連射用に作った玉発射用メソッド
    public void shots()
    {
        Instantiate(shot, playerPos.position, Quaternion.identity);
    }


    void Start(){
    playerPos = GetComponent<Transform>();
    }

  void Update(){

        //if (Input.GetMouseButtonDown(0)){
        //    Instantiate(shot, playerPos.position, Quaternion.identity);
        //    //MouseButtonは0が左クリック、1が右クリック、3がマウスホイールのクリック
        //    //マウスを左クリックすると、shotというゲームオブジェクトがプレーヤーの位置(playerPos)から出てくる。
        //}

        if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime; // 秒指定なので
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (time < 1.0f)
            {
                // 通常ショット   
                Instantiate(shot, playerPos.position, Quaternion.identity);
            }
            else
            {
                //無理やり3連ショット
                //通常で一発撃った後にInvokeメソッドで遅延させてあと２発撃ってる
                Instantiate(shot, playerPos.position, Quaternion.identity);
                Invoke("shots", 0.1f);
                Invoke("shots", 0.2f);





            }

            time = 0;
        }

    }

}
