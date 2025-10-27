namespace SoftIV_ChatGPT
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Inicializa configuración de la aplicación
            ApplicationConfiguration.Initialize();

            // Ejecuta el formulario principal correcto
            Application.Run(new FormChat());
        }
    }
}
