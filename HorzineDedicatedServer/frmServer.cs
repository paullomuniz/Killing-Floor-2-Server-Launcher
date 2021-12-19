using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class frmServer : Form
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



        public frmServer()
        {
            InitializeComponent();
        }

        string strProcessos = "";

        string strStartingMap = "";
        string strGameMode = "";
        string strGameDifficult = "";
        string strGameLenght = "";
        string strServerName = "";
        string strServerPassword = "";
        string strWebAdminUser = "";
        string strWebAdminPassword = "";
        string strServerTitle = "";
        string strSiteLink = "";
        string strBannerLink = "";
        string strServerTakeover = "";
        string strTeamCollision = "";
        string strCanPause = "";
        string strSoundEffects = "";
        string strGameLength = "";

        string strServerMOTDColor = "";
        string strServerMOTDText = "";


        bool soundClickActivated = true;
        bool boolServerTakeover = true;
        bool boolTeamCollision = true;
        bool boolCanPause = true;

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








        void listBoxStartingMap_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            int index = e.Index;
            Graphics g = e.Graphics;
            foreach (int selectedIndex in this.listBoxStartingMap.SelectedIndices)
            {
                if (index == selectedIndex)
                {
                    // Draw the new background colour
                    e.DrawBackground();
                    g.FillRectangle(new SolidBrush(Color.FromArgb(189, 90, 18)), e.Bounds);
                }
            }

            // Get the item details
            Font font = listBoxStartingMap.Font;
            Color colour = listBoxStartingMap.ForeColor;
            string text = listBoxStartingMap.Items[index].ToString();

            // Print the text
            g.DrawString(text, font, new SolidBrush(Color.White), (float)e.Bounds.X, (float)e.Bounds.Y);
            e.DrawFocusRectangle();
        }






       






        private void listBoxGameMode_DrawItem_1(object sender, DrawItemEventArgs e)
        {
            
            int index = e.Index;
            Graphics g = e.Graphics;
            foreach (int selectedIndex in this.listBoxGameMode.SelectedIndices)
            {
                if (index == selectedIndex)
                {
                    // Draw the new background colour
                    e.DrawBackground();
                    g.FillRectangle(new SolidBrush(Color.FromArgb(189, 90, 18)), e.Bounds);
                }
            }

            // Get the item details
            Font font = listBoxGameMode.Font;
            Color colour = listBoxGameMode.ForeColor;
            string text = listBoxGameMode.Items[index].ToString();

            // Print the text
            g.DrawString(text, font, new SolidBrush(Color.White), (float)e.Bounds.X, (float)e.Bounds.Y);
            e.DrawFocusRectangle();
        }









        private void listBoxDifficult_DrawItem(object sender, DrawItemEventArgs e)
        {

            int index = e.Index;
            Graphics g = e.Graphics;
            foreach (int selectedIndex in this.listBoxDifficult.SelectedIndices)
            {
                if (index == selectedIndex)
                {
                    // Draw the new background colour
                    e.DrawBackground();
                    g.FillRectangle(new SolidBrush(Color.FromArgb(189, 90, 18)), e.Bounds);
                }
            }

            // Get the item details
            Font font = listBoxDifficult.Font;
            Color colour = listBoxDifficult.ForeColor;
            string text = listBoxDifficult.Items[index].ToString();

            // Print the text
            g.DrawString(text, font, new SolidBrush(Color.White), (float)e.Bounds.X, (float)e.Bounds.Y);
            e.DrawFocusRectangle();
        }








        private void listBoxLenght_DrawItem(object sender, DrawItemEventArgs e)
        {

            int index = e.Index;
            Graphics g = e.Graphics;
            foreach (int selectedIndex in this.listBoxLenght.SelectedIndices)
            {
                if (index == selectedIndex)
                {
                    // Draw the new background colour
                    e.DrawBackground();
                    g.FillRectangle(new SolidBrush(Color.FromArgb(189, 90, 18)), e.Bounds);
                }
            }

            // Get the item details
            Font font = listBoxLenght.Font;
            Color colour = listBoxLenght.ForeColor;
            string text = listBoxLenght.Items[index].ToString();

            // Print the text
            g.DrawString(text, font, new SolidBrush(Color.White), (float)e.Bounds.X, (float)e.Bounds.Y);
            e.DrawFocusRectangle();
        }





        private void listBoxStartingMap_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SoundEffectClick();

            this.listBoxStartingMap.Invalidate();

            lbStartingMapSelected.Text = listBoxStartingMap.GetItemText(listBoxStartingMap.SelectedItem);
            listBoxStartingMap.Visible = false;

            this.BackgroundImage = HorzineDedicatedServer.Properties.Resources.KF_server_interface;

            lbStartingMapSelected.Visible = true;

            GetMapPreview();
        }











        private void listBoxGameMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SoundEffectClick();

            this.listBoxGameMode.Invalidate();

            lbGameModeSelected.Text = listBoxGameMode.GetItemText(listBoxGameMode.SelectedItem);
            listBoxGameMode.Visible = false;

            this.BackgroundImage = HorzineDedicatedServer.Properties.Resources.KF_server_interface;

            lbGameModeSelected.Visible = true;







            lbStartingMapSelected.Text = "";
            pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreview;






            if (lbGameModeSelected.Text.Contains("Survival"))
            {
                lbDifficultSelected.ForeColor = Color.White;
                lbLenghtSelected.ForeColor = Color.White;

                pbLenght.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
                lbLenght.BackColor = Color.Transparent;

                pbDifficult.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
                lbDifficult.BackColor = Color.Transparent;



                listBoxDifficult.Items.Clear();

                listBoxDifficult.Items.Add("Normal");
                listBoxDifficult.Items.Add("Hard");
                listBoxDifficult.Items.Add("Suicidal");
                listBoxDifficult.Items.Add("Hell on Earth");




                listBoxLenght.Items.Clear();

                listBoxLenght.Items.Add("Short - 4 Waves");
                listBoxLenght.Items.Add("Medium - 7 Waves");
                listBoxLenght.Items.Add("Long - 10 Waves");


                listBoxStartingMap.Items.Clear();

                listBoxStartingMap.Items.Add("Airship");
                listBoxStartingMap.Items.Add("Ashwood Asylum");
                listBoxStartingMap.Items.Add("Biolapse");
                listBoxStartingMap.Items.Add("Biotics Lab");
                listBoxStartingMap.Items.Add("Black Forest");
                listBoxStartingMap.Items.Add("Burning Paris");
                listBoxStartingMap.Items.Add("Carillon Hamlet");
                listBoxStartingMap.Items.Add("Catacombs");
                listBoxStartingMap.Items.Add("Containment Station");
                listBoxStartingMap.Items.Add("Desolation");
                listBoxStartingMap.Items.Add("Dystopia 2029");
                listBoxStartingMap.Items.Add("Elysium");
                listBoxStartingMap.Items.Add("Evacuation Point");
                listBoxStartingMap.Items.Add("Farmhouse");
                listBoxStartingMap.Items.Add("Hellmark Station");
                listBoxStartingMap.Items.Add("Hostile Grounds");
                listBoxStartingMap.Items.Add("Infernal Realm");
                listBoxStartingMap.Items.Add("Krampus Lair");
                listBoxStartingMap.Items.Add("Lockdown");
                listBoxStartingMap.Items.Add("Monster Ball");
                listBoxStartingMap.Items.Add("Moonbase");
                listBoxStartingMap.Items.Add("Netherhold");
                listBoxStartingMap.Items.Add("Nightmare");
                listBoxStartingMap.Items.Add("Nuked");
                listBoxStartingMap.Items.Add("Outpost");
                listBoxStartingMap.Items.Add("Power Core");
                listBoxStartingMap.Items.Add("Prison");
                listBoxStartingMap.Items.Add("Sanitarium");
                listBoxStartingMap.Items.Add("Santas Workshop");
                listBoxStartingMap.Items.Add("Shopping Spree");
                listBoxStartingMap.Items.Add("Spillway");
                listBoxStartingMap.Items.Add("Steam Fortress");
                listBoxStartingMap.Items.Add("The Descent");
                listBoxStartingMap.Items.Add("The Tragic Kingdom");
                listBoxStartingMap.Items.Add("Volter Manor");
                listBoxStartingMap.Items.Add("ZED Landing");




            }
            else if (lbGameModeSelected.Text.Contains("Weekley"))
            {


                listBoxLenght.Items.Clear();

                listBoxLenght.Items.Add("Medium - 7 Waves");
                listBoxLenght.Items.Add("Long - 10 Waves");


                lbDifficultSelected.ForeColor = Color.White;
                lbLenghtSelected.ForeColor = Color.White;

                pbLenght.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
                lbLenght.BackColor = Color.Transparent;

                pbDifficult.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
                lbDifficult.BackColor = Color.Transparent;



                listBoxDifficult.Items.Clear();

                listBoxDifficult.Items.Add("Hard");
                listBoxDifficult.Items.Add("Suicidal");
                listBoxDifficult.Items.Add("Hell on Earth");




                listBoxStartingMap.Items.Clear();

                listBoxStartingMap.Items.Add("Airship");
                listBoxStartingMap.Items.Add("Ashwood Asylum");
                listBoxStartingMap.Items.Add("Biolapse");
                listBoxStartingMap.Items.Add("Biotics Lab");
                listBoxStartingMap.Items.Add("Black Forest");
                listBoxStartingMap.Items.Add("Burning Paris");
                listBoxStartingMap.Items.Add("Catacombs");
                listBoxStartingMap.Items.Add("Containment Station");
                listBoxStartingMap.Items.Add("Desolation");
                listBoxStartingMap.Items.Add("Dystopia 2029");
                listBoxStartingMap.Items.Add("Elysium");
                listBoxStartingMap.Items.Add("Evacuation Point");
                listBoxStartingMap.Items.Add("Farmhouse");
                listBoxStartingMap.Items.Add("Hellmark Station");
                listBoxStartingMap.Items.Add("Hostile Grounds");
                listBoxStartingMap.Items.Add("Infernal Realm");
                listBoxStartingMap.Items.Add("Krampus Lair");
                listBoxStartingMap.Items.Add("Lockdown");
                listBoxStartingMap.Items.Add("Monster Ball");
                listBoxStartingMap.Items.Add("Moonbase");
                listBoxStartingMap.Items.Add("Netherhold");
                listBoxStartingMap.Items.Add("Nightmare");
                listBoxStartingMap.Items.Add("Nuked");
                listBoxStartingMap.Items.Add("Outpost");
                listBoxStartingMap.Items.Add("Power Core");
                listBoxStartingMap.Items.Add("Prison");
                listBoxStartingMap.Items.Add("Sanitarium");
                listBoxStartingMap.Items.Add("Santas Workshop");
                listBoxStartingMap.Items.Add("Shopping Spree");
                listBoxStartingMap.Items.Add("Spillway");
                listBoxStartingMap.Items.Add("Steam Fortress");
                listBoxStartingMap.Items.Add("The Descent");
                listBoxStartingMap.Items.Add("The Tragic Kingdom");
                listBoxStartingMap.Items.Add("Volter Manor");
                listBoxStartingMap.Items.Add("ZED Landing");





            }
            else if (lbGameModeSelected.Text.Contains("Versus"))
            {

                pbDifficult.BackgroundImage = HorzineDedicatedServer.Properties.Resources.kfLenghtLocked;
                lbDifficult.BackColor = Color.FromArgb(57, 54, 51);
                lbDifficultSelected.ForeColor = Color.FromArgb(75, 71, 64);

                pbLenght.BackgroundImage = HorzineDedicatedServer.Properties.Resources.kfLenghtLocked;
                lbLenght.BackColor = Color.FromArgb(57, 54, 51);
                lbLenghtSelected.ForeColor = Color.FromArgb(75, 71, 64);




                lbDifficultSelected.Text = "Normal";
                lbDifficultSelected.Visible = true;

                lbLenghtSelected.Text = "Medium - 7 Waves";
                lbLenghtSelected.Visible = true;





                listBoxStartingMap.Items.Clear();

                listBoxStartingMap.Items.Add("Ashwood Asylum");
                listBoxStartingMap.Items.Add("Biolapse");
                listBoxStartingMap.Items.Add("Biotics Lab");
                listBoxStartingMap.Items.Add("Black Forest");
                listBoxStartingMap.Items.Add("Burning Paris");
                listBoxStartingMap.Items.Add("Catacombs");
                listBoxStartingMap.Items.Add("Containment Station");
                listBoxStartingMap.Items.Add("Desolation");
                listBoxStartingMap.Items.Add("Evacuation Point");
                listBoxStartingMap.Items.Add("Farmhouse");
                listBoxStartingMap.Items.Add("Infernal Realm");
                listBoxStartingMap.Items.Add("Lockdown");
                listBoxStartingMap.Items.Add("Monster Ball");
                listBoxStartingMap.Items.Add("Nuked");
                listBoxStartingMap.Items.Add("Outpost");
                listBoxStartingMap.Items.Add("Prison");
                listBoxStartingMap.Items.Add("Sanitarium");
                listBoxStartingMap.Items.Add("Spillway");
                listBoxStartingMap.Items.Add("Volter Manor");
                listBoxStartingMap.Items.Add("ZED Landing");





            }
            else if (lbGameModeSelected.Text.Contains("Endless"))
            {

                pbDifficult.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
                lbDifficult.BackColor = Color.Transparent;

                pbLenght.BackgroundImage = HorzineDedicatedServer.Properties.Resources.kfLenghtLocked;
                lbLenght.BackColor = Color.FromArgb(57, 54, 51);
                lbLenghtSelected.ForeColor = Color.FromArgb(75, 71, 64);

                lbLenghtSelected.Text = "Medium - 7 Waves";
                lbLenghtSelected.Visible = true;



                listBoxDifficult.Items.Clear();

                listBoxDifficult.Items.Add("Normal");
                listBoxDifficult.Items.Add("Hard");
                listBoxDifficult.Items.Add("Suicidal");
                listBoxDifficult.Items.Add("Hell on Earth");



                listBoxStartingMap.Items.Clear();

                listBoxStartingMap.Items.Add("Airship");
                listBoxStartingMap.Items.Add("Ashwood Asylum");
                listBoxStartingMap.Items.Add("Biolapse");
                listBoxStartingMap.Items.Add("Biotics Lab");
                listBoxStartingMap.Items.Add("Black Forest");
                listBoxStartingMap.Items.Add("Burning Paris");
                listBoxStartingMap.Items.Add("Catacombs");
                listBoxStartingMap.Items.Add("Containment Station");
                listBoxStartingMap.Items.Add("Desolation");
                listBoxStartingMap.Items.Add("DieSector");
                listBoxStartingMap.Items.Add("Dystopia 2029");
                listBoxStartingMap.Items.Add("Elysium");
                listBoxStartingMap.Items.Add("Evacuation Point");
                listBoxStartingMap.Items.Add("Farmhouse");
                listBoxStartingMap.Items.Add("Hellmark Station");
                listBoxStartingMap.Items.Add("Hostile Grounds");
                listBoxStartingMap.Items.Add("Infernal Realm");
                listBoxStartingMap.Items.Add("Krampus Lair");
                listBoxStartingMap.Items.Add("Lockdown");
                listBoxStartingMap.Items.Add("Monster Ball");
                listBoxStartingMap.Items.Add("Moonbase");
                listBoxStartingMap.Items.Add("Netherhold");
                listBoxStartingMap.Items.Add("Nightmare");
                listBoxStartingMap.Items.Add("Nuked");
                listBoxStartingMap.Items.Add("Outpost");
                listBoxStartingMap.Items.Add("Power Core");
                listBoxStartingMap.Items.Add("Prison");
                listBoxStartingMap.Items.Add("Sanitarium");
                listBoxStartingMap.Items.Add("Santas Workshop");
                listBoxStartingMap.Items.Add("Shopping Spree");
                listBoxStartingMap.Items.Add("Spillway");
                listBoxStartingMap.Items.Add("Steam Fortress");
                listBoxStartingMap.Items.Add("The Descent");
                listBoxStartingMap.Items.Add("The Tragic Kingdom");
                listBoxStartingMap.Items.Add("Volter Manor");
                listBoxStartingMap.Items.Add("ZED Landing");





            }
            else if (lbGameModeSelected.Text.Contains("Objective"))
            {
                lbDifficultSelected.ForeColor = Color.White;
                lbLenghtSelected.ForeColor = Color.White;

                pbLenght.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
                lbLenght.BackColor = Color.Transparent;

                pbDifficult.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
                lbDifficult.BackColor = Color.Transparent;




                lbLenghtSelected.Text = "";



                listBoxLenght.Items.Clear();

                listBoxLenght.Items.Add("Medium - 7 Waves");
                listBoxLenght.Items.Add("Long - 10 Waves");





                listBoxStartingMap.Items.Clear();

                listBoxStartingMap.Items.Add("Biotics Lab");
                listBoxStartingMap.Items.Add("Nuked");
                listBoxStartingMap.Items.Add("Outpost");
                listBoxStartingMap.Items.Add("Steam Fortress");
                listBoxStartingMap.Items.Add("ZED Landing");





            }
            else
            {

            }



            CheckGameMode();



        }

        private void listBoxDifficult_SelectedIndexChanged(object sender, EventArgs e)
        {
            SoundEffectClick();

            this.listBoxDifficult.Invalidate();

            lbDifficultSelected.Text = listBoxDifficult.GetItemText(listBoxDifficult.SelectedItem);
            listBoxDifficult.Visible = false;

            this.BackgroundImage = HorzineDedicatedServer.Properties.Resources.KF_server_interface;

            lbDifficultSelected.Visible = true;
        }

        private void listBoxLenght_SelectedIndexChanged(object sender, EventArgs e)
        {
            SoundEffectClick();

            this.listBoxLenght.Invalidate();

            lbLenghtSelected.Text = listBoxLenght.GetItemText(listBoxLenght.SelectedItem);
            listBoxLenght.Visible = false;

            this.BackgroundImage = HorzineDedicatedServer.Properties.Resources.KF_server_interface;

            lbLenghtSelected.Visible = true;
        }












        private void SalvarInformacoesIniciais()
        {
            
            //Limpar campos editaveis

            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            INIFile inif = new INIFile(appPath + @"\Horzine-Config.ini");

            inif.Write("Server.Info", "GameMode", ""); //
            inif.Write("Server.Info", "GameDifficult", ""); //
            inif.Write("Server.Info", "GameLenght", ""); //
            inif.Write("Server.Info", "WebAdminUser", txtWebAdminUsername.Text);
            inif.Write("Server.Info", "WebAdminPassword", txtWebAdminPassword.Text);
            inif.Write("Server.Info", "SoundEffects", soundClickActivated.ToString());
            inif.Write("Server.Info", "ServerMOTDColor", strServerMOTDColor);
            inif.Write("Server.Info", "ServerMOTDText", "");//


            INIFile inif2 = new INIFile(appPath + "\\KFGame\\Config\\PCServer-KFGame.ini");

            inif2.Write("Engine.GameReplicationInfo", "ServerName", ""); //
            inif2.Write("Engine.AccessControl", "GamePassword", ""); //
            inif2.Write("KFGame.KFGameInfo", "ClanMotto", ""); //
            inif2.Write("Engine.GameInfo", "ClanMotto", ""); //

            inif2.Write("Engine.GameInfo", "ServerMOTD", ""); //
            inif2.Write("KFGame.KFGameInfo", "ServerMOTD", ""); //

            inif2.Write("KFGame.KFGameInfo", "WebsiteLink", ""); //
            inif2.Write("Engine.GameInfo", "WebsiteLink", ""); //
            inif2.Write("KFGame.KFGameInfo", "BannerLink", ""); //
            inif2.Write("Engine.GameInfo", "BannerLink", ""); //




            switch (lbLenghtSelected.Text)
            {
                case "Short - 4 Waves":
                    inif2.Write("Engine.GameInfo", "GameLength", "0");
                    inif2.Write("KFGame.KFGameInfo", "GameLength", "0");
                    break;
                case "Medium - 7 Waves":
                    inif2.Write("Engine.GameInfo", "GameLength", "1");
                    inif2.Write("KFGame.KFGameInfo", "GameLength", "1");
                    break;
                case "Long - 10 Waves":
                    inif2.Write("Engine.GameInfo", "GameLength", "2");
                    inif2.Write("KFGame.KFGameInfo", "GameLength", "2");
                    break;
            }


            


            switch (boolCanPause)
            {
                case true:
                    inif2.Write("Engine.GameInfo", "bAdminCanPause", "true");
                    break;
                case false:
                    inif2.Write("Engine.GameInfo", "bAdminCanPausen", "false");
                    break;
            }





            switch (boolTeamCollision)
            {
                case true:
                    inif2.Write("KFGame.KFGameInfo", "bDisableTeamCollision", "true");
                    inif2.Write("Engine.GameReplicationInfo", "bDisableTeamCollision", "true");
                    break;
                case false:
                    inif2.Write("KFGame.KFGameInfo", "bDisableTeamCollision", "false");
                    inif2.Write("Engine.GameReplicationInfo", "bDisableTeamCollision", "false");
                    break;
            }






            INIFile inif3 = new INIFile(appPath + "\\KFGame\\Config\\PCServer-KFEngine.ini");

            if (boolServerTakeover == true)
            {
                inif3.Write("KFGame.KFGameInfo", "bUsedForTakeover", "true");
                inif3.Write("Engine.GameEngine", "bUsedForTakeover", "true");
            }
            else if (boolServerTakeover == false)
            {
                inif3.Write("KFGame.KFGameInfo", "bUsedForTakeover", "false");
                inif3.Write("Engine.GameEngine", "bUsedForTakeover", "false");
            }







            string strServerMOTDCode = "<font color='#" + strServerMOTDColor + "' size='24'>" + txtMessage.Text + "</font>";










            //Montar novo arquivo


            string strHorzineConfig = File.ReadAllText(appPath + @"\Horzine-Config.ini", Encoding.UTF8);

            strHorzineConfig = strHorzineConfig.Replace("StartingMap=", "StartingMap=" + lbStartingMapSelected.Text);
            strHorzineConfig = strHorzineConfig.Replace("GameMode=", "GameMode=" + lbGameModeSelected.Text);
            strHorzineConfig = strHorzineConfig.Replace("GameDifficult=", "GameDifficult=" + lbDifficultSelected.Text);
            strHorzineConfig = strHorzineConfig.Replace("GameLenght=", "GameLenght=" + lbLenghtSelected.Text);
            strHorzineConfig = strHorzineConfig.Replace("ServerMOTDText=", "ServerMOTDText=" + txtMessage.Text);

            System.IO.File.Delete(appPath + @"\Horzine-Config.ini");

            using (StreamWriter swClifor = new StreamWriter(appPath + @"\Horzine-Config.ini", true, Encoding.Unicode))
            {
                string cString = strHorzineConfig;
                swClifor.WriteLine(cString);
            }


            // Montar novo arquivo
            

            string strPCServerKFGame = File.ReadAllText(appPath + "\\KFGame\\Config\\PCServer-KFGame.ini", Encoding.UTF8);

            strPCServerKFGame = strPCServerKFGame.Replace("ServerName=", "ServerName=" + txtServerName.Text);
            strPCServerKFGame = strPCServerKFGame.Replace("GamePassword=", "GamePassword=" + txtServerPassword.Text);
            strPCServerKFGame = strPCServerKFGame.Replace("ClanMotto=", "ClanMotto=" + txtTitle.Text);
            strPCServerKFGame = strPCServerKFGame.Replace("ServerMOTD=", "ServerMOTD=" + strServerMOTDCode);
            strPCServerKFGame = strPCServerKFGame.Replace("WebsiteLink=", "WebsiteLink=" + txtSiteLink.Text);
            strPCServerKFGame = strPCServerKFGame.Replace("BannerLink=", "BannerLink=" + txtBannerLink.Text);

            System.IO.File.Delete(appPath + "\\KFGame\\Config\\PCServer-KFGame.ini");

            using (StreamWriter swClifor2 = new StreamWriter(appPath + "\\KFGame\\Config\\PCServer-KFGame.ini", true, Encoding.Unicode))
            {
                string cString = strPCServerKFGame;
                swClifor2.WriteLine(cString);
            }
            
        }












        private void CarregarInformacoesIniciais()
        {
            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            INIFile inif = new INIFile(appPath + "\\Horzine-Config.ini");

            strGameMode = inif.Read("Server.Info", "GameMode");
            strGameDifficult = inif.Read("Server.Info", "GameDifficult");
            strGameLenght = inif.Read("Server.Info", "GameLenght");
            strWebAdminUser = inif.Read("Server.Info", "WebAdminUser");
            strWebAdminPassword = inif.Read("Server.Info", "WebAdminPassword");
            strSoundEffects = inif.Read("Server.Info", "SoundEffects");
            strServerMOTDColor = inif.Read("Server.Info", "ServerMOTDColor");
            strServerMOTDText = inif.Read("Server.Info", "ServerMOTDText");




            INIFile inif2 = new INIFile(appPath + "\\KFGame\\Config\\PCServer-KFGame.ini");

            strServerName = inif2.Read("Engine.GameReplicationInfo", "ServerName");
            strServerPassword = inif2.Read("Engine.AccessControl", "GamePassword");
            strServerTitle = inif2.Read("KFGame.KFGameInfo", "ClanMotto");
            //strServerMessage = inif2.Read("KFGame.KFGameInfo", "ServerMOTD");
            strSiteLink = inif2.Read("KFGame.KFGameInfo", "WebsiteLink");
            strBannerLink = inif2.Read("KFGame.KFGameInfo", "BannerLink");
            strTeamCollision = inif2.Read("KFGame.KFGameInfo", "bDisableTeamCollision");
            strCanPause = inif2.Read("Engine.GameInfo", "bAdminCanPause");
            strGameLength = inif2.Read("Engine.GameInfo", "GameLength");



            INIFile inif3 = new INIFile(appPath + "\\KFGame\\Config\\PCServer-KFEngine.ini");

            strServerTakeover = inif3.Read("Engine.GameEngine", "bUsedForTakeover");


            lbGameModeSelected.Text = strGameMode;
            lbDifficultSelected.Text = strGameDifficult;
            lbLenghtSelected.Text = strGameLenght;
            txtServerName.Text = strServerName;
            txtServerPassword.Text = strServerPassword;
            txtWebAdminUsername.Text = strWebAdminUser;
            txtWebAdminPassword.Text = strWebAdminPassword;
            txtTitle.Text = strServerTitle;
            txtMessage.Text = strServerMOTDText;
            txtSiteLink.Text = strSiteLink;
            txtBannerLink.Text = strBannerLink;


            txtMessage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#" + strServerMOTDColor);






            if(strGameLength == "0")
            {
                lbLenghtSelected.Text = "Short - 4 Waves";
            }
            else if(strGameLenght == "1")
            {
                lbLenghtSelected.Text = "Medium - 7 Waves";
            }
            else if(strGameLenght == "2")
            {
                lbLenghtSelected.Text = "Long - 10 Waves";
            }





            strServerTakeover = strServerTakeover.ToLower();
            if (strServerTakeover.Contains("true"))
            {
                ServerTakeoverEnablClick();
            }
            else if (strServerTakeover.Contains("false"))
            {
                ServerTakeoverDisableClick();
            }









            strTeamCollision = strTeamCollision.ToLower();
            if (strTeamCollision.Contains("false"))
            {
                TeamCollisionEnableClick();
            }
            else if (strTeamCollision.Contains("true"))
            {
                TeamCollisionDisableClick();
            }









            

            strCanPause = strCanPause.ToLower();
            if (strCanPause.Contains("true"))
            {
                CanPauseEnableClick();
            }
            else if (strCanPause.Contains("false"))
            {
                CanPauseDisableClick();
            }


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
            






            string line;

            
            System.IO.StreamReader file = new System.IO.StreamReader(@"customMaps.db");
            while ((line = file.ReadLine()) != null)
            {
                if (line == "") { }
                else
                {
                    listBoxStartingMap.Items.Add(line);
                }
            }

            file.Close();





            CheckGameMode();


        }


        private void pbColors_Click(object sender, EventArgs e)
        {
            SoundEffectClick();

            byte R = 255;
            byte G = 255;
            byte B = 255;
            byte A = 255;

            ColorDialog cd = new ColorDialog();

            DialogResult result = cd.ShowDialog();

            if (result == DialogResult.OK)
            {
                Color color = cd.Color;

                B = color.B;
                G = color.G;
                R = color.R;
                A = color.A;

                txtMessage.ForeColor = cd.Color;
            }

            Color myColor = Color.FromArgb(R, G, B);
            string hex = myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");

            strServerMOTDColor = hex;

        }


        private void VerificarConexaoInternet()
        {
            try
            {
                using (var client = new WebClient())
                using (var streamCheck = client.OpenRead("http://www.google.com"))
                {
                    pnInternetConection.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconConected;
                }
            }
            catch
            {
                MessageBox.Show("No Internet Conection.", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                
                pnInternetConection.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconNotConected;
            }
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            VerificarConexaoInternet();

            timer1.Start();
            
            lbStartingMapSelected.Text = "";

            CarregarInformacoesIniciais();

            pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreview;



            listBoxStartingMap.BackColor = Color.FromArgb(18, 16, 16);
            listBoxGameMode.BackColor = Color.FromArgb(18, 16, 16);
            listBoxDifficult.BackColor = Color.FromArgb(18, 16, 16);
            listBoxLenght.BackColor = Color.FromArgb(18, 16, 16);

            txtServerName.BackColor = Color.FromArgb(88, 10, 5);
            txtWebAdminUsername.BackColor = Color.FromArgb(88, 10, 5);

            this.listBoxStartingMap.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBoxStartingMap.DrawItem += new System.Windows.Forms.DrawItemEventHandler(listBoxStartingMap_DrawItem);
            this.listBoxStartingMap.SelectedIndexChanged += new System.EventHandler(listBoxStartingMap_SelectedIndexChanged_1);


            this.listBoxGameMode.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBoxGameMode.DrawItem += new System.Windows.Forms.DrawItemEventHandler(listBoxGameMode_DrawItem_1);
            this.listBoxGameMode.SelectedIndexChanged += new System.EventHandler(listBoxGameMode_SelectedIndexChanged);


            this.listBoxDifficult.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBoxDifficult.DrawItem += new System.Windows.Forms.DrawItemEventHandler(listBoxDifficult_DrawItem);
            this.listBoxDifficult.SelectedIndexChanged += new System.EventHandler(listBoxDifficult_SelectedIndexChanged);


            this.listBoxLenght.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBoxLenght.DrawItem += new System.Windows.Forms.DrawItemEventHandler(listBoxLenght_DrawItem);
            this.listBoxLenght.SelectedIndexChanged += new System.EventHandler(listBoxLenght_SelectedIndexChanged);






            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            Custom_font();
            //this.Font = new Font(font_Insomnia, 10, FontStyle.Regular);

            lbFrenetico.Font = new Font(font_Insomnia, 12, FontStyle.Regular);
            lbSasuke.Font = new Font(font_Insomnia, 12, FontStyle.Regular);
            lbLaunchServer.Font = new Font(font_Insomnia, 15, FontStyle.Regular);

            lbStartingMap.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            lbGameMode.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            lbDifficult.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            lbLenght.Font = new Font(font_Insomnia, 10, FontStyle.Regular);

            lbTitle.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            lbMessage.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            lbSiteLink.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            lbBannerLink.Font = new Font(font_Insomnia, 10, FontStyle.Regular);

            listBoxStartingMap.Font = new Font(font_Insomnia, 12, FontStyle.Regular);
            listBoxGameMode.Font = new Font(font_Insomnia, 12, FontStyle.Regular);
            listBoxDifficult.Font = new Font(font_Insomnia, 12, FontStyle.Regular);
            listBoxLenght.Font = new Font(font_Insomnia, 12, FontStyle.Regular);


            lbStartingMapSelected.Font = new Font(font_Insomnia, 12, FontStyle.Regular);
            lbGameModeSelected.Font = new Font(font_Insomnia, 12, FontStyle.Regular);
            lbDifficultSelected.Font = new Font(font_Insomnia, 12, FontStyle.Regular);
            lbLenghtSelected.Font = new Font(font_Insomnia, 12, FontStyle.Regular);

            txtServerName.Font = new Font(font_Insomnia, 12, FontStyle.Regular);
            txtServerPassword.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            txtWebAdminUsername.Font = new Font(font_Insomnia, 12, FontStyle.Regular);
            txtWebAdminPassword.Font = new Font(font_Insomnia, 10, FontStyle.Regular);

            txtTitle.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            txtMessage.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            txtSiteLink.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            txtBannerLink.Font = new Font(font_Insomnia, 10, FontStyle.Regular);

            lbFrenetico.BackColor = Color.FromArgb(88, 10, 5);
            lbSasuke.BackColor = Color.FromArgb(88, 10, 5);
            lbLaunchServer.BackColor = Color.FromArgb(88, 10, 5);

            lbLaunchServer.BackColor = Color.FromArgb(88, 10, 5);
            lbLaunchServer.BackColor = Color.FromArgb(88, 10, 5);
            lbLaunchServer.BackColor = Color.FromArgb(88, 10, 5);

            lbStartingMap.BackColor = Color.FromArgb(88, 10, 5);
            lbGameMode.BackColor = Color.FromArgb(88, 10, 5);
            lbDifficult.BackColor = Color.FromArgb(88, 10, 5);
            lbLenght.BackColor = Color.FromArgb(88, 10, 5);

            lbTitle.BackColor = Color.FromArgb(88, 10, 5);
            lbMessage.BackColor = Color.FromArgb(88, 10, 5);
            lbSiteLink.BackColor = Color.FromArgb(88, 10, 5);
            lbBannerLink.BackColor = Color.FromArgb(88, 10, 5);


            pbColors.BackColor = Color.FromArgb(88, 10, 5);

            lbFrenetico.ForeColor = corTexto;
            lbSasuke.ForeColor = corTexto;
            lbLaunchServer.ForeColor = corTexto;

            lbStartingMap.ForeColor = corTexto;
            lbGameMode.ForeColor = corTexto;
            lbDifficult.ForeColor = corTexto;
            lbLenght.ForeColor = corTexto;

            lbTitle.ForeColor = corTexto;
            lbMessage.ForeColor = corTexto;
            lbSiteLink.ForeColor = corTexto;
            lbBannerLink.ForeColor = corTexto;

            txtServerName.ForeColor = corTexto;
            txtWebAdminUsername.ForeColor = corTexto;


        }

        private void pbFrenetico_MouseMove(object sender, MouseEventArgs e)
        {
            pbFrenetico.BackgroundImage = HorzineDedicatedServer.Properties.Resources.imgCreditoMouseMove;
            lbFrenetico.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbFrenetico_MouseLeave(object sender, EventArgs e)
        {
            pbFrenetico.BackgroundImage = HorzineDedicatedServer.Properties.Resources.imgCreditos;
            lbFrenetico.BackColor = Color.FromArgb(88, 10, 5);
        }

        private void lbFrenetico_MouseMove(object sender, MouseEventArgs e)
        {
            pbFrenetico.BackgroundImage = HorzineDedicatedServer.Properties.Resources.imgCreditoMouseMove;
            lbFrenetico.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbFrenetico_MouseLeave(object sender, EventArgs e)
        {
            pbFrenetico.BackgroundImage = HorzineDedicatedServer.Properties.Resources.imgCreditos;
            lbFrenetico.BackColor = Color.FromArgb(88, 10, 5);
        }







        private void pbSasuke_MouseMove(object sender, MouseEventArgs e)
        {
            pbSasuke.BackgroundImage = HorzineDedicatedServer.Properties.Resources.imgCreditoMouseMove;
            lbSasuke.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbSasuke_MouseLeave(object sender, EventArgs e)
        {
            pbSasuke.BackgroundImage = HorzineDedicatedServer.Properties.Resources.imgCreditos;
            lbSasuke.BackColor = Color.FromArgb(88, 10, 5);
        }

        private void lbSasuke_MouseMove(object sender, MouseEventArgs e)
        {
            pbSasuke.BackgroundImage = HorzineDedicatedServer.Properties.Resources.imgCreditoMouseMove;
            lbSasuke.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbSasuke_MouseLeave(object sender, EventArgs e)
        {
            pbSasuke.BackgroundImage = HorzineDedicatedServer.Properties.Resources.imgCreditos;
            lbSasuke.BackColor = Color.FromArgb(88, 10, 5);
        }








        private void pbFrenetico_Click(object sender, EventArgs e)
        {
            SoundEffectClick();
            System.Diagnostics.Process.Start("https://github.com/paullomuniz");
        }

        private void lbFrenetico_Click(object sender, EventArgs e)
        {
            SoundEffectClick();
            System.Diagnostics.Process.Start("https://github.com/paullomuniz");
        }

        private void pbSasuke_Click(object sender, EventArgs e)
        {
            SoundEffectClick();
            System.Diagnostics.Process.Start("https://steamcommunity.com/id/Sasuke_Louis/myworkshopfiles/?appid=232090&p=1");
        }

        private void lbSasuke_Click(object sender, EventArgs e)
        {
            SoundEffectClick();
            System.Diagnostics.Process.Start("https://steamcommunity.com/id/Sasuke_Louis/myworkshopfiles/?appid=232090&p=1");
        }










        private void pbLaunchServer_MouseMove(object sender, MouseEventArgs e)
        {
            pbLaunchServer.BackgroundImage = HorzineDedicatedServer.Properties.Resources.launchServerMouseMove;
            lbLaunchServer.BackColor = Color.FromArgb(7, 230, 239);
            lbLaunchServer.ForeColor = Color.Black;
        }

        private void pbLaunchServer_MouseLeave(object sender, EventArgs e)
        {
            pbLaunchServer.BackgroundImage = HorzineDedicatedServer.Properties.Resources.launchServer;
            lbLaunchServer.BackColor = Color.FromArgb(88, 10, 5);
            lbLaunchServer.ForeColor = corTexto;
        }

        private void lbLaunchServer_MouseMove(object sender, MouseEventArgs e)
        {
            pbLaunchServer.BackgroundImage = HorzineDedicatedServer.Properties.Resources.launchServerMouseMove;
            lbLaunchServer.BackColor = Color.FromArgb(7, 230, 239);
            lbLaunchServer.ForeColor = Color.Black;
        }

        private void lbLaunchServer_MouseLeave(object sender, EventArgs e)
        {
            pbLaunchServer.BackgroundImage = HorzineDedicatedServer.Properties.Resources.launchServer;
            lbLaunchServer.BackColor = Color.FromArgb(88, 10, 5);
            lbLaunchServer.ForeColor = corTexto;
        }







        
        private void pbServerName_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerName.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverNameMouseMove;
            txtServerName.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbServerName_MouseLeave(object sender, EventArgs e)
        {
            pbServerName.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverName;
            txtServerName.BackColor = Color.FromArgb(88, 10, 5);
        }

        private void pbWebAdmin_MouseMove(object sender, MouseEventArgs e)
        {
            pbWebAdmin.BackgroundImage = HorzineDedicatedServer.Properties.Resources.webAdminMouseMove;
            txtWebAdminUsername.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbWebAdmin_MouseLeave(object sender, EventArgs e)
        {
            pbWebAdmin.BackgroundImage = HorzineDedicatedServer.Properties.Resources.webAdmin;
            txtWebAdminUsername.BackColor = Color.FromArgb(88, 10, 5);
        }

        private void txtServerName_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerName.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverNameMouseMove;
            txtServerName.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void txtServerName_MouseLeave(object sender, EventArgs e)
        {
            pbServerName.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverName;
            txtServerName.BackColor = Color.FromArgb(88, 10, 5);
        }

        private void txtWebAdminUsername_MouseMove(object sender, MouseEventArgs e)
        {
            pbWebAdmin.BackgroundImage = HorzineDedicatedServer.Properties.Resources.webAdminMouseMove;
            txtWebAdminUsername.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void txtWebAdminUsername_MouseLeave(object sender, EventArgs e)
        {
            pbWebAdmin.BackgroundImage = HorzineDedicatedServer.Properties.Resources.webAdmin;
            txtWebAdminUsername.BackColor = Color.FromArgb(88, 10, 5);
        }























        private void pbMenuMutators_MouseMove(object sender, MouseEventArgs e)
        {
            pbMenuMutators.BackgroundImage = HorzineDedicatedServer.Properties.Resources.buttonMutatorsMouseMove;
        }

        private void pbMenuMutators_MouseLeave(object sender, EventArgs e)
        {
            pbMenuMutators.BackgroundImage = HorzineDedicatedServer.Properties.Resources.buttonMutators;
        }












        private void pbMenuCustomMaps_MouseMove(object sender, MouseEventArgs e)
        {
            pbMenuCustomMaps.BackgroundImage = HorzineDedicatedServer.Properties.Resources.buttonCustomMapsMouseMove;
        }

        private void pbMenuCustomMaps_MouseLeave(object sender, EventArgs e)
        {
            pbMenuCustomMaps.BackgroundImage = HorzineDedicatedServer.Properties.Resources.buttonCustomMaps;
        }









        private void pbMenuHelp_MouseMove(object sender, MouseEventArgs e)
        {
            pbMenuHelp.BackgroundImage = HorzineDedicatedServer.Properties.Resources.buttonHelpMouseMove;
        }

        private void pbMenuHelp_MouseLeave(object sender, EventArgs e)
        {
            pbMenuHelp.BackgroundImage = HorzineDedicatedServer.Properties.Resources.buttonHelp;
        }

































        private void pbServerTitle_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerTitle.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbTitle.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbServerTitle_MouseLeave(object sender, EventArgs e)
        {
            pbServerTitle.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbTitle.BackColor = Color.Transparent;
        }

        private void lbTitle_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerTitle.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbTitle.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbTitle_MouseLeave(object sender, EventArgs e)
        {
            pbServerTitle.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbTitle.BackColor = Color.Transparent;
        }









        private void txtTitle_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerTitle.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbTitle.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void txtTitle_MouseLeave(object sender, EventArgs e)
        {
            pbServerTitle.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbTitle.BackColor = Color.Transparent;
        }







        private void pbServerMessage_MouseMove(object sender, MouseEventArgs e)
        {

            pbServerMessage.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbMessage.BackColor = Color.FromArgb(189, 90, 18);
            pbColors.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbServerMessage_MouseLeave(object sender, EventArgs e)
        {
            pbServerMessage.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbMessage.BackColor = Color.Transparent;
            pbColors.BackColor = Color.Transparent;
        }

        private void lbMessage_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerMessage.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbMessage.BackColor = Color.FromArgb(189, 90, 18);
            pbColors.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbMessage_MouseLeave(object sender, EventArgs e)
        {
            pbServerMessage.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbMessage.BackColor = Color.Transparent;
            pbColors.BackColor = Color.Transparent;
        }

        private void pbColors_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerMessage.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbMessage.BackColor = Color.FromArgb(189, 90, 18);
            pbColors.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbColors_MouseLeave(object sender, EventArgs e)
        {
            pbServerMessage.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbMessage.BackColor = Color.Transparent;
            pbColors.BackColor = Color.Transparent;
        }



        private void txtMessage_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerMessage.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbMessage.BackColor = Color.FromArgb(189, 90, 18);
            pbColors.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void txtMessage_MouseLeave(object sender, EventArgs e)
        {
            pbServerMessage.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbMessage.BackColor = Color.Transparent;
            pbColors.BackColor = Color.Transparent;
        }












        private void pbServerSiteLink_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerSiteLink.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbSiteLink.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbServerSiteLink_MouseLeave(object sender, EventArgs e)
        {
            pbServerSiteLink.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbSiteLink.BackColor = Color.Transparent;
        }

        private void lbSiteLink_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerSiteLink.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbSiteLink.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbSiteLink_MouseLeave(object sender, EventArgs e)
        {
            pbServerSiteLink.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbSiteLink.BackColor = Color.Transparent;
        }


        private void txtSiteLink_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerSiteLink.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbSiteLink.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void txtSiteLink_MouseLeave(object sender, EventArgs e)
        {
            pbServerSiteLink.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbSiteLink.BackColor = Color.Transparent;
        }












        private void pbServerBannerLink_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerBannerLink.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbBannerLink.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbServerBannerLink_MouseLeave(object sender, EventArgs e)
        {
            pbServerBannerLink.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbBannerLink.BackColor = Color.Transparent;
        }

        private void lbBannerLink_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerBannerLink.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbBannerLink.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbBannerLink_MouseLeave(object sender, EventArgs e)
        {
            pbServerBannerLink.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbBannerLink.BackColor = Color.Transparent;
        }




        private void txtBannerLink_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerBannerLink.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbBannerLink.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void txtBannerLink_MouseLeave(object sender, EventArgs e)
        {
            pbServerBannerLink.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbBannerLink.BackColor = Color.Transparent;
        }















































        private void frmServer_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = HorzineDedicatedServer.Properties.Resources.KF_server_interface;

            listBoxStartingMap.Visible = false;
            listBoxGameMode.Visible = false;
            listBoxDifficult.Visible = false;
            listBoxLenght.Visible = false;

            if (lbGameModeSelected.Text.Contains("Endless"))
            {
                lbLenght.BackColor = Color.FromArgb(57, 54, 51);
            }
            else
            {
                lbLenght.BackColor = Color.FromArgb(88, 10, 5);
            }
        }



















        private void GetMapPreview()
        {

            
            switch (lbStartingMapSelected.Text)
            {
                case "Airship":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewAirship;
                    break;

                case "Ashwood Asylum":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewAshwoodAsylum;
                    break;

                case "Biolapse":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewBiolapse;
                    break;

                case "Biotics Lab":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewBioticsLab;
                    break;

                case "Black Forest":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewBlackForest;
                    break;

                case "Burning Paris":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewBurningParis;
                    break;

                case "Catacombs":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewCatacombs;
                    break;

                case "Containment Station":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewContainmentStation;
                    break;

                case "Desolation":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewDesolation;
                    break;

                case "DieSector":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewDieSector;
                    break;

                case "Dystopia 2029":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewDystopia2029;
                    break;

                case "Elysium":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewElysium;
                    break;

                case "Evacuation Point":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewEvacuationPoint;
                    break;

                case "Farmhouse":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewFarmhouse;
                    break;

                case "Hellmark Station":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewHellmarkStation;
                    break;

                case "Hostile Grounds":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewHostileGrounds;
                    break;

                case "Infernal Realm":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewInfernalRealm;
                    break;

                case "Krampus Lair":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewKrampusLair;
                    break;

                case "Lockdown":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewLockdown;
                    break;

                case "Monster Ball":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewMonsterBall;
                    break;

                case "Moonbase":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewMoonbase;
                    break;

                case "Netherhold":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewNetherhold;
                    break;

                case "Nightmare":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewNightmare;
                    break;

                case "Nuked":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewNuked;
                    break;

                case "Outpost":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewOutpost;
                    break;

                case "Power Core":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewPowerCore;
                    break;

                case "Prison":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewPrison;
                    break;

                case "Sanitarium":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewSanitarium;
                    break;

                case "Santas Workshop":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewSantasWorkshop;
                    break;

                case "Shopping Spree":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewShoppingSpree;
                    break;

                case "Spillway":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewSpillway;
                    break;

                case "Steam Fortress":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewSteamFortress;
                    break;

                case "The Descent":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewTheDescent;
                    break;

                case "The Tragic Kingdom":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewTheTragicKingdom;
                    break;

                case "Volter Manor":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewVolterManor;
                    break;

                case "ZED Landing":
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreviewZEDLanding;
                    break;

                default:
                    pbMapPreview.BackgroundImage = HorzineDedicatedServer.Properties.Resources.mapPreview;

                    break;
            }

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
            else if(soundClickActivated == false)
            {
                soundClickActivated = true;
            }


            inif.Write("Server.Info", "SoundEffects", soundClickActivated.ToString());
        }





















        private void CheckGameMode()
        {

            if (lbGameModeSelected.Text.Contains("Versus"))
            {
                pbDifficult.BackgroundImage = HorzineDedicatedServer.Properties.Resources.kfLenghtLocked;
                lbDifficult.BackColor = Color.FromArgb(57, 54, 51);
                lbDifficultSelected.ForeColor = Color.FromArgb(75, 71, 64);

                pbLenght.BackgroundImage = HorzineDedicatedServer.Properties.Resources.kfLenghtLocked;
                lbLenght.BackColor = Color.FromArgb(57, 54, 51);
                lbLenghtSelected.ForeColor = Color.FromArgb(75, 71, 64);

                lbDifficultSelected.Text = "Normal";
                lbLenghtSelected.Text = "Medium - 7 Waves";

            }
            else if (lbGameModeSelected.Text.Contains("Endless"))
            {
                pbLenght.BackgroundImage = HorzineDedicatedServer.Properties.Resources.kfLenghtLocked;
                lbLenght.BackColor = Color.FromArgb(57, 54, 51);
                lbLenghtSelected.ForeColor = Color.FromArgb(75, 71, 64);

                lbLenghtSelected.Text = "Medium - 7 Waves";

            }


        }

















        private void ServerTakeoverEnablClick()
        {
            
            rbServerTakeoverEnable.BackgroundImage = HorzineDedicatedServer.Properties.Resources.rbEnable;
            rbServerTakeoverDisable.BackgroundImage = HorzineDedicatedServer.Properties.Resources.rbDisable;

            boolServerTakeover = true;
        }

        private void ServerTakeoverDisableClick()
        {
            
            rbServerTakeoverDisable.BackgroundImage = HorzineDedicatedServer.Properties.Resources.rbEnable;
            rbServerTakeoverEnable.BackgroundImage = HorzineDedicatedServer.Properties.Resources.rbDisable;

            boolServerTakeover = false;
        }







        private void TeamCollisionEnableClick()
        {
            
            rbTeamCollisionEnable.BackgroundImage = HorzineDedicatedServer.Properties.Resources.rbEnable;
            rbTeamCollisionDisable.BackgroundImage = HorzineDedicatedServer.Properties.Resources.rbDisable;

            boolTeamCollision = false;
        }

        private void TeamCollisionDisableClick()
        {
            
            rbTeamCollisionDisable.BackgroundImage = HorzineDedicatedServer.Properties.Resources.rbEnable;
            rbTeamCollisionEnable.BackgroundImage = HorzineDedicatedServer.Properties.Resources.rbDisable;

            boolTeamCollision = true;
        }








        private void CanPauseEnableClick()
        {
            
            rbCanPauseEnable.BackgroundImage = HorzineDedicatedServer.Properties.Resources.rbEnable;
            rbCanPauseDisable.BackgroundImage = HorzineDedicatedServer.Properties.Resources.rbDisable;

            boolCanPause = true;
        }

        private void CanPauseDisableClick()
        {
            
            rbCanPauseDisable.BackgroundImage = HorzineDedicatedServer.Properties.Resources.rbEnable;
            rbCanPauseEnable.BackgroundImage = HorzineDedicatedServer.Properties.Resources.rbDisable;

            boolCanPause = false;
        }













        private void StartingMapMouseMove()
        {
            pbStartingMap.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbStartingMap.BackColor = Color.FromArgb(189, 90, 18);
            lbStartingMapSelected.BackColor = Color.FromArgb(21, 4, 3);

            CheckGameMode();
        }

        private void StartingMapMouseLeave()
        {
            pbStartingMap.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbStartingMap.BackColor = Color.Transparent;
            lbStartingMapSelected.BackColor = Color.Transparent;

            CheckGameMode();
        }






        private void GameModeMouseMove()
        {
            pbGameMode.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbGameMode.BackColor = Color.FromArgb(189, 90, 18);
            lbGameModeSelected.BackColor = Color.FromArgb(21, 4, 3);

            CheckGameMode();
        }

        private void GameModeMapMouseLeave()
        {
            pbGameMode.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbGameMode.BackColor = Color.Transparent;
            lbGameModeSelected.BackColor = Color.Transparent;

            CheckGameMode();
        }







        private void DifficultMouseMove()
        {
            pbDifficult.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbDifficult.BackColor = Color.FromArgb(189, 90, 18);
            lbDifficultSelected.BackColor = Color.FromArgb(21, 4, 3);

            CheckGameMode();
        }

        private void DifficultMouseLeave()
        {
            pbDifficult.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbDifficult.BackColor = Color.Transparent;
            lbDifficultSelected.BackColor = Color.Transparent;

            CheckGameMode();
        }







        private void LenghtMapMouseMove()
        {
            pbLenght.BackgroundImage = HorzineDedicatedServer.Properties.Resources.serverTitleMouseMove;
            lbLenght.BackColor = Color.FromArgb(189, 90, 18);
            lbLenghtSelected.BackColor = Color.FromArgb(21, 4, 3);

            CheckGameMode();
        }

        private void LenghtMouseLeave()
        {
            pbLenght.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbLenght.BackColor = Color.Transparent;
            lbLenghtSelected.BackColor = Color.Transparent;

            CheckGameMode();
        }











        private void StartingMapClick()
        {
            SoundEffectClick();

            this.BackgroundImage = HorzineDedicatedServer.Properties.Resources.KF_map;

            listBoxStartingMap.Visible = true;

            listBoxGameMode.Visible = false;
            listBoxDifficult.Visible = false;
            listBoxLenght.Visible = false;


            CheckGameMode();
        }

        private void GameModeClick()
        {
            SoundEffectClick();

            this.BackgroundImage = HorzineDedicatedServer.Properties.Resources.KF_gamemode;

            listBoxGameMode.Visible = true;

            listBoxStartingMap.Visible = false;
            listBoxDifficult.Visible = false;
            listBoxLenght.Visible = false;

            CheckGameMode();
        }

        private void DifficultClick()
        {
            SoundEffectClick();

            if (lbGameModeSelected.Text.Contains("Versus")) { }
            else
            {
                this.BackgroundImage = HorzineDedicatedServer.Properties.Resources.KF_difficulty;

                listBoxDifficult.Visible = true;

                listBoxStartingMap.Visible = false;
                listBoxGameMode.Visible = false;
                listBoxLenght.Visible = false;

                CheckGameMode();
            }
        }

        private void LenghtClick()
        {
            SoundEffectClick();

            if (lbGameModeSelected.Text.Contains("Endless") || lbGameModeSelected.Text.Contains("Versus")) { }
            else
            {
                //lbLenghtSelected.Text = "";

                this.BackgroundImage = HorzineDedicatedServer.Properties.Resources.KF_Lenght;

                lbLenght.BackColor = Color.Transparent;
                lbLenghtSelected.BackColor = Color.Transparent;

                listBoxLenght.Visible = true;

                listBoxStartingMap.Visible = false;
                listBoxGameMode.Visible = false;
                listBoxDifficult.Visible = false;

                lbLenght.BackColor = Color.Transparent;


                CheckGameMode();
            }
        }

















        private void rbServerTakeoverEnable_Click(object sender, EventArgs e)
        {
            ServerTakeoverEnablClick();
        }

        private void rbServerTakeoverDisable_Click(object sender, EventArgs e)
        {
            ServerTakeoverDisableClick();
        }

        private void pbServerTakeoverEnable_Click(object sender, EventArgs e)
        {
           ServerTakeoverEnablClick();
        }

        private void pbServerTakeoverDisable_Click(object sender, EventArgs e)
        {
            ServerTakeoverDisableClick();
        }











        private void rbTeamCollisionEnable_Click(object sender, EventArgs e)
        {
            TeamCollisionEnableClick();
        }

        private void rbTeamCollisionDisable_Click(object sender, EventArgs e)
        {
            TeamCollisionDisableClick();
        }

        private void pbTeamCollisionEnable_Click(object sender, EventArgs e)
        {
            TeamCollisionEnableClick();
        }

        private void pbTeamCollisionDisable_Click(object sender, EventArgs e)
        {
            TeamCollisionDisableClick();
        }







        private void rbCanPauseEnable_Click(object sender, EventArgs e)
        {
            CanPauseEnableClick();
        }

        private void rbCanPauseDisable_Click(object sender, EventArgs e)
        {
            CanPauseDisableClick();
        }

        private void pbCanPauseEnable_Click(object sender, EventArgs e)
        {
            CanPauseEnableClick();
        }

        private void pbCanPauseDisable_Click(object sender, EventArgs e)
        {
            CanPauseDisableClick();
        }













        private void pbStartingMap_MouseMove(object sender, MouseEventArgs e)
        {
            StartingMapMouseMove();
        }

        private void pbStartingMap_MouseLeave(object sender, EventArgs e)
        {
            StartingMapMouseLeave();
        }

        private void lbStartingMapSelected_MouseMove(object sender, MouseEventArgs e)
        {
            StartingMapMouseMove();
        }

        private void lbStartingMapSelected_MouseLeave(object sender, EventArgs e)
        {
            StartingMapMouseLeave();
        }

        private void lbStartingMap_MouseMove(object sender, MouseEventArgs e)
        {
            StartingMapMouseMove();
        }

        private void lbStartingMap_MouseLeave(object sender, EventArgs e)
        {
            StartingMapMouseLeave();
        }








        private void pbGameMode_MouseMove(object sender, MouseEventArgs e)
        {
            GameModeMouseMove();
        }

        private void pbGameMode_MouseLeave(object sender, EventArgs e)
        {
            GameModeMapMouseLeave();
        }

        private void lbGameModeSelected_MouseMove(object sender, MouseEventArgs e)
        {
            GameModeMouseMove();
        }

        private void lbGameModeSelected_MouseLeave(object sender, EventArgs e)
        {
            GameModeMapMouseLeave();
        }

        private void lbGameMode_MouseMove(object sender, MouseEventArgs e)
        {
            GameModeMouseMove();
        }

        private void lbGameMode_MouseLeave(object sender, EventArgs e)
        {
            GameModeMapMouseLeave();
        }










        private void pbDifficult_MouseMove(object sender, MouseEventArgs e)
        {
            DifficultMouseMove();
        }

        private void pbDifficult_MouseLeave(object sender, EventArgs e)
        {
            DifficultMouseLeave();
        }

        private void lbDifficultSelected_MouseMove(object sender, MouseEventArgs e)
        {
            DifficultMouseMove();
        }

        private void lbDifficultSelected_MouseLeave(object sender, EventArgs e)
        {
            DifficultMouseLeave();
        }

        private void lbDifficult_MouseMove(object sender, MouseEventArgs e)
        {
            DifficultMouseMove();
        }

        private void lbDifficult_MouseLeave(object sender, EventArgs e)
        {
            DifficultMouseLeave();
        }










        private void pbLenght_MouseMove(object sender, MouseEventArgs e)
        {
            LenghtMapMouseMove();
        }

        private void pbLenght_MouseLeave(object sender, EventArgs e)
        {
            LenghtMouseLeave();
        }

        private void lbLenghtSelected_MouseMove(object sender, MouseEventArgs e)
        {
            LenghtMapMouseMove();
        }

        private void lbLenghtSelected_MouseLeave(object sender, EventArgs e)
        {
            LenghtMouseLeave();
        }

        private void lbLenght_MouseMove(object sender, MouseEventArgs e)
        {
            LenghtMapMouseMove();
        }

        private void lbLenght_MouseLeave(object sender, EventArgs e)
        {
            LenghtMouseLeave();
        }











        private void lbStartingMap_Click(object sender, EventArgs e)
        {
            StartingMapClick();
        }

        private void lbStartingMapSelected_Click(object sender, EventArgs e)
        {
            StartingMapClick();
        }

        private void pbStartingMap_Click(object sender, EventArgs e)
        {
            StartingMapClick();
        }











        private void lbGameMode_Click(object sender, EventArgs e)
        {
            GameModeClick();
        }

        private void pbGameMode_Click(object sender, EventArgs e)
        {
            GameModeClick();
        }

        private void lbGameModeSelected_Click(object sender, EventArgs e)
        {
            GameModeClick();
        }











        private void lbDifficult_Click(object sender, EventArgs e)
        {
            DifficultClick();
        }

        private void pbDifficult_Click(object sender, EventArgs e)
        {
            DifficultClick();
        }

        private void lbDifficultSelected_Click(object sender, EventArgs e)
        {
            DifficultClick();
        }













        private void lbLenght_Click(object sender, EventArgs e)
        {
            LenghtClick();
        }

        private void pbLenght_Click(object sender, EventArgs e)
        {
            LenghtClick();
        }

        private void lbLenghtSelected_Click(object sender, EventArgs e)
        {
            LenghtClick();
        }











        private void pbMenuMutators_Click(object sender, EventArgs e)
        {
            SoundEffectClick();

            frmMutators formMutators = new frmMutators();

            formMutators.Show();

            this.Hide();





        }

        private void pbMenuCustomMaps_Click(object sender, EventArgs e)
        {
            SoundEffectClick();

            frmCustomMaps formCustomMaps = new frmCustomMaps();

            formCustomMaps.Show();

            this.Hide();
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

        private void ListProcesses()
        {

            Process[] processCollection = Process.GetProcesses();
            foreach (Process p in processCollection)
            {
                strProcessos = strProcessos + p.ProcessName + Environment.NewLine; ;
            }

        }

        private void pbLaunchServer_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new WebClient())
                using (var streamCheck = client.OpenRead("http://www.google.com"))
                {
                    pnInternetConection.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconConected;
                    LaunchServer();
                }
            }
            catch
            {
                MessageBox.Show("No Internet Conection.", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                pnInternetConection.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconNotConected;
            }
            
        }

        private void lbLaunchServer_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new WebClient())
                using (var streamCheck = client.OpenRead("http://www.google.com"))
                {
                    pnInternetConection.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconConected;
                    LaunchServer();
                }
            }
            catch
            {
                MessageBox.Show("No Internet Conection.", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                pnInternetConection.BackgroundImage = HorzineDedicatedServer.Properties.Resources.iconNotConected;
            }
        }

        private void LaunchServer()
        {
            SoundEffectClick();

            SalvarInformacoesIniciais();

            ListProcesses();


            if (strProcessos.Contains("KFServer"))
            {
                if (MessageBox.Show("The server is already running, do you want to reboot to apply the new settings?", "Notification!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    Process[] ps = Process.GetProcessesByName("KFServer");

                    foreach (Process p in ps)
                    {
                        p.Kill();
                    }

                    VerificarInformações();
                }
            }
            else
            {
                VerificarInformações();
            }


        }

        private void VerificarInformações()
        {

            if (lbGameModeSelected.Text.Contains("Weekly") || lbGameModeSelected.Text.Contains("Versus") || lbGameModeSelected.Text.Contains("Endless"))
            {
                if (lbGameModeSelected.Text == "" || lbDifficultSelected.Text == "" || lbStartingMapSelected.Text == "" || txtServerName.Text == "" || txtMessage.Text == "" || txtTitle.Text == "" || txtSiteLink.Text == "" || txtBannerLink.Text == "" || txtWebAdminUsername.Text == "" || txtWebAdminPassword.Text == "")
                {
                    MessageBox.Show("Fill in all fields.", "Notification!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    VerificarMapa();
                    IniciarServidor();
                }
            }
            else
            {
                if (lbGameModeSelected.Text == "" || lbDifficultSelected.Text == "" || lbStartingMapSelected.Text == "" || txtServerName.Text == "" || txtMessage.Text == "" || txtTitle.Text == "" || txtSiteLink.Text == "" || txtBannerLink.Text == "" || txtWebAdminUsername.Text == "" || txtWebAdminPassword.Text == "")
                {
                    MessageBox.Show("Fill in all fields.", "Notification!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    VerificarMapa();
                    IniciarServidor();
                }
            }

        }

        private void VerificarMapa()
        {

            switch (lbStartingMapSelected.Text)
            {
                case "Airship":
                    strStartingMap = "kf-airship";
                    break;
                case "Ashwood Asylum":
                    strStartingMap = "kf-ashwoodasylum";
                    break;
                case "Biolapse":
                    strStartingMap = "kf-biolapse";
                    break;
                case "Biotics Lab":
                    strStartingMap = "kf-bioticslab";
                    break;
                case "Black Forest":
                    strStartingMap = "kf-blackforest";
                    break;
                case "Burning Paris":
                    strStartingMap = "kf-burningparis";
                    break;
                case "Catacombs":
                    strStartingMap = "kf-catacombs";
                    break;
                case "Containment Station":
                    strStartingMap = "kf-containmentstation";
                    break;
                case "Desolation":
                    strStartingMap = "kf-desolation";
                    break;
                case "DieSector":
                    strStartingMap = "kf-diesector";
                    break;
                case "Dystopia 2029":
                    strStartingMap = "kf-dystopia2029";
                    break;
                case "Elysium":
                    strStartingMap = "kf-elysium";
                    break;
                case "Evacuation Point":
                    strStartingMap = "kf-evacuationpoint";
                    break;
                case "Farmhouse":
                    strStartingMap = "kf-farmhouse";
                    break;
                case "Hellmark Station":
                    strStartingMap = "kf-hellmarkstation";
                    break;
                case "Hostile Grounds":
                    strStartingMap = "kf-hostilegrounds";
                    break;
                case "Infernal Realm":
                    strStartingMap = "kf-infernalrealm";
                    break;
                case "Krampus Lair":
                    strStartingMap = "kf-krampuslair";
                    break;
                case "Lockdown":
                    strStartingMap = "kf-lockdown";
                    break;
                case "Monster Ball":
                    strStartingMap = "kf-monsterball";
                    break;
                case "Moonbase":
                    strStartingMap = "kf-moonbase";
                    break;
                case "Netherhold":
                    strStartingMap = "kf-netherhold";
                    break;
                case "Nightmare":
                    strStartingMap = "kf-nightmare";
                    break;
                case "Nuked":
                    strStartingMap = "kf-nuked";
                    break;
                case "Outpost":
                    strStartingMap = "kf-outpost";
                    break;
                case "Power Core":
                    strStartingMap = "kf-powercore_holdout";
                    break;
                case "Prison":
                    strStartingMap = "kf-prison";
                    break;
                case "Sanitarium":
                    strStartingMap = "kf-sanitarium";
                    break;
                case "Santas Workshop":
                    strStartingMap = "kf-santasworkshop";
                    break;
                case "Shopping Spree":
                    strStartingMap = "kf-shoppingspree";
                    break;
                case "Spillway":
                    strStartingMap = "kf-spillway";
                    break;
                case "Steam Fortress":
                    strStartingMap = "kf-steamfortress";
                    break;
                case "The Descent":
                    strStartingMap = "kf-thedescent";
                    break;
                case "The Tragic Kingdom":
                    strStartingMap = "kf-tragickingdom";
                    break;
                case "Volter Manor":
                    strStartingMap = "kf-voltermanor";
                    break;
                case "ZED Landing":
                    strStartingMap = "kf-zedlanding";
                    break;
                default:
                    strStartingMap = lbStartingMapSelected.Text;
                    break;

            }

        }


        private void IniciarServidor()
        {
            if (lbDifficult.Text.Contains("Normal"))
            {
                lbDifficultSelected.Text = "0";
            }
            else if (lbDifficultSelected.Text.Contains("Hard"))
            {
                lbDifficultSelected.Text = "1";
            }
            else if (lbDifficultSelected.Text.Contains("Suicidal"))
            {
                lbDifficultSelected.Text = "2";
            }
            else if (lbDifficultSelected.Text.Contains("Hell on Earth"))
            {
                lbDifficultSelected.Text = "3";
            }






            if (lbGameModeSelected.Text.Contains("Survival"))
            {
                lbGameModeSelected.Text = "Survival";
            }
            else if (lbGameModeSelected.Text.Contains("Weekly"))
            {
                lbGameModeSelected.Text = "WeeklySurvival";
            }
            else if (lbGameModeSelected.Text.Contains("Versus"))
            {
                lbGameModeSelected.Text = "VersusSurvival";
            }
            else if (lbGameModeSelected.Text.Contains("Endless"))
            {
                lbGameModeSelected.Text = "Endless";
            }
            else if (lbGameModeSelected.Text.Contains("Objective"))
            {
                lbGameModeSelected.Text = "Objective";
            }







            if (lbLenghtSelected.Text.Contains("Short - 4 Waves"))
            {
                lbLenghtSelected.Text = "0";
            }
            else if (lbLenghtSelected.Text.Contains("Medium - 7 Waves"))
            {
                lbLenghtSelected.Text = "1";
            }
            else if (lbLenghtSelected.Text.Contains("Long - 10 Waves"))
            {
                lbLenghtSelected.Text = "2";
            }


            string stringMutators = "";             
            
            try
            {
                stringMutators = File.ReadAllText(@"mutatorString.db", Encoding.UTF8);
            }
            catch { }



            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
            startInfo.FileName = "cmd.exe";

            startInfo.Arguments = @"/C start .\Binaries\win64\kfserver " + strStartingMap + "?Game=KFGameContent.KFGameInfo_" + lbGameModeSelected.Text + "?Difficulty=" + lbDifficultSelected.Text + "?WebAdminPort=8080?AdminName=" + txtWebAdminUsername.Text + "?AdminPassword=" + txtWebAdminPassword.Text + stringMutators;

            process.StartInfo = startInfo;
            process.Start();


            Environment.Exit(0);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            CheckGameMode();

            txtFocus.Focus();

            timer1.Stop();
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

        private void txtServerName_Click(object sender, EventArgs e)
        {
            SoundEffectClick();
        }

        private void txtServerPassword_Click(object sender, EventArgs e)
        {
            SoundEffectClick();
        }

        private void txtWebAdminUsername_Click(object sender, EventArgs e)
        {
            SoundEffectClick();
        }

        private void txtWebAdminPassword_Click(object sender, EventArgs e)
        {
            SoundEffectClick();
        }

        private void txtTitle_Click(object sender, EventArgs e)
        {
            SoundEffectClick();
        }

        private void txtMessage_Click(object sender, EventArgs e)
        {
            SoundEffectClick();
        }

        private void txtSiteLink_Click(object sender, EventArgs e)
        {
            SoundEffectClick();
        }

        private void txtBannerLink_Click(object sender, EventArgs e)
        {
            SoundEffectClick();
        }

        private void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
