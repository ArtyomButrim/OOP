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
            this.playerInfo = new System.Windows.Forms.ListBox();
            this.AddPlayer = new System.Windows.Forms.Button();
            this.DeletePlayer = new System.Windows.Forms.Button();
            this.EditPanel = new System.Windows.Forms.Panel();
            this.charactersType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.editInfo = new System.Windows.Forms.Button();
            this.EditPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(356, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Текущий список игроков";
            // 
            // playerInfo
            // 
            this.playerInfo.FormattingEnabled = true;
            this.playerInfo.ItemHeight = 16;
            this.playerInfo.Location = new System.Drawing.Point(294, 29);
            this.playerInfo.Name = "playerInfo";
            this.playerInfo.Size = new System.Drawing.Size(308, 356);
            this.playerInfo.TabIndex = 2;
            // 
            // AddPlayer
            // 
            this.AddPlayer.Location = new System.Drawing.Point(340, 391);
            this.AddPlayer.Name = "AddPlayer";
            this.AddPlayer.Size = new System.Drawing.Size(90, 23);
            this.AddPlayer.TabIndex = 3;
            this.AddPlayer.Text = "Добавить";
            this.AddPlayer.UseVisualStyleBackColor = true;
            // 
            // DeletePlayer
            // 
            this.DeletePlayer.Location = new System.Drawing.Point(436, 391);
            this.DeletePlayer.Name = "DeletePlayer";
            this.DeletePlayer.Size = new System.Drawing.Size(90, 23);
            this.DeletePlayer.TabIndex = 4;
            this.DeletePlayer.Text = "Удалить";
            this.DeletePlayer.UseVisualStyleBackColor = true;
            // 
            // EditPanel
            // 
            this.EditPanel.Controls.Add(this.editInfo);
            this.EditPanel.Location = new System.Drawing.Point(661, 29);
            this.EditPanel.Name = "EditPanel";
            this.EditPanel.Size = new System.Drawing.Size(352, 356);
            this.EditPanel.TabIndex = 5;
            // 
            // charactersType
            // 
            this.charactersType.FormattingEnabled = true;
            this.charactersType.Location = new System.Drawing.Point(36, 38);
            this.charactersType.Name = "charactersType";
            this.charactersType.Size = new System.Drawing.Size(201, 24);
            this.charactersType.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Выберите тип персонажа:";
            // 
            // editInfo
            // 
            this.editInfo.Location = new System.Drawing.Point(262, 10);
            this.editInfo.Name = "editInfo";
            this.editInfo.Size = new System.Drawing.Size(75, 23);
            this.editInfo.TabIndex = 0;
            this.editInfo.Text = "Изменить";
            this.editInfo.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 438);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.charactersType);
            this.Controls.Add(this.EditPanel);
            this.Controls.Add(this.DeletePlayer);
            this.Controls.Add(this.AddPlayer);
            this.Controls.Add(this.playerInfo);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.EditPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox playerInfo;
        private System.Windows.Forms.Button AddPlayer;
        private System.Windows.Forms.Button DeletePlayer;
        private System.Windows.Forms.Panel EditPanel;
        private System.Windows.Forms.ComboBox charactersType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button editInfo;
    }
}

