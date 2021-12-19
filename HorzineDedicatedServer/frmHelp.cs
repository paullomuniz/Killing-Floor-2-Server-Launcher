using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorzineDedicatedServer
{
    public partial class frmHelp : Form
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

        public frmHelp()
        {
            InitializeComponent();
        }

        bool soundClickActivated = true;

        string strSoundEffects = "";

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

        private void frmHelp_Load(object sender, EventArgs e)
        {
            CarregarInformacoesIniciais();

            Custom_font();

            lbCustomContents.ForeColor = corTexto;
            lbAdminCommands.ForeColor = corTexto;
            lbServerConfig.ForeColor = corTexto;
            lbWebAdmin.ForeColor = corTexto;

            lbCustomContents.BackColor = Color.Transparent;
            lbAdminCommands.BackColor = Color.Transparent;
            lbServerConfig.BackColor = Color.Transparent;
            lbWebAdmin.BackColor = Color.Transparent;

            textBox1.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            textBox1.BackColor = Color.FromArgb(18, 16, 16);

            lbCustomContents.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            lbAdminCommands.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            lbServerConfig.Font = new Font(font_Insomnia, 10, FontStyle.Regular);
            lbWebAdmin.Font = new Font(font_Insomnia, 15, FontStyle.Regular);

        }


















        private void pbCustomContents_MouseMove(object sender, MouseEventArgs e)
        {
            pbCustomContents.BackgroundImage = HorzineDedicatedServer.Properties.Resources.help_button_mousemove;
            lbCustomContents.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbCustomContents_MouseLeave(object sender, EventArgs e)
        {
            pbCustomContents.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbCustomContents.BackColor = Color.Transparent;
        }

        private void lbCustomContents_MouseMove(object sender, MouseEventArgs e)
        {
            pbCustomContents.BackgroundImage = HorzineDedicatedServer.Properties.Resources.help_button_mousemove;
            lbCustomContents.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbCustomContents_MouseLeave(object sender, EventArgs e)
        {
            pbCustomContents.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbCustomContents.BackColor = Color.Transparent;
        }












        private void pbAdminCommands_MouseMove(object sender, MouseEventArgs e)
        {
            pbAdminCommands.BackgroundImage = HorzineDedicatedServer.Properties.Resources.help_button_mousemove;
            lbAdminCommands.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbAdminCommands_MouseLeave(object sender, EventArgs e)
        {
            pbAdminCommands.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbAdminCommands.BackColor = Color.Transparent;
        }

        private void lbAdminCommands_MouseMove(object sender, MouseEventArgs e)
        {
            pbAdminCommands.BackgroundImage = HorzineDedicatedServer.Properties.Resources.help_button_mousemove;
            lbAdminCommands.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbAdminCommands_MouseLeave(object sender, EventArgs e)
        {
            pbAdminCommands.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbAdminCommands.BackColor = Color.Transparent;
        }











        private void pbServerConfig_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerConfig.BackgroundImage = HorzineDedicatedServer.Properties.Resources.help_button_mousemove;
            lbServerConfig.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbServerConfig_MouseLeave(object sender, EventArgs e)
        {
            pbServerConfig.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbServerConfig.BackColor = Color.Transparent;
        }

        private void lbServerConfig_MouseMove(object sender, MouseEventArgs e)
        {
            pbServerConfig.BackgroundImage = HorzineDedicatedServer.Properties.Resources.help_button_mousemove;
            lbServerConfig.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbServerConfig_MouseLeave(object sender, EventArgs e)
        {
            pbServerConfig.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbServerConfig.BackColor = Color.Transparent;
        }























        private void lbWebAdmin_MouseMove(object sender, MouseEventArgs e)
        {
            pbWebAdmin.BackgroundImage = HorzineDedicatedServer.Properties.Resources.launchSerMouseMove2;
            lbWebAdmin.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void lbWebAdmin_MouseLeave(object sender, EventArgs e)
        {
            pbWebAdmin.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbWebAdmin.BackColor = Color.Transparent;
        }

        private void pbAddMap_MouseMove(object sender, MouseEventArgs e)
        {
            pbWebAdmin.BackgroundImage = HorzineDedicatedServer.Properties.Resources.launchSerMouseMove2;
            lbWebAdmin.BackColor = Color.FromArgb(189, 90, 18);
        }

        private void pbAddMap_MouseLeave(object sender, EventArgs e)
        {
            pbWebAdmin.BackgroundImage = HorzineDedicatedServer.Properties.Resources.transparencia;
            lbWebAdmin.BackColor = Color.Transparent;
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

        private void pbMenuCustomMaps_Click(object sender, EventArgs e)
        {
            SoundEffectClick();

            frmCustomMaps formCustomMaps = new frmCustomMaps();

            formCustomMaps.Show();

            this.Hide();
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

        private void pbMenuCustomMaps_MouseMove(object sender, MouseEventArgs e)
        {
            pbMenuCustomMaps.BackgroundImage = HorzineDedicatedServer.Properties.Resources.buttonMutatorsMouseMove;
        }

        private void pbMenuCustomMaps_MouseLeave(object sender, EventArgs e)
        {
            pbMenuCustomMaps.BackgroundImage = HorzineDedicatedServer.Properties.Resources.buttonMutators;
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













        private void pbAdminCommands_Click(object sender, EventArgs e)
        {
            AdminCommands();
        }

        private void lbAdminCommands_Click(object sender, EventArgs e)
        {
            AdminCommands();
        }

        private void AdminCommands()
        {
            SoundEffectClick();

            this.BackgroundImage = HorzineDedicatedServer.Properties.Resources.KF_help_interface_adminCommands;

            textBox1.Text = "";

            textBox1.Text = File.ReadAllText("adminCommands.db", Encoding.UTF8);
        }

        private void lbCustomContents_Click(object sender, EventArgs e)
        {
            CustomContents();
        }

        private void pbCustomContents_Click(object sender, EventArgs e)
        {
            CustomContents();
        }

        private void CustomContents()
        {
            SoundEffectClick();

            this.BackgroundImage = HorzineDedicatedServer.Properties.Resources.KF_help_interface_customContents;

            textBox1.Text = "";

            textBox1.Text = File.ReadAllText("customContents.db", Encoding.UTF8);

        }
        
        public string GetEmbeddedResource(string namespacename, string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = namespacename + "." + filename;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }

        private void pbServerConfig_Click(object sender, EventArgs e)
        {
            ServerConfig();
        }

        private void lbServerConfig_Click(object sender, EventArgs e)
        {
            ServerConfig();
        }

        private void ServerConfig()
        {
            SoundEffectClick();

            this.BackgroundImage = HorzineDedicatedServer.Properties.Resources.KF_help_interface_serverConfig;

            textBox1.Text = "";

            textBox1.Text = File.ReadAllText("serverConfig.db", Encoding.UTF8);
        }

        private void pbWebAdmin_Click(object sender, EventArgs e)
        {
            AbrirDiscord();
        }

        private void lbWebAdmin_Click(object sender, EventArgs e)
        {
            AbrirDiscord();
        }

        private void AbrirDiscord()
        {
            SoundEffectClick();

            System.Diagnostics.Process.Start("https://discord.gg/cX3zUxBKMW");
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

        private void frmHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
