using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedFaded : MonoBehaviour {

    private static int ColorId = Shader.PropertyToID("_Color");

#pragma warning disable 649
    [SerializeField]
    private MeshFilter _meshFilter;
    [SerializeField]
    private MeshRenderer _renderer;
#pragma warning restore 649

    private float _appearTime;
    private float _disappearTime;

    public static AnimatedFaded Create(float appearTime, float disappearTime)
    {
        var result = Instantiate(Resources.Load<AnimatedFaded>("Faded/AnimatedFaded"));
        result.transform.position = new Vector3(0, 0, -1);
        result._appearTime = appearTime;
        result._disappearTime = disappearTime;

        var mesh = new Mesh();

        var camera = Camera.main;
        float height = camera.orthographicSize;
        float width = height * camera.aspect;

        mesh.vertices = new Vector3[4]
        {
            new Vector3(-width, -height, 0),
            new Vector3(width, -height, 0),
            new Vector3(width, height, 0),
            new Vector3(-width, height, 0),
        };
        mesh.normals = new Vector3[4] { new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1) };
        mesh.triangles = new int[6] { 0, 2, 1, 0, 3, 2 };
        result._meshFilter.mesh = mesh;
        return result;
    }

    private void Start()
    {
        this.StartCoroutine(this.Animate());
    }

    private IEnumerator Animate()
    {
        float time = 0.0f;
        Color start = Color.black;
        start.a = 0;
        Color end = Color.black;
        while (time < this._appearTime)
        {
            time += Time.deltaTime;
            this._renderer.material.SetColor(ColorId, Color.Lerp(start, end, time / this._appearTime));
            yield return null;
        }
        this._renderer.material.SetColor(ColorId, end);

        yield return null;
        time = 0f;
        while (time < this._disappearTime)
        {
            time += Time.deltaTime;
            this._renderer.material.SetColor(ColorId, Color.Lerp(end, start, time / this._disappearTime));
            yield return null;
        }
        this._renderer.material.SetColor(ColorId, start);
        yield return null;

        Destroy(this.gameObject);
    }


}
