using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ColZone : MonoBehaviour
{
    public string SceneToLoadAfterCol;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneToLoadAfterCol);
        }
        else
        {
            Destroy(col);   
        }
    }
}
