using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
            Debug.Log("player collider triggered");
    }
}
