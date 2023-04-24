
namespace Jenga.Mods
{
    public class ResetMod : Mod
    {
        public override void ModClicked()
        {
            foreach (var item in _buildStacksManager.GetStacks())
            {
                item.ResetStack();
            }
        }
    }
}