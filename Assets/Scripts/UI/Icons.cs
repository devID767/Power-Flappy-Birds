using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IconsType
{
    Birds,
    Energy
}

public class Icons : MonoBehaviour
{
    public Canvas Canvas;

    public static Icons birds;
    public static Icons energy;

    public IconsType IconsType;

    [SerializeField] private GameObject[] _prefabs;
    public int SpawnPrefabWithStart;

    public float X;
    public float X_indent;
    public float Y;
    public float Y_indent;

    private List<GameObject> OpenIcons;
    public int Count
    {
        get
        {
            return OpenIcons.Count;
        }
    }

    public int[] MaxCountOfLevels;
    public int MaxCount
    {
        get
        {
            int indexOfIcon = -1;
            if (IconsType == IconsType.Birds) indexOfIcon = AbilitiesLevel.instance.Abilities[(int)EAbility.Icons].Level;
            else if (IconsType == IconsType.Energy) indexOfIcon = 0;

            return MaxCountOfLevels[indexOfIcon];
        }
    }

    private void Start()
    {
        if (birds == null && IconsType == IconsType.Birds)
            birds = this;
        else if (energy == null && IconsType == IconsType.Energy)
            energy = this;
        else if (this != birds && this != energy)
            Destroy(this);

        OpenIcons = new List<GameObject>();

        for (int i = 0; i < SpawnPrefabWithStart; i++)
        {
            Add(x => true);
        }
    }

    public void Add(Func<GameObject, bool> func)
    {
        if (OpenIcons.Count < MaxCount)
        {
            foreach (var icon in _prefabs)
            {
                if (func(icon))
                {
                    GameObject Icon = Instantiate(icon, transform.position, Quaternion.identity, Canvas.transform);
                    SetPosition(Icon, OpenIcons.Count);
                    OpenIcons.Add(Icon);
                }
            }
        }
    }

    public bool Do(Func<GameObject, bool> func, int index)
    {
        return func(OpenIcons[index]);
    }

    public void RemoveThis(GameObject icon)
    {
        Destroy(icon);
        OpenIcons.Remove(icon);
        RepositionIcons();
    }

    public void Remove()
    {
        Destroy(OpenIcons[OpenIcons.Count - 1]);
        OpenIcons.Remove(OpenIcons[OpenIcons.Count - 1]);
        RepositionIcons();
    }

    private void RepositionIcons()
    {
        for (int i = 0; i < OpenIcons.Count; i++)
        {
            SetPosition(OpenIcons[i], i);
        }
    }
    private void SetPosition(GameObject icon, int count)
    {
        icon.GetComponent<RectTransform>().anchoredPosition = new Vector3(X + X_indent * count, Y + Y_indent * count, 0);
    }
}
