using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreArea : MonoBehaviour
{
    public ParticleSystem winEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && GameController.instance.canScore == true) {
            Debug.Log("Canasta");
            winEffect.Play();
        }
    }
}
