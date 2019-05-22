using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotProjectile : MonoBehaviour
{
  public float speed;
  private Transform playerPos;
  private Vector2 target;
  public GameObject effect;//はじけるエフェクト
  private Player player;

  void Start(){
    playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    target = new Vector2(playerPos.position.x, playerPos.position.y);
  }
  void Update(){
    transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

    if(transform.position.x == target.x && transform.position.y == target.y){
      DestroyProjectile();
    }

  }

  void OnTriggerEnter2D(Collider2D other){
    if(other.CompareTag("Player")){
      player.health--;
      Instantiate(effect, transform.position, Quaternion.identity);//弾がプレーヤーに接触すると敵に「はじけるエフェクト」をその地点(transform.position)で発動
      DestroyProjectile();
    }
  }

  void DestroyProjectile(){
    Destroy(gameObject);
  }

}
