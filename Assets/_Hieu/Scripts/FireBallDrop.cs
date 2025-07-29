using System;
using System.Collections;
using System.Collections.Generic;
using _Dat;
using UnityEngine;

public class FireBallDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    
    [SerializeField]
    SpriteRenderer spriteRenderer;

    private GameObject clone;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        clone = Instantiate(bullet,transform.position + Vector3.up * 10,bullet.transform.rotation);
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = Color.clear;
    }

    private void Update()
    {
        if (!clone)
        {
            return;
        }
        clone.transform.position -= Vector3.up * (10 * Time.deltaTime);
        if (clone.transform.position.y < transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
