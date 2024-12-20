using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    private int bombRemaining = 0;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

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

        position = bomb.transform.position;
        position.x=Mathf.Round(position.x);
        position.y= Mathf.Round(position.y);

        Explosion explosion = Instantiate(explosionPrefab,position,Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);
        Destroy(explosion.gameObject, explosionDuration);

        Explode(position,Vector2.up,explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);


        Destroy(bomb);
        bombRemaining++;
    }
    private void Explode(Vector2 position,Vector2 direction,int lenghth)
    {
        if(lenghth <= 0)
        {
            return;
        }
        position += direction;

        if(Physics2D.OverlapBox(position, Vector2.one/2f,0f,explosionLayerMask))
        {
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(lenghth>1 ? explosion.middle:explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);
       

        Explode(position,direction,lenghth-1);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }
}
