using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hit_sprites;

    Level level;
    GameSession gamestatus;
    int times_hit;

    private void Start()
    {
        CountBreakableBlocks();
        gamestatus = FindObjectOfType<GameSession>();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        times_hit++;
        int max_hits = hit_sprites.Length + 1;
        if (times_hit >= max_hits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int sprite_index = times_hit - 1;
        if (hit_sprites[sprite_index] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hit_sprites[sprite_index];
        }
        else
        {
            Debug.LogError("Block Sprite is missing! " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        gamestatus.AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparkleVFX();
    }

    private void TriggerSparkleVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
