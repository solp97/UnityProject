using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float _speed = 0.5f;
    public float distance;


    private float elapsedtme = 0f;
    void Start()
    {

    }

    void Update()
    {
        elapsedtme += Time.deltaTime;
        if (elapsedtme * _speed >= distance)
        {
            gameObject.SetActive(false);
        }

            transform.Translate(0f, 0f, _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }

}
