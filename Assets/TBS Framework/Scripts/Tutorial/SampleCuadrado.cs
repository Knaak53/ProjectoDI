using UnityEngine;

class SampleCuadrado : Square
{
    public GameObject casillaVerde;
    GameObject go;
    public SpriteRenderer spriteReachable;
    public override Vector3 GetCellDimensions()
    {
        return GetComponent<Renderer>().bounds.size;
    }
    public void Start()
    {
        spriteReachable = getSpriteReachable();

    }
    public override void MarkAsHighlighted()
    {

        spriteReachable.enabled = true;
        spriteReachable.material.color = Color.grey;
    }

    public override void MarkAsPath()
    {
        spriteReachable.enabled = true;
        spriteReachable.material.color = Color.green;
        //GetComponentInChildren<SpriteRenderer>().material.color = Color.green;
    }

    public override void MarkAsReachable()
    {
        spriteReachable.enabled = true;
        getSpriteReachable().material.color = Color.white;
        //GetComponentInChildren<SpriteRenderer>().material.color = Color.yellow;
    }

    public override void UnMark()
    {
        spriteReachable.enabled = false;
        //GetComponentInChildren<SpriteRenderer>().material.color = Color.white;
    }

    public SpriteRenderer getSpriteReachable()
    {
        foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            if (spriteRenderer.gameObject.name == "casilla de movimiento")
            {
                return spriteRenderer;
            }
        }
        return null;
    }
}

