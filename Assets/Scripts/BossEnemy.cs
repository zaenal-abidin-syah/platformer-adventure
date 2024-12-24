using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject crawshotProjectile;
    public GameObject longshotProjectile;
    public float crawshotRange = 5f;
    public float longshotRange = 10f;
    public LayerMask playerLayer;
    public float speed = 2f;
    private float crawshotCooldown = 2f;
    private float longshotCooldown = 3f;
    private float crawshotTimer = 0f;
    private float longshotTimer = 0f;



    void Update()
    {
        // behaviorTree.Evaluate();
        UpdateCooldowns();
    }

    private void UpdateCooldowns()
    {
        if (crawshotTimer > 0) crawshotTimer -= Time.deltaTime;
        if (longshotTimer > 0) longshotTimer -= Time.deltaTime;
    }

    public bool IsCrawshotReady() => crawshotTimer <= 0;
    public bool IsLongshotReady() => longshotTimer <= 0;

    public void FireCrawshot()
    {
        Instantiate(crawshotProjectile, transform.position, Quaternion.identity);
        crawshotTimer = crawshotCooldown;
    }

    public void FireLongshot()
    {
        Instantiate(longshotProjectile, transform.position, Quaternion.identity);
        longshotTimer = longshotCooldown;
    }

    public void MoveRandomly()
    {
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, crawshotRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, longshotRange);
    }
}