using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpy : MonoBehaviour
{
    public Animator animator;
    public Animator SentryAnimator;
    static bool IsMoving = false;
    static bool IsSap = false;
    public GameObject Sapper;
    public GameObject CompletePanel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetBool("IsMoving", true);
            IsMoving = true;
            return;
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            animator.SetBool("IsMoving", false);
            IsMoving = false;
            return;
        }
        if(IsMoving == true && IsSap == false)
        {
            this.transform.localPosition += new Vector3(-0.01f, 0, 0);
        }
        if (this.transform.position.x <= -6)
        {
            IsSap = true;
            this.transform.localPosition += new Vector3(-0.01f, 0, 0);
            animator.SetBool("IsMoving", true);
            SentryAnimator.SetTrigger("Sap");
            Sapper.active = true;
            SentryBehavior.Sapping();
            StartCoroutine(IsComplete());
        }
    }
    public static bool GetMoveValue()
    {
        return IsMoving;
    }
    public void Disapear()
    {
        Destroy(gameObject);
    }
    IEnumerator IsComplete()
    {
        yield return new WaitForSeconds(1f);
        CompletePanel.SetActive(true);
    }

    public void ButtonON()
    {
        animator.SetBool("IsMoving", true);
        IsMoving = true;
        return;
    }
    public void ButtonOFF()
    {
        animator.SetBool("IsMoving", false);
        IsMoving = false;
        return;
    }
}
