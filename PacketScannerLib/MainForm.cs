using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using Microsoft.VisualBasic;
using System.Net;
using System.Net.Sockets;
using PacketScanner.Display;
using PacketScanner.Headers;
using PacketScanner.Common;

namespace PacketScanner
{
    public partial class MainForm : Form
    {
        private Socket _mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
        private byte[] _recvData;
        private IPAddress _selectedIP;
        private int _selectedProtocol = 0;
        private volatile bool _showPacketinGrid = false;
        private IPAddress _filterIP = new IPAddress(0);
        private bool _performIPFilter = false;
        private System.Net.NetworkInformation.NetworkInterface[] _listOfNetworkInterfaces;
        private int _maxBuffer = 50;

        Action<IPDisplayPacket> _dgvUpdateHandler;
        Queue<IPDisplayPacket> _displayPackets = new Queue<IPDisplayPacket>();
        private object _syncObject = new object();
        
        public MainForm()
        {
            InitializeComponent();

            _dgvUpdateHandler = DGVUpdate;

            Load += MainForm_Load;

            adapterComboBox.SelectedIndexChanged += adapterComboBox_SelectedIndexChanged;

            startStopBttn.Click += startStopBttn_Click;

            showIPTextBox.TextChanged += showIPTextBox_TextChanged;
            protocolTypeCombo.SelectedIndex = 0;
            protocolTypeCombo.SelectedIndexChanged += ProtocolTypeCombo_SelectedIndexChanged;


            //typeColumn.ValuesChosenForFiltering.Add("TCP");
            //typeColumn.ValuesChosenForFiltering.Add("UDP");
            //typeColumn.ValuesChosenForFiltering.Add("ICMP");

            //DGV.UpdateColumnFiltering();

        }

        private void ProtocolTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedProtocol = protocolTypeCombo.SelectedIndex;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            _listOfNetworkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();

            for (var i = 0; i <= _listOfNetworkInterfaces.Length - 1; i++)
            {
                adapterComboBox.Items.Add(_listOfNetworkInterfaces[i].Name);
            }

            startStopBttn.Text = "Start";
        }

        /// <summary> 
        /// This function parses the incoming packets and extracts the data based upon 
        /// the protocol being carried by the IP datagram. 
        /// </summary> 
        /// <param name="byteData">Incoming bytes</param> 
        /// <param name="nReceived">The number of bytes received</param> 
        private IPDisplayPacket GetDisplayPacket(IpV4Header ipHeader)
        {
            IPDisplayPacket retVal;
            // Since all protocol packets are encapsulated in the IP datagram 
            // so we start by parsing the IP header and see what protocol data 
            // is being carried by it. 
            // Now according to the protocol being carried by the IP datagram we parse 
            // the data field of the datagram. 
            switch (ipHeader.ProtocolType)
            {
                case Protocol.TCP:
                    {
                        TcpHeader tcpHeader = new TcpHeader(ipHeader.Data, ipHeader.MessageLength);
                        retVal = new TCPDisplayPacket(ipHeader,tcpHeader);
                    }
                    break;
                case Protocol.UDP:
                    {
                        UdpHeader udpHeader = new UdpHeader(ipHeader.Data, (int)ipHeader.MessageLength);
                        retVal = new UDPDisplayPacket(ipHeader,udpHeader);
                    }
                    break;
                case Protocol.ICMP:
                    {
                        IcmpHeader icmpHeader = new IcmpHeader(ipHeader.Data, (int)ipHeader.MessageLength);
                        retVal = new IcmpDisplayPacket(ipHeader, icmpHeader);
                    }
                    break;
                case Protocol.IGMP:
                    {
                        IgmpHeader igmpHeader = new IgmpHeader(ipHeader.Data, (int)ipHeader.MessageLength);
                        retVal = new IgmpDisplayPacket(ipHeader, igmpHeader);
                    }
                    break;
                case Protocol.DCCP:
                    {
                        DCCPHeader icmpHeader = new DCCPHeader(ipHeader.Data, (int)ipHeader.MessageLength);
                        retVal = new DCCPDisplayPacket(ipHeader, icmpHeader);
                    }
                    break;
                case Protocol.EIGRP:
                    {
                        EIGRPHeader icmpHeader = new EIGRPHeader(ipHeader.Data, (int)ipHeader.MessageLength);
                        retVal = new EIGRPDisplayPacket(ipHeader, icmpHeader);
                    }
                    break;
                case Protocol.GREs:
                    {
                        GREHeader icmpHeader = new GREHeader(ipHeader.Data, (int)ipHeader.MessageLength);
                        retVal = new GREDisplayPacket(ipHeader, icmpHeader);
                    }
                    break;
                case Protocol.OSPF:
                    {
                        OSPFHeader icmpHeader = new OSPFHeader(ipHeader.Data, (int)ipHeader.MessageLength);
                        retVal = new OSPFDisplayPacket(ipHeader, icmpHeader);
                    }
                    break;
                default:
                case Protocol.Unknown:
                    retVal = new IPDisplayPacket(ipHeader);
                    break;
            }
            return retVal;
        }

        private void BeginReceive()
        {
            try
            {
                _mainSocket.BeginReceive(_recvData, 0, _recvData.Length, SocketFlags.None, OnReceive, null);
            }
            catch (Exception) { }
        }

        private void OnReceive(IAsyncResult asyncresult)
        {
            try
            {
                lock (_syncObject)
                {
                    int nReceived = _mainSocket.EndReceive(asyncresult);
                    bool showPacket = false;

                    IpV4Header ipHeader = new IpV4Header(_recvData, nReceived);
                    // If this is a packet to / from me and not from myself then...
                    if ((ipHeader.SourceAddress.Equals(_selectedIP) == true | 
                        ipHeader.DestinationAddress.Equals(_selectedIP) == true) & 
                        ipHeader.DestinationAddress.Equals(ipHeader.SourceAddress) == false)
                    {
                        if (_performIPFilter == false |
                            (_performIPFilter == true & (_filterIP.Equals(ipHeader.SourceAddress) | _filterIP.Equals(ipHeader.DestinationAddress))))
                        {
                            //all
                            if (_selectedProtocol == 0 || _selectedProtocol == 3)
                            {
                                showPacket = true;
                            }
                            else if ((_selectedProtocol == 2 || _selectedProtocol == 5)&& ipHeader.ProtocolType == Protocol.TCP) //TCP
                            {
                                showPacket = true;
                            }
                            else if ((_selectedProtocol == 1 || _selectedProtocol == 4) && ipHeader.ProtocolType == Protocol.UDP) //UDP
                            {
                                showPacket = true;
                            }
                        }
                    }
                    else
                    {
                        if (_performIPFilter == false | (_performIPFilter == true & (_filterIP.Equals(ipHeader.SourceAddress) | _filterIP.Equals(ipHeader.DestinationAddress))))
                        {
                            if (_selectedProtocol == 3)
                            {
                                showPacket = true;
                            }
                            else if (_selectedProtocol == 5 && ipHeader.ProtocolType == Protocol.TCP) //TCP
                            {
                                showPacket = true;
                            }
                            else if (_selectedProtocol == 4 && ipHeader.ProtocolType == Protocol.UDP) //UDP
                            {
                                showPacket = true;
                            }
                        }
                    }

                    if (showPacket)
                    {
                        var displayPacket = GetDisplayPacket(ipHeader);
                        DGV.Invoke(_dgvUpdateHandler, displayPacket);
                    }

                    if (_showPacketinGrid == true)
                    {
                        // Restart the Receiving
                        BeginReceive();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void DGVUpdate(IPDisplayPacket packet)
        {
            // Remove rows if there are too many
            if (DGV.Items.Count > _maxBuffer)
            {
                var firstItem = _displayPackets.Dequeue();
                DGV.RemoveObject(firstItem);
            }

            _displayPackets.Enqueue(packet);
            DGV.AddObject(packet);
        }

        private byte[] InPlaceByteSwap(byte[] bytez, uint index)
        {
            byte tempData;
            tempData = bytez[index + 1];
            bytez[index + 1] = bytez[index];
            bytez[index] = tempData;
            return bytez;
        }

        private void startStopBttn_Click(System.Object sender, System.EventArgs e)
        {
            if (_showPacketinGrid == true)
            {
                startStopBttn.Text = "Start";
                _showPacketinGrid = false;
                adapterComboBox.Enabled = true;
                protocolTypeCombo.Enabled = true;
            }
            else
            {
                startStopBttn.Text = "Stop";
                _showPacketinGrid = true;
                adapterComboBox.Enabled = false;
                protocolTypeCombo.Enabled = false;
                this.Refresh();
                lock (_syncObject)
                {
                    BeginReceive();
                }
            }
        }

        private void showIPTextBox_TextChanged(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (showIPTextBox.Text != "" & showIPTextBox.Text != null)
                {
                    _filterIP = IPAddress.Parse(showIPTextBox.Text);
                    _performIPFilter = true;
                    showIPTextBox.BackColor = Color.LimeGreen;
                }
                else
                {
                    _performIPFilter = false;
                    showIPTextBox.BackColor = Color.White;
                }
            }
            catch (Exception)
            {
                _performIPFilter = false;
                showIPTextBox.BackColor = Color.White;
            }
        }

        private void adapterComboBox_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            try
            {
                var unicatAddress = _listOfNetworkInterfaces[adapterComboBox.SelectedIndex].GetIPProperties().UnicastAddresses;
                for (var i = 0; i <= unicatAddress.Count - 1; i++)
                {
                    if (unicatAddress[i].Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        _selectedIP = unicatAddress[i].Address;
                        BindSocket();
                    }
                }
            }
            catch(Exception)
            {
                adapterComboBox.BackColor = Color.Red;
            }
        }

        private void BindSocket()
        {
            try
            {
                lock (_syncObject)
                {
                    _mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
                    _mainSocket.Bind(new IPEndPoint(_selectedIP, 0));
                    _mainSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
                    byte[] bytrue = new byte[] { 1, 0, 0, 0 };
                    byte[] byout = new byte[] { 1, 0, 0, 0 };
                    _mainSocket.IOControl(IOControlCode.ReceiveAll, bytrue, byout);
                    _mainSocket.Blocking = false;
                    _recvData = new byte[_mainSocket.ReceiveBufferSize + 1];
                    BeginReceive();
                }
                adapterComboBox.Enabled = false;
                protocolTypeCombo.Enabled = false;
                _showPacketinGrid = true;
                startStopBttn.Text = "Stop";
                adapterComboBox.BackColor = SystemColors.Window;
            }
            catch (Exception)
            {
                adapterComboBox.BackColor = Color.Red;
            }
        }

        private void displayBufferNumeric_ValueChanged(object sender, EventArgs e)
        {
            _maxBuffer = (int) displayBufferNumeric.Value;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            while(_displayPackets.Count > 0)
            { 
                var firstItem = _displayPackets.Dequeue();
                DGV.RemoveObject(firstItem);
            }
        }

        private void groupBySourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DGV.ShowGroups = true;
            DGV.BuildGroups(srcColumn,SortOrder.None);
        }

        private void clearGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DGV.ShowGroups = false;
        }

        private void groupByDestinationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DGV.ShowGroups = true;
            DGV.BuildGroups(destColumn, SortOrder.None);
        }

        private void groupBySourcePortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DGV.ShowGroups = true;
            DGV.BuildGroups(srcPortColumn, SortOrder.None);
        }

        private void groupByDestinationPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DGV.ShowGroups = true;
            DGV.BuildGroups(destPortColumn, SortOrder.None);
        }

        private void groupByTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DGV.ShowGroups = true;
            DGV.BuildGroups(typeColumn, SortOrder.None);
        }

        private void groupBySeqNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DGV.ShowGroups = true;
            DGV.BuildGroups(seqNumberColumn, SortOrder.None);
        }

        private void groupByAckNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DGV.ShowGroups = true;
            DGV.BuildGroups(ackNumberColumn, SortOrder.None);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DGV.CopySelectionToClipboard();
        }
    }
}