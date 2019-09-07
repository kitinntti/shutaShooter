using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // 移動可能な範囲
    //引数に与えてる数値で範囲を指定してるみたいやけど、何の数値なんかはわからんかったから探り探りで siwta
    public static Vector2 m_moveLimit = new Vector2(8.5f, 4.5f);
    

    // 指定された位置を移動可能な範囲に収めた値を返す
    public static Vector3 ClampPosition(Vector3 position)
    {
        return new Vector3
        (
            Mathf.Clamp(position.x, -m_moveLimit.x, m_moveLimit.x),
            Mathf.Clamp(position.y, -m_moveLimit.y, m_moveLimit.y),
            0
        );
    }
}