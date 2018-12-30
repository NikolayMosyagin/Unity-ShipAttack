using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

#pragma warning disable 649
    [SerializeField]
    private Camera _mainCamera;
#pragma warning restore 649

    private BackLogic[] _backs;
    private ShipMove _playerShip;


    // Use this for initialization
    void Start () {

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

        if (this._backs != null)
        {
            for (int i = 0; i < this._backs.Length; ++i)
            {
                this.StartCoroutine(this._backs[i].Play());
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
