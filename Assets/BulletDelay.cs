using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDelay : MonoBehaviour
{
    public float delay;

    [SerializeField]
    private GameObject vfx;

    private Rigidbody2D rb;
    IEnumerator Start()
    {
        rb = GetComponent<Rigidbody2D>();
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1);
        var clVfx = Instantiate(vfx,transform.position,Quaternion.identity);
        Destroy(clVfx,1);
        Destroy(gameObject);
    }
}
