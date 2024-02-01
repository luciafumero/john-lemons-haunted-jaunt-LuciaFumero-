using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    bool m_IsPlayerInRange;
    public GameObject exclamation;
    public AudioSource youWereSpotted;
    public float fadeDuration = 1f;
    public float displayImageDuration = 2f;
    float m_Timer;
    bool m_HasAudioPlayed;
    float m_CatchingTimer;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Spotted(AudioSource wasSpotted, bool isSpotted)
    {
        m_Timer += Time.deltaTime;
        m_CatchingTimer = 2f;

        if (m_IsPlayerInRange)
        {
            if (isSpotted == true) 
            {
                GameObject.SetActive.CompareTag("Exclamation");
                if (m_Timer > fadeDuration + displayImageDuration)
                {
                    wasSpotted.Play();
                }
            }
        }
        else 
        {
            if (!isSpotted) 
            {
                if (m_Timer > fadeDuration + displayImageDuration)
                {
                    wasSpotted.Stop();
                }
            }
        }
        if (!m_HasAudioPlayed)
        {
            wasSpotted.Play();
            m_HasAudioPlayed = true;
        }
    }
    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}
