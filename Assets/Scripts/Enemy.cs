using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Transform playerPos;

    void Start(){
      playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
      //Playerというタグのついたゲームオブジェクトを探し出す。(GameObject.FindGameObjectWithTag("Player"))
      //その上でそのオブジェクトの位置を取得する(GetComponent<Transform>())
      //つまりplayerPosはプレーヤーの場所に関する変数。
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        //enemyをplayerPosに向けて動かす = (Vector2.MoveTowards)。
        //現在のポジション(transform.position)からプレーヤーの場所(playerPos.position)に、一定のスピード(speed * Time.deltaTime)で向かわせる。
        //speedはpublic floatなのでコンポネント上で自由な値に設定できる。
    }
}
