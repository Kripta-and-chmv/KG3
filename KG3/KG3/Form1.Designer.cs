namespace KG3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.glControl1 = new OpenTK.GLControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chckbxCarcass = new System.Windows.Forms.CheckBox();
            this.chckbxProection = new System.Windows.Forms.CheckBox();
            this.chkbxNormal = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(12, 12);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(1027, 577);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chckbxCarcass
            // 
            this.chckbxCarcass.AutoSize = true;
            this.chckbxCarcass.Location = new System.Drawing.Point(1065, 39);
            this.chckbxCarcass.Name = "chckbxCarcass";
            this.chckbxCarcass.Size = new System.Drawing.Size(69, 17);
            this.chckbxCarcass.TabIndex = 1;
            this.chckbxCarcass.Text = "Каркасс";
            this.chckbxCarcass.UseVisualStyleBackColor = true;
            // 
            // chckbxProection
            // 
            this.chckbxProection.AutoSize = true;
            this.chckbxProection.Location = new System.Drawing.Point(1065, 62);
            this.chckbxProection.Name = "chckbxProection";
            this.chckbxProection.Size = new System.Drawing.Size(117, 17);
            this.chckbxProection.TabIndex = 1;
            this.chckbxProection.Text = "Ортографическая";
            this.chckbxProection.UseVisualStyleBackColor = true;
            this.chckbxProection.CheckedChanged += new System.EventHandler(this.chckbxProection_CheckedChanged);
            // 
            // chkbxNormal
            // 
            this.chkbxNormal.AutoSize = true;
            this.chkbxNormal.Location = new System.Drawing.Point(1065, 85);
            this.chkbxNormal.Name = "chkbxNormal";
            this.chkbxNormal.Size = new System.Drawing.Size(147, 17);
            this.chkbxNormal.TabIndex = 1;
            this.chkbxNormal.Text = "Сглаживание нормалей";
            this.chkbxNormal.UseVisualStyleBackColor = true;
            this.chkbxNormal.CheckedChanged += new System.EventHandler(this.chkbxNormal_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(1065, 108);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(147, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Сглаживание нормалей";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 601);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.chkbxNormal);
            this.Controls.Add(this.chckbxProection);
            this.Controls.Add(this.chckbxCarcass);
            this.Controls.Add(this.glControl1);
            this.Name = "Form1";
            this.Text = "г";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chckbxCarcass;
        private System.Windows.Forms.CheckBox chckbxProection;
        private System.Windows.Forms.CheckBox chkbxNormal;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

