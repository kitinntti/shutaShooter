using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;//プレーヤーと敵の間で、敵が動きを止める距離
    public float retreatDistance;//プレーヤーと敵の間で、敵がプレーヤーから逃げ出す距離

    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    //敵に弾を打たせる。やり方はSpawnerと一緒

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
      timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {

        if(Vector2.Distance (transform.position, playerPos.position) > stoppingDistance){
          transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
          //もし敵の位置(transform.position)とプレーヤーの位置（playerPos.position）との距離が、stoppingDistanceよりも大きければ(遠ければ)
          //enemyをplayerPosに向けて動かす = (Vector2.MoveTowards)。
          //現在のポジション(transform.position)からプレーヤーの場所(playerPos.position)に、一定のスピード(speed * Time.deltaTime)で向かわせる。
          //speedはpublic floatなのでコンポネント上で自由な値に設定できる。
        }else if (Vector2.Distance (transform.position, playerPos.position) < stoppingDistance && Vector2.Distance (transform.position, playerPos.position) > retreatDistance){
          //もし敵の位置がプレーヤーの位置がstoppingDistanceよりも小さく、なおかつretreatDistanceよりも大きければ
          transform.position = this.transform.position;
          //敵はその場に静止する

        }else if (Vector2.Distance (transform.position, playerPos.position) < retreatDistance){
          transform.position = Vector2.MoveTowards(transform.position, playerPos.position, -speed * Time.deltaTime);
          //もし敵の位置とプレーヤーの位置がretreatDistanceよりも小さければ(近ければ)、敵はプレーヤーから離れていく
        }

        if(timeBtwShots <= 0){
          Instantiate(projectile, transform.position, Quaternion.identity);
          timeBtwShots = startTimeBtwShots;
        }else{
          timeBtwShots -= Time.deltaTime;
        }
        //敵に弾を一定の間隔で撃たせる。Spawnerとやり方は一緒。


    }

    void OnTriggerEnter2D(Collider2D other){
      if(other.CompareTag("Player")){
        player.health--;
        Instantiate(effect, transform.position, Quaternion.identity);//敵がプレーヤーに接触すると敵に「はじけるエフェクト」をその地点(transform.position)で発動
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
