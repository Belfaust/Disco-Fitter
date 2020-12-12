using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOfsetAnimation : MonoBehaviour
{
    Animator m_Animator;

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.SetFloat("offset", Random.Range(0f, 1f));
    }

    void OnEnable()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.SetFloat("offset", Random.Range(0f, 1f));
    }
}
