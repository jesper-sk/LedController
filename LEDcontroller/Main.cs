#define DEBUG
//#define TEST

using LedController.Bass;
using LedController.LedProfiles;
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

namespace LedController
{
    public partial class MainForm : Form
    {
        public void Test()
        {
            MusicLedProfile prof = new MusicLedProfile();
            prof.DeviceName = "Device name lol";
            using (TextWriter w = new StreamWriter(@".\Testprof.xml")) new XmlSerializer(typeof(LedProfile)).Serialize(w, prof);
            LedProfile prof1;
            using (StreamReader r = new StreamReader(@".\Testprof.xml")) prof1 = new XmlSerializer(typeof(LedProfile)).Deserialize(r) as LedProfile;
            Console.WriteLine((prof1 as MusicLedProfile)?.DeviceName ?? "Null :(");
        }

        #region Definitions
        ComHandler comHandler;
        Config config;
        LedProfile ActiveProfile
        {
            get
            {
                return comHandler?.ActiveProfile;
            }
            set
            {
                foreach (ToolStripItem i in ActiveProfileToolStripMenuItem.DropDownItems) if (i is ToolStripMenuItem mi) mi.CheckState = CheckState.Unchecked;
                try { (ActiveProfileToolStripMenuItem.DropDownItems[value?.ProfileIndex + 2 ?? 0] as ToolStripMenuItem).CheckState = CheckState.Checked; }
                catch {; }
                if (comHandler != null)
                {
                    activeProfileLabel.Text = FormatProfileName(value);
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

        const string appName = "Led Controller";
        const string profileDirectory = "LedProfiles";

        bool isConnected = false;
        bool firstRun;
        int initState;

        bool capToggle = false;

        bool visualizerActive = false;
        Visualizer visualizer;

        [XmlArray("AspectRatioProfiles"), XmlArrayItem("profile")]
        List<RatioProfile> RatioProfiles;

        bool analyzingAudio = false;

        Form[] captureForms;


        List<string> profileSetNames;
        List<List<LedProfile>> profiles;
    #endregion

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
#if TEST
            Test();
#endif
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

            Logger.Log("Refreshing comports...");
            RefreshComPorts();
            Logger.Log("Loading profiles...");
            //LoadAllProfiles();
            LoadAllProfiles();
            Logger.Log("Refreshing profilesets...");
            RefreshProfileSetComboBox();
            Logger.Log($"Selecting profileset {config.ProfileSetOnStartup}");
            SelectProfileSet(config.ProfileSetOnStartup);
            Logger.Log("Filling controls...");
            RefreshProfileControls();
            Console.WriteLine("Selecting right profile...");
            if (!firstRun && config.ProfileIndexOnStartup < profileListView.Items.Count) profileListView.SelectedIndices.Add(config.ProfileIndexOnStartup);
            Console.WriteLine("Updating view from selected profle...");
            UpdateSelectedProfileFromListView();
            Console.WriteLine("Activating selected profile...");
            if (comHandler.IsConnected) ActivateSelectedProfile();

            if (profiles[profileSetComboBox.SelectedIndex].Count > 0)
            {
                profileListView.SelectedIndices.Add(config.ProfileIndexOnStartup);
            }

            StripInfoLabel.Text = $"Number of LEDs: {LedMatrix.MasterLength}\nLength: {LedMatrix.Width}\nHeight: {LedMatrix.Height}\nOverlap: {LedMatrix.MasterLength - LedMatrix.Length}\nRotating {(LedMatrix.IsCw ? "Clockwise" : "Counterclockwise")}\n\nTopleft index: {LedMatrix.TopLeft}\nTopright index: {LedMatrix.TopRight}\nBottomright index: {LedMatrix.BottomRight}\nBottomleft index: {LedMatrix.BottomLeft}\nStrip start index {LedMatrix.Start}";

            SaveAllProfiles();

            initState = 0; //We're now fully initiated

            //Set correct form size
            /*TabControl.ClientSize = new Size
                (
                NullGroupBox.Location.X + NullGroupBox.Size.Width + 14,
                NullGroupBox.Location.Y + NullGroupBox.Size.Height + 30
                );*/
            TabControl.Size = new Size(907, 419);

            ClientSize = new Size(TabControl.Size.Width + 8, TabControl.Size.Height + 32);
            Console.WriteLine("boop");
        }

        private void OnClose()
        {
            if (initState == 0)
            {
                Logger.Log("Freeing BassDriver...");
                BassDriver.Free();
                Logger.Log("Updating config file...");
                UpdateConfig();
                Logger.Log("Saving Config file...");
                SaveConfig();
                Logger.Log("Saving Profiles...");
                SaveAllProfiles();
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

        private XmlSerializer GetCompatibleSerializer(ProfileType type)
        {
            var ser = type switch
            {
                ProfileType.Static => new XmlSerializer(typeof(StaticLedProfile)),
                ProfileType.Rainbow => new XmlSerializer(typeof(RainbowLedProfile)),
                ProfileType.Ambilight => new XmlSerializer(typeof(AmbilightLedProfile)),
                ProfileType.Music => new XmlSerializer(typeof(MusicLedProfile)),
                _ => new XmlSerializer(typeof(LedProfile)),
            };
            return ser;
        }

        private LedProfile GetProfileAs(ProfileType id, string name, int psind)
        {
            switch (id)
            {
                case ProfileType.Static:
                    {
                        return new StaticLedProfile(name, profiles[profileSetComboBox.SelectedIndex].Count, psind, LedMatrix);
                    }
                case ProfileType.Rainbow:
                    {
                        return new RainbowLedProfile(name, profiles[profileSetComboBox.SelectedIndex].Count, psind, LedMatrix);
                    }
                case ProfileType.Ambilight:
                    {
                        return new AmbilightLedProfile(name, profiles[profileSetComboBox.SelectedIndex].Count, psind, LedMatrix);
                    }
                case ProfileType.Music:
                    {
                        return new MusicLedProfile(name, profiles[profileSetComboBox.SelectedIndex].Count, psind, LedMatrix);
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
            return id switch
            {
                ProfileType.Static => StaticGroupBox,
                ProfileType.Rainbow => RainbowGroupBox,
                ProfileType.Ambilight => AmbilightGroupBox,
                ProfileType.Music => MusicGroupBox,

                _ => NullGroupBox,
            };
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

        private void RefreshComButton_Click(object sender, EventArgs e)
        {
            RefreshComPorts();
        }

        private void RefreshProfileControls()
        {
            RefreshProfileListView();
            RefreshActiveProfileToolStripMenuItem();
        }

        private void RefreshProfileSetComboBox()
        {
            int sind = profileSetComboBox.SelectedIndex;
            profileSetComboBox.Items.Clear();
            for (int i = 0; i < profiles.Count; i++)
            {
                profileSetComboBox.Items.Add(profileSetNames[i]);
            }
            for (; sind >= profileSetComboBox.Items.Count; sind--) ;
            profileSetComboBox.SelectedIndex = sind;
        }

        private void RefreshProfileListView()
        {
            profileListView.Items.Clear();

            foreach (LedProfile p in profiles[profileSetComboBox.SelectedIndex])
            {
                profileListView.Items.Add(new CListViewItem(p));
            }

            if (profileListView.Items.Count > 0) profileListView.SelectedIndices.Add(0);
        }
        private void RefreshActiveProfileToolStripMenuItem()
        {
            ActiveProfileToolStripMenuItem.DropDownItems.Clear();
            ActiveProfileToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem()
            {
                Text = "[None]",
                Checked = true,
                CheckState = CheckState.Unchecked
            });
            ActiveProfileToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());

            foreach (LedProfile p in profiles[profileSetComboBox.SelectedIndex])
            {
                if (p != null)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem()
                    {
                        Text = p.Name,
                        Checked = true,
                        CheckState = CheckState.Unchecked,
                    };
                    ActiveProfileToolStripMenuItem.DropDownItems.Insert(p.ProfileIndex + 2, item);
                }
            }

            //Check right entry for toolstripmenuitem
            if (ActiveProfile == null) (ActiveProfileToolStripMenuItem.DropDownItems[0] as ToolStripMenuItem).CheckState = CheckState.Checked;
            else
            {
                if (ActiveProfile.ProfileSetIndex == profileSetComboBox.SelectedIndex)
                {
                    (ActiveProfileToolStripMenuItem.DropDownItems[ActiveProfile.ProfileIndex + 2] as ToolStripMenuItem).CheckState = CheckState.Checked;
                }
                else
                {
                    ActiveProfileToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
                    ActiveProfileToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem()
                    {
                        Text = FormatProfileName(ActiveProfile),
                        Checked = true,
                        CheckState = CheckState.Checked
                    });
                }
            }
        }

        private void MainNotifyIcon_MouseDown(object sender, MouseEventArgs e)
        {
            RefreshActiveProfileToolStripMenuItem();
        }
        #endregion

        #region ProfileSet Control
        private void AddNewProfileSet()
        {
            AddNewProfileSetForm anpsmf = new AddNewProfileSetForm();
            if (anpsmf.ShowDialog() == DialogResult.OK)
            {
                string name = anpsmf.profileSetNameBox.Text;
                profiles.Add(new List<LedProfile>());
                profileSetNames.Add(name);
                SaveNewProfileSet(name);
                RefreshProfileSetComboBox();
            }
        }

        private void SelectProfileSet(string ps)
        {
            if ((string)profileSetComboBox.SelectedItem != ps)
            {
                profileSetComboBox.SelectedItem = ps;
            }

            RefreshProfileControls();
        }

        private void SelectProfileSet(int index)
        {
            if (profileSetComboBox.SelectedIndex != index)
            {
                profileSetComboBox.SelectedIndex = index;
                RefreshProfileListView();
            }
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
            SelectProfileSet((string)profileSetComboBox.SelectedItem);
        }

        private void ProfileSetComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            SaveProfileSet(profileSetComboBox.SelectedIndex);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            RemoveSelectedProfileset();
        }
#endregion

        #region Profile Control/Activation
        private string FormatProfileName(LedProfile prof)
        {
            if (prof == null) return "none";
            return $"{profileSetNames[prof.ProfileSetIndex]} -> {prof.Name}";
        }

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
                foreach (LedProfile p in profiles[profileSetComboBox.SelectedIndex])
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
                LedProfile prof = GetProfileAs((ProfileType)intProfileMode, profileName, profileSetComboBox.SelectedIndex);
                profiles[profileSetComboBox.SelectedIndex].Add(prof);
                SaveProfileSet(profileSetComboBox.SelectedIndex);
                RefreshProfileControls();
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
                SelectedProfile = profiles[profileSetComboBox.SelectedIndex][profileIndex];
            }
        }

        private void SelectActiveProfile()
        {
            if (ActiveProfile != null)
            {
                SelectProfileSet(ActiveProfile.ProfileSetIndex);
                profileListView.SelectedIndices.Clear();
                profileListView.SelectedIndices.Add(ActiveProfile.ProfileIndex);
                UpdateSelectedProfileFromListView();
            }
        }

        /*
         * Activating a profile
         */
        private void ActivateSelectedProfile()
        {
            ActiveProfile = SelectedProfile;
            ActiveProfileToolStripMenuItem.Enabled = true;
        }

        private void SetActiveButton_Click(object sender, EventArgs e)
        {
            ActivateSelectedProfile();
        }

        private void ProfileListView_DoubleClick(object sender, EventArgs a)
        {
            if (isConnected) ActivateSelectedProfile();
        }

        private void ActiveProfileToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs a)
        {
            ToolStripMenuItem item = a.ClickedItem as ToolStripMenuItem;
            int index = ActiveProfileToolStripMenuItem.DropDownItems.IndexOf(item);
            if (index == 0) DeactivateLeds();
            else if (index < profileListView.Items.Count + 4)
            {
                profileListView.SelectedItems.Clear();
                profileListView.SelectedIndices.Add(index - 2);
                UpdateSelectedProfileFromListView();
                ActivateSelectedProfile();
                if (ActiveProfileToolStripMenuItem.DropDownItems.Count - 4 > profileListView.Items.Count)
                {
                    ActiveProfileToolStripMenuItem.DropDownItems.RemoveAt(profileListView.Items.Count + 2);
                    ActiveProfileToolStripMenuItem.DropDownItems.RemoveAt(profileListView.Items.Count + 3);
                }
                /*foreach(ToolStripMenuItem i in ActiveProfileToolStripMenuItem.DropDownItems)
                {
                    i.CheckState = CheckState.Unchecked;
                };
                item.CheckState = CheckState.Checked;*/
            }
        }
        /*
         * Deactivating LEDs
         */
        private void DeactivateLEDsButton_Click(object sender, EventArgs e)
        {
            DeactivateLeds();
        }

        private void DeactivateLeds()
        {
            Logger.Log("Deativating Leds");
            comHandler.Deactivate(true);
            foreach (ToolStripItem item in ActiveProfileToolStripMenuItem.DropDownItems) { if (item is ToolStripMenuItem mitem) mitem.CheckState = CheckState.Unchecked; }
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
        const string profileDir = @".\data\profiles";
        const string configDir = @".\data\configuration.xml";
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

        private void LoadAllProfiles()
        {
            if (!Directory.Exists(profileDir))
                Directory.CreateDirectory(profileDir);

            if (!File.Exists($@"{profileDir}/_PsNames.xml")) //If this is the case, we know no profilesets are currently saved
            {
                profiles = new List<List<LedProfile>>() { new List<LedProfile>() };
                profileSetNames = new List<string>() { "Default" };
            }
            else
            {
                using (StreamReader r = new StreamReader($@"{profileDir}\_PsNames.xml"))
                    profileSetNames = (new XmlSerializer(typeof(List<string>)).Deserialize(r)) as List<string>;

                int c = profileSetNames.Count;
                profiles = new List<List<LedProfile>>(c);
                XmlSerializer ser = new XmlSerializer(typeof(List<LedProfile>));
                for (int i = 0; i < c; i++)
                {
                    using StreamReader r = new StreamReader($@"{profileDir}\ProfileSet_{i}.xml");
                    profiles.Add((ser.Deserialize(r)) as List<LedProfile>);
                }
            }
        }

        private void SaveAllProfiles()
        {
            if (!Directory.Exists(profileDir))
                Directory.CreateDirectory(profileDir);

            using (TextWriter w = new StreamWriter($@"{profileDir}\_PsNames.xml"))
                new XmlSerializer(typeof(List<string>)).Serialize(w, profileSetNames);

            XmlSerializer ser = new XmlSerializer(typeof(List<LedProfile>));
            for (int i = 0; i < profiles.Count; i++)
                using (TextWriter w = new StreamWriter($@"{profileDir}/ProfileSet_{i}.xml"))
                    ser.Serialize(w, profiles[i]);
        }

        private void SaveProfileSet(int psIndex)
        {
            Logger.Log($"Saving profileset no. {psIndex}...");
            using (TextWriter w = new StreamWriter($@"{profileDir}\_PsNames.xml"))
                new XmlSerializer(typeof(List<string>)).Serialize(w, profileSetNames);

            using (TextWriter w = new StreamWriter($@"{profileDir}\ProfileSet_{psIndex}.xml"))
                new XmlSerializer(typeof(List<LedProfile>)).Serialize(w, profiles[psIndex]);
        }

        private async void SaveProfileSetAsync(int psIndex)
        {
            Logger.Log($"Saving profileset no. {psIndex}...");
            using (TextWriter w = new StreamWriter($@"{profileDir}\_PsNames.xml"))
                new XmlSerializer(typeof(List<string>)).Serialize(w, profileSetNames);

            await Task.Run(() =>
            {
                using TextWriter w = new StreamWriter($@"{profileDir}\ProfileSet_{psIndex}.xml");
                new XmlSerializer(typeof(List<LedProfile>)).Serialize(w, profiles[psIndex]);
            });
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

        private LedProfile LoadSingleProfile(string profileSet, int index)
        {
            string dir = Directory.GetCurrentDirectory() + $@"\ProfileSets\{profileSet}";
            string[] files = Directory.GetFiles(dir);
            XmlSerializer profSerializer = new XmlSerializer(typeof(LedProfile));
            StreamReader r = new StreamReader(files[index]);
            LedProfile res = profSerializer.Deserialize(r) as LedProfile;
            r.Dispose();
            return res;
        }

        private LedProfile LoadSingleProfile(int psind, int index)
        {
            XmlSerializer s = new XmlSerializer(typeof(LedProfile[]));
            using StreamReader r = new StreamReader($@".\data\profiles\ProfileSet_{psind}.xml");
            return (s.Deserialize(r) as LedProfile[])[index];
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
            SaveProfileSet(profileSetComboBox.SelectedIndex);
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
                config.ProfileSetOnStartup = ActiveProfile.ProfileSetIndex;
                config.ProfileIndexOnStartup = ActiveProfile.ProfileIndex;
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
            /*
            SelectedProfile = new TestAmbilightLedProfile("TestAmbilight", "root", 0, LedMatrix);
            ActivateSelectedProfile();
            */
            if (comHandler.IsConnected)
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
        private void ShowHideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) WindowState = FormWindowState.Normal;
            else WindowState = FormWindowState.Minimized;
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
                        ShowHideToolStripMenuItem.Text = "Hide";
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
                        ShowHideToolStripMenuItem.Text = "Show";
                        ShowInTaskbar = false;
                        break;
                    }
                case FormWindowState.Maximized:
                    {
                        ShowHideToolStripMenuItem.Text = "Hide";
                        ShowInTaskbar = true;
                        break;
                    }
            }
        }

        private void MainNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowHideToolStripMenuItem_Click(sender, e);
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

                do { visualizer.FillRect(c[i++], (++x * visualizer.SqrSize), (y * visualizer.SqrSize)); } while (x < LedMatrix.Width - 1);
                do { visualizer.FillRect(c[i++], (x * visualizer.SqrSize), (++y * visualizer.SqrSize)); } while (y < LedMatrix.Height - 1);
                do { visualizer.FillRect(c[i++], (--x * visualizer.SqrSize), (y * visualizer.SqrSize)); } while (x > 0);
                do { visualizer.FillRect(c[i++], (x * visualizer.SqrSize), (--y * visualizer.SqrSize)); } while (y > 0);
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

                using Graphics e = VisualizerPanel.CreateGraphics();
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
        private bool analyzerWindowPresent = false;
        private SpectrumAnalyzer analyzer;
        private void AudioTabEnabled()
        {
            ClearAnalyzer();
            if (BassDriver.State == BassDriverState.Enabled)
            {
                visStatusLabel.Text = $"Visualizing {BassDriver.CurrentDeviceName}, index {BassDriver.CurrentDeviceIndex}";
                audioUpdateTimer.Enabled = true;
                analyzingAudio = true;
                StartAudioVisButton.Text = "Stop";
            }
        }
        private void AudioTabDisabled()
        {
            if (analyzingAudio)
            {
                if ((ActiveProfile == null || ActiveProfile.ProfileType != ProfileType.Music) && !analyzerWindowPresent)
                {
                    BassDriver.Disable();
                    BassDriver.Free();
                }
                ClearAnalyzer();
                audioUpdateTimer.Enabled = false;
                analyzingAudio = false;
                StartAudioVisButton.Text = "Start";
                visStatusLabel.Text = "Press \"Start\" to start visualizin\'";
            }
        }
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
                if (ActiveProfile == null || ActiveProfile.ProfileType != ProfileType.Music)
                {
                    BassDriver.Disable();
                    BassDriver.Free();
                }
                ClearAnalyzer();
                audioUpdateTimer.Enabled = false;
                analyzingAudio = false;
                StartAudioVisButton.Text = "Start";
                visStatusLabel.Text = "Press \"Start\" to start visualizin\'";
            }
            else
            {
                var dial = new SelectWasapiDeviceForm();
                dial.ShowDialog();
                if(dial.DialogResult == DialogResult.OK)
                {
                    int devInd = dial.GetAbsoluteSelectedIndex(out string name);
                    BassDriver.Enable(devInd);
                    visStatusLabel.Text = $"Visualizing {name}, index {devInd}";
                    audioUpdateTimer.Enabled = true;
                    analyzingAudio = true;
                    StartAudioVisButton.Text = "Stop";
                }
            }
        }

        private void ShowAnalyzerButton_Click(object sender, EventArgs e)
        {
            if (!analyzerWindowPresent)
            {
                if (!analyzingAudio)
                {
                    var dial = new SelectWasapiDeviceForm();
                    dial.ShowDialog();
                    if (dial.DialogResult == DialogResult.OK)
                    {
                        int devInd = dial.GetAbsoluteSelectedIndex(out string name);
                        BassDriver.Enable(devInd);
                    }
                }
                analyzer = new SpectrumAnalyzer();
                analyzer.Show();
                analyzer.FormClosing += new FormClosingEventHandler(Analyzer_Close);
                analyzerWindowPresent = true;
                ShowAnalyzerButton.Text = "Close analyzer";
            }
            else
            {
                analyzer.Close();
            }
        }

        private void Analyzer_Close(object sender, FormClosingEventArgs e)
        {
            analyzer.Dispose();
            analyzerWindowPresent = false;
            ShowAnalyzerButton.Text = "Show in new window";
            if (!analyzingAudio && (ActiveProfile == null || ActiveProfile.ProfileType != ProfileType.Music))
            {
                BassDriver.Disable();
                BassDriver.Free();
            }
        }
        private void AudioUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (BassDriver.State == BassDriverState.Enabled)
            {
                DrawAnalyzer(BassDriver.GetBands(numBandsTrackBar.Value, out short l, out short r), l, r);
            }
            else
            {
                ClearAnalyzer();
                audioUpdateTimer.Enabled = false;
                analyzingAudio = false;
                StartAudioVisButton.Text = "Start";
                visStatusLabel.Text = "Press \"Start\" to start visualizin\'";
            }
        }

        private bool NumBandsChanged = false;
        private void DrawAnalyzer(List<byte> bands, short l, short r)
        {
            int n = bands.Count;
            int w = (int)((AudioVisualizerPanel.Width - (n - 1)) / (double)n);
            int xs = (int)((double)(AudioVisualizerPanel.Width - (n * w + (n - 1) * 1)) / 2);
            int y = AudioVisualizerPanel.Height - 3;
            int x = xs;
            using (Graphics e = AudioVisualizerPanel.CreateGraphics())
            {
                if (NumBandsChanged)
                {
                    e.Clear(SystemColors.ControlLightLight);
                    NumBandsChanged = false;
                }
                for(int b = 0; b < n; b++)
                {
                    double perc = (double)bands[b] / byte.MaxValue;
                    int h = (int)(perc * (AudioVisualizerPanel.Width - y));
                    //int h = bands[b];
                    //Console.WriteLine(h);
                    using (SolidBrush brush = new SolidBrush(SystemColors.Control))
                    {
                        e.FillRectangle(brush, new Rectangle(x, 0, w, y));
                    }
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(92, 33, 24)))
                    {
                        e.FillRectangle(brush, new Rectangle(x, y - h, w, h));
                        //Console.WriteLine($"X={x}Y={y}W={w}H{h}");
                    }
                    x += w + 1;
                }
            }
            int lh = (AudioLevelPanel.Height - 1) / 2;
            int lys = (AudioLevelPanel.Height - ((lh * 2) + 1)) / 2;

            double percl = (double)l / short.MaxValue;
            double percr = (double)r / short.MaxValue;
            int hl = (int)(percl * AudioLevelPanel.Width);
            int hr = (int)(percr * AudioLevelPanel.Width);
            using (Graphics e = AudioLevelPanel.CreateGraphics())
            {
                using (SolidBrush brush = new SolidBrush(SystemColors.Control))
                {
                    e.FillRectangle(brush, new Rectangle(0, lys, AudioLevelPanel.Width, lh));
                    e.FillRectangle(brush, new Rectangle(0, lys + 1 + lh, AudioLevelPanel.Width, lh));
                }
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(92, 33, 24)))
                {
                    e.FillRectangle(brush, new Rectangle(0, lys, hl, lh));
                    e.FillRectangle(brush, new Rectangle(0, lys + 1 + lh, hr, lh));
                }
            }
        }

        private void ClearAnalyzer() { DrawAnalyzer(new byte[numBandsTrackBar.Value].ToList(), 0, 0); }

        private void NumBandsTrackBar_Scroll(object sender, EventArgs e)
        {
            AudioNumBandsLabel.Text = numBandsTrackBar.Value.ToString();
            NumBandsChanged = true;
        }
        #endregion

        #region Logger
        public void ShowBalloon(string title, string text, ToolTipIcon icon, int customTimeout = 5000)
        {
            MainNotifyIcon.ShowBalloonTip(customTimeout, title, text, icon);
        }

        async void CheckLog()
        { 
            int num = Logger.Q.Count;
            for (int i = 0; i < num; i++) LogRichTextBox.AppendText(Logger.Q.Dequeue());

            int bnum = Logger.BalloonQueue.Count;
            for (int i = 0; i < bnum; i++)
            {
                var info = Logger.BalloonQueue.Dequeue();
                ShowBalloon(info.Item1, info.Item2, info.Item4, info.Item3);
                await Task.Run(() => { Thread.Sleep(info.Item3); });
            }
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
            //From Audiotab
            if (TabControl.SelectedIndex == 2) AudioTabEnabled();
            else AudioTabDisabled();
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
            Text = lp?.ToString() ?? "!!NULL!!";
            SubItems.Add(lp?.ProfileType.ToString() ?? "!!NULL!!");
            SubItems.Add(hk?.ToString() ?? "not assigned");
            shortcut = hk;
            Shortcut = hk;
        }
    }

    public enum ProfileType
    {
        Static, Rainbow, Ambilight, Music, None, Other
    }

    
    public class Config
    {
        public string Com;
        public bool ConnectOnStartup;
        public bool StartupOnLogin;
        public bool StartMinimized;
        public int ProfileSetOnStartup;
        public int ProfileIndexOnStartup;
        public StripInfo Strip;
        //add more things here if needed

        public Config()
        {
            Com = null;
            ConnectOnStartup = false;
            ProfileSetOnStartup = 0;
            ProfileIndexOnStartup = 0;
            StartupOnLogin = false;
            StartMinimized = false;
            Strip = new StripInfo(1, 1, 0, true);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Com: {Com}");
            sb.Append($"\nConnect on startup: {ConnectOnStartup}");
            sb.Append($"\nStartup on login: {StartupOnLogin}");
            sb.Append($"\nProfile on startup: {ProfileSetOnStartup} nr. {ProfileIndexOnStartup} ");
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
