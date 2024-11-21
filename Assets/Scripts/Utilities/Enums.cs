using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// һ�������ص��κ������ϵĽű� ����дһЩEnum������������ʹ��
/// �����ǣ��������͡�DataUID���͡��浵��š�Line����״̬������λ��ש�鳯��
/// </summary>

public enum SceneType
{
    GameScene, MenuScene
};

public enum PersistentType
{
    ReadWrite, OnltRead
};

public enum ArchieveNumber
{
    Ar1, Ar2, Ar3
};

public enum LineAnimeState
{
    None, Appear, DisAppear
};

public enum MouseButton
{
    Left, Right, Middle, None
};

public enum CellDirection
{
    North, East, South, West
};

public enum CellAltitude
{
    Zero, Low, Middle, High
}

public enum WaterInformationType
{
    CheckConnect, Divertion
}

public enum WaterNodeType
{
    Pipe, Source ,Demand
}

public enum NumericalChangeType
{
    Money, Sewage, Purify
}

public enum RenderCellType
{
    None, Water, Mountain
}