using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class ZombieAnimationSound : MonoBehaviour
{
    [SerializeField] public AudioClip[] footStepSound;
    private AudioSource soundSource;

    void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }

    public void LeftFoot()
    {
        int rand = Random.Range(1, footStepSound.Length);
        soundSource.clip= footStepSound[rand];
        soundSource.PlayOneShot(soundSource.clip);

        footStepSound[rand] = footStepSound[0];
        footStepSound[0] = soundSource.clip;
    }

    public void RightFoot()
    {
        int rand = Random.Range(1, footStepSound.Length);
        soundSource.clip = footStepSound[rand];
        soundSource.PlayOneShot(soundSource.clip);

        footStepSound[rand] = footStepSound[0];
        footStepSound[0] = soundSource.clip;
    }
}
