using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    public GameObject muzzleFlash;
    public Sprite[] flashSprites;
    public SpriteRenderer flashSpriteRenderer1;
    public SpriteRenderer flashSpriteRenderer2;

    public float duration;

    private void Start()
    {
        Hide();
    }

    public void Displat()
    {
        muzzleFlash.SetActive(true);

        Sprite s = flashSprites[Random.Range (0, flashSprites.Length)];
        flashSpriteRenderer1.sprite = s;
        flashSpriteRenderer2.sprite = s;

        Invoke("Hide", duration);
    }

    public void Hide()
    {
        muzzleFlash.SetActive(false);
    }
}
