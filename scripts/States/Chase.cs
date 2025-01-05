using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    private AudioSource audioSource;    
    public AudioClip[] roarClips;
    private Animator animationController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animationController = GetComponent<Animator>();
    }

    public void Roar()
    {
        if (roarClips != null && audioSource != null)
        {
            int Index = Random.Range(0, roarClips.Length);
            AudioClip clip = roarClips[Index];
            audioSource.PlayOneShot(clip);
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        if (animationController != null)
        {
            animationController.SetBool("isRunning", true);
        }
        if (roarClips.Length != 0)
        {
            Roar();
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (animationController != null)
        {
            animationController.SetBool("isRunning", false);
        }
    }

    public override void Update()
    {
        if (agent.target == null)
        {
            return;
        }
        if (animationController != null)
        {
            animationController.SetBool("isRunning", true);
        }

        var direction = agent.target.transform.position - gameObject.transform.position;
        direction.Normalize();
        movementController.Move(gameObject.transform.InverseTransformDirection(direction));
    }

}