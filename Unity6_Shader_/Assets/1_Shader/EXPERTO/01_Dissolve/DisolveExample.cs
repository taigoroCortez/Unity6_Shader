using System.Collections;
using UnityEngine;

public class DisolveExample : MonoBehaviour
{
    [Header ("Disolve")]
    [SerializeField] private float _disolveSpeed = 1f;
    [SerializeField] private float _dissolveWait = 1f;
    [Space]
    [SerializeField] private bool _useIndex;
    [SerializeField] private MeshRenderer _dissolveMesh;
    [SerializeField] private int _dissolveMeshIndex;

    [Header("Reference")]
    [SerializeField] private Material _disolveMaterial;

    private bool _isDisolving;

    private float _dissolveValue;
    private float valueStart = 1f;
    private float valueEnd = 0f;

    private int hash_Dissolve = Shader.PropertyToID("_DISSOLVE");

    private void Start()
    {
        if(_useIndex)   _disolveMaterial = _dissolveMesh.materials[_dissolveMeshIndex];

        _disolveMaterial.SetFloat(hash_Dissolve, valueStart);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && !_isDisolving)
        {
            Debug.Log("lkashdihas");
            StartCoroutine(MakeDisaolve());
        }
        
    }

    private IEnumerator MakeDisaolve()
    {
        _isDisolving = true;

        _dissolveValue = valueStart;

        while(_dissolveValue > valueEnd)
        {
            _dissolveValue -= Time.deltaTime * _disolveSpeed;
            _disolveMaterial.SetFloat(hash_Dissolve, _dissolveValue);
            yield return null;
        }

        _dissolveValue = valueEnd;

        yield return new WaitForSeconds(_dissolveWait);

        while (_dissolveValue < valueStart)
        {
            _dissolveValue += Time.deltaTime * _disolveSpeed;
            _disolveMaterial.SetFloat(hash_Dissolve, _dissolveValue);
            yield return null;
        }

        _isDisolving = false;
    }

}
