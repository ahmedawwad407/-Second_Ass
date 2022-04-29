using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int score = 10;
    
    private void OnTriggerEnter2D(Collider2D collider2D) {
        if (collider2D.gameObject.CompareTag("MyCharacter")) {
            gameObject.SetActive(false);
          //  Destroy(collider2D.gameObject); 

          GameManager.instance.ChangeScoreValue(score);
          GameManager.instance.SaveState();
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {

    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        

    }
}
