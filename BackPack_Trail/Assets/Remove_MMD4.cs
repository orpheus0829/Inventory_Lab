using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove_MMD4 : MonoBehaviour
{
    [ContextMenu("批量删除所有 MMD4MecanimBone 组件")]
    void ClearAllBoneComp()
    {
        // 获取所有子物体（包含自身）
        Behaviour[] allComps = GetComponentsInChildren<Behaviour>(true);
        int count = 0;

        foreach (var comp in allComps)
        {
            // 匹配组件脚本名
            if (comp.GetType().Name == "MMD4MecanimBone")
            {
                DestroyImmediate(comp);
                count++;
            }
        }

        Debug.Log($"成功删除 {count} 个 MMD4MecanimBone 组件");
    }
}
