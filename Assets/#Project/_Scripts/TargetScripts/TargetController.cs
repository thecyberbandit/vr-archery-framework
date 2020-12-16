using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

    public ParticleSystem hitParticle;


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "arrow")
        {
            hitParticle.transform.position = transform.position;
            AudioManager.instance.PlaySound("pop");
            Destroy(gameObject);

            hitParticle.Play();

            EventManager.TriggerEvent("TargetHit");            
        }

        else if (other.gameObject.tag == "ground")
        {
            hitParticle.transform.position = transform.position;
            AudioManager.instance.PlaySound("pop");
            Destroy(gameObject);

            hitParticle.Play();
        }
    }

    private void Start()
    {
        hitParticle = GameObject.Find("BeamOfLight").GetComponent<ParticleSystem>();
    }
}
