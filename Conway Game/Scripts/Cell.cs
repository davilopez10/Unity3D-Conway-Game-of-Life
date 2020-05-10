
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour
{
    public bool IsDie
    {
        get
        {
            return isDie;
        }

        set
        {
            isDie = value;
            renderer.color = isDie ? Color.black : Color.white;
        }
    }

    [SerializeField]
    private SpriteRenderer renderer;

    private bool isDie = true;

    private void OnMouseDown()
    {
        IsDie = !isDie;
    }

}
