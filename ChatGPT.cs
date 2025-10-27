using OpenAI;
using OpenAI.Chat;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftIV_ChatGPT
{
    public partial class FormChat : Form
    {
        private ChatClient chatClient;

        // Historial de conversación para memoria corta
        private List<string> conversationHistory = new List<string>();
        private const int MaxHistory = 10; // Últimos 10 turnos

        public FormChat()
        {
            InitializeComponent();
            InicializarOpenAI();
            AsociarEventos();
        }

        /// <summary>
        /// Inicializa el cliente OpenAI con la API key desde variable de entorno
        /// </summary>
        private void InicializarOpenAI()
        {
            try
            {
                // KEY
                string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
                if (string.IsNullOrEmpty(apiKey))
                {
                    lblEstado.Text = "API key no definida. Por favor, configura OPENAI_API_KEY";
                    btnEnviar.Enabled = false;
                    return; // Terminar inicialización
                }

                chatClient = new ChatClient(
                    model: "gpt-4o-mini",
                    credential: new ApiKeyCredential(apiKey)
                );


                lblEstado.Text = "Listo";
            }
            catch (Exception ex)
            {
                lblEstado.Text = "Error OpenAI: " + ex.Message;
                btnEnviar.Enabled = false;
            }
        }

        /// <summary>
        /// Asocia eventos Click y KeyDown
        /// </summary>
        private void AsociarEventos()
        {
            btnEnviar.Click += async (s, e) => await EnviarMensajeAsync();
            btnLimpiar.Click += BtnLimpiar_Click;
            txtPrompt.KeyDown += TxtPrompt_KeyDown;
        }

        /// <summary>
        /// Permite enviar mensaje con Enter sin salto de línea
        /// </summary>
        private async void TxtPrompt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                await EnviarMensajeAsync();
            }
        }

        /// <summary>
        /// Envía el mensaje al modelo de OpenAI y actualiza UI e historial
        /// </summary>
        private async Task EnviarMensajeAsync()
        {
            string prompt = txtPrompt.Text.Trim();

            // Validaciones de entrada
            if (string.IsNullOrWhiteSpace(prompt)) return;

            if (prompt.Length > 1000)
            {
                MessageBox.Show("Mensaje demasiado largo (máx. 1000 caracteres).");
                return;
            }

            // Mostrar mensaje del usuario
            txtChat.AppendText("Tú: " + prompt + Environment.NewLine);
            txtPrompt.Clear();
            btnEnviar.Enabled = false;
            lblEstado.Text = "Enviando...";

            try
            {
                // Normalizar saltos de línea
                prompt = prompt.Replace("\r\n", "\n");

                // Mantener historial de memoria corta
                conversationHistory.Add("Tú: " + prompt);
                if (conversationHistory.Count > MaxHistory)
                    conversationHistory.RemoveAt(0);

                string context = string.Join("\n", conversationHistory);

                // Llamada asincrónica al modelo
                ChatCompletion completion = await chatClient.CompleteChatAsync(context);
                string respuesta = completion.Content[0].Text.Trim();

                txtChat.AppendText("Asistente: " + respuesta + Environment.NewLine);

                conversationHistory.Add("Asistente: " + respuesta);
            }
            catch (HttpRequestException)
            {
                // Error de red / servidor inaccesible
                txtChat.AppendText("No se pudo conectar al servidor. Verifica tu conexión a Internet." + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Detectar errores comunes de la librería
                string msg = ex.Message.ToLower();

                if (msg.Contains("401"))
                    txtChat.AppendText("API key inválida o ausente." + Environment.NewLine);
                else if (msg.Contains("429"))
                    txtChat.AppendText("Límite alcanzado, espera unos segundos." + Environment.NewLine);
                else if (msg.Contains("5") || msg.Contains("retry failed"))
                    txtChat.AppendText("Servicio ocupado o no disponible. Reintentar más tarde." + Environment.NewLine);
                else
                    txtChat.AppendText("Error inesperado: " + ex.Message + Environment.NewLine);
            }
            finally
            {
                btnEnviar.Enabled = true;
                lblEstado.Text = "Listo";
            }
        }

        /// <summary>
        /// Limpia todo el historial y la UI
        /// </summary>
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            txtChat.Clear();
            txtPrompt.Clear();
            conversationHistory.Clear();
        }
    }
}
