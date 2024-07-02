using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ColZone : MonoBehaviour
{
    public string SceneToLoadAfterCol;
    public bool WillKill;

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (WillKill)  
            { 
                player.curHealth = 0;
                return;
            }

            if (!WillKill)
                SceneManager.LoadScene(SceneToLoadAfterCol);           
        }
        else
        {
            col.gameObject.SetActive(false);
            //Destroy(col);   
        }
    }
}
