using UnityEngine;
using TMPro;

public class IndicadorInteraccion : MonoBehaviour
{
    [Header("Configuraci�n")]
    public float amplitudFlotacion = 0.3f;
    public float velocidadFlotacion = 2f;

    [Header("Referencias")]
    public TextMeshPro textoTecla;
    public SpriteRenderer spriteIcono;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.localPosition;

        if (textoTecla != null)
            textoTecla.text = "E";
    }

    void Update()
    {
        // Efecto de flotaci�n
        float nuevaY = posicionInicial.y + Mathf.Sin(Time.time * velocidadFlotacion) * amplitudFlotacion;
        transform.localPosition = new Vector3(posicionInicial.x, nuevaY, posicionInicial.z);

        // Efecto de parpadeo en el texto
        if (textoTecla != null)
        {
            Color color = textoTecla.color;
            color.a = 0.5f + Mathf.PingPong(Time.time * 2f, 0.5f);
            textoTecla.color = color;
        }
    }
}