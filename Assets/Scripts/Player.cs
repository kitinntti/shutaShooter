using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public int health = 10;
    //プレーヤーの体力。とりあえず10に設定。
    public Animator animator;

    public Text healthDisplay;
    //テキストで体力(healthDisplay)を表示する。
    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        //moveInputで縦横のインプットを取得
        //moveVelocityでインプットに対してどれだけ動くか、すなわちspeedを設定する.
        //speedはpublic floatだからコンポネントで10にでも10000にでも好きに調整できる。

        healthDisplay.text = "HEALTH :" + health;
        //これで「HealthDisplay」に現在のhealthが表示される。

        if(health <= 0){
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
          //プレーヤーの体力が0以下になったら、シーンをリロードする。
        }

    }

    void FixedUpdate(){
      rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
      //Playerの場所(rb)を、moveVelocityのスピードで、Inputが押された時間(Time.DeltaTime)分だけ動かす。
      //Time.fixedDeltaTimeはフレームレートに関わらず動かせるという意味
      //Unityのメソッドは1フレーム毎に処理される。仮に1フレーム毎に[1m]右に動くとした場合、
      //フレームレート60の機器では1秒間に右に60m動くがフレームレート30の機器では1秒間に右に30mしか動かない。
      //この問題をなくすためにフレームレートに関わらず動かせるTime.DeltaTimeを使う。
    }

}
