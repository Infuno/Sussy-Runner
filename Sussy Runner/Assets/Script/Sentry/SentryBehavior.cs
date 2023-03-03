using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryBehavior : MonoBehaviour
{
    public Animator animator;
    public Animator SpyAnimator;
    public GameObject GameOverPanel;
    bool IsLookAway = false;
    public float MinTime;
    public float MaxTime;

    public MoveSpy moveSpy;

    static bool IsSap = false;
    bool IsFail = false;
    void Start()
    {
        StartCoroutine(TimerLookAway(0.1f));
        IsFail = false;
    }
    void Update()
    {
        if(IsLookAway == false && MoveSpy.GetMoveValue() == true && IsFail == false)
        {
            SpyAnimator.SetTrigger("Detected");
            StartCoroutine(IsDetected());
            IsFail = true;
        }
    }
    public float WaitTimer()
    {
        float Timer = Random.Range(MinTime, MaxTime);
        return Timer;
    }
    IEnumerator TimerLookAway(float Timer)
    {
        if (IsSap == false)
        {
            yield return new WaitForSeconds(Timer);
            animator.SetBool("Away", true);
            PlaySentrySound();
            StartCoroutine(TimerLookPlayer(WaitTimer()));
        }
    }
    IEnumerator TimerLookPlayer(float Timer)
    {
        if (IsSap == false)
        {
            yield return new WaitForSeconds(Timer);
            animator.SetBool("Away", false);
            FindObjectOfType<AudioManager>().PlaySound("SentryAlert");
            if (IsSap == false)
            StartCoroutine(TimerLookAway(WaitTimer()));
        }
    }
    IEnumerator IsDetected()
    {
        yield return new WaitForSeconds(1f);
        GameOverPanel.SetActive(true);
    }
    public void LookAway()
    {
        IsLookAway = true;
    }
    public void LookPlayer()
    {
        IsLookAway = false;
    }
    public static void Sapping()
    {
        IsSap = true;
    }
    public void PlaySentrySound()
    {
        int RandomValue = Random.Range(1, 3);
        switch (RandomValue)
        {
            case 1:
                FindObjectOfType<AudioManager>().PlaySound("SentryScan1");
                break;
            case 2:
                FindObjectOfType<AudioManager>().PlaySound("SentryScan2");
                break;
            case 3:
                FindObjectOfType<AudioManager>().PlaySound("SentryScan3");
                break;
        }
    }
}
