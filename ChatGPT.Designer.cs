using System.Windows.Forms;

namespace SoftIV_ChatGPT
{
    partial class FormChat
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblEstado = new Label();
            txtChat = new TextBox();
            txtPrompt = new TextBox();
            btnEnviar = new Button();
            btnLimpiar = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblTitulo.Location = new System.Drawing.Point(12, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(560, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "ChatGPT – Cliente WinForms";
            lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // lblEstado
            // 
            lblEstado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            lblEstado.Location = new System.Drawing.Point(12, 42);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new System.Drawing.Size(560, 20);
            lblEstado.TabIndex = 1;
            lblEstado.Text = "Listo";
            lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            lblEstado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // txtChat
            // 
            txtChat.Location = new System.Drawing.Point(12, 65);
            txtChat.Multiline = true;
            txtChat.Name = "txtChat";
            txtChat.ReadOnly = true;
            txtChat.ScrollBars = ScrollBars.Vertical;
            txtChat.Size = new System.Drawing.Size(560, 350);
            txtChat.TabIndex = 2;
            txtChat.TabStop = false;
            txtChat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // txtPrompt
            // 
            txtPrompt.Location = new System.Drawing.Point(12, 425);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.Size = new System.Drawing.Size(350, 50);
            txtPrompt.TabIndex = 3;
            txtPrompt.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // btnEnviar
            // 
            btnEnviar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnEnviar.Location = new System.Drawing.Point(368, 425);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new System.Drawing.Size(100, 24);
            btnEnviar.TabIndex = 4;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnLimpiar.Location = new System.Drawing.Point(368, 451);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new System.Drawing.Size(100, 24);
            btnLimpiar.TabIndex = 5;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            // 
            // FormChat
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(584, 491);
            Controls.Add(lblTitulo);
            Controls.Add(lblEstado);
            Controls.Add(txtChat);
            Controls.Add(txtPrompt);
            Controls.Add(btnEnviar);
            Controls.Add(btnLimpiar);
            Name = "FormChat";
            Text = "ChatGPT – Cliente WinForms";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblEstado;
        private TextBox txtChat;
        private TextBox txtPrompt;
        private Button btnEnviar;
        private Button btnLimpiar;
    }
}
