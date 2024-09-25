using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// һ��SO�ļ� ����洢��Ϸ�������� ���ڳ����л�
/// sceneReference������һ��Enumֵ �������Լ������������͵ĳ��� �����sceneReference��������洢�������� ����˵������Addressable�ڵĴ洢 
/// </summary>

[CreateAssetMenu(menuName = ("SceneSO/GameSceneSO"))]
public class GameSceneSO : ScriptableObject
{
    public SceneType sceneType;
    public AssetReference sceneReference;
}
