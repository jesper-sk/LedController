#define DEBUG

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using LedController.LedProfiles;
using LedController.Bass;

namespace LedController
{
    public partial class MainForm : Form
    {
        ComHandler comHandler;
        Config config;
        List<LedProfile> profiles;
        LedProfile ActiveProfile
        {
            get
            {
                return comHandler?.ActiveProfile;
            }
            set
            {
                if (comHandler != null)
                {
                    activeProfileLabel.Text = Util.FormatProfileName(value?.UName);
                    comHandler.SetActive(value);
                }
                else throw new InvalidOperationException("comHandler is null");
            }
        }
        LedProfile SelectedProfile
        {
            get
            {
                return selectedProfile;
            }
            set
            {
                selectedProfile = value;
                if (activeGroupBox != null) activeGroupBox.Visible = false;
                if (selectedProfile == null)
                {
                    activeGroupBox = NullGroupBox;
                }
                else
                {
                    activeGroupBox = GetProfileGroupBox(selectedProfile.ProfileType);
                }
                activeGroupBox.Visible = true;
            }
        }
        LedProfile selectedProfile;
        GroupBox activeGroupBox;
        ColorMatrix LedMatrix
        {
            get
            {
                return comHandler?.Matrix;
            }
            set
            {
                if (comHandler != null) comHandler.Matrix = value;
            }
        }

        string[] profileSets;
        string selectedProfileSet;
        const string appName = "Led Controller";
        bool isConnected = false;
        bool firstRun;
        int initState;

        bool capToggle = false;

        bool visualizerActive = false;
        Visualizer visualizer;

        [XmlArray("AspectRatioProfiles"), XmlArrayItem("profile")]
        List<RatioProfile> RatioProfiles;

        bool analyzingAudio = false;
        BassDriver d;

        Form[] captureForms;

        #region Constructor & Load
        public MainForm()
        {
            OnStart();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OnLoad();
        }
        #endregion

        #region Startup & Shutdown
        private void OnStart()
        {
            //Visible = false;
            initState = 1;
            Logger.Log("Hello world!");

            InitializeComponent();

            comHandler = new ComHandler(this);
            UpdateLogTimer.Enabled = true;

            //The working directory should never change as we save our xml there
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            //Load or create config object
            firstRun = !GetCanLoadConfig();
            config = !firstRun ? LoadConfig() : new Config();
            //Eventhandlers
            profileListView.DoubleClick += new System.EventHandler(ProfileListView_DoubleClick);
        }

        private void OnLoad()
        {
            initState = 2;
            if (firstRun)
            {
                OnVisible();
            }
            else
            {
                ApplyConfig();
                if (config.StartMinimized)
                {
                    WindowState = FormWindowState.Minimized;
                    LoadAndActivateProfileFromConfig();
                }
                else OnVisible();
            }
        }

        private void OnVisible()
        {
            InitializeGroupBoxEventHandlers();
            profiles = new List<LedProfile>();

            //Define all Profile GroupBoxes
            List<GroupBox> profileGroupBox = new List<GroupBox>()
            {
                NullGroupBox,
                StaticGroupBox,
                RainbowGroupBox,
                AmbilightGroupBox,
                MusicGroupBox
            };

            //Give all Profile GroupBoxes the same Location and make them invisible
            Point groupBoxLocation = NullGroupBox.Location;
            foreach (GroupBox g in profileGroupBox)
            {
                g.Visible = false;
                g.Location = groupBoxLocation;
            }
            activeGroupBox = NullGroupBox;
            activeGroupBox.Visible = true;
            LogUpdateIntervalComboBox.SelectedIndex = (int)Math.Log10(UpdateLogTimer.Interval);

            if (firstRun)
            {
                SetupLedMatrix();
            }

            RefreshComPorts();
            RefreshProfileSets();
            SelectProfileSet(config.ProfileSetOnStartup);
            if (!firstRun) profileListView.SelectedIndices.Add(config.ProfileIndexOnStartup);
            UpdateSelectedProfileFromListView();
            if (comHandler.IsConnected) ActivateSelectedProfile();

            if (profiles.Count > 0)
            {
                profileListView.SelectedIndices.Add(config.ProfileIndexOnStartup);
            }

            StripInfoLabel.Text = $"Number of LEDs: {LedMatrix.MasterLength}\nLength: {LedMatrix.Width}\nHeight: {LedMatrix.Height}\nOverlap: {LedMatrix.MasterLength - LedMatrix.Length}\nRotating {(LedMatrix.IsCw ? "Clockwise" : "Counterclockwise")}\n\nTopleft index: {LedMatrix.TopLeft}\nTopright index: {LedMatrix.TopRight}\nBottomright index: {LedMatrix.BottomRight}\nBottomleft index: {LedMatrix.BottomLeft}\nStrip start index {LedMatrix.Start}";

            initState = 0; //We're now fully initiated

            //Set correct form size
            /*TabControl.ClientSize = new Size
                (
                NullGroupBox.Location.X + NullGroupBox.Size.Width + 14,
                NullGroupBox.Location.Y + NullGroupBox.Size.Height + 30
                );*/
            TabControl.Size = new Size(907, 419);

            ClientSize = new Size(TabControl.Size.Width + 8, TabControl.Size.Height + 32);
        }

        private void OnClose()
        {
            if (initState == 0)
            {
                Logger.Log("Updating config file...");
                UpdateConfig();
                Logger.Log("Saving Config file...");
                SaveConfig();
                Logger.Log("Saving Profiles...");
                SaveProfilesInCurrentProfileSet();
                Logger.Log("\tSaving Aspect Ratio profiles...");
                SaveRatioProfiles();
            }
            Logger.Log("\tDisconnecting Arduino...");
            if (isConnected)
            {
                comHandler.Deactivate();
                Disconnect();
            }
            Logger.Log("Bye!");
        }
        #endregion

        /*  <!!!Add new LedProfiles over here!!!>  */
        #region LedProfile Definitions
        private LedProfile GetProfileType(ProfileType id, LedProfile curr)
        {
            switch (id)
            {
                case ProfileType.Static:
                    {
                        return curr as StaticLedProfile;
                    }
                case ProfileType.Rainbow:
                    {
                        return curr as RainbowLedProfile;
                    }
                case ProfileType.Ambilight:
                    {
                        return curr as AmbilightLedProfile;
                    }
                case ProfileType.Music:
                    {
                        return curr as MusicLedProfile;
                    }

                default:
                    {
                        throw new InvalidOperationException("Didn't recognise type ID");
                    }
            }
        }

        private LedProfile GetProfileAs(ProfileType id, string name, string parent)
        {
            switch (id)
            {
                case ProfileType.Static:
                    {
                        return new StaticLedProfile(name, parent, profiles.Count, LedMatrix);
                    }
                case ProfileType.Rainbow:
                    {
                        return new RainbowLedProfile(name, parent, profiles.Count, LedMatrix);
                    }
                case ProfileType.Ambilight:
                    {
                        return new AmbilightLedProfile(name, parent, profiles.Count, LedMatrix);
                    }
                case ProfileType.Music:
                    {
                        return new MusicLedProfile(name, parent, profiles.Count, LedMatrix);
                    }

                default:
                    {
                        throw new InvalidOperationException("Didn't recognise type ID");
                    }
            }
        }

        //Add GroupBox of LedProfile in here
        private GroupBox GetProfileGroupBox(ProfileType id)
        {
            switch (id)
            {
                case ProfileType.Static: return StaticGroupBox;
                case ProfileType.Rainbow: return RainbowGroupBox;
                case ProfileType.Ambilight: return AmbilightGroupBox;
                case ProfileType.Music: return MusicGroupBox;

                default: return NullGroupBox;
            }
        }
        #endregion
        /*  </!!!Add new LedProfiles over here!!!>  */

        #region Connect/Disconnect
        private void ConnectComButton_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                Disconnect();
            }
            else
            {
                TryConnect();
            }
        }

        private async void TryConnect(string com = null)
        {
            if (ComPortComboBox.SelectedIndex != 0 || com != null)
            {
                if (comHandler.ConnectCom(com ?? (string)ComPortComboBox.SelectedItem))
                {
                    toolStripStatusLabel1.Text = "Connected";
                    connectComButton.Text = "Disconnect";
                    ComPortComboBox.Enabled = false;
                    config.Com = com ?? (string)ComPortComboBox.SelectedItem;
                    isConnected = true;
                    bool isArduino = await Task.Run(() => comHandler.CheckIfArduino());
                    toolStripStatusLabel1.Text = (isArduino) ? "Connected with Arduino on " + config.Com : "Connected, device possibly incompatible";
                }
                else
                {
                    toolStripStatusLabel1.Text = "Connecting unsuccesful";
                }
            }
            else
            {
                MessageBox.Show("Please select a COM-port first.");
                toolStripStatusLabel1.Text = "Please select a COM Port";
            }
        }

        private void Disconnect()
        {
            comHandler.DisconnectCom();
            connectComButton.Text = "Connect";
            toolStripStatusLabel1.Text = "Disconnected";
            ComPortComboBox.Enabled = true;
            isConnected = false;
        }

        private void ComPortComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                toolStripStatusLabel1.Text = "Disconnected";
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e) //override connection requiered
        {
            isConnected = checkBox1.Checked || comHandler.IsConnected;
        }
        #endregion

        #region Refresh Controls
        private void RefreshComPorts()
        {
            string temp = (string)ComPortComboBox.SelectedItem;
            ComPortComboBox.Items.Clear();
            ComPortComboBox.Items.Add("COM Port");
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {
                ComPortComboBox.Items.Add(s);
            }
            if (temp != null && ComPortComboBox.Items.Contains(temp))
            {
                ComPortComboBox.SelectedItem = temp;
            }
            if (comHandler.IsConnected)
            {
                ComPortComboBox.SelectedItem = comHandler.Com;
            }
            else
            {
                ComPortComboBox.SelectedIndex = 0;
            }
        }

        private void RefreshProfileSets(bool jumpToNew = false) //if jumptonew is true, we expect a max of 1 new profileset 
        {
            profileSets = LoadProfileSets();
            profileSetComboBox.Items.Clear();
            foreach (string k in profileSets)
            {
                profileSetComboBox.Items.Add(k);
                if (jumpToNew)
                {
                    profileSetComboBox.SelectedItem = k;
                }
            }
        }

        private void RefreshProfiles(bool jumpToNew = false)
        {
            profiles = LoadProfiles();
            if (ActiveProfile != null && ActiveProfile.Parent == selectedProfileSet) ActiveProfile = profiles[ActiveProfile.Index];
            profileListView.Items.Clear();
            foreach (LedProfile p in profiles)
            {
                profileListView.Items.Add(new CListViewItem(p));
            }
            if (ActiveProfile != null && !jumpToNew)
            {

            }
        }

        private void RefreshComButton_Click(object sender, EventArgs e)
        {
            RefreshComPorts();
        }
        #endregion

        #region ProfileSet Control
        private void AddNewProfileSet()
        {
            AddNewProfileSetForm anpsmf = new AddNewProfileSetForm();
            if (anpsmf.ShowDialog() == DialogResult.OK)
            {
                string name = anpsmf.profileSetNameBox.Text;
                SaveNewProfileSet(name);
                RefreshProfileSets(true);
            }
        }

        private void SelectProfileSet(string ps)
        {
            if ((string)profileSetComboBox.SelectedItem != ps)
            {
                profileSetComboBox.SelectedItem = ps;
            }
            selectedProfileSet = ps;
            RefreshProfiles();
        }

        private void RemoveSelectedProfileset()
        {

        }

        private void ProfileSetCreateButton_Click(object sender, EventArgs e)
        {
            AddNewProfileSet();
        }

        private void ProfileSetBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initState == 0) { SaveProfilesInCurrentProfileSet(); }
            SelectProfileSet((string)profileSetComboBox.SelectedItem);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            RemoveSelectedProfileset();
        }
        #endregion

        #region Profile Control/Activation
        /*
         * Adding a new profile
         */
        private void AddProfile()
        {
            AddNewProfileForm ADNF = new AddNewProfileForm();
            if (ADNF.ShowDialog() == DialogResult.OK)
            {
                int intProfileMode = ADNF.comboBox1.SelectedIndex;
                string profileName = ADNF.textBox1.Text;
                bool nameExists = false;
                foreach (LedProfile p in profiles)
                {
                    if (p.Name == profileName)
                    {
                        nameExists = true;
                        break;
                    }
                }
                if (nameExists || profileName == "" || profileName.Contains(':'))
                {
                    NewProfileButton_Click(null, null);
                    return;
                }
                profiles.Add(GetProfileAs((ProfileType)intProfileMode, profileName, selectedProfileSet));
                SaveProfilesInCurrentProfileSet();                
                RefreshProfiles();
            }
        }
        
        private void NewProfileButton_Click(object sender, EventArgs e)
        {
            AddProfile();
        }

        /*
        * Selecting a profile
        */
        private void ProfileListView_SelectedIndexChanged(object sender, EventArgs e)//Selecting another profile
        {
            UpdateSelectedProfileFromListView();
        }

        private void UpdateSelectedProfileFromListView()
        {
            if (profileListView.SelectedIndices.Count == 0)
            {
                SelectedProfile = null;
            }
            else
            {
                int profileIndex = profileListView.SelectedIndices[0];
                SelectedProfile = profiles[profileIndex];
            }
        }

        private void SelectActiveProfile()
        {
            if (ActiveProfile != null)
            {
                SelectProfileSet(ActiveProfile.UName.Split(':')[0]);
                profileListView.SelectedIndices.Clear();
                profileListView.SelectedIndices.Add(ActiveProfile.Index);
                ActiveProfile = profiles[ActiveProfile.Index];
                UpdateSelectedProfileFromListView();
            }
        }

        /*
         * Activating a profile
         */
        private void ActivateSelectedProfile()
        {
            ActiveProfile = SelectedProfile;
        }

        private void SetActiveButton_Click(object sender, EventArgs e)
        {
            ActivateSelectedProfile();
        }

        private void ProfileListView_DoubleClick(object sender, EventArgs a)
        {
            if (isConnected) ActivateSelectedProfile();
        }

        /*
         * Deactivating LEDs
         */
        private void DeactivateLEDsButton_Click(object sender, EventArgs e)
        {
            Logger.Log("Deativating Leds");
            comHandler.Deactivate(true);
            ActiveProfile = null;
        }

        private void SelectActiveProfileButton_Click(object sender, EventArgs e)
        {
            SelectActiveProfile();
        }

        public void UpdateFpsLabel(int fps = 0)
        {
            FpsLabel.Text = (fps == 0) ? "-- Fps" : $"{fps} Fps";
        }
        #endregion

        #region Hotkey Assignments
        private void AssignHotkey()
        {
            AssignHotkeyForm AHF = new AssignHotkeyForm();
            if (AHF.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void AssignHotkeyButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet implemented! (it sucks)");
            //AssignHotkey();
        }
        #endregion

        #region XML Serialization
        private void SaveConfig()
        {
            try 
            {
                string dir = Directory.GetCurrentDirectory();
                XmlSerializer configSerializer = new XmlSerializer(typeof(Config));
                TextWriter w = new StreamWriter(dir + @"\Configuration.xml");
                configSerializer.Serialize(w, config);
                w.Dispose();
            }
            catch (Exception e) when (!Env.Debugging)
            {
                MessageBox.Show("Saving Config failed:\n" + e.Message);
            }
        }

        private Config LoadConfig()
        {
            try
            {
                Logger.Log("Loading config file...");
                string dir = Directory.GetCurrentDirectory();
                XmlSerializer configSerializer = new XmlSerializer(typeof(Config));
                StreamReader r = new StreamReader(dir + @"\Configuration.xml");
                Config c = configSerializer.Deserialize(r) as Config;
                r.Dispose();
                return c;
            }
            catch(Exception e) when (!Env.Debugging)
            {
                MessageBox.Show("Loading Config failed:\n" + e.Message);
                return config;
            }
        }

        private bool GetCanLoadConfig()
        {
            return File.Exists(Directory.GetCurrentDirectory() + @"\Configuration.xml");
        }

        private void SaveProfilesInCurrentProfileSet()
        {
            try
            {
                Logger.Log("Saving profiles in current profileset...");
                string dir = Directory.GetCurrentDirectory() + @"\Profilesets";
                string profileSetDir = dir + @"\" + selectedProfileSet;
                if (!Directory.Exists(profileSetDir))
                {
                    Directory.CreateDirectory(profileSetDir);
                }
                XmlSerializer profileSerializer = new XmlSerializer(typeof(LedProfile));
                foreach(LedProfile p  in profiles)
                {
                    TextWriter w = new StreamWriter($@"{profileSetDir}\Profile{p.Index}.xml");
                    Logger.Log($"\tSaving profile {p.Name}");
                    profileSerializer.Serialize(w, p);
                    w.Dispose();
                }}
            catch(Exception e) when (!Env.Debugging)
            {
                MessageBox.Show("Saving profiles failed:\n" + e.Message);
            }

        }

        private void SaveNewProfileSet(string profileSetName)
        {
            try
            {
                string dir = Directory.GetCurrentDirectory() + @"\Profilesets";
                Directory.CreateDirectory(dir + @"\" + profileSetName);
            }
            catch(Exception e) when (!Env.Debugging)
            {
                MessageBox.Show("Saving Profilesets failed:\n" + e.Message);
            }
        }

        private List<LedProfile> LoadProfiles()
        {
            try
            {
                string dir = Directory.GetCurrentDirectory() + @"\Profilesets";
                string[] files = Directory.GetFiles(dir + @"\" + selectedProfileSet);
                XmlSerializer profSerializer = new XmlSerializer(typeof(LedProfile));
                LedProfile[] arrRes = new LedProfile[files.Length];
                Parallel.ForEach(files, (d) =>
                {
                    StreamReader r = new StreamReader(d);
                    LedProfile curr = profSerializer.Deserialize(r) as LedProfile;
                    r.Dispose();
                    curr.SetMatrix(LedMatrix);
                    arrRes[curr.Index] = GetProfileType(curr.ProfileType, curr);
                });
                List<LedProfile> res = (arrRes.Length == 0) ? new List<LedProfile>(1) : new List<LedProfile>(arrRes);
                return res;
            }
            catch (Exception e) when (!Env.Debugging)
            {
                MessageBox.Show("Loading Profiles failed:\n" + e.Message);
                return profiles;
            }
        }

        private string[] LoadProfileSets()
        {
            try
            {
                string dir = Directory.GetCurrentDirectory() + @"\Profilesets";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                string[] res = Directory.GetDirectories(dir);
                if (res.Length == 0) //There are no profilesets yet, so let's make our first one!
                {
                    Directory.CreateDirectory(dir + @"\Default");
                    return new string[1] { "Default" };
                }
                for (int i = 0; i < res.Length; i++)
                {
                    string d = res[i];
                    string[] dsplit = d.Split('\\');
                    res[i] = (dsplit[dsplit.Length - 1]);
                }
                return res;
            }
            catch(Exception e) when (!Env.Debugging)
            {
                MessageBox.Show("Loading Profilesets failed:\n" + e.Message);
                return profileSets;
            }
        }

        private LedProfile LoadSingleProfile(string profileSet, int index)
        {
            string dir = Directory.GetCurrentDirectory() + $@"\ProfileSets\{profileSet}";
            string[] files = Directory.GetFiles(dir);
            XmlSerializer profSerializer = new XmlSerializer(typeof(LedProfile));
            StreamReader r = new StreamReader(files[index]);
            LedProfile res = profSerializer.Deserialize(r) as LedProfile;
            r.Dispose();
            res.SetMatrix(LedMatrix);
            return res;
        }

        private void LoadRatioProfiles(RatioProfile activeRatio = null)
        {
            try
            {
                RatioProfiles = new List<RatioProfile>();
                string dir = $@"{Directory.GetCurrentDirectory()}\Ratioprofiles.xml";
                StreamReader r = new StreamReader(dir);
                XmlSerializer serializer = new XmlSerializer(RatioProfiles.GetType());
                RatioProfiles = serializer.Deserialize(r) as List<RatioProfile>;
                r.Dispose();
            }
            catch (FileNotFoundException)
            {
                if(activeRatio == null)
                {
                    Screen curr = Screen.PrimaryScreen;
                    Rectangle bounds = curr.Bounds;
                    activeRatio = new RatioProfile(bounds.Width / (double)bounds.Height);
                }
                List<double> defRatios = new List<double>() { 1.33, 1.50, 1.60, 1.78, 2.00, 2.33, 2.39, 3.56 }; //4:3, 3:2, 16:10, 16:9, 18:9, 21:9, 43:18, 32:9
                RatioProfiles = new List<RatioProfile>(defRatios.Count);
                for (int i = 0; i < defRatios.Count; i++)
                {
                    RatioProfiles.Add(new RatioProfile(defRatios[i]));
                }
                if (!RatioProfiles.Contains(activeRatio)) RatioProfiles.Add(activeRatio);
            }
        }

        private void SaveRatioProfiles()
        {
            if (RatioProfiles != null)
            {
                string dir = $@"{Directory.GetCurrentDirectory()}\Ratioprofiles.xml";
                XmlSerializer s = new XmlSerializer(RatioProfiles.GetType());
                TextWriter w = new StreamWriter(dir);
                s.Serialize(w, RatioProfiles);
                w.Dispose();
            }
        }

        private void SaveToXmlButton_Click(object sender, EventArgs e)
        {
            SaveProfilesInCurrentProfileSet();
        }
        #endregion

        #region LED-strip Initiation
        private void SetupLedMatrix()
        {
            SetupLedForm setupLedForm = new SetupLedForm(comHandler);
            if (setupLedForm.ShowDialog() == DialogResult.OK)
            {
                LedMatrix = setupLedForm.Result;
            }
        }

        private void SetupLEDstripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!comHandler.IsConnected)
            {
                MessageBox.Show("Please connect to Arduino first.");
                return;
            }
            SetupLedMatrix();
            comHandler.Deactivate();
        }

        private void ComSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        { 

        }
        #endregion

        #region Start/Shutdown Eventhandlers
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnClose();
        }

        private void openOnStartupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetOpenOnStartup(OpenOnStartupCheckBox.Checked);
        }

        private void SetOpenOnStartup(bool b)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (b)
            {
                rk.SetValue(appName, Application.ExecutablePath);
                Logger.Log("Open app at startup enabled.");
            }
            else
            { 
                rk.DeleteValue(appName, false);
                Logger.Log("Open app at startup disabled.");
            }
        }
        #endregion

        #region Config
        private void UpdateConfig()
        {
            config.StartupOnLogin = OpenOnStartupCheckBox.Checked;
            if (ActiveProfile != null)
            {
                config.ProfileSetOnStartup = ActiveProfile.UName.Split(':')[0];
                config.ProfileIndexOnStartup = ActiveProfile.Index;
            }
            config.ConnectOnStartup = ConnectOnOpenCheckBox.Checked;
            config.StartMinimized = StartMinimizedCheckBox.Checked;
            config.Strip = LedMatrix.ToStripConfig();
        }

        private void ApplyConfig()
        {
            OpenOnStartupCheckBox.Checked = config.StartupOnLogin;
            ConnectOnOpenCheckBox.Checked = config.ConnectOnStartup;
            StartMinimizedCheckBox.Checked = config.StartMinimized;
            LedMatrix = new ColorMatrix(config.Strip);
            if (config.ConnectOnStartup)
            {
                TryConnect(config.Com);
            }
        }

        private void LoadAndActivateProfileFromConfig()
        {
            if (config.ProfileSetOnStartup != null && comHandler.IsConnected)
            {
                try
                {
                    SelectedProfile = LoadSingleProfile(config.ProfileSetOnStartup, config.ProfileIndexOnStartup);
                    ActivateSelectedProfile();
                }
                catch (Exception e) when (!Env.Debugging)
                {
                    Logger.Log(e.Message);
                }

            }
        }
        #endregion

        #region Form Show & Hide
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Normal:
                    {
                        if (initState == 2)
                        {
                            OnVisible();
                            initState = 0;
                        }
                        ShowInTaskbar = true;
                        break;
                    }
                case FormWindowState.Minimized:
                    {
                        ShowInTaskbar = false;
                        break;
                    }
                case FormWindowState.Maximized:
                    {
                        ShowInTaskbar = true;
                        break;
                    }
            }
        }
        #endregion

        #region Visualizer
        public void VisualizeColors(CColor[] c)
        {
            if (visualizerActive)
            {
                int x, y, i;
                x = y = i = 0;
                //Action<int, int, CColor> a = new Action<int, int, CColor>((xCoord, yCoord, cColor) => { visualizer.FillRect(xCoord, yCoord, cColor); });

                do { visualizer.FillRect(c[i++], (++x * Visualizer.SqrSize), (y * Visualizer.SqrSize)); } while (x < LedMatrix.Width - 1);
                do { visualizer.FillRect(c[i++], (x * Visualizer.SqrSize), (++y * Visualizer.SqrSize)); } while (y < LedMatrix.Height - 1);
                do { visualizer.FillRect(c[i++], (--x * Visualizer.SqrSize), (y * Visualizer.SqrSize)); } while (x > 0);
                do { visualizer.FillRect(c[i++], (x * Visualizer.SqrSize), (--y * Visualizer.SqrSize)); } while (y > 0);
            }
            if (TabControl.SelectedIndex == 1)
            {
                int width = VisualizerPanel.Width;
                int height = VisualizerPanel.Height;
                int lw = width / LedMatrix.Width;
                int lh = height / LedMatrix.Height;
                int sqr = (lw > lh) ? lh : lw;
                int xs = (width - (sqr * LedMatrix.Width)) / 2;
                int ys = (height - (sqr * LedMatrix.Height)) / 2;

                int x, y, i;
                x = y = i = 0;

                using (Graphics e = VisualizerPanel.CreateGraphics())
                {
                    do { FillRect(c[i++], xs + (++x * sqr), ys + (y * sqr)); } while (x < LedMatrix.Width - 1);
                    do { FillRect(c[i++], xs + (x * sqr), ys + (++y * sqr)); } while (y < LedMatrix.Height - 1);
                    do { FillRect(c[i++], xs + (--x * sqr), ys + (y * sqr)); } while (x > 0);
                    do { FillRect(c[i++], xs + (x * sqr), ys + (--y * sqr)); } while (y > 0);

                    void FillRect(CColor ccolor, int xstart, int ystart)
                    {
                        using (SolidBrush b = new SolidBrush(ccolor.ToColor()))
                            e.FillRectangle(b, new Rectangle(xstart, ystart, sqr, sqr));
                    }
                }
            }
        }

        private void OpenVisualizerFormButton_Click(object sender, EventArgs e)
        {
            if(visualizerActive)
            {
                visualizer.Close();
                visualizer.Dispose();
                visualizer = null;
                visualizerActive = false;
                OpenVisualizerFormButton.Text = "Open in new window";
            }
            else
            {
                visualizer = new Visualizer(LedMatrix.Width, LedMatrix.Height);
                visualizer.Show();
                visualizer.FormClosing += (o, ea) =>
                {
                    visualizerActive = false;
                    Logger.Log("Visualizer closed in form");
                    visualizer.Dispose();
                    OpenVisualizerFormButton.Text = "Open in new window";
                    visualizer = null;
                };
                visualizerActive = true;
                OpenVisualizerFormButton.Text = "Close visualizer";
            }
        }
        #endregion

        #region Ambilight
        private void AmbilightShowCaptureAreasButton_Click(object sender, EventArgs e)
        {
            if (SelectedProfile is AmbilightLedProfile prof)
            {
                if (!capToggle)
                {
                    AmbilightShowCaptureAreasButton.Text = "Hide capture areas";
                    capToggle = true;
                    captureForms = new Form[LedMatrix.Length];
                    int i = 0;
                    foreach (Rect r in prof.Rects)
                    {
                        Form f = new Form()
                        {
                            StartPosition = FormStartPosition.Manual,
                            FormBorderStyle = FormBorderStyle.FixedToolWindow,
                            Location = new Point(r.X, r.Y),
                            Width = r.Width,
                            Height = r.Height
                            
                        };
                        f.Show();
                        captureForms[i] = f;
                        i++;
                    }
                }
                else
                {
                    AmbilightShowCaptureAreasButton.Text = "Show capture areas";
                    capToggle = false;
                    foreach (Form f in captureForms)
                    {
                        f.Close();
                        f.Dispose();
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Expected selectedProfile of type AmbilightLedProfile");
            }
        }
        #endregion

        #region Music
        private void ShowSelectWasapiDevice()
        {
            if (SelectedProfile is MusicLedProfile p)
            {
                var dial = new SelectWasapiDeviceForm();
                dial.ShowDialog();
                if(dial.DialogResult == DialogResult.OK)
                {
                    int deviceIndex = dial.GetAbsoluteSelectedIndex(out string name);
                    p.DeviceIndex = deviceIndex;
                    p.DeviceName = name;
                    MusicDeviceLabel.Text = name;
                }
            }
        }

        private void MusicSelectDeviceButton_Click(object sender, EventArgs e)
        {
            ShowSelectWasapiDevice();
        }

        private void StartAudioVisButton_Click(object sender, EventArgs e)
        {
            if (analyzingAudio)
            {
                audioUpdateTimer.Enabled = false;
                d.Disable();
                d.Dispose();
                analyzingAudio = false;
                StartAudioVisButton.Text = "Start";
            }
            else
            {
                d = new BassDriver(32, 200);
                d.Enable();
                audioUpdateTimer.Enabled = true;
                analyzingAudio = true;
                StartAudioVisButton.Text = "Stop";
            }
        }
        private void AudioUpdateTimer_Tick(object sender, EventArgs e)
        {
            DrawAnalyzer(d.GetBands(out short l, out short r), l, r);
        }

        private void DrawAnalyzer(List<byte> bands, short l, short r)
        {
            int w = (int)((AudioVisualizerPanel.Width - (d.NumBands - 1)) / (double)d.NumBands);
            int xs = (int)((double)(AudioVisualizerPanel.Width - (d.NumBands * w + (d.NumBands - 1) * 1)) / 2);
            int y = AudioVisualizerPanel.Height - 12;
            int x = xs;
            using (Graphics e = AudioVisualizerPanel.CreateGraphics())
            {
                //e.Clear(Color.Transparent);
                for(int b = 0; b < d.NumBands; b++)
                {
                    double perc = (double)bands[b] / 255;
                    int h = (int)(perc * (AudioVisualizerPanel.Width - y));
                    //int h = bands[b];
                    //Console.WriteLine(h);
                    using (SolidBrush brush = new SolidBrush(SystemColors.Control))
                    {
                        e.FillRectangle(brush, new Rectangle(x, 0, w, AudioVisualizerPanel.Height - 12));
                    }
                    using (SolidBrush brush = new SolidBrush(Color.Red))
                    {
                        e.FillRectangle(brush, new Rectangle(x, y - h, w, h));
                        //Console.WriteLine($"X={x}Y={y}W={w}H{h}");
                    }
                    x += w + 1;
                }
            }
        }
        #endregion

        #region Logger
        void CheckLog()
        { 
            int num = Logger.Q.Count;
            for (int i = 0; i < num; i++) LogRichTextBox.AppendText(Logger.Q.Dequeue());
        }

        private void UpdateLogTimer_Tick(object sender, EventArgs e)
        {
            CheckLog();
        }

        private async void ArchiveLogButton_Click(object sender, EventArgs e)
        {
            Logger.Archive();
            ArchiveLogButton.Text = "Archived!";
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
                Action a = () => { ArchiveLogButton.Text = "Archive"; };
                CopyLogToClipboardButton.Invoke(a);
            });
        }

        private async void CopyLogToClipboardButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(LogRichTextBox.Text);
            CopyLogToClipboardButton.Text = "Copied!";
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
                Action a = () => { CopyLogToClipboardButton.Text = "Copy to clipboard"; };
                CopyLogToClipboardButton.Invoke(a);
            });
        }

        private void LogUpdateIntervalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLogTimer.Interval = (int)Math.Pow(10, LogUpdateIntervalComboBox.SelectedIndex);
        }
        #endregion

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            BassDriver d = new BassDriver(32);
            d.Enable();
            for(int i = 0; i < 100; i++)
            {
                var p = d.GetBands(out short levelL, out short levelR);
                Logger.Log(Util.ByteToString(p));
                Thread.Sleep(1000);
            }
            d.Disable();
            d.Dispose();
        }
    }

    public class CListViewItem : ListViewItem
    {
        private Hotkey shortcut;

        public Hotkey Shortcut
        {
            get { return shortcut; }
            set
            {
                SubItems.RemoveAt(2);
                SubItems.Insert(2, new ListViewSubItem(this, value?.ToString() ?? "not assigned"));
                shortcut = value;
            }
        }

        public CListViewItem(LedProfile lp, Hotkey hk = null)
        {
            Text = lp.ToString();
            SubItems.Add(lp.ProfileType.ToString());
            SubItems.Add(hk?.ToString() ?? "not assigned");
            shortcut = hk;
            Shortcut = hk;
        }
    }

    public enum ProfileType
    {
        Static, Rainbow, Ambilight, Music, Other
    }

    
    public class Config
    {
        public string Com;
        public bool ConnectOnStartup;
        public bool StartupOnLogin;
        public bool StartMinimized;
        public string ProfileSetOnStartup;
        public int ProfileIndexOnStartup;
        public StripConfig Strip;
        //add more things here if needed

        public Config()
        {
            Com = null;
            ConnectOnStartup = false;
            ProfileSetOnStartup = "Default";
            ProfileIndexOnStartup = 0;
            StartupOnLogin = false;
            StartMinimized = false;
            Strip = new StripConfig(1, 1, 0, true);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Com: {Com}");
            sb.Append($"\nConnect on startup: {ConnectOnStartup}");
            sb.Append($"\nStartup on login: {StartupOnLogin}");
            sb.Append($"\nProfile on startup: {ProfileSetOnStartup ?? "None"} nr. {(ProfileSetOnStartup != null ? ProfileIndexOnStartup.ToString() : "")} ");
            return sb.ToString();
        }
    }

    public static class Env
    {
#if DEBUG
        public static readonly bool Debugging = true;
#else
    public static readonly bool Debugging = false;
#endif
    }
}
