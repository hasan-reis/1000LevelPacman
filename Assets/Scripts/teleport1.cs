using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport1 : MonoBehaviour
{
    public Transform teleportLocation;
    public float pasifTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = teleportLocation.position;
            StartCoroutine(activeTelLoc(teleportLocation.gameObject));
        }
    }

    IEnumerator activeTelLoc(GameObject teleportLocation)
    {
        teleportLocation.SetActive(false);
        yield return new WaitForSeconds(pasifTime);
        teleportLocation.SetActive(true);
    }
}
