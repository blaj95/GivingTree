using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePlaque : MonoBehaviour
{
    public Collider plaqueCollider1;
    public Collider plaqueCollider2;

    public void EnablePlaqueCollider()
    {
        plaqueCollider1.enabled = true;
        plaqueCollider2.enabled = true;
    }
}
