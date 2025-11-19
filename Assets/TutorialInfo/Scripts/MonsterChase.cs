using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChase : MonoBehaviour
{
    public Rigidbody MonsterRigid;
    public Transform MonsterTrans, playTrans;
    public int MonsterSpeed;

    void FixedUpdate()
    {
        MonsterRigid.linearVelocity = transform.forward * MonsterSpeed * Time.deltaTime;
    }
    void Update()
    {
        MonsterTrans.LookAt(playTrans);
    }
}

