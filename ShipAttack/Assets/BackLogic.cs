using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackLogic : MonoBehaviour {

    private const int BackSpeed = 3;

#pragma warning disable 649
    [SerializeField]
    private SpriteRenderer _renderer;
#pragma warning restore 649

    private int _totalCount;

    public Vector2 backSize
    {
        get { return this._renderer.size; }
    }

    public void Initialize(Sprite sprite, int totalCount)
    {
        this._renderer.sprite = sprite;
        this._totalCount = totalCount;
    }

    public void Run(float delta)
    {
        var position = this.transform.position;

        position.x -= delta * BackSpeed;
        if (position.x <= -this._renderer.size.x)
        {
            position.x = (this._totalCount - 1) * this._renderer.size.x;
        }
        this.transform.position = position;
    }

}
