using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  private Vector2 target;
  public float speed;

  void Start(){
    target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //弾(projectile)が出現した時、それが向かう位置はカーソルの位置と同様になる
    //あとはその位置に向かわせれば良い(下記のUpdateファンクションがそれ)。
  }

  void Update(){
    transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    //弾(projectile)の向かう先を指定(Vector2.MoveTowards)
    //現在位置(transform.position)から上で指定したTarget(カーソルの位置)へ、コンポネントで指定したスピードで向かう。
    if(Vector2.Distance(transform.position, target) < 0.2f){
      Destroy(gameObject);
      //弾の距離が、Target距離と0.2f以下になったら弾を消去する
      //こうしないとその場に一生残る。
    }
  }
}
