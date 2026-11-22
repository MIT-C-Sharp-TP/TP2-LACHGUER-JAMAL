using System;
using UnityEngine;

// A coin that the car picks up. Its collider has "Is Trigger" ticked.
// The coin spins like a flipped coin and floats up and down a little.
public class Coin : MonoBehaviour
{
    // raised when any coin is picked up
    public static event Action Collected;

    [Header("Spin")]
    [SerializeField] private float rotationSpeed = 120f;   // degrees per second

    [Header("Float (optional, set Bob Height to 0 to turn it off)")]
    [SerializeField] private float bobHeight = 0.15f;      // metres, up and down
    [SerializeField] private float bobSpeed = 2f;          // how fast it floats

    [Header("Other")]
    [SerializeField] private GameObject collectEffect;     // optional

    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        // Space.World: the coin turns around the vertical axis, like a flipped coin.
        // Without it, an upright coin would only spin inside its own face,
        // and you would see nothing move.
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.World);

        // float up and down
        Vector3 position = transform.position;
        position.y = startY + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = position;
    }

    void OnTriggerEnter(Collider other)
    {
        // only the car collects coins
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }

        Collected?.Invoke();
        Destroy(gameObject);
    }
}