using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private bool homing;
    private Transform target;
    private float speed = 15f;
    private float aliveTimer = 5f;
    private float rocketStrength = 15f;

    // Start is called before the first frame update
    public void Fire(Transform homingTarget)
    {
        target = homingTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (homing && target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (target != null)
        {
            if (col.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidbody = col.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -col.contacts[0].normal;
                targetRigidbody.AddForce(away * rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
