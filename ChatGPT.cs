using OpenAI;
using OpenAI.Chat;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftIV_ChatGPT
{
    public partial class FormChat : Form
    {
        // Cliente oficial ChatClient 2.5.0
        private ChatClient chatClient;

        // Historial de conversación (solo strings para mantener memoria corta)
        private List<string> conversationHistory = new List<string>();
        private const int MaxHistory = 10; // Últimos 10 turnos

        public FormChat()
        {
            InitializeComponent();

            // Inicializa cliente de OpenAI
            InicializarOpenAI();

            // Asociar eventos a controles
            AsociarEventos();
        }

        /// <summary>
        /// Inicializa el cliente ChatClient con API key desde variable de entorno
        /// </summary>
        private void InicializarOpenAI()
        {
            try
            {
                string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
                if (string.IsNullOrEmpty(apiKey))
                    throw new Exception("Variable de entorno OPENAI_API_KEY no definida.");

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

            // Validaciones
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

                // Agregar mensaje al historial
                conversationHistory.Add("Tú: " + prompt);
                if (conversationHistory.Count > MaxHistory)
                    conversationHistory.RemoveAt(0);

                // Concatenar historial para enviar al modelo
                string context = string.Join("\n", conversationHistory);

                // Llamada asincrónica al modelo
                ChatCompletion completion = await chatClient.CompleteChatAsync(context);

                string respuesta = completion.Content[0].Text.Trim();

                // Mostrar respuesta del asistente
                txtChat.AppendText("Asistente: " + respuesta + Environment.NewLine);

                // Agregar respuesta al historial
                conversationHistory.Add("Asistente: " + respuesta);
            }
            catch (Exception ex)
            {
                // Captura errores de red / API
                txtChat.AppendText("No se pudo conectar. Intenta de nuevo." + Environment.NewLine);
                txtChat.AppendText("Error: " + ex.Message + Environment.NewLine);
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
