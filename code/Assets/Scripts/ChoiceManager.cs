using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceManager : MonoBehaviour
{
    GameObject objCube;
    Transform tfCube;
    public Font customFont;
    float modelHeight;
    Collider collid;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (name == "Level1")
                SceneManager.LoadScene("LoadSceneTo1");
            if (name == "Level2")
                SceneManager.LoadScene("LoadSceneTo2");
            if (name == "End")
                SceneManager.LoadScene("LoadSceneToEnd");
        }
    }

    private void Start()
    {
        objCube = GameObject.Find(name);
        tfCube = objCube.GetComponent<Transform>();
        collid = objCube.GetComponent<Collider>();//获取物体身上的碰撞器

        float model_y = collid.bounds.size.y; //由碰撞器来获取模型的初始高度
        float scale_y = tfCube.localScale.y; //获取模型的缩放比例

        modelHeight = model_y * scale_y; //获取模型的真实高度


    }

    private void OnGUI()
    {
        //需要获取模型头顶所在位置的3D坐标
        //根据模型坐标原点的位置来决定其高度要加多少，在此由于坐标原点在中心，所以Y方向上加模型一半的高度
        Vector3 worldPos = new Vector3(tfCube.position.x, tfCube.position.y + modelHeight / 2, tfCube.position.z);
        //根据3D坐标来转换成2D屏幕上的对应坐标
        Vector2 mapPosition = Camera.main.WorldToScreenPoint(worldPos);
        //计算得到真实的头顶2D坐标
        Vector2 pos = new Vector2(mapPosition.x, Screen.height - mapPosition.y);

        // 计算要显示的 GUI.Label 尺寸
        Vector2 nameSize = GUI.skin.label.CalcSize(new GUIContent(name));


        GUI.skin.label.normal.textColor = Color.white;//设置GUI.Label字体的颜色
        GUI.skin.label.fontSize = 40;//字体的大小
        GUI.skin.label.font = customFont;//字体的样式
        GUI.Label(new Rect(pos.x - (nameSize.x / 2), pos.y - nameSize.y, nameSize.x, nameSize.y), name);
    }
}
