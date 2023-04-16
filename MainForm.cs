using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using ExifLibrary;

namespace ModPhoto
{
	public partial class MainForm : Form
	{
		private string currentDirPath;

		public MainForm()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			InitializeListView();
			InitializeDateTimePicker();
		}

		private void InitializeListView()
		{
			int listViewWidth = listView1.Width;
			ColumnHeader colHead;
			colHead = new ColumnHeader();
			colHead.Text = "파일";
			colHead.Width = 300;
			listView1.Columns.Add(colHead);

			colHead = new ColumnHeader();
			colHead.Text = "DateTime";
			colHead.Width = 190;
			listView1.Columns.Add(colHead);

			colHead = new ColumnHeader();
			colHead.Text = "DateTimeDigitized";
			colHead.Width = 190;
			listView1.Columns.Add(colHead);

			colHead = new ColumnHeader();
			colHead.Text = "DateTimeOriginal";
			colHead.Width = 190;
			listView1.Columns.Add(colHead);
		}

		private void InitializeDateTimePicker()
		{
			dateTimePicker1.MinDate = new DateTime(1985, 6, 20);
			//dateTimePicker1.MaxDate = DateTime.Now;

			dateTimePicker1.Format = DateTimePickerFormat.Custom;
			dateTimePicker1.CustomFormat = "yyyy-MM-dd HH-mm-ss";
			dateTimePicker1.ShowUpDown = true;
			dateTimePicker1.Value = DateTime.Now;
		}


        #region Load Files
        private void listView1_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		private void listView1_DragDrop(object sender, DragEventArgs e)
		{
			CheckForIllegalCrossThreadCalls = false;
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

			if(files!=null && files.Length > 0)
            {
				var fileFirst = files[0];
				var file = new FileInfo(fileFirst);

				var dir = file.Directory.ToString();
				if (!string.IsNullOrWhiteSpace(dir))
				{
					currentDirPath = dir;
				}
			}

			foreach (string file2 in files)
			{
				FileInfo file = new FileInfo(file2);
                 LoadFile(file);
			}
		} 
		 
        private void btnImport_Click(object sender, EventArgs e) //선택한 디렉토리에서 파일리스트 처리
		{
			string dirPath = textBox1.Text;
			if (dirPath != String.Empty)
			{
				if (Directory.Exists(dirPath))
				{  
					CheckForIllegalCrossThreadCalls = false;
					listView1.Items.Clear();

					currentDirPath = dirPath;
					DirectoryInfo dir = new DirectoryInfo(dirPath);
					FileInfo[] files = dir.GetFiles();
					//string[] fileList = Directory.GetFiles(dirPath);
					foreach (FileInfo file in files)
					{
						LoadFile(file);
					}
				}
				else
				{
					MessageBox.Show("경로가 존재하지 않습니다.", "경고");
				} 
			}
			else
			{
				MessageBox.Show("경로를 입력하세요", "경고");
			}
		}

		private void LoadFile(FileInfo file)
		{

			try
			{
				//if (file.Extension.ToLower() == ".jpg"
				//	|| file.Extension.ToLower() == ".png")
				//{
					ListViewItem item = new ListViewItem();

					item.Text = file.Name;

					//Image img = new Bitmap(file.FullName);
					//PropertyItem[] propItems = img.PropertyItems;
					//ASCIIEncoding encoding = new ASCIIEncoding();
					//string shootDate = encoding.GetString(propItems[14].Value);

					string dateText1 = "";
					string dateText2 = "";
					string dateText3 = "";

					try
					{
						// Extract exif metadata
						ExifFile exifFile = ExifFile.Read(file.FullName);

						//if (exifFile.Properties.Count != 0)
						if (exifFile.Properties.ContainsKey(ExifTag.DateTime))
						{
							dateText1 = exifFile.Properties[ExifTag.DateTime].Value.ToString();
						}

						if (exifFile.Properties.ContainsKey(ExifTag.DateTimeDigitized))
						{
							dateText2 = exifFile.Properties[ExifTag.DateTimeDigitized].Value.ToString();
						}

						if (exifFile.Properties.ContainsKey(ExifTag.DateTimeOriginal))
						{
							dateText3 = exifFile.Properties[ExifTag.DateTimeOriginal].Value.ToString();
						}
					}
					catch (System.FormatException fe)
					{ 
					}
					catch(Exception ex)
                    {

                    }

					if (string.IsNullOrWhiteSpace(dateText1))
					{
						if (!string.IsNullOrWhiteSpace(dateText3))
						{
							dateText1 = dateText3;
							dateText2 = dateText3;
						}
						else
						{
							if (file.Name.Length >= 8)
							{
								dateText1 = GetDateByFileName(file);
							}
							 
							if (string.IsNullOrWhiteSpace(dateText1))
							{
								dateText1 = file.CreationTime.ToString();
							}
							dateText2 = dateText1;
							dateText3 = dateText1;
						}
					}

					if (string.IsNullOrWhiteSpace(dateText2))
					{
						dateText2 = dateText1;
					}

					item.SubItems.Add(dateText1);
					item.SubItems.Add(dateText2);
					item.SubItems.Add(dateText3);

					listView1.Items.Add(item);
				//}
				//else if (file.Extension.ToLower() == ".mp4")
				//{
				//	ListViewItem item = new ListViewItem();

				//	item.Text = file.Name;

				//	//Image img = new Bitmap(file.FullName);
				//	//PropertyItem[] propItems = img.PropertyItems;
				//	//ASCIIEncoding encoding = new ASCIIEncoding();
				//	//string shootDate = encoding.GetString(propItems[14].Value);

				//	string dateText1 = "";
				//	string dateText2 = "";
				//	string dateText3 = "";

				//	if (file.Name.Length >= 8)
				//	{
				//		dateText1 = GetDateByFileName(file);
				//	}

				//	dateText2 = dateText1;
				//	dateText3 = dateText1;

				//	item.SubItems.Add(dateText1);
				//	item.SubItems.Add(dateText2);
				//	item.SubItems.Add(dateText3);

				//	listView1.Items.Add(item);
				//}
			}
			catch (Exception exception)
			{
				//MessageBox.Show("예외발생 : " + exception.Message, "경고");
			}
		}

        private string GetDateByFileName(FileInfo file)
		{
			string result = string.Empty;

			int nIndexYYYY = file.Name.IndexOf("20");
			if (nIndexYYYY >= 0)
			{
				if (file.Name.Length >= nIndexYYYY + 8)
				{
					var date = file.Name.Substring(nIndexYYYY, 8);

					long lDate;
					if (long.TryParse(date, out lDate))
					{
						if (lDate > 0 && lDate > 20000000 && lDate < 20990000)
						{
							var year = Convert.ToInt32(date.Substring(0, 4));
							var month = Convert.ToInt32(date.Substring(4, 2));
							var day = Convert.ToInt32(date.Substring(6, 2));
							var hour = 0;
							var min = 0;
							var sec = 0;
							if (file.Name.Length >= 15)
							{
								var date2 = file.Name.Substring(9, 6);
								long lDate2;
								if (long.TryParse(date2, out lDate2))
								{
									if (lDate2 > 0)
									{
										hour = Convert.ToInt32(date2.Substring(0, 2));
										min = Convert.ToInt32(date2.Substring(2, 2));
										sec = Convert.ToInt32(date2.Substring(4, 2));
									}
								}
							}
							try
							{
								DateTime dt = new DateTime(year, month, day, hour, min, sec);
								result = dt.ToString();
							}
							catch (Exception ex)
							{

							}

						}
						else
						{
							result = file.LastWriteTime.ToString();
						}
					}
					else
					{
						result = file.LastWriteTime.ToString();
					}
				}
			}

			return result;
		}
        #endregion

        private void btnFolder_Click(object sender, EventArgs e) //디렉토리 선택버튼
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();

			dialog.ShowDialog();
			string dirPath = dialog.SelectedPath;
			textBox1.Text = dirPath;
		}

        #region Update This
        private void btnUpdateThis_Click(object sender, EventArgs e)
		{

			//MessageBox.Show(dateTimePicker1.Value.ToString());

			if (listView1.SelectedItems.Count == 0)
			{
				MessageBox.Show("선택된 파일이 없습니다.", "경고");
			}
			else
			{
				UpdateDateTime();
			}
		}

		private void UpdateDateTime()
		{ 
			try
			{
				DateTime dt = dateTimePicker1.Value;

				foreach (ListViewItem item in listView1.SelectedItems)
				{

					string fullPath = currentDirPath + "\\" + item.Text;
					bool tagFlag = false;
					if (File.Exists(fullPath))
					{
						//richTextBox1.Text += fullPath + "->" + dt.ToString() + "\n";
						//파일 날짜 변경
						//FileInfo fi = new FileInfo(fullPath);
						//fi.CreationTime = dt;
						//fi.LastWriteTime = dt;
						//fi.LastAccessTime = dt;


						if (fullPath.ToLower().Contains(".mp4"))
						{

						}
						else
						{
							try
							{
								ExifFile exifFile = ExifFile.Read(fullPath);
								if (exifFile.Properties.ContainsKey(ExifTag.DateTime))
								{
									exifFile.Properties[ExifTag.DateTime].Value = dt;
									item.SubItems[1].Text = dt.ToString();
									tagFlag = true;
								}

								if (exifFile.Properties.ContainsKey(ExifTag.DateTimeDigitized))
								{
									exifFile.Properties[ExifTag.DateTimeDigitized].Value = dt;
									item.SubItems[2].Text = dt.ToString();
									tagFlag = true;
								}

								if (exifFile.Properties.ContainsKey(ExifTag.DateTimeOriginal))
								{
									exifFile.Properties[ExifTag.DateTimeOriginal].Value = dt;
									item.SubItems[3].Text = dt.ToString();
									tagFlag = true;
								}

								if (tagFlag)
								{
									exifFile.Save(fullPath);
								}
							}
							catch(Exception ex) { }
						}

						File.SetCreationTime(fullPath, dt);
						File.SetLastWriteTime(fullPath, dt);
						File.SetLastAccessTime(fullPath, dt);
						//dt = dt.AddSeconds(30); //30초 추가한 파일로 변경함
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show("예외발생 : " + exception.Message, "경고");
			}
		} 

		#endregion


		private void btnFileName_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (ListViewItem item in listView1.SelectedItems)
				{
					string fullPath = currentDirPath + "\\" + item.Text;
					if (File.Exists(fullPath))
					{
						FileInfo fileInfo = null;
						string fileName;
						bool tagFlag = false;

						if (fullPath.ToLower().Contains(".jpg"))
						{
							ExifFile exifFile = ExifFile.Read(fullPath);
							if (exifFile.Properties.ContainsKey(ExifTag.DateTime))
							{
								fileName = ((DateTime)exifFile.Properties[ExifTag.DateTime].Value).ToString("yyyyMMdd_HHmmss");
								fileInfo = new FileInfo(fullPath);
								fileInfo.MoveTo(Path.Combine(fileInfo.DirectoryName, $"{fileName}.{fileInfo.Extension.ToLower()}"));
								tagFlag = true;
							}
							if (!tagFlag)
							{
								fileInfo = new FileInfo(fullPath);
								fileName = ((DateTime)fileInfo.CreationTime).ToString("yyyyMMdd_HHmmss");
								fileInfo.MoveTo(Path.Combine(fileInfo.DirectoryName, $"{fileName}.{fileInfo.Extension.ToLower()}"));
							}
						}
                        else
						{
							fileName = ((DateTime)fileInfo.CreationTime).ToString("yyyyMMdd_HHmmss");
							fileInfo.MoveTo(Path.Combine(fileInfo.DirectoryName, $"{fileName}.{fileInfo.Extension.ToLower()}"));
						}
						//else
						//{
						//try
						//{
						//	ExifFile exifFile = ExifFile.Read(fullPath);
						//	if (exifFile.Properties.ContainsKey(ExifTag.DateTime))
						//	{
						//		var fileName = ((DateTime)exifFile.Properties[ExifTag.DateTime].Value).ToString("yyyyMMdd_HHmmss");
						//		var fileInfo = new FileInfo(fullPath);
						//		fileInfo.MoveTo(Path.Combine(fileInfo.DirectoryName, $"{fileName}.{fileInfo.Extension}"));
						//		exifFile.Properties[ExifTag.DateTime].Value = dt;
						//		item.SubItems[1].Text = dt.ToString();
						//		tagFlag = true;
						//	}

						//	if (exifFile.Properties.ContainsKey(ExifTag.DateTimeDigitized))
						//	{
						//		exifFile.Properties[ExifTag.DateTimeDigitized].Value = dt;
						//		item.SubItems[2].Text = dt.ToString();
						//		tagFlag = true;
						//	}

						//	if (exifFile.Properties.ContainsKey(ExifTag.DateTimeOriginal))
						//	{
						//		exifFile.Properties[ExifTag.DateTimeOriginal].Value = dt;
						//		item.SubItems[3].Text = dt.ToString();
						//		tagFlag = true;
						//	}

						//	if (tagFlag)
						//	{
						//		exifFile.Save(fullPath);
						//	}
						//}
						//catch (Exception ex) { }
						//}
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show("예외발생 : " + exception.Message, "경고");
			}

			//string path = @"C:\Users\ssd\Pictures\hp\IMG_0001.JPG";
			//DateTime dt = new DateTime(1999, 4, 3);
			////FileInfo fi = new FileInfo("l:\\DCIM\\100EOS5D\\hp\\IMG_9465.JPG");
			//richTextBox1.Text += path + "->" + dt.ToString() + "\n";
			////fi.CreationTime = dateTimePicker1.Value;
			//File.SetCreationTime(path, dt);
			//File.SetLastWriteTime(path, dt);
			//File.SetLastAccessTime(path, dt);
		}

        #region Update
        private void btnUpdateAuto_Click(object sender, EventArgs e)
		{

			if (listView1.Items.Count == 0)
			{
				MessageBox.Show("선택된 파일이 없습니다.", "경고");
			}
			else
			{
				UpdateDateAuto();
			}
		}
		 
        private void UpdateDateAuto()
		{ 
			foreach (ListViewItem item in listView1.Items)
			{
				try
				{
					string fullPath = currentDirPath + "\\" + item.Text;
					bool tagFlag = false;
					if (File.Exists(fullPath))
					{
						//richTextBox1.Text += fullPath + "->" + dt.ToString() + "\n";
						//파일 날짜 변경
						//FileInfo fi = new FileInfo(fullPath);
						//fi.CreationTime = dt;
						//fi.LastWriteTime = dt;
						//fi.LastAccessTime = dt;

						//
						DateTime dt;
						if (DateTime.TryParse(item.SubItems[1].Text, out dt))
						{
							if (fullPath.ToLower().Contains(".mp4"))
							{

							}
							else
							{
								try
								{
									ExifFile exifFile = ExifFile.Read(fullPath);
									if (exifFile.Properties.ContainsKey(ExifTag.DateTime))
									{
										exifFile.Properties[ExifTag.DateTime].Value = dt;
										item.SubItems[1].Text = dt.ToString();
										tagFlag = true;
									}

									if (exifFile.Properties.ContainsKey(ExifTag.DateTimeDigitized))
									{
										exifFile.Properties[ExifTag.DateTimeDigitized].Value = dt;
										item.SubItems[2].Text = dt.ToString();
										tagFlag = true;
									}

									if (exifFile.Properties.ContainsKey(ExifTag.DateTimeOriginal))
									{
										exifFile.Properties[ExifTag.DateTimeOriginal].Value = dt;
										item.SubItems[3].Text = dt.ToString();
										tagFlag = true;
									}
									if (tagFlag)
									{
										exifFile.Save(fullPath);
									}
								}
								catch(System.FormatException fe)
                                {

                                }

							}


							File.SetCreationTime(fullPath, dt);
							File.SetLastWriteTime(fullPath, dt);
							File.SetLastAccessTime(fullPath, dt);
							//dt = dt.AddSeconds(30); //30초 추가한 파일로 변경함
						}
					} 
				}
				catch (Exception exception)
				{
					//MessageBox.Show("예외발생 : " + exception.Message, "경고");
				}
			}
		}
		 

        #endregion

        private void listView1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.A && e.Control)
			{
				listView1.MultiSelect = true;
				foreach (ListViewItem item in listView1.Items)
				{
					item.Selected = true;
				}
			}
		}

        private void btnClear_Click(object sender, EventArgs e)
        {
			listView1.Items.Clear();
			currentDirPath = string.Empty;
        }
    }
}
