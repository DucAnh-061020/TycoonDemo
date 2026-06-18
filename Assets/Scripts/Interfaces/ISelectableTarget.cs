using UnityEngine;

public interface ISelectableTarget
{
    Vector3 UiFocusPoint { get; }
    Sprite ItemImage { get; }
}

public interface IUnlockable : ISelectableTarget
{
    string Name { get; }
    float UnlockPrice { get; }
    void Execute();
}

public interface IUpgradable : ISelectableTarget
{
    int CurrentLevel { get; }
    int MaxLevel { get; }
    string Name { get; }
    float Income { get; }
    float Growtime { get; }

    void Execute();
}