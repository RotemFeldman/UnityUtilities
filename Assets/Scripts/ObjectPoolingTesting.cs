using System;
using System.Collections.Generic;
using DefaultNamespace;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityUtilities.Attributes;
using UnityUtilities.UnityUtilities;
using UnityEngine.Pool;
using UnityUtilities;

public class ObjectPoolingTesting : MonoBehaviour
{
    
    public Cube prefab;

    
    private bool UsePool = true;
    public float Timing = 0.1f;
    private LoopingCountdownTimer timer;
    private GameObjectPool<Cube> pool;

    private TextMeshProUGUI _textMeshPro;
    private int activeCubes = 0;

    private void Start()
    {
        _textMeshPro = UnityUtilities.Create.UIText("",GridDirection.TopLeft);

        
    }


    [Button]
    public void ChangeMethod()
    {
        UsePool = !UsePool;
    
        if (!UsePool)
            pool.Dispose();
        
    }
    
    private void Awake()
    {
        pool = new GameObjectPool<Cube>(prefab,transform,100);
        timer = new LoopingCountdownTimer(Timing,false);
        timer.OnTimerComplete += Method;
        timer.Start();
        
    }

    private void Update()
    {
        if (timer != null)
        {
            timer.Tick(Time.deltaTime);
        }
        
        _textMeshPro.text = UsePool ? $"Using Pooling  {pool.CountActive}/{pool.CountAll}" : $"Using Instantiate {activeCubes}";
        
    }

    private void Method()
    {
        if (UsePool)
            pool.Get();
        else
        {
            Cube obj =Instantiate(prefab);
            obj.Init();
            activeCubes++;
            obj.AboutToDestroy += () => { activeCubes--; };
        }
    }
}
