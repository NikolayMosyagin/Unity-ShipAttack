using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

#pragma warning disable 649
    [SerializeField]
    private Camera _mainCamera;
#pragma warning restore 649

    public static GameLogic instance;

    public float width { get; private set; }
    public float height { get; private set; }

    private BackLogic[] _backs;
    private ShipMove _playerShip;
    private Transform _enemy;
    private List<Ship> _pooledEnemyShip;


    // Use this for initialization
    void Start () {

        instance = this;
        this.height = 2.0f * this._mainCamera.orthographicSize;
        this.width = this.height * this._mainCamera.aspect;

        var backs = this.transform.Find("Backs");
        if (backs)
        {
            this._backs = backs.GetComponentsInChildren<BackLogic>();
        }
        var player = this.transform.Find("Player");
        if (player)
        {
            this._playerShip = player.GetComponentInChildren<ShipMove>();
            if (this._playerShip)
            {
                this._playerShip.Initialize(this._mainCamera);
            }
        }
        this._enemy = this.transform.Find("Enemy");
        this._pooledEnemyShip = new List<Ship>(this._enemy.childCount);
        this._pooledEnemyShip.AddRange(this._enemy.GetComponentsInChildren<Ship>());

        if (this._backs != null)
        {
            for (int i = 0; i < this._backs.Length; ++i)
            {
                this.StartCoroutine(this._backs[i].Play());
            }
        }

        var faded = AnimatedFaded.Create(2.0f, 2.0f);

        this.StartCoroutine(this.GenerateCommonEnemy());
	}

    private Ship GetShip(ShipType type)
    {
        Ship result = null;
        for (int i = 0; !result && i < this._pooledEnemyShip.Count; ++i)
        {
            if (this._pooledEnemyShip[i].type == type)
            {
                result = this._pooledEnemyShip[i];
                this._pooledEnemyShip.RemoveAt(i);
            }
        }
        if (!result)
        {
            var prototype = Ship.FindShip(type);
            if (!prototype)
            {
                return null;
            }
            result = Instantiate(prototype, this._enemy);
            result.gameObject.SetActive(false);
        }
        return result;
    }

    private IEnumerator GenerateCommonEnemy()
    {
        yield return new WaitForSeconds(1.0f);
        do
        {
            var ship = this.GetShip(ShipType.Common);
            ship.Play();
            yield return new WaitForSeconds(10.0f);
        } while (true);
    }
}
