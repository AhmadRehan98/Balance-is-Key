using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SoundManager : MonoBehaviour
{
    public AudioSource p1FootSteps, p2FootSteps, handMove, ballLandSoft;
    private float _lastBallLand;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("volumeLevel"))
        {
            Console.Write("Previous Volume Found");
            AudioListener.volume = PlayerPrefs.GetFloat("volumeLevel");
        }
        _lastBallLand = 0.0f;
    }
    
    public void PlayFootStep(float scale=1.0f)
    {
        p1FootSteps.volume = Random.Range(0.15f, 0.25f) * scale;
        p1FootSteps.pitch = Random.Range(0.5f, 0.6f) * scale;
        if (!p1FootSteps.isPlaying)
            p1FootSteps.Play();
        
        p2FootSteps.volume = Random.Range(0.15f, 0.25f) * scale;
        p2FootSteps.pitch = Random.Range(0.6f, 0.7f) * scale;
        if (!p2FootSteps.isPlaying)
            p2FootSteps.Play();
    }

    public void PlayHandMove(float scale = 1.0f)
    {
        handMove.volume = Random.Range(0.8f, 1) * scale;
        handMove.pitch = Random.Range(0.8f, 1.1f) * scale;
        handMove.Play();
    }

    public void PlayBallRolling(float scale = 1.0f)
    {
        
    }

    public void PlayBallLandSoft(float scale = 1.0f)
    {
        if (Time.time - _lastBallLand >= 1f)
        {
            ballLandSoft.volume = Random.Range(0.8f, 1) * scale;
            ballLandSoft.pitch = Random.Range(0.8f, 1.1f) * scale;
            ballLandSoft.Play();
            _lastBallLand = Time.time;
        }
    }
}
