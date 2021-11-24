using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SoundManager : MonoBehaviour
{
    public AudioSource[] p1FootStepsDirt, p2FootStepsDirt,
                         p1FootStepsStone, p2FootStepsStone;
    public AudioSource handMove, ballLandSoft, torchBurn, doorCrack;
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
    
    public void PlayFootStepsDirt(float scale=1.0f)
    {
        int rand = Random.Range(0, 3);
        p1FootStepsDirt[rand].volume = Random.Range(0.1f, 0.2f) * scale;
        p1FootStepsDirt[rand].pitch = Random.Range(0.5f, 0.6f) * scale;
        bool stepsPlayingNow = false;
        foreach (var dirt in p1FootStepsDirt) {
            if (dirt.isPlaying)
                stepsPlayingNow = true;
        }
        foreach (var stone in p1FootStepsStone) {
            if (stone.isPlaying)
                stepsPlayingNow = true;
        }
        if (!stepsPlayingNow)
            p1FootStepsDirt[rand].Play();
        
        p2FootStepsDirt[rand].volume = Random.Range(0.1f, 0.2f) * scale;
        p2FootStepsDirt[rand].pitch = Random.Range(0.6f, 0.7f) * scale;
        stepsPlayingNow = false;
        foreach (var dirt in p2FootStepsDirt) {
            if (dirt.isPlaying)
                stepsPlayingNow = true;
        }
        foreach (var stone in p2FootStepsStone) {
            if (stone.isPlaying)
                stepsPlayingNow = true;
        }
        if (!stepsPlayingNow)
            p2FootStepsDirt[rand].Play();
    }

    public void PlayFootStepsStone(float scale=1.0f)
    {
        int rand = Random.Range(0, 3);
        p1FootStepsStone[rand].volume = Random.Range(0.1f, 0.2f) * scale;
        p1FootStepsStone[rand].pitch = Random.Range(0.5f, 0.6f) * scale;
        bool stepsPlayingNow = false;
        foreach (var dirt in p1FootStepsDirt) {
            if (dirt.isPlaying)
                stepsPlayingNow = true;
        }
        foreach (var stone in p1FootStepsStone) {
            if (stone.isPlaying)
                stepsPlayingNow = true;
        }
        if (!stepsPlayingNow)
            p1FootStepsStone[rand].Play();
        
        p2FootStepsStone[rand].volume = Random.Range(0.1f, 0.2f) * scale;
        p2FootStepsStone[rand].pitch = Random.Range(0.6f, 0.7f) * scale;
        stepsPlayingNow = false;
        foreach (var dirt in p2FootStepsDirt) {
            if (dirt.isPlaying)
                stepsPlayingNow = true;
        }
        foreach (var stone in p2FootStepsStone) {
            if (stone.isPlaying)
                stepsPlayingNow = true;
        }
        if (!stepsPlayingNow)
            p2FootStepsStone[rand].Play();
    }
    
    public void PlayHandMove(float scale = 1.0f)
    {
        handMove.volume = Random.Range(0.1f, 0.2f) * scale;
        handMove.pitch = Random.Range(0.95f, 1.05f) * scale;
        if (!handMove.isPlaying) {
            // Debug.Log("playing now: " + Time.time);
            handMove.Play();
        }
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
