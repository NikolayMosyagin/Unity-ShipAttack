using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipType
{
    Common = 0,
    Boss = 1,
}

public class Ship : MonoBehaviour {

#pragma warning disable 649
    [SerializeField]
    private ShipType _type;
    [SerializeField]
    private SpriteRenderer _sprite;
#pragma warning restore 649

    public Vector2 size
    {
        get { return this._sprite.size; }
    }

    private List<Vector2> _moves;

    private static Ship[] s_palette;

    private static void LoadPalette()
    {
        if (s_palette != null)
        {
            return;
        }
        s_palette = Resources.LoadAll<Ship>("Ships");
    }

    public static Ship FindShip(ShipType type)
    {
        Ship result = null;
        LoadPalette();
        for (int i = 0; !result && i < s_palette.Length; ++i)
        {
            if (s_palette[i].type == type)
            {
                result = s_palette[i];
            }
        }
        return result;
    }

    public ShipType type
    {
        get { return this._type; }
    }

    public void Play()
    {
        float halfHeight = GameLogic.instance.height / 2.0f;
        float halfWidth = GameLogic.instance.width / 2.0f;
        float posY = UnityEngine.Random.Range(-halfHeight + this.size.y / 2.0f, halfHeight - this.size.y / 2.0f);
        this.transform.position = new Vector3(halfWidth + this.size.x / 2.0f, posY, 0);
        this._moves = new List<Vector2>(3)
        {
            new Vector2(halfWidth - this.size.x / 2.0f, posY),
            new Vector2(0, posY),
            new Vector2(0, posY < 0 ? -halfHeight - this.size.y / 2.0f : halfHeight + this.size.y / 2.0f),
        };

        this.gameObject.SetActive(true);
        this.StartCoroutine(this.Run());
    }

    private IEnumerator Run()
    {
        for (int i = 0; i < this._moves.Count; ++i)
        {
            Vector3 start = this.transform.position;
            Vector3 end = this._moves[i];
            float distance = (end - start).magnitude;
            float time = distance / 5.0f;
            float t = 0.0f;
            while (t < time)
            {
                t += Time.deltaTime;
                this.transform.position = Vector3.Lerp(start, end, t / time);
                yield return null;
            }
            this.transform.position = end;
            yield return null;
        }
    }

}
