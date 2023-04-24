
namespace Jenga.Mods
{
    public class TestMyStackController : Mod
    {
        public override void ModClicked()
        {
            foreach (var item in _buildStacksManager.GetStacks())
            {
                foreach(var glass in item.GetGlassBlocks())
                {
                    glass.gameObject.SetActive(false);
                }
            }
        }
    }
}