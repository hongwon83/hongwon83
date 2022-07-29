namespace ModPhoto
{
	partial class MainForm
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnUpdateThis = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.workerImport = new System.ComponentModel.BackgroundWorker();
            this.workerUpdateThis = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.btnUpdateData = new System.Windows.Forms.Button();
            this.workerUpdateDate = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 452);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(806, 25);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "D:\\test";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(919, 452);
            this.btnImport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(86, 29);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "불러오기";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(827, 452);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 29);
            this.button2.TabIndex = 3;
            this.button2.Text = "폴더";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnUpdateThis
            // 
            this.btnUpdateThis.Location = new System.Drawing.Point(226, 486);
            this.btnUpdateThis.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdateThis.Name = "btnUpdateThis";
            this.btnUpdateThis.Size = new System.Drawing.Size(86, 29);
            this.btnUpdateThis.TabIndex = 4;
            this.btnUpdateThis.Text = "날짜변경";
            this.btnUpdateThis.UseVisualStyleBackColor = true;
            this.btnUpdateThis.Click += new System.EventHandler(this.btnUpdateThis_Click);
            // 
            // listView1
            // 
            this.listView1.AllowDrop = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(14, 15);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(994, 408);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 434);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "디렉토리선택";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(14, 486);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(205, 25);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // workerImport
            // 
            this.workerImport.WorkerReportsProgress = true;
            this.workerImport.WorkerSupportsCancellation = true;
            this.workerImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerImport_DoWork);
            this.workerImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerImport_RunWorkerCompleted);
            // 
            // workerUpdateThis
            // 
            this.workerUpdateThis.WorkerReportsProgress = true;
            this.workerUpdateThis.WorkerSupportsCancellation = true;
            this.workerUpdateThis.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerUpdateThis_DoWork);
            this.workerUpdateThis.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerUpdateThis_RunWorkerCompleted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 494);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "30초간격으로 시간을 기록함";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(14, 520);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(991, 134);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(505, 484);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 29);
            this.button4.TabIndex = 10;
            this.button4.Text = "test set";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnUpdateData
            // 
            this.btnUpdateData.Location = new System.Drawing.Point(827, 483);
            this.btnUpdateData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdateData.Name = "btnUpdateData";
            this.btnUpdateData.Size = new System.Drawing.Size(178, 29);
            this.btnUpdateData.TabIndex = 11;
            this.btnUpdateData.Text = "날짜변경 자동";
            this.btnUpdateData.UseVisualStyleBackColor = true;
            this.btnUpdateData.Click += new System.EventHandler(this.btnUpdateDate_Click);
            // 
            // workerUpdateDate
            // 
            this.workerUpdateDate.WorkerReportsProgress = true;
            this.workerUpdateDate.WorkerSupportsCancellation = true;
            this.workerUpdateDate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerUpdateDate_DoWork);
            this.workerUpdateDate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerUpdateDate_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 670);
            this.Controls.Add(this.btnUpdateData);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnUpdateThis);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.textBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "사진날짜변경";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button btnUpdateThis;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.ComponentModel.BackgroundWorker workerImport;
        private System.ComponentModel.BackgroundWorker workerUpdateThis;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnUpdateData;
        private System.ComponentModel.BackgroundWorker workerUpdateDate;
    }
}

