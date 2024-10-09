
/// <summary>
/// 一个不搭载到任何物体上的脚本 负责写一些Enum变量供开发者使用
/// 依次是：场景类型、DataUID类型、存档序号、Line动画状态、鼠标键位、砖块朝向
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