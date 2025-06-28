using System.Collections.Generic;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using Avalonia.Interactivity;
using Avalonia.Input;
using Avalonia.Threading;

namespace SourceGit.Views
{
    public partial class CommitChanges : UserControl
    {
        public CommitChanges()
        {
            InitializeComponent();
        }

        private void OnChangeContextRequested(object sender, ContextRequestedEventArgs e)
        {
            e.Handled = true;

            if (sender is not ChangeCollectionView view || DataContext is not ViewModels.CommitDetail vm)
                return;

            var changes = view.SelectedChanges ?? [];
            var container = view.FindDescendantOfType<ChangeCollectionContainer>();
            if (container is { SelectedItems.Count: 1, SelectedItem: ViewModels.ChangeTreeNode { IsFolder: true } node })
            {
                var menu = vm.CreateChangeContextMenuByFolder(node, changes);
                menu.Open(view);
                return;
            }

            if (changes.Count == 1)
            {
                var menu = vm.CreateChangeContextMenu(changes[0]);
                menu.Open(view);
            }
        }
    }
}
