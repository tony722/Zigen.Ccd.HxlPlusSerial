using System;
using Crestron.RAD.Common.Interfaces;
using Crestron.RAD.Common.Transports;
using Crestron.RAD.DeviceTypes.AudioVideoSwitcher;
using Crestron.RAD.ProTransports;

namespace AET.Zigen.Ccd.HxlPlusSerial {
  public class ZigenHxlPlusSerial : AAudioVideoSwitcher, ISerialComport, ISimpl {

    private void Initialize() {
      ConnectionTransport.EnableLogging = InternalEnableLogging;
      ConnectionTransport.EnableTxDebug = InternalEnableTxDebug;
      ConnectionTransport.EnableRxDebug = InternalEnableRxDebug;
      ConnectionTransport.CustomLogger = InternalCustomLogger;

      AudioVideoSwitcherProtocol = new ZigenHxlPlusSerialProtocol(ConnectionTransport, Id) {
        EnableLogging = InternalEnableLogging,
        CustomLogger = InternalCustomLogger
      };
      AudioVideoSwitcherProtocol.RxOut += SendRxOut;
      AudioVideoSwitcherProtocol.Initialize(AudioVideoSwitcherData);

      Connected = true;
    }

    public void Initialize(IComPort comPort) {
      Log("Zigen HXL Initialize(IComPort)"); 
      ConnectionTransport = new CommonSerialComport(comPort);
      Initialize();
      Connected = true;
    }

    public SimplTransport Initialize(Action<string, object[]> send) {
      Log("Zigen HXL Initialize(Action<string, object[]>)");
      ConnectionTransport = new SimplTransport();
      Initialize();
      ConnectionTransport.Send = send;      
      return (SimplTransport)ConnectionTransport;
    }

  }
}