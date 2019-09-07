using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
  private Inventory inventory;
  public GameObject itemButton;

  private void Start(){
    inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
  }

  void OnTriggerEnter2D(Collider2D other){
    //プレイヤーが他のコリジョンと接触で発動「OnTriggerEnter2D(Collider2D other)」
    if(other.CompareTag("Player")){
      //もしプレーヤーというタグのついたタグが他のコリジョンと接触すると
      for (int i = 0; i < inventory.slots.Length; i++){
        //アイテムインベンドリーは0から始まる(i = 0)
        //容量分(inventory.slots.Length)だけピックアップできる「i < inventory.slots.Length」
        //アイテムを一つピックアップすると、アイテムが一つ増える「i++」

        if(inventory.isFull[i] == false){
          //もしインベントリーが満パンじゃなかったら、アイテムがインベントリーに入る
          inventory.isFull[i] = true;
          Instantiate(itemButton,inventory.slots[i].transform, false);
          Destroy(gameObject);
          break;
          //インベントリーが満パンになったら、ピックアップの処理が終わる
          //拾えたら、そのアイテムを消滅(destroy)させる
        }

      }
    }
  }

}
