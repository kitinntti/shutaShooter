using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵をスポーンスポットから一定の間隔でスポーンさせる
public class Spawner : MonoBehaviour
{
  public GameObject enemy;
  //enemyというゲームオブジェクトを作る
  public Transform[] spawnSpots;
  //spawnSpotsをランダムに選択する
  private float timeBtwSpawns;
  public float startTimeBtwSpawns;
  //敵をスポーンさせるスパン
  //startTimeBtWSpawnsから何秒でtimeBtwSpawnsが実行されるか。秒数はpublic floatなのでコンポネント上で設定できる

  void Start(){
    timeBtwSpawns = startTimeBtwSpawns;
  }

  void Update(){
    if(timeBtwSpawns <= 0){　
      int randPos = Random.Range(0, spawnSpots.Length - 1);
      Instantiate(enemy,spawnSpots[randPos].position,Quaternion.identity);
      timeBtwSpawns = startTimeBtwSpawns;
      //randPosは、複数ある敵をスポーンさせる位置(spawnSpots.Length)の中から一つをランダムに選ぶ。
      //選んだ位置(spawnSpots[randPos].position)からenemyを出現(instantiate)させる
      //最後に、スポーンさせたらTimeBtwSpawnsをリセットさせる。(timeBtwSpawns = startTimeBtwSpawns)
      //こうしないと1フレーム毎に敵がスポーンされるのでゲームがクラッシュする。
    }else{
      timeBtwSpawns -= Time.deltaTime;
    }
  }
}
