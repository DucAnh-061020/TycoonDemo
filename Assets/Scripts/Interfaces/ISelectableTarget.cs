using UnityEngine;

public interface ISelectableTarget
{
    Vector3 UiFocusPoint { get; }
    Sprite ItemImage { get; }
}

public interface IUnlockable : ISelectableTarget
{
    string Name { get; }
    double UnlockPrice { get; }
    void Execute();
}

public interface IUpgradable : ISelectableTarget
{
    int CurrentLevel { get; }
    int MaxLevel { get; }
    string Name { get; }
    double Income { get; }
    float Growtime { get; }
    double UpgradeCost { get; }

    void Execute();
}