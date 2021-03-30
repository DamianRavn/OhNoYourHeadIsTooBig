using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speed = 1f;
    public AudioClip coinSound;
    Vector3 pos;
    Vector3 rot;

    private void Update()
    {
        rot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rot.x, rot.y += 1, rot.z);
        pos = transform.position;
        pos.y += Mathf.Sin(Time.timeSinceLevelLoad * speed) * Time.deltaTime;
        transform.position = pos;

    }
    private void OnTriggerEnter(Collider other)
    {
        TriggerHandler handler = other.GetComponent<TriggerHandler>();
        if (handler != null)
        {
            handler.HandleCoinTrigger();
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            Destroy(gameObject);
        }
    }
}
