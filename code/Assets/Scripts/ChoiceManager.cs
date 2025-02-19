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
        collid = objCube.GetComponent<Collider>();//��ȡ�������ϵ���ײ��

        float model_y = collid.bounds.size.y; //����ײ������ȡģ�͵ĳ�ʼ�߶�
        float scale_y = tfCube.localScale.y; //��ȡģ�͵����ű���

        modelHeight = model_y * scale_y; //��ȡģ�͵���ʵ�߶�


    }

    private void OnGUI()
    {
        //��Ҫ��ȡģ��ͷ������λ�õ�3D����
        //����ģ������ԭ���λ����������߶�Ҫ�Ӷ��٣��ڴ���������ԭ�������ģ�����Y�����ϼ�ģ��һ��ĸ߶�
        Vector3 worldPos = new Vector3(tfCube.position.x, tfCube.position.y + modelHeight / 2, tfCube.position.z);
        //����3D������ת����2D��Ļ�ϵĶ�Ӧ����
        Vector2 mapPosition = Camera.main.WorldToScreenPoint(worldPos);
        //����õ���ʵ��ͷ��2D����
        Vector2 pos = new Vector2(mapPosition.x, Screen.height - mapPosition.y);

        // ����Ҫ��ʾ�� GUI.Label �ߴ�
        Vector2 nameSize = GUI.skin.label.CalcSize(new GUIContent(name));


        GUI.skin.label.normal.textColor = Color.white;//����GUI.Label�������ɫ
        GUI.skin.label.fontSize = 40;//����Ĵ�С
        GUI.skin.label.font = customFont;//�������ʽ
        GUI.Label(new Rect(pos.x - (nameSize.x / 2), pos.y - nameSize.y, nameSize.x, nameSize.y), name);
    }
}
