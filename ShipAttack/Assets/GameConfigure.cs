using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigure : MonoBehaviour {

    private const int BackCount = 2;
#pragma warning disable 649
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private GameObject _game;
    [SerializeField]
    private BackLogic _backPrototype;
#pragma warning restore 649

    private void Awake()
    {
        var sprites = Resources.LoadAll<Sprite>("Backs");

        var backs = new GameObject("Backs");
        backs.transform.parent = this._game.transform;
        var backsParent = backs.transform;

        var size = Vector2.zero;
        for (int i = 0; i < BackCount; ++i)
        {
            var back = Instantiate(this._backPrototype, backsParent);
            back.Initialize(sprites[i % sprites.Length], BackCount);
            size = back.backSize;
            back.transform.position = new Vector3(size.x * i, 0, 0);
        }
        if (size != Vector2.zero)
        {
            this._camera.orthographicSize = size.y / 2;
            this._camera.aspect = size.x / size.y;
        }
        this._game.SetActive(true);
    }
}
