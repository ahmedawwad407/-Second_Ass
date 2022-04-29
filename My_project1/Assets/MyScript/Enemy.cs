using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]

    private int damage = 10;


     private void OnCollisionEnter2D(Collision2D collision2D ){

         if(collision2D.gameObject.CompareTag("MyCharacter")){
           //var positionCharacter =  GameObject.FindGameObjectWithTag("MyCharacter").transform.position;

           
            GameManager.instance.PlayerGotDamged(damage);
         }
    }
    
}
