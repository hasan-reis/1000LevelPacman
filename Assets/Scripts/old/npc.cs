using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    public GameObject player;
    public float range = 1f;
    public float speed = 1f;
    public LayerMask obstacles;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveToTarget(player.transform);
    }

    public void MoveToTarget(Transform _target)
    {
        Vector2 _dir = _target.position - transform.position;
        RaycastHit2D[] _hits = Physics2D.RaycastAll(transform.position, _dir, range, obstacles);

        foreach (RaycastHit2D hit in _hits)
        {
            if (hit.collider != null)
            {
                // Düşmanın hareket yönünden sağa veya sola dön
                _dir = Vector2.Lerp(_dir, Vector2.Perpendicular(_dir).normalized, Time.deltaTime);
            }
        }

        rb.velocity = _dir.normalized * speed;
    }
}
