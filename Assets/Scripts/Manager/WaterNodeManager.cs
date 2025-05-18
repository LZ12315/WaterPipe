using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

[Serializable]
public class DicNode
{
    public WaterNodeType nodeType;
    public List<IWaterRelated> connectNodes = new List<IWaterRelated>();
}

public class WaterNodeManager : MonoBehaviour
{
    public static WaterNodeManager Instance;
    [SerializeField] private Dictionary<IWaterRelated,DicNode> pipeDictionary = new Dictionary<IWaterRelated, DicNode>();
    [SerializeField] private List<IWaterRelated> waterSources = new List<IWaterRelated>();
    public event Action<WaterNodeManager> SetWaterContainsOff;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(Instance);
            Instance = this;
        }
    }

    public void AddNode(IWaterRelated nodeToAdd, List<IWaterRelated> waterNodes)
    {
        if (pipeDictionary.ContainsKey(nodeToAdd))
        {
            NodeChange(nodeToAdd, waterNodes);
            return;
        }

        pipeDictionary[nodeToAdd] = new DicNode();
        pipeDictionary[nodeToAdd].connectNodes = waterNodes;
        pipeDictionary[nodeToAdd].nodeType = nodeToAdd.NodeType;

        if (pipeDictionary[nodeToAdd].nodeType == WaterNodeType.Source)
            waterSources.Add(nodeToAdd);
        else
            SetWaterContainsOff += (value) => nodeToAdd.SetWaterBreak(value);

        foreach (var pipe in pipeDictionary[nodeToAdd].connectNodes)
        {
            if (pipeDictionary.ContainsKey(pipe) && !pipeDictionary[pipe].connectNodes.Contains(nodeToAdd))
                pipeDictionary[pipe].connectNodes.Add(nodeToAdd);
        }

        WaterDivertion();
    }

    public void NodeChange(IWaterRelated addedNode, List<IWaterRelated> waterNodes)
    {
        if (!pipeDictionary.ContainsKey(addedNode))
            return;
        pipeDictionary[addedNode].connectNodes = waterNodes;
    }

    public void DeleteNode(IWaterRelated nodeToDelete)
    {
        if (!pipeDictionary.ContainsKey(nodeToDelete))
            return;

        foreach (var pipe in pipeDictionary[nodeToDelete].connectNodes)
        {
            if (pipeDictionary.ContainsKey(pipe) && pipeDictionary[pipe].connectNodes.Contains(nodeToDelete))
                pipeDictionary[pipe].connectNodes.Remove(nodeToDelete);
        }

        pipeDictionary.Remove(nodeToDelete);
        if (waterSources.Contains(nodeToDelete))
            waterSources.Remove(nodeToDelete);

        WaterDivertion();
    }

    public void WaterContainsCheck(IWaterRelated startNode)
    {
        WaterDivertion();
    }

    private void WaterDivertion()
    {
        SetWaterContainsOff.Invoke(this);
        foreach (var source in waterSources)
            DFS(source);
    }

    private void DFS(IWaterRelated startNode)
    {
        HashSet<IWaterRelated> visited = new HashSet<IWaterRelated>();
        DFSRecursive(startNode, visited);
    }

    private void DFSRecursive(IWaterRelated node, HashSet<IWaterRelated> visited)
    {
        if (visited.Contains(node))
            return;

        visited.Add(node);
        node.WaterDivertion();

        if (pipeDictionary.ContainsKey(node))
        {
            foreach (var neighbor in pipeDictionary[node].connectNodes)
                DFSRecursive(neighbor, visited);
        }
    }

}
