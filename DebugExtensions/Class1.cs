using Avalonia.Controls;
using TuneLab.Base.Structures;
using TuneLab.CommonDialog;
using TuneLab.Extensions.Voices;

namespace DebugExtensions
{
    [VoiceEngine("CommonDialogTest")]
    public class Class1 : IVoiceEngine
    {
        public IReadOnlyOrderedMap<string, VoiceSourceInfo> VoiceInfos => new OrderedMap<string, VoiceSourceInfo>();

        public IVoiceSource CreateVoiceSource(string id)
        {
            return null;
        }

        public void Destroy()
        {
        }

        public bool Init(string enginePath, out string? error)
        {
            /*
            WindowBuilder.Prompt("TEST", "TESTMSG", new WindowBuilder.ButtonAction[1] { new WindowBuilder.ButtonAction() {
                Text="OK",
                Action=()=>{ }
            }});
            */
            //CommonDialog.Prompt("TEST", "TESTMARK", "111", (e) => { CommonDialog.MessageBox("TEST",e,new CommonDialog.ButtonAction[0]); });
            /* int v = 0;
             CommonDialog.Progress("Process", "TTTT", new CommonDialog.ButtonAction[1] {new CommonDialog.ButtonAction() { Text = "1", Action = () => { } } }, () => {
                 v++;
                 return v / 10;
             });*/
            //CommonDialog.PromptPath("TEST", "TST", (x) => { });
            CommonDialog.PromptList("TEST", "TEST", ["T1", "T2"], (i) => { });
            error = "";
            return true;
        }
    }
}
