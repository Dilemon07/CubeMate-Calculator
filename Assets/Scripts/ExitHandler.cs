using UnityEngine;

public class ExitHandler : MonoBehaviour
{
    public GameObject exitPanel; // ������ �� ���� �������������

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ���� ������ ������ "�����"
        {
            if (!exitPanel.activeSelf) // ���� ������ ��������� � ��������
            {
                exitPanel.SetActive(true);
            }
            else // ���� ��� ������� � ��������
            {
                exitPanel.SetActive(false);
            }
        }
    }

    // ����� �� ����
    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // �������� ���� ��� ������
    public void CancelExit()
    {
        exitPanel.SetActive(false);
    }
}
