using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public AudioClip[] foostepVariants;
    public AudioSource footstepAudio;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rigidBody.velocity != Vector2.zero)
            Play();
    }

    void Play()
    {
        if (footstepAudio.isPlaying)
            return;
            
        footstepAudio.clip = foostepVariants[Random.Range(0, foostepVariants.Count())];
        footstepAudio.Play();
    }
}
