using System;
using Crestron.RAD.Common.BasicDriver;
using Crestron.RAD.Common.Enums;
using Crestron.RAD.Common.Transports;
using Crestron.RAD.DeviceTypes.AudioVideoSwitcher;
using Crestron.SimplSharp;

namespace AET.Zigen.Ccd.HxlPlusSerial {
  public class ZigenHxlPlusSerialProtocol : AAudioVideoSwitcherProtocol {

    public ZigenHxlPlusSerialProtocol(ISerialTransport transport, byte id) : base(transport, id) {
      base.PowerIsOn = true;      
    }

    protected override ValidatedRxData ResponseValidator(string response, CommonCommandGroupType commandGroup) {
      return base.ResponseValidator(response, commandGroup);
    }

    /*
    public override void RouteVideoInput(string inputId, string outputId) {
      var cmdText = string.Format("Overrride Switch Input {0} to {1}", inputId, outputId);
      var command = new CommandSet("AudioVideoSwitcherRoute", cmdText, CommonCommandGroupType.AudioVideoSwitcher, null, false, CommandPriority.Normal, StandardCommandsEnum.AudioVideoSwitcherRoute);
      SendCommand(command);
    }
     */

    protected override bool PrepareStringThenSend(CommandSet commandSet) {
      if (base.EnableLogging) base.Log("Zigen HXL TX$: '" + commandSet.Command + "'");
      commandSet.Command += "\n";
      return base.PrepareStringThenSend(commandSet);      
    }
    
  }
}
