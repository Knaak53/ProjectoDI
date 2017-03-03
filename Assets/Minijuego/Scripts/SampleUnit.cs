using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SampleUnit : Unit
{
    public Color LeadingColor;
    public override void Initialize()
    {
        base.Initialize();
        transform.position += new Vector3(0, 0, -0.5f);
       // GetComponent<Renderer>().material.color = LeadingColor;
    }
    public override void MarkAsAttacking(Unit other)
    {
        StartCoroutine(Jerk(other));
    }
    private IEnumerator Jerk(Unit other)
    {
        //GetComponent<SpriteRenderer>().sortingOrder = 6;
        var heading = other.transform.position - transform.position;
        var direction = heading / heading.magnitude;
        float startTime = Time.time;

        while (startTime + 0.25f > Time.time)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + ((direction*2) / 50f), ((startTime + 0.25f) - Time.time));
            yield return 0;
        }
        startTime = Time.time;
        while (startTime + 0.25f > Time.time)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - ((direction*2) / 50f), ((startTime + 0.25f) - Time.time));
            yield return 0;
        }
        transform.position = Cell.transform.position + new Vector3(0, 0, -0.5f);
        //GetComponent<SpriteRenderer>().sortingOrder = 4;
    }
    public override void MarkAsDefending(Unit other)
    {

        StartCoroutine(Glow(new Color(1, 0, 0, 0.25f), 0.5f));

    }
    private IEnumerator Glow(Color color, float cooloutTime)
    {
        var _renderer = GetComponentInChildren<SpriteRenderer>();
        float startTime = Time.time;

        while (startTime + cooloutTime > Time.time)
        {
            _renderer.color = Color.Lerp(new Color(1, 1, 1, 0.5f), color, (startTime + cooloutTime) - Time.time);
            yield return 0;
        }

        _renderer.color = Color.white;
    }


    public override void MarkAsDestroyed()
    {      
    }

    public override void MarkAsFinished()
    {
    }

    public override void MarkAsFriendly()
    {
        GetComponent<Renderer>().material.color = LeadingColor + new Color(0.8f, 1, 0.8f,0.2f);
    }

    public override void MarkAsReachableEnemy()
    {
            getReachableEnemySprite().enabled = true;
            GetComponent<Renderer>().material.color = LeadingColor + new Color(1,0,0,0.2f) ;
    }

    public override void MarkAsSelected()
    {
        GetComponent<Renderer>().material.color = LeadingColor + new Color(0, 1, 0, 0.2f);
    }

    public override void UnMark()
    {
        SpriteRenderer EnemyReachable = getReachableEnemySprite();
        if(EnemyReachable!=null)
            EnemyReachable.enabled = false;
            GetComponent<Renderer>().material.color = LeadingColor;
    }

    public override void OnUnitSelected()
    {
        base.OnUnitSelected();
    }

    public SpriteRenderer getReachableEnemySprite()
    {
        Collider[] colliders;
        if ((colliders = Physics.OverlapSphere(transform.position, AttackRange /* Radius */)).Length > 0)
        {
            foreach (var collider in colliders)
            {
                if ((collider.gameObject.transform.position.x == transform.position.x) && (collider.gameObject.transform.position.y == transform.position.y))
                {
                    foreach (var spriteRenderer in collider.gameObject.GetComponentsInChildren<SpriteRenderer>())
                    {
                        if (spriteRenderer.gameObject.name == "casilla ataque")
                        {
                            return spriteRenderer;
                        }
                    }
                }
            }
        }
        return null;
    }
}
