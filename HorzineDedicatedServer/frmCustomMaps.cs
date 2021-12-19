using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorzineDedicatedServer
{
    public partial class frmCustomMaps : Form
    {

        class INIFile
        {
            private string filePath;

            [DllImport("kernel32")]
            private static extern long WritePrivateProfileString(string section,
            string key,
            string val,
            string filePath);

            [DllImport("kernel32")]
            private static extern int GetPrivateProfileString(string section,
            string key,
            string def,
            StringBuilder retVal,
            int size,
            string filePath);

            public INIFile(string filePath)
            {
                this.filePath = filePath;
            }

            public void Write(string section, string key, string value)
            {
                WritePrivateProfileString(section, key, value.ToLower(), this.filePath);
            }

            public string Read(string section, string key)
            {
                StringBuilder SB = new StringBuilder(255);
                int i = GetPrivateProfileString(section, key, "", SB, 255, this.filePath);
                return SB.ToString();
            }

            public string FilePath
            {
                get { return this.filePath; }
                set { this.filePath = value; }
            }
        }

        public frmCustomMaps()
        {
            InitializeComponent();
        }

        string strItemTitle = "";
        string strItemId = "";
        string strImageLink = "";
        string strActivated = "false";

        string strTituloSelected = "";
        string strImageLinkSelected = "";
        string strActivatedSelected = "";

        string nomeTecnicoDoMapa = "";

        string strFriendlyMapName = "";
        string strTecnMapName = "";

        string strSoundEffects = "";

        bool soundClickActivated = true;


        Color corTexto = Color.FromArgb(251, 210, 191);

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
        FontFamily font_Insomnia;

        private void Custom_font()
        {
            PrivateFontCollection new_Font = new PrivateFontCollection();

            int long_font = Properties.Resources.Insomnia_1.Length;

            byte[] fontdata = Properties.Resources.Insomnia_1;

            IntPtr replace = Marshal.AllocCoTaskMem(long_font);

            Marshal.Copy(fontdata, 0, replace, long_font);

            uint cFonts = 0;

            AddFontMemResourceEx(replace, (uint)fontdata.Length, IntPtr.Zero, ref cFonts);

            new_Font.AddMemoryFont(replace, long_font);

            Marshal.FreeCoTaskMem(replace);

            font_Insomnia = new_Font.Families[0];
        }

        private void listBoxActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SoundEffectClick();
            try
            {
                using (var client = new WebClient())
                using (var streamCheck = client.OpenRead("http://www.google.com"))
                {
                    pnInternetConection.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconConected;
                    this.listBoxActive.Invalidate();
                    RetornarActive();
                }
            }
            catch
            {
                MessageBox.Show("No Internet Conection.", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                pnInternetConection.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconNotConected;
            }

            
        }

        private void listBoxNotActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SoundEffectClick();

            try
            {
                using (var client = new WebClient())
                using (var streamCheck = client.OpenRead("http://www.google.com"))
                {
                    pnInternetConection.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconConected;
                    this.listBoxNotActive.Invalidate();
                    RetornarNotActive();
                }
            }
            catch
            {
                MessageBox.Show("No Internet Conection.", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                pnInternetConection.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconNotConected;
            }
            
        }

        private void VerificarSeTagWorkshopExiste()
        {
            string novoTexto = "";
            string text = File.ReadAllText(@"KFGame\Config\PCServer-KFEngine.ini", Encoding.UTF8);

            if (text.Contains("[OnlineSubsystemSteamworks.KFWorkshopSteamworks]")) { }
            else
            {
                novoTexto = "[OnlineSubsystemSteamworks.KFWorkshopSteamworks]" + Environment.NewLine + "<WorkshopItemId>" + Environment.NewLine + Environment.NewLine + text;

                File.WriteAllText(@"KFGame\Config\PCServer-KFEngine.ini", novoTexto);

            }

            if (text.Contains("http://skillzservers.com/kf2-redirect/")) { }
            else
            {
                text = File.ReadAllText(@"KFGame\Config\PCServer-KFEngine.ini", Encoding.UTF8);

                text = text.Replace("[IpDrv.TcpNetDriver]", "[IpDrv.TcpNetDriver]" + Environment.NewLine + "DownloadManagers=OnlineSubsystemSteamworks.SteamWorkshopDownload");

                text = text.Replace("RedirectToURL=", "RedirectToURL=http://skillzservers.com/kf2-redirect/");

                File.WriteAllText(@"KFGame\Config\PCServer-KFEngine.ini", text);
            }
           


        }

        private void CarregarInformacoesIniciais()
        {
            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            INIFile inif = new INIFile(appPath + "\\Horzine-Config.ini");

            strSoundEffects = inif.Read("Server.Info", "SoundEffects");


            strSoundEffects = strSoundEffects.ToLower();
            if (strSoundEffects.Contains("false"))
            {
                soundClickActivated = false;
                pbSound.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconSoundMuted;
            }
            else if (strSoundEffects.Contains("true"))
            {
                soundClickActivated = true;
                pbSound.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconSound;
            }

        }

        private void frmCustomMaps_Load(object sender, EventArgs e)
        {
            CarregarInformacoesIniciais();

            VisualizarDados();

            VerificarSeTagWorkshopExiste();

            Custom_font();



            listBoxActive.BackColor = Color.FromArgb(18, 16, 16);

            this.listBoxActive.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBoxActive.DrawItem += new System.Windows.Forms.DrawItemEventHandler(listBoxActive_DrawItem);
            this.listBoxActive.SelectedIndexChanged += new System.EventHandler(listBoxActive_SelectedIndexChanged);



            listBoxNotActive.BackColor = Color.FromArgb(18, 16, 16);

            this.listBoxNotActive.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBoxNotActive.DrawItem += new System.Windows.Forms.DrawItemEventHandler(listBoxNotActive_DrawItem);
            this.listBoxNotActive.SelectedIndexChanged += new System.EventHandler(listBoxNotActive_SelectedIndexChanged);



            lbAddMap.Font = new Font(font_Insomnia, 15, FontStyle.Regular);
            lbMapLink.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            txtMapLink.Font = new Font(font_Insomnia, 11, FontStyle.Regular);

            listBoxActive.Font = new Font(font_Insomnia, 12, FontStyle.Regular);
            listBoxNotActive.Font = new Font(font_Insomnia, 12, FontStyle.Regular);


            lbAdd.Font = new Font(font_Insomnia, 9, FontStyle.Regular);
            lbAddAll.Font = new Font(font_Insomnia, 9, FontStyle.Regular);
            lbRemove.Font = new Font(font_Insomnia, 9, FontStyle.Regular);
            lbRemoveAll.Font = new Font(font_Insomnia, 9, FontStyle.Regular);

            lbUninstall.Font = new Font(font_Insomnia, 9, FontStyle.Regular);

            lbAdd.ForeColor = corTexto;
            lbAddAll.ForeColor = corTexto;
            lbRemove.ForeColor = corTexto;
            lbRemoveAll.ForeColor = corTexto;

            lbUninstall.ForeColor = corTexto;

            lbMapLink.ForeColor = corTexto;
            lbAddMap.ForeColor = corTexto;


            lbAdd.BackColor = Color.FromArgb(88, 10, 5);
            lbAddAll.BackColor = Color.FromArgb(88, 10, 5);
            lbRemove.BackColor = Color.FromArgb(88, 10, 5);
            lbRemoveAll.BackColor = Color.FromArgb(88, 10, 5);

            lbUninstall.BackColor = Color.FromArgb(88, 10, 5);


            lbAddMap.BackColor = Color.FromArgb(88, 10, 5);
            lbMapLink.BackColor = Color.FromArgb(88, 10, 5);

        }

        private void pbMenuServer_Click(object sender, EventArgs e)
        {
            SoundEffectClick();

            frmServer formServer = new frmServer();

            formServer.Show();

            this.Hide();
        }

        private void pbMenuMutators_Click(object sender, EventArgs e)
        {
            SoundEffectClick();

            frmMutators formMutators = new frmMutators();

            formMutators.Show();

            this.Hide();
        }










        private void listBoxActive_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                int index = e.Index;
                Graphics g = e.Graphics;
                foreach (int selectedIndex in this.listBoxActive.SelectedIndices)
                {
                    if (index == selectedIndex)
                    {
                        // Draw the new background colour
                        e.DrawBackground();
                        g.FillRectangle(new SolidBrush(Color.FromArgb(189, 90, 18)), e.Bounds);
                    }
                }

                // Get the item details
                Font font = listBoxActive.Font;
                Color colour = listBoxActive.ForeColor;
                string text = listBoxActive.Items[index].ToString();

                // Print the text
                g.DrawString(text, font, new SolidBrush(Color.White), (float)e.Bounds.X, (float)e.Bounds.Y);
                e.DrawFocusRectangle();
            }
            catch { }
        }

        private void listBoxNotActive_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                int index = e.Index;
                Graphics g = e.Graphics;
                foreach (int selectedIndex in this.listBoxNotActive.SelectedIndices)
                {
                    if (index == selectedIndex)
                    {
                        // Draw the new background colour
                        e.DrawBackground();
                        g.FillRectangle(new SolidBrush(Color.FromArgb(189, 90, 18)), e.Bounds);
                    }
                }

                // Get the item details
                Font font = listBoxNotActive.Font;
                Color colour = listBoxNotActive.ForeColor;
                string text = listBoxNotActive.Items[index].ToString();

                // Print the text
                g.DrawString(text, font, new SolidBrush(Color.White), (float)e.Bounds.X, (float)e.Bounds.Y);
                e.DrawFocusRectangle();
            }
            catch { }
        }









        private void pbMenuServer_MouseMove(object sender, MouseEventArgs e)
        {
            pbMenuServer.BackgroundImage = HorzineDedicatedServer.Properties.Resources.buttonServerMouseMove;
        }

        private void pbMenuServer_MouseLeave(object sender, EventArgs e)
        {
            pbMenuServer.BackgroundImage = HorzineDedicatedServer.Properties.Resources.buttonServer;
        }

        private void pbMenuMutators_MouseMove(object sender, MouseEventArgs e)
        {
            pbMenuMutators.BackgroundImage = HorzineDedicatedServer.Properties.Resources.buttonMutatorsMouseMove;
        }

        private void pbMenuMutators_MouseLeave(object sender, EventArgs e)
        {
            pbMenuMutators.BackgroundImage = HorzineDedicatedServer.Properties.Resources.buttonMutators;
        }

        private void pbMenuHelp_MouseMove(object sender, MouseEventArgs e)
        {
            pbMenuHelp.BackgroundImage = HorzineDedicatedServer.Properties.Resources.buttonHelpMouseMove;
        }

        private void pbMenuHelp_MouseLeave(object sender, EventArgs e)
        {
            pbMenuHelp.BackgroundImage = HorzineDedicatedServer.Properties.Resources.buttonHelp;
        }









        private void pbAdvanced_MouseMove(object sender, MouseEventArgs e)
        {
            pbAdvanced.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconAdvancedMoseMove;
        }

        private void pbAdvanced_MouseLeave(object sender, EventArgs e)
        {
            pbAdvanced.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconAdvanced;
        }

        private void pbSound_MouseMove(object sender, MouseEventArgs e)
        {
            pbSound.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconSoundMouseMove;
        }

        private void pbSound_MouseLeave(object sender, EventArgs e)
        {
            pbSound.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconSound;

            if (soundClickActivated == false)
            {
                pbSound.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconSoundMuted;
            }
            else
            if (soundClickActivated == true)
            {
                pbSound.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconSound;
            }
        }

        private void pbSound_Click(object sender, EventArgs e)
        {
            SoundEffectClick();

            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            INIFile inif = new INIFile(appPath + @"\Horzine-Config.ini");

            if (soundClickActivated == true)
            {
                soundClickActivated = false;
            }
            else if (soundClickActivated == false)
            {
                soundClickActivated = true;
            }


            inif.Write("Server.Info", "SoundEffects", soundClickActivated.ToString());
        }












        private void lbMapLink_MouseMove(object sender, MouseEventArgs e)
        {
            MapLinkMouseMove();
        }

        private void lbMapLink_MouseLeave(object sender, EventArgs e)
        {
            MapLinkMouseLeave();
        }

        private void pbMapLink_MouseMove(object sender, MouseEventArgs e)
        {
            MapLinkMouseMove();
        }

        private void pbMapLink_MouseLeave(object sender, EventArgs e)
        {
            MapLinkMouseLeave();
        }

        private void txtMapLink_MouseMove(object sender, MouseEventArgs e)
        {
            MapLinkMouseMove();
        }

        private void txtMapLink_MouseLeave(object sender, EventArgs e)
        {
            MapLinkMouseLeave();
        }

        private void MapLinkMouseMove()
        {
            pbMapLink.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbMapLink.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void MapLinkMouseLeave()
        {
            pbMapLink.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitle;
            lbMapLink.BackColor = Color.FromArgb(88, 10, 5);
        }















        private void pbAddMap_MouseMove(object sender, MouseEventArgs e)
        {
            pbAddMap.BackgroundImage = HorzineDedicatedServer.Properties.Resources.launchSerMouseMove2;
            lbAddMap.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbAddMap_MouseLeave(object sender, EventArgs e)
        {
            pbAddMap.BackgroundImage = HorzineDedicatedServer.Properties.Resources.launchServer;
            lbAddMap.BackColor = Color.FromArgb(88, 10, 5);
        }

        private void lbAddMap_MouseMove(object sender, MouseEventArgs e)
        {
            pbAddMap.BackgroundImage = HorzineDedicatedServer.Properties.Resources.launchSerMouseMove2;
            lbAddMap.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbAddMap_MouseLeave(object sender, EventArgs e)
        {
            pbAddMap.BackgroundImage = HorzineDedicatedServer.Properties.Resources.launchServer;
            lbAddMap.BackColor = Color.FromArgb(88, 10, 5);
        }












        private void pbAdd_MouseMove(object sender, MouseEventArgs e)
        {
            pbAdd.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove_movemouse;
            lbAdd.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbAdd_MouseLeave(object sender, EventArgs e)
        {
            pbAdd.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove;
            lbAdd.BackColor = Color.FromArgb(88, 10, 5);
        }

        private void lbAdd_MouseMove(object sender, MouseEventArgs e)
        {
            pbAdd.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove_movemouse;
            lbAdd.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbAdd_MouseLeave(object sender, EventArgs e)
        {
            pbAdd.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove;
            lbAdd.BackColor = Color.FromArgb(88, 10, 5);
        }










        private void pnAddAll_MouseMove(object sender, MouseEventArgs e)
        {
            pnAddAll.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove_movemouse;
            lbAddAll.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pnAddAll_MouseLeave(object sender, EventArgs e)
        {
            pnAddAll.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove;
            lbAddAll.BackColor = Color.FromArgb(88, 10, 5);
        }

        private void lbAddAll_MouseMove(object sender, MouseEventArgs e)
        {
            pnAddAll.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove_movemouse;
            lbAddAll.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbAddAll_MouseLeave(object sender, EventArgs e)
        {
            pnAddAll.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove;
            lbAddAll.BackColor = Color.FromArgb(88, 10, 5);
        }











        private void pnRemove_MouseMove(object sender, MouseEventArgs e)
        {
            pnRemove.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove_movemouse;
            lbRemove.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pnRemove_MouseLeave(object sender, EventArgs e)
        {
            pnRemove.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove;
            lbRemove.BackColor = Color.FromArgb(88, 10, 5);
        }

        private void lbRemove_MouseMove(object sender, MouseEventArgs e)
        {
            pnRemove.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove_movemouse;
            lbRemove.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbRemove_MouseLeave(object sender, EventArgs e)
        {
            pnRemove.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove;
            lbRemove.BackColor = Color.FromArgb(88, 10, 5);
        }














        private void pnRemoveAll_MouseMove(object sender, MouseEventArgs e)
        {
            pnRemoveAll.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove_movemouse;
            lbRemoveAll.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pnRemoveAll_MouseLeave(object sender, EventArgs e)
        {
            pnRemoveAll.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove;
            lbRemoveAll.BackColor = Color.FromArgb(88, 10, 5);
        }

        private void lbRemoveAll_MouseMove(object sender, MouseEventArgs e)
        {
            pnRemoveAll.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove_movemouse;
            lbRemoveAll.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbRemoveAll_MouseLeave(object sender, EventArgs e)
        {
            pnRemoveAll.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove;
            lbRemoveAll.BackColor = Color.FromArgb(88, 10, 5);
        }












        private void pbAddMap_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new WebClient())
                using (var streamCheck = client.OpenRead("http://www.google.com"))
                {
                    pnInternetConection.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconConected;

                    ExtrairInformacoes();
                }
            }
            catch
            {
                MessageBox.Show("No Internet Conection.", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                pnInternetConection.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconNotConected;
            }
            
        }

        private void lbAddMap_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new WebClient())
                using (var streamCheck = client.OpenRead("http://www.google.com"))
                {
                    pnInternetConection.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconConected;
                    ExtrairInformacoes();
                }
            }
            catch
            {
                MessageBox.Show("No Internet Conection.", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                pnInternetConection.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconNotConected;
            }
            
        }

        private void ExtrairInformacoes()
        {
            SoundEffectClick();

            string strPageSource = "";

            if (txtMapLink.Text.Contains("https://steamcommunity.com/sharedfiles/filedetails/?id="))
            {

                using (WebClient client = new WebClient())
                {

                    var htmlData = client.DownloadData(txtMapLink.Text);
                    strPageSource = Encoding.UTF8.GetString(htmlData);
                }


                string[] resultTitle = strPageSource.Split(new string[] { "Steam Workshop::", "</title>" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in resultTitle)
                {
                    if (s.Contains("<")) { }
                    else
                    {
                        strItemTitle = s;
                    }
                }


                if (txtMapLink.Text.Contains("&searchtext"))
                {
                    string[] resultId1 = txtMapLink.Text.Split(new string[] { "?id=", "&searchtext" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in resultId1)
                    {

                        if (s.Contains("steamcommunity.com") || s.Contains("=")) { }
                        else
                        {
                            strItemId = s;
                        }
                    }
                }
                else
                {
                    string[] result = txtMapLink.Text.Split(new string[] { "?id=" }, StringSplitOptions.None);
                    foreach (string s in result)
                    {
                        if (s.Contains("steamcommunity.com") || s.Contains("=")) { }
                        else
                        {
                            strItemId = s;
                        }
                    }

                }





                if (strPageSource.Contains("<img id=\x22previewImage\x22 class=\x22workshopItemPreviewImageEnlargeable\x22 src=\x22"))
                {





                    string[] resultImage1 = strPageSource.Split(new string[] { "img id=\x22previewImage\x22 class=\x22workshopItemPreviewImageEnlargeable\x22 src=\x22", "\x22/><span class=\x22zoom-icon\x22>" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in resultImage1)
                    {
                        if (s.Contains("<")) { }
                        else
                        {
                            strImageLink = s;
                        }
                    }

                    var request = WebRequest.Create(strImageLink);

                    using (var response = request.GetResponse())
                    using (var stream = response.GetResponseStream())
                    {
                        pbPreview.BackgroundImage = Bitmap.FromStream(stream);
                    }



                }
                else if (strPageSource.Contains(""))
                {





                    string[] resultImage2 = strPageSource.Split(new string[] { "<img id=\x22previewImageMain\x22 class=\x22workshopItemPreviewImageMain\x22 src=\x22", "\x22/>" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in resultImage2)
                    {
                        if (s.Contains("<")) { }
                        else
                        {
                            strImageLink = s;
                        }
                    }

                    var request = WebRequest.Create(strImageLink);

                    using (var response = request.GetResponse())
                    using (var stream = response.GetResponseStream())
                    {
                        pbPreview.BackgroundImage = Bitmap.FromStream(stream);
                    }



                }
                else
                {
                    MessageBox.Show("Insert a valid link!", "Note", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    txtMapLink.Focus();
                }


                DownloadMap();




            }
            else
            {
                MessageBox.Show("Insert a valid link!", "Note", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                txtMapLink.Focus();
            }

           
        }

        private void VisualizarDados()
        {
            listBoxActive.Items.Clear();
            listBoxNotActive.Items.Clear();

            // Connection string and SQL query    
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb";
            string strSQL = "SELECT * FROM  customMaps";
            // Create a connection    
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Create a command and set its connection    
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                // Open the connection and execute the select command.    
                try
                {
                    // Open connecton    
                    connection.Open();
                    // Execute command    
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        //MessageBox.Show(reader["tituto"].ToString());
                        while (reader.Read())
                        {
                            string titulo = reader["titulo"].ToString();
                            if (titulo == "") { }
                            else
                            {
                                
                                string isActivated = reader["activated"].ToString();

                                if (isActivated == "true")
                                {
                                    listBoxActive.Items.Add(reader["titulo"].ToString());
                                }
                                else if (isActivated == "false")
                                {
                                    listBoxNotActive.Items.Add(reader["titulo"].ToString());
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                // The connection is automatically closed becasuse of using block.    
            }



        }


        private void SalvarDados()
        {

            strItemTitle = strItemTitle.Replace("'", "");
            strItemTitle = strItemTitle.Replace(":", "");

            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb");
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "Insert into customMaps(id,titulo,imageLink,activated,tagName)Values(?,?,?,?,?)";
            cmd.Parameters.AddWithValue("@id", strItemId);
            cmd.Parameters.AddWithValue("@titulo", strItemTitle);
            cmd.Parameters.AddWithValue("@imageLink", strImageLink);
            cmd.Parameters.AddWithValue("@activated", "false");
            cmd.Parameters.AddWithValue("@tagName", strTecnMapName.Substring(0, strTecnMapName.Length - 4));

            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            MessageBox.Show("The '" + strItemTitle + "' has been successfully installed!", "Note", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

            System.IO.Directory.Delete(@"steamcmd\steamapps\workshop", true);

            //VisualizarDados();
            con.Close();




            string stringMutator = "ServerSubscribedWorkshopItems=" + strItemId + Environment.NewLine + "<WorkshopItemId>";

            string text = File.ReadAllText(@"KFGame\Config\PCServer-KFEngine.ini", Encoding.UTF8);

            string novoTexto = text.Replace("<WorkshopItemId>", stringMutator);

            File.WriteAllText(@"KFGame\Config\PCServer-KFEngine.ini", novoTexto);

            VisualizarDados();
            /*
            try
            {
                
            }
            catch (Exception ex)
            {
                string strException = ex.ToString();

                if (strException.Contains("duplicate"))
                {
                    MessageBox.Show("Duplicated ID", "Note", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            */


        }

        private void DownloadMap()
        {
            System.Diagnostics.Process.Start("CMD.exe", "/C steamcmd\\steamcmd.exe +login anonymous +workshop_download_item 232090 " + strItemId + " validate +quit");

            
            timer1.Start();

            this.Hide();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ListProcesses();


        }

        private void ListProcesses()
        {
            string strProcessos = "";

            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                strProcessos = strProcessos + p.ProcessName + Environment.NewLine; ;
            }

            if (strProcessos.Contains("cmd")) { }
            else
            {
                timer1.Stop();
                CopiarArquivo();
                this.Show();
                VisualizarDados();
            }

        }

        private void CopiarArquivo()
        {
            string strMapDirectory = "";

            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            if(System.IO.Directory.Exists(appPath + @"\steamcmd\steamapps\workshop\content\232090\" + strItemId + @"\BrewedPC\Maps\"))
            {
                strMapDirectory = appPath + @"\steamcmd\steamapps\workshop\content\232090\" + strItemId + @"\BrewedPC\Maps\";
            }
            else if(System.IO.Directory.Exists(appPath + @"\steamcmd\steamapps\workshop\content\232090\" + strItemId + @"\BrewedPC\Packages\Maps\"))
            {
                strMapDirectory = appPath + @"\steamcmd\steamapps\workshop\content\232090\" + strItemId + @"\BrewedPC\Packages\Maps\";
            }
            else if(System.IO.Directory.Exists(appPath + @"\steamcmd\steamapps\workshop\content\232090\" + strItemId + @"\BrewedPC\"))
            {
                strMapDirectory = appPath + @"\steamcmd\steamapps\workshop\content\232090\" + strItemId + @"\BrewedPC\";
            }
            else
            {
                strMapDirectory = appPath + @"\steamcmd\steamapps\workshop\content\232090\" + strItemId;
            }


            string[] dirs = Directory.GetDirectories(strMapDirectory);


            foreach (string item2 in dirs)
            {
                FileInfo f = new FileInfo(item2);

                strFriendlyMapName = f.Name;

            }


            string[] fileArray = Directory.GetFiles(strMapDirectory + strFriendlyMapName);



            foreach (string item2 in fileArray)
            {
                FileInfo f = new FileInfo(item2);

                strTecnMapName = f.Name;
            }



            strFriendlyMapName = strFriendlyMapName.Replace("'", "").Replace(":", "");
            strItemTitle = strItemTitle.Replace("'", "").Replace(":", "");


            ///////////////////////
            ///

            string caminhoOrigem = strMapDirectory + strFriendlyMapName + "\\";


            System.IO.Directory.CreateDirectory(appPath + @"\KFGame\BrewedPC\Maps\" + strItemTitle);



            try
            {
                System.IO.Directory.Delete(caminhoOrigem + strTecnMapName);
            }
            catch { }


            System.Diagnostics.Process.Start("CMD.exe", "/C copy /b " + caminhoOrigem + strTecnMapName + " " + appPath + @"\KFGame\BrewedPC\Maps\" + strItemTitle + "\\" + strTecnMapName);

            try
            {
                File.Copy(caminhoOrigem + strTecnMapName, appPath + @"\KFGame\BrewedPC\Maps\" + strItemTitle + "\\" + strTecnMapName);
            }
            catch { }

            //System.IO.Directory.Delete(appPath + @"\steamcmd\steamapps\workshop\content\232090\" + strItemId + @"\BrewedPC\Maps" + strFriendlyMapName);


            //////////////
            
            nomeTecnicoDoMapa = strTecnMapName.Replace(".kfm", "");
            nomeTecnicoDoMapa = nomeTecnicoDoMapa.Replace(".upk", "");

            string text = File.ReadAllText(@"KFGame\Config\PCServer-KFGame.ini", Encoding.UTF8);

            text = text + Environment.NewLine + Environment.NewLine + Environment.NewLine + "[" + nomeTecnicoDoMapa + " KFMapSummary]" + Environment.NewLine + "MapName=" + nomeTecnicoDoMapa;

            File.WriteAllText(@"KFGame\Config\PCServer-KFGame.ini", text);

            SalvarDados();
            CriarStringCustomMaps();
            VisualizarDados();
        }

        private void RetornarNotActive()
        {
            /*try
            {
                listBoxActive.SetSelected(listBoxActive.Items.Count - 1, false);
            }
            catch { }*/

            // Connection string and SQL query    
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb";
            string strSQL = "SELECT * FROM  customMaps";
            // Create a connection    
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Create a command and set its connection    
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                // Open the connection and execute the select command.    
                // Open connecton    
                connection.Open();
                // Execute command    
                using (OleDbDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        string titulo = reader["titulo"].ToString();
                        if (titulo == listBoxNotActive.GetItemText(listBoxNotActive.SelectedItem))
                        {
                            strItemId = reader["id"].ToString();
                            strTituloSelected = reader["titulo"].ToString();
                            strImageLinkSelected = reader["imageLink"].ToString();
                            strActivated = reader["activated"].ToString();
                            nomeTecnicoDoMapa = reader["tagName"].ToString();

                            try
                            {
                                var request = WebRequest.Create(strImageLinkSelected);

                                using (var response = request.GetResponse())
                                using (var stream = response.GetResponseStream())
                                {
                                    pbPreview.BackgroundImage = Bitmap.FromStream(stream);
                                }
                            }
                            catch
                            {
                                pbPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
                            }
                        }
                    }
                }

                try
                {

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                // The connection is automatically closed becasuse of using block.    
            }

        }

        private void RetornarActive()
        {
            /*try
            {
                listBoxNotActive.SetSelected(listBoxNotActive.Items.Count - 1, false);
            }
            catch { }*/

            //Connection string and SQL query    
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb";
            string strSQL = "SELECT * FROM  customMaps";
            // Create a connection    
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Create a command and set its connection    
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                // Open the connection and execute the select command.    
                try
                {
                    // Open connecton    
                    connection.Open();
                    // Execute command    
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        //MessageBox.Show(reader["tituto"].ToString());
                        while (reader.Read())
                        {
                            string titulo = reader["titulo"].ToString();
                            if (titulo == listBoxActive.GetItemText(listBoxActive.SelectedItem))
                            {
                                strItemId = reader["id"].ToString();
                                strTituloSelected = reader["titulo"].ToString();
                                strImageLinkSelected = reader["imageLink"].ToString();
                                strActivated = reader["activated"].ToString();
                                nomeTecnicoDoMapa = reader["tagName"].ToString();


                                try
                                {
                                    var request = WebRequest.Create(strImageLinkSelected);

                                    using (var response = request.GetResponse())
                                    using (var stream = response.GetResponseStream())
                                    {
                                        pbPreview.BackgroundImage = Bitmap.FromStream(stream);
                                    }
                                }
                                catch
                                {
                                    pbPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                // The connection is automatically closed becasuse of using block.    
            }

        }

        private void pbMenuHelp_Click(object sender, EventArgs e)
        {
            SoundEffectClick();

            frmHelp formHelp = new frmHelp();

            formHelp.Show();

            this.Hide();
        }

        private void pbAdvanced_Click(object sender, EventArgs e)
        {
            SoundEffectClick();

            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            System.Diagnostics.Process.Start(appPath + "\\KFGame\\Config");
        }

        private void pbAdd_Click(object sender, EventArgs e)
        {
            AddCustomMap();
        }

        private void lbAdd_Click(object sender, EventArgs e)
        {
            AddCustomMap();
        }

        private void AddCustomMap()
        {
            SoundEffectClick();

            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb");
            OleDbCommand cmd = con.CreateCommand();

            con.Open();

            cmd.CommandText = "UPDATE customMaps SET activated = @activated  WHERE id = '" + strItemId + "'";
            cmd.Parameters.AddWithValue("@activated", "true");
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();





            RetornarNomeTecnico();

            string text = File.ReadAllText(@"KFGame\Config\PCServer-KFGame.ini", Encoding.UTF8);

            string novoTexto = text.Replace("GameMapCycles=(Maps=(", "GameMapCycles=(Maps=(\x22" + nomeTecnicoDoMapa + "\x22,");


            File.WriteAllText(@"KFGame\Config\PCServer-KFGame.ini", novoTexto);




            VisualizarDados();
            CriarStringCustomMaps();
        }

        private void lbAddAll_Click(object sender, EventArgs e)
        {
            AddAllCustomMap();
        }

        private void pnAddAll_Click(object sender, EventArgs e)
        {
            AddAllCustomMap();
        }

        private void AddAllCustomMap()
        {
            SoundEffectClick();

            foreach (var listBoxItem in listBoxNotActive.Items)
            {
                
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb");
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "UPDATE customMaps SET activated = @activated  WHERE titulo = '" + listBoxItem.ToString() + "'";
                cmd.Parameters.AddWithValue("@activated", "true");
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();








                RetornarNomeTecnico();

                string text = File.ReadAllText(@"KFGame\Config\PCServer-KFGame.ini", Encoding.UTF8);

                string novoTexto = text.Replace("GameMapCycles=(Maps=(", "GameMapCycles=(Maps=(\x22" + nomeTecnicoDoMapa + "\x22,");


                File.WriteAllText(@"KFGame\Config\PCServer-KFGame.ini", novoTexto);







                //
            }
            VisualizarDados();
            CriarStringCustomMaps();
        }

        private void pnRemove_Click(object sender, EventArgs e)
        {
            RemoveCustomMap();
        }

        private void lbRemove_Click(object sender, EventArgs e)
        {
            RemoveCustomMap();
        }

        private void RemoveCustomMap()
        {
            SoundEffectClick();

            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb");
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = "UPDATE customMaps SET activated = @activated  WHERE id = '" + strItemId + "'";
            cmd.Parameters.AddWithValue("@activated", "false");
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();





            RetornarNomeTecnico();

            string text = File.ReadAllText(@"KFGame\Config\PCServer-KFGame.ini", Encoding.UTF8);

            string novoTexto = text.Replace("\x22" + nomeTecnicoDoMapa + "\x22,", "");


            File.WriteAllText(@"KFGame\Config\PCServer-KFGame.ini", novoTexto);






            VisualizarDados();
            CriarStringCustomMaps();
        }

        private void pnRemoveAll_Click(object sender, EventArgs e)
        {
            RemoveAllCustomMap();
        }

        private void lbRemoveAll_Click(object sender, EventArgs e)
        {
            RemoveAllCustomMap();
        }

        private void RemoveAllCustomMap()
        {
            SoundEffectClick();

            foreach (var listBoxItem in listBoxActive.Items)
            {
                
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb");
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "UPDATE customMaps SET activated = @activated  WHERE titulo = '" + listBoxItem.ToString() + "'";
                cmd.Parameters.AddWithValue("@activated", "false");
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();






                RetornarNomeTecnico();

                string text = File.ReadAllText(@"KFGame\Config\PCServer-KFGame.ini", Encoding.UTF8);

                string novoTexto = text.Replace("\x22" + nomeTecnicoDoMapa + "\x22,", "");


                File.WriteAllText(@"KFGame\Config\PCServer-KFGame.ini", novoTexto);






            }
            VisualizarDados();
            CriarStringCustomMaps();
        }

        private void pbUninstall_Click(object sender, EventArgs e)
        {
            UninstallCustomMaps();
        }

        private void lbUninstall_Click(object sender, EventArgs e)
        {
            UninstallCustomMaps();
        }

        private void UninstallCustomMaps()
        {
            SoundEffectClick();

            if (strTituloSelected == "")
            {
                MessageBox.Show("Select an item to uninstall!", "Note", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to uninstall '" + strTituloSelected + "'?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb");
                    OleDbCommand cmd = con.CreateCommand();
                    con.Open();
                    cmd.CommandText = "DELETE FROM customMaps WHERE titulo ='" + strTituloSelected + "'";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();






                    string text = File.ReadAllText(@"KFGame\Config\PCServer-KFEngine.ini", Encoding.UTF8);

                    string novoTexto = text.Replace("\n" + "ServerSubscribedWorkshopItems=" + strItemId, "");


                    File.WriteAllText(@"KFGame\Config\PCServer-KFEngine.ini", novoTexto);

                    try
                    {
                        string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

                        System.IO.Directory.Delete(appPath + @"\KFGame\BrewedPC\Maps\" + strItemTitle, true);
                    }
                    catch { }






                    RetornarNomeTecnico();

                    text = File.ReadAllText(@"KFGame\Config\PCServer-KFGame.ini", Encoding.UTF8);

                    novoTexto = text.Replace("\x22" + nomeTecnicoDoMapa + "\x22,", "");


                    File.WriteAllText(@"KFGame\Config\PCServer-KFGame.ini", novoTexto);



                    CriarStringCustomMaps();

                    VisualizarDados();
                }
            }
            
        }





        private void CriarStringCustomMaps()
        {
            RetornarNomeTecnico();

            string strTextoMapasCustomAtivado = "";

            //Connection string and SQL query    
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb";
            string strSQL = "SELECT * FROM  customMaps";
            // Create a connection    
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Create a command and set its connection    
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                // Open the connection and execute the select command.    
                try
                {
                    // Open connecton    
                    connection.Open();
                    // Execute command    
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        //MessageBox.Show(reader["tituto"].ToString());
                        while (reader.Read())
                        {
                            string strActivated = reader["activated"].ToString();
                            if (strActivated == "true")
                            {

                                strTextoMapasCustomAtivado = strTextoMapasCustomAtivado + reader["tagName"].ToString() + Environment.NewLine;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "aaaaaaaaa");
                }
                // The connection is automatically closed becasuse of using block.    
            }


            File.WriteAllText("customMaps.db", strTextoMapasCustomAtivado);






        }







        private void lbUninstall_MouseMove(object sender, MouseEventArgs e)
        {
            pbUninstall.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove_movemouse;
            lbUninstall.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbUninstall_MouseLeave(object sender, EventArgs e)
        {
            pbUninstall.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove;
            lbUninstall.BackColor = Color.FromArgb(88, 10, 5);
        }

        private void pbUninstall_MouseMove(object sender, MouseEventArgs e)
        {
            pbUninstall.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove_movemouse;
            lbUninstall.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbUninstall_MouseLeave(object sender, EventArgs e)
        {
            pbUninstall.BackgroundImage = HorzineDedicatedServer.Properties.Resources.add_remove;
            lbUninstall.BackColor = Color.FromArgb(88, 10, 5);
        }

        private void RetornarNomeTecnico()
        {

            //Connection string and SQL query    
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb";
            string strSQL = "SELECT * FROM  customMaps WHERE id = '" + strItemId + "'";
            // Create a connection    
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Create a command and set its connection    
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                // Open the connection and execute the select command.    

                // Open connecton    
                connection.Open();
                // Execute command    
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        string readerTitulo = reader["titulo"].ToString();
                        if (readerTitulo == listBoxActive.GetItemText(listBoxActive.SelectedItem))
                        {
                            
                            nomeTecnicoDoMapa = reader["tagName"].ToString();


                        }
                    }
                }


            }
        }

        private void SoundEffectClick()
        {
            if (soundClickActivated == true)
            {
                System.IO.Stream str = Properties.Resources.Mouse_click_1;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
            }

        }

        private void txtMapLink_Click(object sender, EventArgs e)
        {
            SoundEffectClick();
        }

        private void frmCustomMaps_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
