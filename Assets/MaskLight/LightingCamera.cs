using UnityEngine;

public class LightingCamera : MonoBehaviour {

    public Camera mainCamera;
    public Camera lightCamera;

    Material _material;
    RenderTexture _texture;

    int _cacheWidth;
    int _cacheHeight;

    void Start() {
        _material = new Material(Shader.Find("Custom/LightCamera"));

        Regenerate();
    }

    void Update() {
        if(_cacheWidth != Screen.width || _cacheHeight != Screen.height) {
            Regenerate();
        }

        if(mainCamera.orthographicSize != lightCamera.orthographicSize) {
            UpdateOrthographicSize();
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        Graphics.Blit(source, destination, _material);
    }

    void Regenerate() {
        Debug.Log("Regenerate");
        if (_texture != null) Destroy(_texture);

        _cacheWidth = Screen.width;
        _cacheHeight = Screen.height;

        _texture = new RenderTexture(_cacheWidth, _cacheHeight, 24);

        lightCamera.targetTexture = _texture;
        _material.SetTexture("_LightTex", _texture);
    }

    void UpdateOrthographicSize() {
        lightCamera.orthographicSize = mainCamera.orthographicSize; ;
    }
}
