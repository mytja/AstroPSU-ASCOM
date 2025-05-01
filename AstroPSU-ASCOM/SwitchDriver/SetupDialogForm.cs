using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ASCOM.Simulators;
using ASCOM.Utilities;

namespace ASCOM.mytjaAstroPSU.Switch
{
    [ComVisible(false)] // Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        const string NO_PORTS_MESSAGE = "No COM ports found";
        TraceLogger tl; // Holder for a reference to the driver's trace logger

        public SetupDialogForm(TraceLogger tlDriver)
        {
            InitializeComponent();

            // Save the provided trace logger for use within the setup dialogue
            tl = tlDriver;

            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void CmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here and update the state variables with results from the dialogue

            tl.Enabled = chkTrace.Checked;

            // Update the COM port variable if one has been selected
            if (comboBoxComPort.SelectedItem is null) // No COM port selected
            {
                tl.LogMessage("Setup OK", $"New configuration values - COM Port: Not selected");
            }
            else if (comboBoxComPort.SelectedItem.ToString() == NO_PORTS_MESSAGE)
            {
                tl.LogMessage("Setup OK", $"New configuration values - NO COM ports detected on this PC.");
            }
            else // A valid COM port has been selected
            {
                SwitchHardware.comPort = (string)comboBoxComPort.SelectedItem;
                tl.LogMessage("Setup OK", $"New configuration values - COM Port: {comboBoxComPort.SelectedItem}");
            }

            SwitchHardware.showGps = gpsShow.Checked;

            /*
            // Establish COM connection
            String comPort = SwitchHardware.DetectCOMPort();
            var objSerial = new Serial
            {
                PortName = comPort,
                Speed = SerialSpeed.ps115200,
                Parity = SerialParity.None,
                DataBits = 8,
                StopBits = SerialStopBits.One,
                Handshake = SerialHandshake.None,
                ReceiveTimeoutMs = 1000,
                DTREnable = true,
            };
            objSerial.Connected = true;
            */

            SwitchHardware.switches = new List<LocalSwitch>();

            for (int i = 0; i < SwitchHardware.allSwitches.Count; i++)
            {
                String id = SwitchHardware.allSwitches[i].InternalID;
                if (id == "DC1") SwitchHardware.allSwitches[i].Name = dc1Name.Text;
                else if (id == "DC2") SwitchHardware.allSwitches[i].Name = dc2Name.Text;
                else if (id == "DC3") SwitchHardware.allSwitches[i].Name = dc3Name.Text;
                else if (id == "DC4") SwitchHardware.allSwitches[i].Name = dc4Name.Text;
                else if (id == "DC5") SwitchHardware.allSwitches[i].Name = dc5Name.Text;
                else if (id == "DEW1") SwitchHardware.allSwitches[i].Name = dew1Name.Text;
                else if (id == "DEW2") SwitchHardware.allSwitches[i].Name = dew2Name.Text;
                else if (id == "DEW3") SwitchHardware.allSwitches[i].Name = dew3Name.Text;
                else if (id == "EXT1_ANALOG_TEMP") SwitchHardware.allSwitches[i].Name = ext1TempName.Text;
                else if (id == "EXT2_ANALOG_TEMP") SwitchHardware.allSwitches[i].Name = ext2TempName.Text;
                else if (id == "EXT3_ANALOG_TEMP") SwitchHardware.allSwitches[i].Name = ext3TempName.Text;
                else if (id == "SHT3X1_TEMP") SwitchHardware.allSwitches[i].Name = sht1TempName.Text;
                else if (id == "SHT3X2_TEMP") SwitchHardware.allSwitches[i].Name = sht2TempName.Text;
                else if (id == "SHT3X3_TEMP") SwitchHardware.allSwitches[i].Name = sht3TempName.Text;
                else if (id == "SHT3X1_HUM") SwitchHardware.allSwitches[i].Name = sht1HumName.Text;
                else if (id == "SHT3X2_HUM") SwitchHardware.allSwitches[i].Name = sht2HumName.Text;
                else if (id == "SHT3X3_HUM") SwitchHardware.allSwitches[i].Name = sht3HumName.Text;
                else if (id == "DEW_POINT") SwitchHardware.allSwitches[i].Name = dewPointName.Text;

                if (id == "DC1") SwitchHardware.allSwitches[i].Hide = dc1Hide.Checked;
                else if (id == "DC2") SwitchHardware.allSwitches[i].Hide = dc2Hide.Checked;
                else if (id == "DC3") SwitchHardware.allSwitches[i].Hide = dc3Hide.Checked;
                else if (id == "DC4") SwitchHardware.allSwitches[i].Hide = dc4Hide.Checked;
                else if (id == "DC5") SwitchHardware.allSwitches[i].Hide = dc5Hide.Checked;
                else if (id == "DEW1") SwitchHardware.allSwitches[i].Hide = dew1Hide.Checked;
                else if (id == "DEW2") SwitchHardware.allSwitches[i].Hide = dew2Hide.Checked;
                else if (id == "DEW3") SwitchHardware.allSwitches[i].Hide = dew3Hide.Checked;
                else if (id == "EXT1_ANALOG_TEMP") SwitchHardware.allSwitches[i].Hide = ext1TempHide.Checked;
                else if (id == "EXT2_ANALOG_TEMP") SwitchHardware.allSwitches[i].Hide = ext2TempHide.Checked;
                else if (id == "EXT3_ANALOG_TEMP") SwitchHardware.allSwitches[i].Hide = ext3TempHide.Checked;
                else if (id == "SHT3X1_TEMP") SwitchHardware.allSwitches[i].Hide = sht1TempHide.Checked;
                else if (id == "SHT3X2_TEMP") SwitchHardware.allSwitches[i].Hide = sht2TempHide.Checked;
                else if (id == "SHT3X3_TEMP") SwitchHardware.allSwitches[i].Hide = sht3TempHide.Checked;
                else if (id == "SHT3X1_HUM") SwitchHardware.allSwitches[i].Hide = sht1HumHide.Checked;
                else if (id == "SHT3X2_HUM") SwitchHardware.allSwitches[i].Hide = sht2HumHide.Checked;
                else if (id == "SHT3X3_HUM") SwitchHardware.allSwitches[i].Hide = sht3HumHide.Checked;
                else if (id == "DEW_POINT") SwitchHardware.allSwitches[i].Hide = dewPointHide.Checked;

                if (!SwitchHardware.allSwitches[i].Hide) SwitchHardware.switches.Add(SwitchHardware.allSwitches[i]);

                /*
                try
                {
                    objSerial.Transmit($"NAMESET;{id};{SwitchHardware.switches[i].Name}");
                }
                catch
                {

                }
                */
            }

            /*
            try
            {
                objSerial.Transmit($"FLUSH");
                objSerial.Connected = false;
                objSerial.Dispose();
            }
            catch
            {

            }
            */
        }

        private void CmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("https://ascom-standards.org/");
            }
            catch (Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {

            // Set the trace checkbox
            chkTrace.Checked = tl.Enabled;

            // set the list of COM ports to those that are currently available
            comboBoxComPort.Items.Clear(); // Clear any existing entries
            using (Serial serial = new Serial()) // User the Se5rial component to get an extended list of COM ports
            {
                comboBoxComPort.Items.AddRange(serial.AvailableCOMPorts);
            }

            // If no ports are found include a message to this effect
            if (comboBoxComPort.Items.Count == 0)
            {
                comboBoxComPort.Items.Add(NO_PORTS_MESSAGE);
                comboBoxComPort.SelectedItem = NO_PORTS_MESSAGE;
            }

            // select the current port if possible
            if (comboBoxComPort.Items.Contains(SwitchHardware.comPort))
            {
                comboBoxComPort.SelectedItem = SwitchHardware.comPort;
            }

            gpsShow.Checked = SwitchHardware.showGps;

            for (int i = 0; i < SwitchHardware.allSwitches.Count; i++)
            {
                String id = SwitchHardware.allSwitches[i].InternalID;
                String value = SwitchHardware.allSwitches[i].Name;
                bool hide = SwitchHardware.allSwitches[i].Hide;
                if (id == "DC1") dc1Name.Text = value;
                else if (id == "DC2") dc2Name.Text = value;
                else if (id == "DC3") dc3Name.Text = value;
                else if (id == "DC4") dc4Name.Text = value;
                else if (id == "DC5") dc5Name.Text = value;
                else if (id == "DEW1") dew1Name.Text = value;
                else if (id == "DEW2") dew2Name.Text = value;
                else if (id == "DEW3") dew3Name.Text = value;
                else if (id == "EXT1_ANALOG_TEMP") ext1TempName.Text = value;
                else if (id == "EXT2_ANALOG_TEMP") ext2TempName.Text = value;
                else if (id == "EXT3_ANALOG_TEMP") ext3TempName.Text = value;
                else if (id == "SHT3X1_TEMP") sht1TempName.Text = value;
                else if (id == "SHT3X2_TEMP") sht2TempName.Text = value;
                else if (id == "SHT3X3_TEMP") sht3TempName.Text = value;
                else if (id == "SHT3X1_HUM") sht1HumName.Text = value;
                else if (id == "SHT3X2_HUM") sht2HumName.Text = value;
                else if (id == "SHT3X3_HUM") sht3HumName.Text = value;
                else if (id == "DEW_POINT") dewPointName.Text = value;

                if (id == "DC1") dc1Hide.Checked = hide;
                else if (id == "DC2") dc2Hide.Checked = hide;
                else if (id == "DC3") dc3Hide.Checked = hide;
                else if (id == "DC4") dc4Hide.Checked = hide;
                else if (id == "DC5") dc5Hide.Checked = hide;
                else if (id == "DEW1") dew1Hide.Checked = hide;
                else if (id == "DEW2") dew2Hide.Checked = hide;
                else if (id == "DEW3") dew3Hide.Checked = hide;
                else if (id == "EXT1_ANALOG_TEMP") ext1TempHide.Checked = hide;
                else if (id == "EXT2_ANALOG_TEMP") ext2TempHide.Checked = hide;
                else if (id == "EXT3_ANALOG_TEMP") ext3TempHide.Checked = hide;
                else if (id == "SHT3X1_TEMP") sht1TempHide.Checked = hide;
                else if (id == "SHT3X2_TEMP") sht2TempHide.Checked = hide;
                else if (id == "SHT3X3_TEMP") sht3TempHide.Checked = hide;
                else if (id == "SHT3X1_HUM") sht1HumHide.Checked = hide;
                else if (id == "SHT3X2_HUM") sht2HumHide.Checked = hide;
                else if (id == "SHT3X3_HUM") sht3HumHide.Checked = hide;
                else if (id == "DEW_POINT") dewPointHide.Checked = hide;
            }

            tl.LogMessage("InitUI", $"Set UI controls to Trace: {chkTrace.Checked}, COM Port: {comboBoxComPort.SelectedItem}");
        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {
            // Bring the setup dialogue to the front of the screen
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            else
            {
                TopMost = true;
                Focus();
                BringToFront();
                TopMost = false;
            }
        }
    }
}