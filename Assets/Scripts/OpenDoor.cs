using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public List<Enemy> enemies;
    void Update()
    {
        if(enemies == null)
        {
            transform.Translate(Vector3.up* 25* Time.deltaTime);
        }
    }
}
