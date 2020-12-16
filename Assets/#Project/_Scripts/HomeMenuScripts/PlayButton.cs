using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("arrow"))
        {
            AudioManager.instance.PlaySound("play");
            SceneManager.LoadScene("archeryScene");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("archeryScene");
        }
    }
}
