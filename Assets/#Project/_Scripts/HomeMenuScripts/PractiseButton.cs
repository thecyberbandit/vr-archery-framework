using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PractiseButton : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("arrow"))
        {
            AudioManager.instance.PlaySound("play");
            SceneManager.LoadScene(2);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(2);
        }
    }
}
