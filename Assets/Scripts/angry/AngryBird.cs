using UnityEngine;

public class AngryBird : MonoBehaviour
{
    private Animator _anim;
    private PolygonCollider2D _col;

    public float health = 2f;
    public GameObject EffectDie;

    private int maxCnt = 0;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _col = GetComponent<PolygonCollider2D>();
        _anim.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > health)
        {
            Die();
        }
    }

    public void Die()
    {
        _anim.enabled = true;
        _col.enabled = false;
        if (maxCnt < 1)
        {
            maxCnt++;
            ManageAngry.instAM.enemiesDead++;
        }
        Invoke("dieWait", 0.3f);
    }

    public void dieWait()
    {
        Destroy(gameObject);
    }
}