using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource p1FootSteps, p2FootSteps, handMove;
    // Start is called before the first frame update
    void Start()
    {
        // FootSteps = gameObject.GetComponent<AudioSource>();
        // MovingHand = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootStep(float scale=1.0f)
    {
        p1FootSteps.volume = Random.Range(0.5f, 0.9f) * scale;
        p1FootSteps.pitch = Random.Range(0.5f, 0.7f) * scale;
        if (!p1FootSteps.isPlaying)
            p1FootSteps.Play();
        
        p2FootSteps.volume = Random.Range(0.5f, 0.9f) * scale;
        p2FootSteps.pitch = Random.Range(0.5f, 0.7f) * scale;
        if (!p2FootSteps.isPlaying)
            p2FootSteps.Play();
    }

    public void PlayHandMove(float scale = 1.0f)
    {
        handMove.volume = Random.Range(0.8f, 1) * scale;
        handMove.pitch = Random.Range(0.8f, 1.1f) * scale;
        handMove.Play();
    }
}
