﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 direction;
    public float speed;

    private Vector2 startingPosition;
    private float timeElapsed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.startingPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        this.timeElapsed = 0;
    }

    void FixedUpdate()
    {
        float angle = Mathf.Atan2(this.direction.y, this.direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.timeElapsed += Time.fixedDeltaTime;
        rb.MovePosition(rb.position + direction.normalized * this.speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Wall")) {
            Destroy(gameObject);
        } else if (collider.CompareTag("Player")) {
            Destroy(gameObject);
            PlayerHealth.Instance.ReduceHP(1);
        }
    }
}