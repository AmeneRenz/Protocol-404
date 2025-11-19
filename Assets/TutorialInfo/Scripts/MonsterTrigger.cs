using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour
{
    public GameObject Monster;
    public Collider collision1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Monster.SetActive(true);
            collision1.enabled = false;
        }
    }
}