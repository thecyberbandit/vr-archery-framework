using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PractiseBoard : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("arrow"))
        {
            AudioManager.instance.PlaySound("hit");

            collision.gameObject.transform.position = collision.contacts[0].point;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void Start()
    {
        AudioManager.instance.PlaySound("background");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
