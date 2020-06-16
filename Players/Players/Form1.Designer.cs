namespace Players
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
            this.label1 = new System.Windows.Forms.Label();
            this.AddPlayer = new System.Windows.Forms.Button();
            this.DeletePlayer = new System.Windows.Forms.Button();
            this.EditPanel = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.editInfo = new System.Windows.Forms.Button();
            this.charactersType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.playerInfo = new System.Windows.Forms.CheckedListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.EditPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Текущий список игроков";
            // 
            // AddPlayer
            // 
            this.AddPlayer.Enabled = false;
            this.AddPlayer.Location = new System.Drawing.Point(29, 464);
            this.AddPlayer.Name = "AddPlayer";
            this.AddPlayer.Size = new System.Drawing.Size(132, 32);
            this.AddPlayer.TabIndex = 3;
            this.AddPlayer.Text = "Добавить";
            this.AddPlayer.UseVisualStyleBackColor = true;
            this.AddPlayer.Click += new System.EventHandler(this.AddPlayer_Click);
            // 
            // DeletePlayer
            // 
            this.DeletePlayer.Location = new System.Drawing.Point(180, 464);
            this.DeletePlayer.Name = "DeletePlayer";
            this.DeletePlayer.Size = new System.Drawing.Size(137, 32);
            this.DeletePlayer.TabIndex = 4;
            this.DeletePlayer.Text = "Удалить";
            this.DeletePlayer.UseVisualStyleBackColor = true;
            this.DeletePlayer.Click += new System.EventHandler(this.DeletePlayer_Click);
            // 
            // EditPanel
            // 
            this.EditPanel.Controls.Add(this.listBox1);
            this.EditPanel.Controls.Add(this.editInfo);
            this.EditPanel.Location = new System.Drawing.Point(374, 19);
            this.EditPanel.Name = "EditPanel";
            this.EditPanel.Size = new System.Drawing.Size(412, 497);
            this.EditPanel.TabIndex = 5;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(19, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(370, 420);
            this.listBox1.TabIndex = 1;
            // 
            // editInfo
            // 
            this.editInfo.Location = new System.Drawing.Point(159, 456);
            this.editInfo.Name = "editInfo";
            this.editInfo.Size = new System.Drawing.Size(97, 32);
            this.editInfo.TabIndex = 0;
            this.editInfo.Text = "Изменить";
            this.editInfo.UseVisualStyleBackColor = true;
            this.editInfo.Click += new System.EventHandler(this.editInfo_Click);
            // 
            // charactersType
            // 
            this.charactersType.FormattingEnabled = true;
            this.charactersType.Location = new System.Drawing.Point(73, 48);
            this.charactersType.Name = "charactersType";
            this.charactersType.Size = new System.Drawing.Size(201, 24);
            this.charactersType.TabIndex = 6;
            this.charactersType.SelectedIndexChanged += new System.EventHandler(this.charactersType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Выберите тип персонажа:";
            // 
            // playerInfo
            // 
            this.playerInfo.FormattingEnabled = true;
            this.playerInfo.Location = new System.Drawing.Point(29, 125);
            this.playerInfo.Name = "playerInfo";
            this.playerInfo.Size = new System.Drawing.Size(288, 327);
            this.playerInfo.TabIndex = 8;
            this.playerInfo.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.playerInfo_ItemCheck);
            this.playerInfo.SelectedIndexChanged += new System.EventHandler(this.playerInfo_SelectedIndexChanged_1);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(104, 502);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(132, 32);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 546);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.playerInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.charactersType);
            this.Controls.Add(this.EditPanel);
            this.Controls.Add(this.DeletePlayer);
            this.Controls.Add(this.AddPlayer);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.EnabledChanged += new System.EventHandler(this.Form1_EnabledChanged);
            this.EditPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddPlayer;
        private System.Windows.Forms.Button DeletePlayer;
        private System.Windows.Forms.Panel EditPanel;
        private System.Windows.Forms.ComboBox charactersType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button editInfo;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.CheckedListBox playerInfo;
        private System.Windows.Forms.Button btnSave;
    }
}

