using System;
using System.Windows.Forms;

namespace ASCOM.NodeFocuser
{
    public partial class Form1 : Form
    {

        private ASCOM.DriverAccess.Focuser driver;

        public Form1()
        {
            InitializeComponent();
            SetUIState();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsConnected)
                driver.Connected = false;

            Properties.Settings.Default.Save();
        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DriverId = ASCOM.DriverAccess.Focuser.Choose(Properties.Settings.Default.DriverId);
            SetUIState();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (IsConnected)
            {
                driver.Connected = false;
            }
            else
            {
                driver = new ASCOM.DriverAccess.Focuser(Properties.Settings.Default.DriverId);
                driver.Connected = true;
            }
            SetUIState();
        }

        private void SetUIState()
        {
            buttonConnect.Enabled = !string.IsNullOrEmpty(Properties.Settings.Default.DriverId);
            buttonChoose.Enabled = !IsConnected;
            buttonConnect.Text = IsConnected ? "Disconnect" : "Connect";
        }

        private bool IsConnected
        {
            get
            {
                return ((this.driver != null) && (driver.Connected == true));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var position = driver.Position;
            positionTextBox.Text = position.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var position = Int32.Parse(positionTextBox.Text);
            driver.Move(position);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            driver.Halt();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            isMovingTextBox.Text = driver.IsMoving ? "YES" : "NO";
        }
    }
}
