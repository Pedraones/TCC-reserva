using UnityEngine;
using UnityEngine.InputSystem;
public class Inventario : MonoBehaviour
{
    public bool isOpen = false;
    public GameObject inventoryPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpen)
            {
                Abrir();
            }
            else
            {
                Fechar();
            }
        }
    }
    public void Fechar()
    {

        inventoryPanel.SetActive(false);
        Time.timeScale = 1;
        isOpen = false;
    }
    public void Abrir()
    {
        inventoryPanel.SetActive(true);
        Time.timeScale = 0;
        isOpen = true;

    }
}
