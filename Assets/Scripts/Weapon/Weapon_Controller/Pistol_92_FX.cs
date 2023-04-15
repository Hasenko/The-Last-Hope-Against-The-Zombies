using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pistol_92_FX : MonoBehaviour
{
    public GameObject muzzleFlash;
    public Sprite[] flashSprites;
    public SpriteRenderer flashSpriteRenderer1;
    public SpriteRenderer flashSpriteRenderer2;

    public AudioClip[] shootSounds;
    public AudioClip reloadSound;

    public float duration;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource> ();
        Hide ();
    }

    public void Display()
    {
        muzzleFlash.SetActive (true);

        Sprite s = flashSprites[Random.Range (0, flashSprites.Length)];
        flashSpriteRenderer1.sprite= s;
        flashSpriteRenderer2.sprite= s;

        Invoke("Hide", duration);
    }

    public void Hide()
    {
        muzzleFlash.SetActive (false);
    }

}
