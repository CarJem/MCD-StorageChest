using System.Threading.Tasks;

using System.Windows.Threading;

namespace MCDStorageChest
{
    public interface IMainWindow
    {
        Dispatcher Dispatcher { get; }
        public Task RefreshUI();
    }
}