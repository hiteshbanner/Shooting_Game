using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public GameObject spiderPrefab; // Reference to the spider prefab to respawn

    private float speed = 0.75f;
    private Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
    private Rigidbody rb;
    private Vector3 initialPosition;
    private bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        // No need for movement logic here
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            Vector3 randomDirection = GetRandomDirection();
            rb.AddForce(randomDirection * speed, ForceMode.VelocityChange);

            float currentDistance = Vector3.Distance(initialPosition, transform.position);
            if (currentDistance > 0.3f)
            {
                Vector3 directionToInitial = (initialPosition - transform.position).normalized;
                rb.velocity = directionToInitial * speed;
            }

            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    Vector3 GetRandomDirection()
    {
        return directions[Random.Range(0, directions.Length)];
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector3 reverseDirection = -rb.velocity.normalized;
            rb.AddForce(reverseDirection * speed, ForceMode.VelocityChange);
        }
    }

    void OnDestroy()
    {
        if (gameObject.activeInHierarchy)
        {
            isDestroyed = true;
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
{
    Debug.Log("Respawn Coroutine Started");
    yield return new WaitForSeconds(5f);

    // Instantiate a new spider GameObject at the initial position
    GameObject newSpider = Instantiate(spiderPrefab, initialPosition, Quaternion.identity);

    // Destroy the current spider GameObject
    Destroy(gameObject);
}

}
