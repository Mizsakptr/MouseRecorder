using System.Runtime.InteropServices;

namespace MouseRecorder
{
    public partial class Form1 : Form
    {

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int VK_A = 0x41;

        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;
        //private const int WM_LBUTTONUP = 0x0202;
        private const int WM_RBUTTONDOWN = 0x0204;
        //private const int WM_RBUTTONUP = 0x0205;

        const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        const uint MOUSEEVENTF_LEFTUP = 0x04;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        const uint MOUSEEVENTF_RIGHTUP = 0x10;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static LowLevelMouseProc _mouseProc = MouseHookCallback;
        private static IntPtr _mouseHookID = IntPtr.Zero;

        bool isRecording = false;
        bool isPlaying = false;

        static bool aKeyPressed = false;
        static bool mb1Pressed = false;
        static bool mb2Pressed = false;

        int n = 0;
        int playbacks = 0;

        List<Point> coordinates = new List<Point>();
        List<bool> buttonClicks = new List<bool>();
        List<bool> mb1Clicks = new List<bool>();
        List<bool> mb2Clicks = new List<bool>();

        System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();
            _hookID = SetHook(_proc);
            _mouseHookID = SetMouseHook(_mouseProc);
        }

        private void recordBtn_Click(object sender, EventArgs e)
        {
            isPlaying = false;
            isRecording = !isRecording;

            if (isRecording)
            {
                isRecordingLabel.Text = "Recording";
                timer = new System.Windows.Forms.Timer();
                timer.Interval = 100;
                timer.Tick += new EventHandler(recording);
                timer.Start();

            }

            if (!isRecording)
            {
                isRecordingLabel.Text = "Not recording";
                timer.Tick -= new EventHandler(recording);
            }
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            isRecording = false;
            isPlaying = !isPlaying;

            if (isPlaying)
            {
                recordBtn.Enabled = false;
                isPlayingLabel.Text = "Playing";
                timer = new System.Windows.Forms.Timer();
                timer.Interval = 100;
                timer.Tick += new EventHandler(playing);
                timer.Start();

            }

            if (!isPlaying)
            {
                recordBtn.Enabled = true;
                isPlayingLabel.Text = "Not playing";
                timer.Tick -= new EventHandler(playing);

            }

        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            coordinates.Clear();
            buttonClicks.Clear();
            mb1Clicks.Clear();
            mb2Clicks.Clear();
            savedOperations.Items.Clear();
            currentOperations.Items.Clear();
        }

        private void recording(object sender, EventArgs e)
        {

            Point currentCoordinates = Control.MousePosition;
            coordinates.Add(currentCoordinates);


            if (aKeyPressed)
            {
                buttonClicks.Add(true);
            }
            else
            {
                buttonClicks.Add(false);
            }

            if (mb1Pressed)
            {
                mb1Clicks.Add(true);
            }
            else
            {
                mb1Clicks.Add(false);
            }

            if (mb2Pressed)
            {
                mb2Clicks.Add(true);
            }
            else
            {
                mb2Clicks.Add(false);
            }

            mouseCurrentCoord.Text = currentCoordinates.ToString() + "  " + mb1Pressed + " " + mb2Pressed;
            savedOperations.Items.Add(mouseCurrentCoord.Text);
            savedOperations.TopIndex = savedOperations.Items.Count - 1;
            aKeyPressed = false;
            mb1Pressed = false;
            mb2Pressed = false;
        }

        private void playing(object sender, EventArgs e)
        {

            playbacks = int.Parse(playbackCount.Text);


            if (n < (coordinates.Count - 1) && playbacks > 0 && isPlaying)
            {

                Cursor.Position = coordinates[n];
                if (buttonClicks[n])
                {
                    SendKeys.Send("A");
                }

                if (mb1Clicks[n])
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }

                if (mb2Clicks[n])
                {
                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                }

                n++;
                currentOperations.Items.Add(coordinates[n] + " " + mb1Clicks[n] + " " + mb2Clicks[n]);
                currentOperations.TopIndex = currentOperations.Items.Count - 1;


            }

            if (n >= (coordinates.Count - 1) && playbacks >= 0)
            {
                n = 0;
                playbacks--;
                playbackCount.Text = playbacks.ToString();
            }

            if (playbacks <= 0)
            {
                playBtn.PerformClick();
            }

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
            UnhookWindowsHookEx(_mouseHookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if ((Keys)vkCode == Keys.A || vkCode == VK_A)
                {
                    aKeyPressed = true;
                }

            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }


        private static IntPtr SetMouseHook(LowLevelMouseProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_LBUTTONDOWN /*|| wParam == (IntPtr)WM_LBUTTONUP*/))

            {
                mb1Pressed = true;
            }

            if (nCode >= 0 && (wParam == (IntPtr)WM_RBUTTONDOWN /*|| wParam == (IntPtr)WM_RBUTTONUP*/))
            {
                mb2Pressed = true;
            }

            return CallNextHookEx(_mouseHookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

    }
}
