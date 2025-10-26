using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Referencias UI")]
    public TextMeshProUGUI textoLlaves;
    public TextMeshProUGUI textoItems;
    public GameObject panel;
    public GameObject panelGameOver;
    public GameObject panelVictoria;
    public TextMeshProUGUI textoInteraccion;
    public GameObject panelPausa;

    [Header("Barra de Vida (Opcional)")]
    public Slider barraVida;
    public TextMeshProUGUI textoVida;

    private PlayerController jugador;
    private bool juegoEnPausa = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        GameObject objJugador = GameObject.FindGameObjectWithTag("Player");
        if (objJugador != null)
            jugador = objJugador.GetComponent<PlayerController>();

        if (panelGameOver != null)
            panelGameOver.SetActive(false);

        if (panelVictoria != null)
            panelVictoria.SetActive(false);

        if (textoInteraccion != null)
            textoInteraccion.gameObject.SetActive(false);

        if (panelPausa != null)
            panelPausa.SetActive(false);
    }

    void Update()
    {
        ActualizarInventario();

        // Pausar con ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoEnPausa)
                ReanudarJuego();
            else
                PausarJuego();
        }
    }

    void ActualizarInventario()
    {
        if (jugador == null) return;

        // Actualizar texto de llaves
        if (textoLlaves != null)
        {
            string infoLlaves = "🔑 LLAVES:\n";

            if (jugador.llaves.Count == 0)
            {
                infoLlaves += "Ninguna";
            }
            else
            {
                foreach (var llave in jugador.llaves)
                {
                    infoLlaves += $"• {llave.Key}: x{llave.Value}\n";
                }
            }

            textoLlaves.text = infoLlaves;
        }

        // Actualizar texto de items
        if (textoItems != null)
        {
            string infoItems = "📦 ITEMS:\n";

            if (jugador.itemsColeccionados.Count == 0)
            {
                infoItems += "Ninguno";
            }
            else
            {
                foreach (var item in jugador.itemsColeccionados)
                {
                    infoItems += $"• {item}\n";
                }
            }

            textoItems.text = infoItems;
        }
    }

    public void MostrarGameOver()
    {
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
            Time.timeScale = 0f; // Pausar el juego
        }
    }

    public void MostrarVictoria()
    {
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void MostrarMensajeInteraccion(string mensaje)
    {
        if (textoInteraccion != null)
        {
            textoInteraccion.text = mensaje;
            textoInteraccion.gameObject.SetActive(true);
            CancelInvoke("OcultarMensajeInteraccion");
            Invoke("OcultarMensajeInteraccion", 2f);
        }
    }

    void OcultarMensajeInteraccion()
    {
        if (textoInteraccion != null)
            textoInteraccion.gameObject.SetActive(false);
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PausarJuego()
    {
        juegoEnPausa = true;
        Time.timeScale = 0f;
        if (panelPausa != null)
            panelPausa.SetActive(true);
    }

    public void ReanudarJuego()
    {
        juegoEnPausa = false;
        Time.timeScale = 1f;
        if (panelPausa != null)
            panelPausa.SetActive(false);
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Carga la primera escena (menú)
    }

    public void SalirDelJuego()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void CargarSiguienteNivel()
    {
        Time.timeScale = 1f;
        if (GameManager.Instance != null)
            GameManager.Instance.CargarNivelSiguiente();
    }
}