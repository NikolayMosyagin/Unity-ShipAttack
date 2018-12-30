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
    [SerializeField]
    private ShipMove _playerPrototype;
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

        var player = new GameObject("Player");
        player.transform.parent = this._game.transform;
        var playerShip = Instantiate(this._playerPrototype, player.transform);

        var enemy = new GameObject("Enemy");
        enemy.transform.parent = this._game.transform;
        var enemiesPrototype = Resources.LoadAll<Ship>("Ships");
        for (int i = 0; i < enemiesPrototype.Length; ++i)
        {
            int count = enemiesPrototype[i].type == ShipType.Common ? 5 : 1;
            for (int j = 0; j < count; ++j)
            {
                var ship = Instantiate(enemiesPrototype[i], enemy.transform);
                ship.gameObject.SetActive(false);
            }
        }





        this._game.SetActive(true);
    }
}
