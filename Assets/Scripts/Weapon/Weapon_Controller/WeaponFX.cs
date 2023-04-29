using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(AudioSource))]
public class WeaponFX : MonoBehaviour
{
    public GameObject muzzleFlash;
    public Sprite[] flashSprites;
    public SpriteRenderer flashSpriteRenderer1;
    public SpriteRenderer flashSpriteRenderer2;

    public AudioClip[] shootSounds;
    public AudioClip reloadSound;

    public float duration;

    private Animation animation;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animation = GetComponent<Animation>();
        Hide();
    }

    public void ShootFX()
    {
        muzzleFlash.SetActive(true);

        Sprite s = flashSprites[Random.Range (0, flashSprites.Length)];
        flashSpriteRenderer1.sprite = s;
        flashSpriteRenderer2.sprite = s;
        audioSource.PlayOneShot(shootSounds[Random.Range (0,shootSounds.Length)]);
        animation.clip = animation.GetClip("RecoilAnimation");
        animation.Play();
        animation.clip = animation.GetClip("RecoilAnimationPistol");
        animation.Play();

        Invoke("Hide", duration);
    }

    private void Hide()
    {
        muzzleFlash.SetActive(false);
    }

    public void PistolReloadFX()
    {
        audioSource.PlayOneShot(reloadSound);
        animation.clip = animation.GetClip("ReloadAnimationPistol");
        animation.Play();
    }
    public void ReloadFX()
    {
        audioSource.PlayOneShot(reloadSound);
        animation.clip = animation.GetClip("ReloadAnimation");
        animation.Play();
    }

}
