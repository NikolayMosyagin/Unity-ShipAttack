using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour {

#pragma warning disable 649
    [SerializeField]
    private SpriteRenderer _sprite;
#pragma warning restore 649

    private bool _init;
    private float _height;
    private Camera _camera;

    public void Initialize(Camera camera)
    {
        this._init = true;
        
        this._camera = camera;
        this._height = this._camera.orthographicSize;
        float width = this._height * this._camera.aspect;
        this.transform.position = new Vector3(-width + this._sprite.size.x / 2.0f, 0, 0);
        
    }
	// Update is called once per frame
	void Update ()
    {
        if (!this._init)
        {
            return;
        }
        var mousePos = Input.mousePosition;
        mousePos = this._camera.ScreenToWorldPoint(mousePos);
        var pos = this.transform.position;
        var size = this._sprite.size;
        pos.y = Mathf.Clamp(mousePos.y, -this._height + size.y / 2.0f, this._height - size.y / 2.0f);
        this.transform.position = pos;
	}
}
