using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public Animator animator;
    public TMP_Text _text;
    public void Show(string text, Color color)
    {
        _text.text = text;
        _text.color = color;
        animator.SetTrigger("damage"+Random.Range(1,3));
        Destroy(gameObject,2f);
    }
}
