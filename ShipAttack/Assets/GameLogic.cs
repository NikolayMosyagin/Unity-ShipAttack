using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

#pragma warning disable 649
    [SerializeField]
    private BackLogic[] _backs;
#pragma warning restore 649


    // Use this for initialization
    void Start () {

        var backs = this.transform.Find("Backs");
        if (backs)
        {
            this._backs = backs.GetComponentsInChildren<BackLogic>();
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
