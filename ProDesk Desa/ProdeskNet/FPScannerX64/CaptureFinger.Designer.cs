//BBV-P-423 RQ-PD-17 2 GVARGAS 02/01/2018 Mejoras carga huella

namespace FPScannerX64
{
    partial class CaptureFinger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbxFingerprint = new System.Windows.Forms.PictureBox();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.btnCaptureFinger = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtRight = new System.Windows.Forms.RadioButton();
            this.rbtLeft = new System.Windows.Forms.RadioButton();
            this.btnSaveFinger = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFingerprint)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbxFingerprint
            // 
            this.pbxFingerprint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxFingerprint.Location = new System.Drawing.Point(16, 140);
            this.pbxFingerprint.Margin = new System.Windows.Forms.Padding(4);
            this.pbxFingerprint.Name = "pbxFingerprint";
            this.pbxFingerprint.Size = new System.Drawing.Size(435, 483);
            this.pbxFingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxFingerprint.TabIndex = 0;
            this.pbxFingerprint.TabStop = false;
            // 
            // tbxMessage
            // 
            this.tbxMessage.BackColor = System.Drawing.SystemColors.InfoText;
            this.tbxMessage.Enabled = false;
            this.tbxMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tbxMessage.ForeColor = System.Drawing.SystemColors.Window;
            this.tbxMessage.Location = new System.Drawing.Point(16, 15);
            this.tbxMessage.Margin = new System.Windows.Forms.Padding(4);
            this.tbxMessage.Multiline = true;
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.ReadOnly = true;
            this.tbxMessage.Size = new System.Drawing.Size(436, 46);
            this.tbxMessage.TabIndex = 1;
            this.tbxMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCaptureFinger
            // 
            this.btnCaptureFinger.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btnCaptureFinger.Location = new System.Drawing.Point(16, 634);
            this.btnCaptureFinger.Margin = new System.Windows.Forms.Padding(1);
            this.btnCaptureFinger.Name = "btnCaptureFinger";
            this.btnCaptureFinger.Size = new System.Drawing.Size(435, 37);
            this.btnCaptureFinger.TabIndex = 4;
            this.btnCaptureFinger.Text = "Iniciar captura";
            this.btnCaptureFinger.UseVisualStyleBackColor = true;
            this.btnCaptureFinger.Click += new System.EventHandler(this.btnCaptureFingerprint_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtRight);
            this.groupBox1.Controls.Add(this.rbtLeft);
            this.groupBox1.Location = new System.Drawing.Point(16, 70);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox1.Size = new System.Drawing.Size(435, 65);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Indique la posición del dedo índice";
            // 
            // rbtRight
            // 
            this.rbtRight.AutoSize = true;
            this.rbtRight.ImeMode = System.Windows.Forms.ImeMode.On;
            this.rbtRight.Location = new System.Drawing.Point(276, 31);
            this.rbtRight.Margin = new System.Windows.Forms.Padding(1);
            this.rbtRight.Name = "rbtRight";
            this.rbtRight.Size = new System.Drawing.Size(83, 21);
            this.rbtRight.TabIndex = 1;
            this.rbtRight.TabStop = true;
            this.rbtRight.Text = "Derecho";
            this.rbtRight.UseVisualStyleBackColor = true;
            // 
            // rbtLeft
            // 
            this.rbtLeft.AutoSize = true;
            this.rbtLeft.Checked = true;
            this.rbtLeft.Location = new System.Drawing.Point(29, 31);
            this.rbtLeft.Margin = new System.Windows.Forms.Padding(1);
            this.rbtLeft.Name = "rbtLeft";
            this.rbtLeft.Size = new System.Drawing.Size(87, 21);
            this.rbtLeft.TabIndex = 0;
            this.rbtLeft.TabStop = true;
            this.rbtLeft.Text = "Izquierdo";
            this.rbtLeft.UseVisualStyleBackColor = true;
            // 
            // btnSaveFinger
            // 
            this.btnSaveFinger.Enabled = false;
            this.btnSaveFinger.ImeMode = System.Windows.Forms.ImeMode.On;
            this.btnSaveFinger.Location = new System.Drawing.Point(17, 683);
            this.btnSaveFinger.Margin = new System.Windows.Forms.Padding(1);
            this.btnSaveFinger.Name = "btnSaveFinger";
            this.btnSaveFinger.Size = new System.Drawing.Size(435, 37);
            this.btnSaveFinger.TabIndex = 6;
            this.btnSaveFinger.Text = "Guardar Captura";
            this.btnSaveFinger.UseVisualStyleBackColor = true;
            this.btnSaveFinger.Click += new System.EventHandler(this.btnSaveFinger_Click);
            // 
            // CaptureFinger
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(469, 733);
            this.Controls.Add(this.btnSaveFinger);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCaptureFinger);
            this.Controls.Add(this.tbxMessage);
            this.Controls.Add(this.pbxFingerprint);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CaptureFinger";
            this.RightToLeftLayout = true;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Capture Fingerprint";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxFingerprint)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxFingerprint;
        private System.Windows.Forms.TextBox tbxMessage;
        private System.Windows.Forms.Button btnCaptureFinger;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtRight;
        private System.Windows.Forms.RadioButton rbtLeft;
        private System.Windows.Forms.Button btnSaveFinger;
    }
}

