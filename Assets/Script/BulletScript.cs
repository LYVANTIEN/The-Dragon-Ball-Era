using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public BoxCollider2D BulletCollider;
    public Animator BulletAnim;
    public float BulletSpeed;
    public bool hit;
    public float direction;
    private float lifeTime;
    void Start()
    {

    }

    void Update()
    {
        if (hit)
        {
            return;
        }

        float movementSpeed = BulletSpeed * Time.deltaTime;
        // Adjust the movement direction based on the 'direction' variable
        transform.Translate(movementSpeed * direction, 0, 0);
        lifeTime += Time.deltaTime;

        if (lifeTime > 3)
        {
            gameObject.SetActive(false);
        }


    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        hit = true;
        BulletCollider.enabled = false;
        if (collison.gameObject.CompareTag("Enemy"))
        { }
        BulletAnim.SetTrigger("Destroy");
    }
    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        BulletCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }

    public void Deactitive()
    {
        gameObject.SetActive(false);
    }
}
