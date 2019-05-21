using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Transform playerPos;
    private Player player;
    public GameObject effect;//はじけるエフェクト

    void Start(){
      playerPos = GameObject.FindGameObjectWithTag("Player").transform;
      //Playerというタグのついたゲームオブジェクトを探し出す。(GameObject.FindGameObjectWithTag("Player"))
      //その上でそのオブジェクトの位置を取得する(.transform())
      //つまりplayerPosはプレーヤーの場所に関する変数。
      player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
      //Playerというタグのついたゲームオブジェクトを探し出す。(GameObject.FindGameObjectWithTag("Player"))
      //その上で"Playerについているスクリプト（コンポネントスクリプト)を取得する (そこにHealthの情報があるから、
      //「敵と接触するとプレーヤーがダメージを受ける」機能を付ける際には、呼び出さないといけない)
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        //enemyをplayerPosに向けて動かす = (Vector2.MoveTowards)。
        //現在のポジション(transform.position)からプレーヤーの場所(playerPos.position)に、一定のスピード(speed * Time.deltaTime)で向かわせる。
        //speedはpublic floatなのでコンポネント上で自由な値に設定できる。
    }

    void OnTriggerEnter2D(Collider2D other){
      if(other.CompareTag("Player")){
        player.health--;
        Instantiate(effect, transform.position, Quaternion.identity);//敵がプレーヤーに接触すると敵に「はじけるエフェクト」発動
        Destroy(gameObject);
        //Enemyがplayerに接触すると、Playerのhealthが1減る。
        //プレーヤーに接触した敵は消滅(Destroy)する。
      }
      if(other.CompareTag("Projectile")){
        Instantiate(effect, transform.position, Quaternion.identity); //敵が玉に接触すると敵に「はじけるエフェクト」発動
        Destroy(gameObject);
        Destroy(other.gameObject);
        //弾(projectile)とEnemyが接触したら、Enemyが消える。
        //球(other.gameObject)も消える
      }
    }
}
