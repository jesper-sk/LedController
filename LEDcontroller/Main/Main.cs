#define DEBUG

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.Win32;

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
                    activeProfileLabel.Text = FormatProfileName(value?.UName);
                    comHandler.SetActive(value);
                }
                else throw new InvalidOperationException("comHandler is null");
            }
        }
        LedProfile SelectedProfile
        {
            get
            {
                return s;
            }
            set
            {
                s = value;
                if (activeGroupBox != null) activeGroupBox.Visible = false;
                if (s == null)
                {
                    activeGroupBox = NullGroupBox;
                }
                else
                {
                    activeGroupBox = GetProfileGroupBox(s.ProfileType);
                }
                activeGroupBox.Visible = true;
            }
        }
        LedProfile s;
        GroupBox activeGroupBox;
        LedMatrix ledMatrix;
        string[] profileSets;
        string selectedProfileSet;
        const string appName = "Led Controller";
        bool isConnected = false;
        bool firstRun;
        int initState;

        bool capToggle = false;

        [XmlArray("AspectRatioProfiles"), XmlArrayItem("profile")]
        List<RatioProfile> RatioProfiles;

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
            Console.WriteLine("Hello world!");

            InitializeComponent();

            comHandler = new ComHandler(this);

            //The working directory should never change as we save our xml there
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            //Load or create config object
            firstRun = !GetCanLoadConfig();
            config = !firstRun ? LoadConfig() : new Config();

            Console.WriteLine($"new config: {firstRun}");
            Console.WriteLine("RGB: 66, 254, 134");
            Util.RgbToHsv(66, 254, 134, out double h, out double s, out double v);
            Console.WriteLine($"HSV: {h}, {s}, {v}");
            //Eventhandlers
            profileListView.DoubleClick += new System.EventHandler(ProfileListView);
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
                if (config.StartMinimized) WindowState = FormWindowState.Minimized;
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
                AmbilightGroupBox
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

            initState = 0; //We're now fully initiated

            //Set correct form size
            Size = new Size
                (
                2 * ConnectionGB.Location.X + NullGroupBox.Location.X + NullGroupBox.Size.Width,
                2 * ConnectionGB.Location.Y + NullGroupBox.Location.Y + NullGroupBox.Size.Height + 11
                );
        }

        private void OnClose()
        {
            if (initState == 0)
            {
                Console.WriteLine("Updating config file...");
                UpdateConfig();
                Console.WriteLine("Saving Config file...");
                SaveConfig();
                Console.WriteLine("Saving Profiles...");
                SaveProfilesInCurrentProfileSet();
                Console.WriteLine("Saving Aspect Ratio profiles...");
                SaveRatioProfiles();
            }
            Console.WriteLine("Disconnecting Arduino...");
            if (isConnected)
            {
                comHandler.Deactivate();
                Disconnect();
            }
            Console.WriteLine("Bye!");
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
                        throw new NotImplementedException();
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
                        return new StaticLedProfile(name, parent, profiles.Count, ledMatrix);
                    }
                case ProfileType.Rainbow:
                    {
                        return new RainbowLedProfile(name, parent, profiles.Count, ledMatrix);
                    }
                case ProfileType.Ambilight:
                    {
                        return new AmbilightLedProfile(name, parent, profiles.Count, ledMatrix);
                    }
                case ProfileType.Music:
                    {
                        throw new NotImplementedException();
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

        private void ProfileListView(object sender, EventArgs a)
        {
            if (isConnected) ActivateSelectedProfile();
        }

        /*
         * Deactivating LEDs
         */
        private void DeactivateLEDsButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Deativating Leds");
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
                    Console.WriteLine("    Saving profile {0}", p.Name);
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
                    curr.SetMatrix(ledMatrix);
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
            res.SetMatrix(ledMatrix);
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
                ledMatrix = setupLedForm.Result;
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
                Console.WriteLine("Open app at startup enabled.");
            }
            else
            { 
                rk.DeleteValue(appName, false);
                Console.WriteLine("Open app at startup disabled.");
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
            config.StartMinimized = startMinimizedCheckBox.Checked;
            config.Strip = ledMatrix.ToStripConfig();
        }

        private void ApplyConfig()
        {
            OpenOnStartupCheckBox.Checked = config.StartupOnLogin;
            ConnectOnOpenCheckBox.Checked = config.ConnectOnStartup;
            startMinimizedCheckBox.Checked = config.StartMinimized;
            ledMatrix = new LedMatrix(config.Strip);
            if (config.ConnectOnStartup)
            {
                TryConnect(config.Com);
                if (config.ProfileSetOnStartup != null && comHandler.IsConnected)
                {
                    try
                    {
                        SelectedProfile = LoadSingleProfile(config.ProfileSetOnStartup, config.ProfileIndexOnStartup);
                        ActivateSelectedProfile();
                    }
                    catch (Exception e) when (!Env.Debugging)
                    {
                        Console.WriteLine(e.Message);
                    }

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

        string ByteToString(byte[] inp)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            sb.Append(inp[0]);
            for (int i = 1; i < inp.Length; i++)
            {
                byte b = inp[i];
                sb.Append(", ");
                sb.Append(b);
            }
            sb.Append(']');
            return sb.ToString();
        }

        private void CreateVisualizer()
        {
            Visualizer v = new Visualizer(ledMatrix);
            comHandler.AddVisualizer(v);
            v.Show();
        }

        private void VisualizeButton_Click(object sender, EventArgs e)
        {
            CreateVisualizer();
        }

        private string FormatProfileName(string name)
        {
            if (name == null) return "None";
            string[] spl = name.Split(':');
            StringBuilder sb = new StringBuilder();
            sb.Append(spl[0]);
            sb.Append(" -> ");
            sb.Append(spl[1]);
            return sb.ToString();
        }

        private void AmbilightShowCaptureAreasButton_Click(object sender, EventArgs e)
        {
            if (SelectedProfile is AmbilightLedProfile prof)
            {
                if (!capToggle)
                {
                    AmbilightShowCaptureAreasButton.Text = "Hide capture areas";
                    capToggle = true;
                    captureForms = new Form[ledMatrix.Length];
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
