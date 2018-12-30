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

    public IEnumerator Play()
    {
        do
        {
            yield return this.Animate();
            this.transform.position = new Vector3((this._totalCount - 1) * this._renderer.size.x, 0, 0);
        } while (true);
    }

    private IEnumerator Animate()
    {
        var start = this.transform.position;
        var end = new Vector3(-this._renderer.size.x, start.y, start.z);
        float distance = (end - start).magnitude;
        float time = distance / BackSpeed;
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
            this.transform.position = Vector3.Lerp(start, end, t / time);
            yield return null;
        }
        this.transform.position = end;
    }
}
