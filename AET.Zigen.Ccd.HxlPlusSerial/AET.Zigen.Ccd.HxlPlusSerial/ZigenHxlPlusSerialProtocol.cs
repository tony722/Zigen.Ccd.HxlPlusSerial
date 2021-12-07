using System;
using Crestron.RAD.Common.BasicDriver;
using Crestron.RAD.Common.Enums;
using Crestron.RAD.Common.Transports;
using Crestron.RAD.DeviceTypes.AudioVideoSwitcher;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro.DM;

namespace AET.Zigen.Ccd.HxlPlusSerial {
  public class ZigenHxlPlusSerialProtocol : AAudioVideoSwitcherProtocol {

    public ZigenHxlPlusSerialProtocol(ISerialTransport transport, byte id) : base(transport, id) {
      base.PowerIsOn = true;      
    }

    /*
    protected override ValidatedRxData ResponseValidator(string response, CommonCommandGroupType commandGroup) {
      return base.ResponseValidator(response, commandGroup);
    }

    public override void RouteVideoInput(string inputId, string outputId) {
      var cmdText = string.Format("Overrride Switch Input {0} to {1}", inputId, outputId);
      var command = new CommandSet("AudioVideoSwitcherRoute", cmdText, CommonCommandGroupType.AudioVideoSwitcher, null, false, CommandPriority.Normal, StandardCommandsEnum.AudioVideoSwitcherRoute);
      SendCommand(command);
    }
     */

    protected override bool PrepareStringThenSend(CommandSet commandSet) {
      if (EnableLogging) Log("Zigen HXL TX$: '" + commandSet.Command + "'");
      //if (commandSet.StandardCommand == StandardCommandsEnum.AudioVideoSwitcherRoute) {
      //  CreateAVRouteCommand(commandSet);
      //}
      commandSet.Command += "\n";
      return base.PrepareStringThenSend(commandSet);
    }

    private void CreateAVRouteCommand(CommandSet commandSet) {
      const int firstAudioOutputNumber = 11;
      int input;
      int output;
      try {
        var route = commandSet.Command.Split(',');
        input = int.Parse(route[0]);
        output = int.Parse(route[1]);
      }
      catch (Exception ex) {
        if (EnableLogging) Log(string.Format("Zigen HXL CreateAVRouteCommand: Error parsing input and output numbers from '{1}',. {0}", ex.Message, commandSet.Command));
        input = 0;
        output = 0;
      }
      var newCommand = string.Format(output < firstAudioOutputNumber ? "switch video {0} {1}" : "switch audio {0} {1}", output, input);
      if (EnableLogging) Log(string.Format("Zigen HXL CreateAVRouteCommand({0}): Input = {1}, Output = {2}. New command = {3}.", commandSet.Command, input, output, newCommand));
      commandSet.Command = newCommand;
    }
  }
}
