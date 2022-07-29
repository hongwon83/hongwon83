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
		WaitForm loading;

		public MainForm()
		{
			InitializeComponent();
			loading = new WaitForm();
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

		private void listView1_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;

		}
		private void listView1_DragDrop(object sender, DragEventArgs e)
		{
			CheckForIllegalCrossThreadCalls = false;
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			foreach (string file2 in files)
			{
				FileInfo file = new FileInfo(file2);
				if (file.Extension.ToLower() == ".jpg")
				{
					ListViewItem item = new ListViewItem();

					item.Text = file.Name;

					string dateText1 = "";
					string dateText2 = "";
					string dateText3 = "";

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

					item.SubItems.Add(dateText1);
					item.SubItems.Add(dateText2);
					item.SubItems.Add(dateText3);

					listView1.Items.Add(item);
				}
			}
		}

		private void GetfileList(string path)
		{
			CheckForIllegalCrossThreadCalls = false;
			listView1.Items.Clear();

			currentDirPath = path;
			DirectoryInfo dir = new DirectoryInfo(path);
			FileInfo[] files = dir.GetFiles();
			//string[] fileList = Directory.GetFiles(dirPath);
			foreach (FileInfo file in files)
			{
				try
				{
					if (file.Extension.ToLower() == ".jpg"
						|| file.Extension.ToLower() == ".png")
					{
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
									int nIndexYYYY = file.Name.IndexOf("20");
									if (nIndexYYYY >= 0)
									{
										if (file.Name.Length >= nIndexYYYY + 8)
										{
											var date = file.Name.Substring(nIndexYYYY, 8);

											long lDate;
											if (long.TryParse(date, out lDate))
											{
												if (lDate > 0 && lDate > 20000000 && lDate < 20230000)
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
														dateText1 = dt.ToString();
													}
													catch (Exception ex)
													{

													}

												}
												else
												{
													dateText1 = file.LastWriteTime.ToString();
												}
											}
											else
											{
												dateText1 = file.LastWriteTime.ToString();
											}
										}
									}
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
					}
					else if (file.Extension.ToLower() == ".mp4")
					{
						ListViewItem item = new ListViewItem();

						item.Text = file.Name;

						//Image img = new Bitmap(file.FullName);
						//PropertyItem[] propItems = img.PropertyItems;
						//ASCIIEncoding encoding = new ASCIIEncoding();
						//string shootDate = encoding.GetString(propItems[14].Value);

						string dateText1 = "";
						string dateText2 = "";
						string dateText3 = "";

						if (file.Name.Length >= 15)
						{
							var date = file.Name.Substring(0, 8);
							long lDate;
							if (long.TryParse(date, out lDate))
							{
								if (lDate > 0 && lDate > 20000000)
								{
									var year = Convert.ToInt32(date.Substring(0, 4));
									var month = Convert.ToInt32(date.Substring(4, 2));
									var day = Convert.ToInt32(date.Substring(6, 2));
									var hour = 0;
									var min = 0;
									var sec = 0;

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

									DateTime dt = new DateTime(year, month, day, hour, min, sec);

									if (!dt.Equals(file.LastWriteTime))
									{
										dateText1 = dt.ToString();
									}
								}
							}
						}
						dateText2 = dateText1;
						dateText3 = dateText1;

						item.SubItems.Add(dateText1);
						item.SubItems.Add(dateText2);
						item.SubItems.Add(dateText3);

						listView1.Items.Add(item);
					}
				}
				catch (Exception exception)
				{
					//MessageBox.Show("예외발생 : " + exception.Message, "경고");
				}
			}
		}

		private void btnImport_Click(object sender, EventArgs e) //선택한 디렉토리에서 파일리스트 처리
		{
			string dirPath = textBox1.Text;
			if (dirPath != String.Empty)
			{

				if (Directory.Exists(dirPath))
				{

					//GetfileList(dirPath);
					workerImport.RunWorkerAsync(dirPath);

					loading.ShowDialog();
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

		private void button2_Click(object sender, EventArgs e) //디렉토리 선택버튼
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();

			dialog.ShowDialog();
			string dirPath = dialog.SelectedPath;
			textBox1.Text = dirPath;
		}

		private void btnUpdateThis_Click(object sender, EventArgs e)
		{

			//MessageBox.Show(dateTimePicker1.Value.ToString());

			if (listView1.SelectedItems.Count == 0)
			{
				MessageBox.Show("선택된 파일이 없습니다.", "경고");
			}
			else
			{
				//if (MessageBox.Show("촬영 날짜와 수정된 날짜가 모두 변경됩니다.\n날짜를 변경하시겠습니까?", "변경확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
				//{
				workerUpdateThis.RunWorkerAsync();

				loading.ShowDialog();

				//}

			}
		}


		private void UpdateDateTime()
		{
			//string path = @"C:\Users\ssd\Pictures\hp\IMG_0001.JPG";
			//DateTime dt2 = new DateTime(2000, 4, 3);
			////FileInfo fi = new FileInfo("l:\\DCIM\\100EOS5D\\hp\\IMG_9465.JPG");
			//richTextBox1.Text += path + "->" + dt2.ToString() + "\n";
			////fi.CreationTime = dateTimePicker1.Value;
			//File.SetCreationTime(path, dt2);
			//File.SetLastWriteTime(path, dt2);
			//File.SetLastAccessTime(path, dt2);



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


						File.SetCreationTime(fullPath, dt);
						File.SetLastWriteTime(fullPath, dt);
						File.SetLastAccessTime(fullPath, dt);
						//dt = dt.AddSeconds(30); //30초 추가한 파일로 변경함
					}


					//string fullPath = currentDirPath + "\\" + item.Text;
					//bool tagFlag = false;
					//if (File.Exists(fullPath))
					//{
					//                   richTextBox1.Text+=fullPath+"->"+dt.ToString()+"\n";
					//                   //파일 날짜 변경
					//                   //FileInfo fi = new FileInfo(fullPath);
					//                   //fi.CreationTime = dt;
					//                   //fi.LastWriteTime = dt;
					//                   //fi.LastAccessTime = dt;

					//                   //


					//	ExifFile exifFile = ExifFile.Read(fullPath);
					//	if (exifFile.Properties.ContainsKey(ExifTag.DateTime))
					//	{
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

					//                   File.SetCreationTime(fullPath, dt);
					//                   File.SetLastWriteTime(fullPath, dt);
					//                   File.SetLastAccessTime(fullPath, dt);
					//                   dt = dt.AddSeconds(30); //30초 추가한 파일로 변경함
					//}

				}

			}
			catch (Exception exception)
			{
				MessageBox.Show("예외발생 : " + exception.Message, "경고");
			}
		}

		private void workerImport_DoWork(object sender, DoWorkEventArgs e)
		{
			string path = e.Argument.ToString();
			GetfileList(path);
		}

		private void workerImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			loading.Close();
		}

		private void workerUpdateThis_DoWork(object sender, DoWorkEventArgs e)
		{
			UpdateDateTime();
		}

		private void workerUpdateThis_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			loading.Close();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			string path = @"C:\Users\ssd\Pictures\hp\IMG_0001.JPG";
			DateTime dt = new DateTime(1999, 4, 3);
			//FileInfo fi = new FileInfo("l:\\DCIM\\100EOS5D\\hp\\IMG_9465.JPG");
			richTextBox1.Text += path + "->" + dt.ToString() + "\n";
			//fi.CreationTime = dateTimePicker1.Value;
			File.SetCreationTime(path, dt);
			File.SetLastWriteTime(path, dt);
			File.SetLastAccessTime(path, dt);
		}

		private void btnUpdateDate_Click(object sender, EventArgs e)
		{

			if (listView1.Items.Count == 0)
			{
				MessageBox.Show("선택된 파일이 없습니다.", "경고");
			}
			else
			{
				//if (MessageBox.Show("촬영 날짜와 수정된 날짜가 모두 변경됩니다.\n날짜를 변경하시겠습니까?", "변경확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
				//{
				workerUpdateDate.RunWorkerAsync();

				loading.ShowDialog();
				//}
			}
		}

		private void workerUpdateDate_DoWork(object sender, DoWorkEventArgs e)
		{
			UpdateDateTime2();
		}

		private void workerUpdateDate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			loading.Close();
		}
		private void UpdateDateTime2()
		{
			//string path = @"C:\Users\ssd\Pictures\hp\IMG_0001.JPG";
			//DateTime dt2 = new DateTime(2000, 4, 3);
			////FileInfo fi = new FileInfo("l:\\DCIM\\100EOS5D\\hp\\IMG_9465.JPG");
			//richTextBox1.Text += path + "->" + dt2.ToString() + "\n";
			////fi.CreationTime = dateTimePicker1.Value;
			//File.SetCreationTime(path, dt2);
			//File.SetLastWriteTime(path, dt2);
			//File.SetLastAccessTime(path, dt2);



			//DateTime dt = dateTimePicker1.Value;

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
    }
}
