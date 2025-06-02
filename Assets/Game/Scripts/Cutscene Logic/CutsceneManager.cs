using System;
using System.Collections.Generic;
using Game.Scripts.Infrastructure.Input;
using UnityEngine;
using Zenject;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private List<CutsceneStruct> _cutscenes = new List<CutsceneStruct>();

    private Dictionary<Cutscenes, GameObject> _cutsceneDataBase = new Dictionary<Cutscenes, GameObject>();
    private GameObject _activeCutscene;
    private IInputHandler _inputHandler;

    [Inject]
    private void Construct(IInputHandler inputHandler)
    {
        _inputHandler =  inputHandler;
    }

    private void Awake()
    {
        InitializeCutsceneDataBase();

        foreach (var cutscene in _cutsceneDataBase)
        {
            cutscene.Value.SetActive(false);
        }
        
        StartCutscene(Cutscenes.StartGame);
    }

    public void StartCutscene(Cutscenes cutsceneKey)
    {
        _inputHandler.Deactivate();
        
        if (!_cutsceneDataBase.ContainsKey(cutsceneKey)) 
        {
            return;
        } 

        if (_activeCutscene != null)
        {
            if (_activeCutscene == _cutsceneDataBase[cutsceneKey])
            {
                return;
            }
        }

        _activeCutscene = _cutsceneDataBase[cutsceneKey];

        foreach (var cutscene in _cutsceneDataBase)
        {
            cutscene.Value.SetActive(false);
        }

        _cutsceneDataBase[cutsceneKey].SetActive(true);
    }

    public void EndCutscene()
    {
        if (_activeCutscene != null)
        {
            _activeCutscene.SetActive(false);
            _activeCutscene = null;
            _inputHandler.Activate();
        }
    }

    private void InitializeCutsceneDataBase()
    {
        _cutsceneDataBase.Clear();

        for (int i = 0; i < _cutscenes.Count; i++)
        {           
            _cutsceneDataBase.Add(_cutscenes[i].cutsceneKey, _cutscenes[i].cutsceneObject);
        }
    }
}

[Serializable]
public struct CutsceneStruct
{
    public Cutscenes cutsceneKey;
    public GameObject cutsceneObject;
}

public enum Cutscenes
{
    StartGame,
    Customer
}