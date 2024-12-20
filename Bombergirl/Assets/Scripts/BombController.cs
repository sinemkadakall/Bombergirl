using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    private int bombRemaining = 0;

    private void OnEnable()
    {
        bombRemaining = bombAmount;
    }

    private void Update()
    {
        if(bombRemaining > 0 && Input.GetKey(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }

    private  IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x=Mathf.Round(position.x);
        position.y=Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab,position,Quaternion.identity);
        bombRemaining--;
        yield return new WaitForSeconds(bombFuseTime);

        Destroy(bomb);
        bombRemaining++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }
}
