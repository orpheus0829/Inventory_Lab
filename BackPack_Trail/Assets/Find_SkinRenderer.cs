using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Find_SkinRenderer : MonoBehaviour
{
    [ContextMenu("查找所有 SkinnedMeshRenderer")]
    public void FindAll()
    {
        SkinnedMeshRenderer[] renderers = FindObjectsOfType<SkinnedMeshRenderer>(includeInactive: true);
        if (renderers.Length == 0)
        {
            Debug.LogError("场景中没有找到任何 SkinnedMeshRenderer！");
            return;
        }
        Debug.Log($"共找到 {renderers.Length} 个 SkinnedMeshRenderer：");
        foreach (var r in renderers)
        {
            string path = GetGameObjectPath(r.gameObject);
            Debug.Log($"- {path}");
        }
    }
    private string GetGameObjectPath(GameObject obj)
    {
        if (obj.transform.parent == null)
        {
            return obj.name;
        }
        return GetGameObjectPath(obj.transform.parent.gameObject) + "/" + obj.name;
    }
}
